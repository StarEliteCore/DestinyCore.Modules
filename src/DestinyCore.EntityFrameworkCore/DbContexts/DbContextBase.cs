﻿using DestinyCore.Audit.Dto;
using DestinyCore.Audit.EntityHistory;
using DestinyCore.Dependency;
using DestinyCore.Entity;
using DestinyCore.Extensions;
using DestinyCore.Options;
using DestinyCore.Reflection;
using DnsClient.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace DestinyCore.EntityFrameworkCore
{

    /// <summary>
    /// 上下文基类
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected  AppOptionSettings _option;
        protected  Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IPrincipal _principal;
        protected DbContextBase(DbContextOptions options, IServiceProvider serviceProvider)
             : base(options)
        {
            ServiceProvider = serviceProvider;
            _option = ServiceProvider.GetService<IObjectAccessor<AppOptionSettings>>()?.Value;
            _logger = ServiceProvider.GetLogger(GetType());
            _principal = ServiceProvider.GetService<IPrincipal>();
        }

        public IUnitOfWork UnitOfWork { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var typeFinder = ServiceProvider.GetService<ITypeFinder>();

            IEntityMappingConfiguration[] mappings = typeFinder.Find(o => o.IsDeriveClassFrom<IEntityMappingConfiguration>()).Select(o => Activator.CreateInstance(o) as IEntityMappingConfiguration).ToArray();
            foreach (var item in mappings)
            {
                item.Map(modelBuilder);
            }
        }

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyConcepts();
            var result = OnBeforeSaveChanges();
            //SavedChanges += DbContextBase_SavedChanges;//EFCore 5.X自带事件触发
            int count = await base.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"成功保存{count}条数据");
            return count;
        }
        private void CheckAdd(EntityEntry entity)
        {

            var creationAudited = entity.GetType().GetInterface(typeof(ICreationAudited<>).Name);

            if (!creationAudited.IsNull())
            {
                var typeArguments = creationAudited?.GenericTypeArguments[0];
            }
            //if (creationAudited == null)
            //{
            //    return entity;
            //}

            //var typeArguments = creationAudited?.GenericTypeArguments[0];
            //var fullName = typeArguments?.FullName;
            //if (fullName == typeof(Guid).FullName)
            //{
            //    entity = CheckIModificationAudited<Guid>(entity);
            //}

            //return entity;
        }

        /// <summary>
        /// 设置公共属性
        /// </summary>

        protected virtual void ApplyConcepts()
        {
            var entries = this.FindChangedEntries().ToList();
            foreach (var entity in entries)
            {
         
                if (entity.Entity is ICreationAudited<Guid> createdTime && entity.State == EntityState.Added) 
                {
                    createdTime.CreatedTime = DateTime.Now;
                    createdTime.CreatorUserId = _principal?.Identity?.GetUesrId<Guid>();
                }

                if (entity.State == EntityState.Modified)
                {
                    if (entity.Entity is IModificationAudited<Guid> modificationAuditedUserId)
                    {
                        modificationAuditedUserId.LastModifionTime = DateTime.Now;
                        modificationAuditedUserId.LastModifierUserId = _principal?.Identity?.GetUesrId<Guid>();
                    }

                    //if (entity.Entity is ISoftDelete softDelete)
                    //{

                    //    softDelete.IsDeleted = true;
                    //}
                }
            
            }
        }

        /// <summary>
        /// 准备重写
        /// </summary>
        /// <param name="count"></param>
        /// <param name="sender"></param>
        protected virtual void OnCompleted(int count, object sender)
        {

            if (_option.AuditEnabled)
            {
                if (count > 0 && sender != null && sender is List<AuditEntryDto> senders)
                {
                    var scoped = ServiceProvider.GetService<DictionaryScoped>();
                    var auditChange = scoped.AuditChange;
                    if (auditChange != null)
                    {
                        auditChange.AuditEntitys.AddRange(senders);
                    }


                }
            }
            _logger.LogInformation($"进入保存更新成功方法");
        }
        protected virtual object OnBeforeSaveChanges()
        {
            _logger.LogInformation($"进入开始保存更改方法");
            return GetAuditEntitys();
        }


        protected virtual IEnumerable<AuditEntryDto> GetAuditEntitys()
        {
            if (!_option.AuditEnabled)
            {
                return null;
            }
            IEnumerable<EntityEntry> entityEntry = FindChangedEntries();
            return ServiceProvider.GetService<IAuditHelper>()?.GetAuditEntity(entityEntry);

        }
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            ApplyConcepts();
            var result = OnBeforeSaveChanges();
            int count = base.SaveChanges();
            _logger.LogInformation($"成功保存{count}条数据");
            OnAfterSaveChanges();
            OnCompleted(count, result);
            return count;
        }
        /// <summary>
        /// 结束保存
        /// </summary>
        protected virtual void OnAfterSaveChanges()
        {

            _logger.LogInformation($"进入结束保存更改");
        }

        protected virtual IReadOnlyList<EntityEntry> FindChangedEntries()
        {
            return this.ChangeTracker.Entries()
                .Where(x =>
                    x.State == EntityState.Added ||
                    x.State == EntityState.Modified ||
                    x.State == EntityState.Deleted)
                .ToList();
        }

        protected virtual bool HasCreationAuditedIdProperty(EntityEntry entity)
        {
            return entity.GetType().GetProperty(nameof(ICreationAudited<Guid>.CreatorUserId)) != null;
        }
        protected virtual bool HasCreatedTimeProperty(EntityEntry entity)
        {
            return entity.GetType().GetProperty(nameof(ICreationAudited<Guid>.CreatedTime)) != null;
        }
    }


}
