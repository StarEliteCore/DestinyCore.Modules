
using DestinyCore;
using DestinyCore.Application.Abstractions;
using DestinyCore.Entity;
using DestinyCore.Extensions;
using DestinyCore.Filter;
using DestinyCore.Filter.Abstract;
using DestinyCore.Ui;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DestinyCore.Application
{
    public class CrudServiceAsync<TPrimaryKey, IEntity, IInputDto, IPagedListDto> : CrudServiceFilter<TPrimaryKey, IEntity, IInputDto, IPagedListDto>, ICrudServiceAsync<TPrimaryKey, IEntity, IInputDto, IPagedListDto>
              where IEntity : class, IEntity<TPrimaryKey>
              where TPrimaryKey : IEquatable<TPrimaryKey>
              where IInputDto : class, IInputDto<TPrimaryKey>, new()
              where IPagedListDto : IPagedListDto<TPrimaryKey>
    {
        protected IServiceProvider ServiceProvider { get; set; }
        protected IRepository<IEntity, TPrimaryKey> Repository { get; set; }
        protected ILogger Logger { get; set; }


        public CrudServiceAsync(IServiceProvider serviceProvider, IRepository<IEntity, TPrimaryKey> repository, ILoggerFactory loggerFactory)
        {
            this.ServiceProvider = serviceProvider;
            this.Repository = repository;
            this.Logger = loggerFactory.CreateLogger<CrudServiceAsync<TPrimaryKey, IEntity, IInputDto, IPagedListDto>>();
        }

        protected IQueryable<IEntity> Entities => this.Repository.Entities;




        protected IQueryable<IEntity> TrackEntities => this.Repository.TrackEntities;

        /// <summary>
        /// 异步创建
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public virtual async Task<OperationResponse> CreateAsync(IInputDto inputDto)
        {
            inputDto.NotNull(nameof(inputDto));
            return await this.Repository.InsertAsync(inputDto, base.InsertCheck);
        }


        ///// <summary>
        ///// 插入检查
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //protected virtual Task InsertCheckAsync(IInputDto dto)
        //{
        //    return Task.CompletedTask;
        //}




        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task<OperationResponse> DeleteAsync(TPrimaryKey key)
        {
            return this.Repository.DeleteAsync(key);
        }


        /// <summary>
        /// 异步更新
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>

        public virtual Task<OperationResponse> UpdateAsync(IInputDto inputDto)
        {
            inputDto.NotNull(nameof(inputDto));
            return this.Repository.UpdateAsync(inputDto,base.UpdateCheckFunc);
        }


   

        /// <summary>
        /// 异步得到分页数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Task<IPagedResult<IPagedListDto>> GetPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            return this.Entities.ToPageAsync<IEntity,IPagedListDto>(request);
        }
    }
}
