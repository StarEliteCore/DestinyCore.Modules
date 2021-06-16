using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using DestinyCore.Extensions;
using DestinyCore.Entity;
using DestinyCore.Exceptions;
using DestinyCore.Options;
using DestinyCore.EntityFrameworkCore.Interceptor;
using DestinyCore.EntityFrameworkCore.DbDrivens;

namespace DestinyCore.EntityFrameworkCore
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// 添加上下文，自动识别数据库驱动
        /// </summary>
        /// <typeparam name="TDbContext">上下文</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="dbOption"></param>
        /// <param name="optionsAction">操作委托</param>
        /// <returns>返回已添加上下文服务集合</returns>

        public static IServiceCollection AddDestinyDbContext<TDbContext>(this IServiceCollection services, Action<DestinyContextOptions> dbOption, Action<IServiceProvider, DbContextOptionsBuilder> optionsAction = null) where TDbContext : DbContextBase
        {
         
            if (dbOption is null)
            {
                throw new AppException($"{nameof(dbOption)}不能为空");
            }
            services.AddDbContext<TDbContext>((provider, builder) =>
            {

                DestinyContextOptions contextOptions = new DestinyContextOptions();
                dbOption?.Invoke(contextOptions);
                builder = provider.AddDbContextOptionsBuilder<TDbContext>(contextOptions,builder);
                optionsAction?.Invoke(provider,builder);
            });
            return services;

        }

        /// <summary>
        /// 添加上下文操作构建器
        /// </summary>
        /// <typeparam name="TDbContext">动态上下文</typeparam>
        /// <param name="provider">提供者</param>
        /// <param name="dbOption">db操作</param>
        /// <param name="builder">构建器</param>
        /// <returns></returns>

        public static DbContextOptionsBuilder AddDbContextOptionsBuilder<TDbContext>(this IServiceProvider provider, DestinyContextOptions dbOption, DbContextOptionsBuilder builder)
        {

            if (dbOption.ConnectionString.IsNullOrEmpty())
            {
                MessageBox.Show("链接不能为空或null");
            }

            if (dbOption.MigrationsAssemblyName.IsNullOrEmpty())
            {
                MessageBox.Show("迁移程序集名不能为空或null");
            }


            var databaseType = dbOption.DatabaseType;


            var drivenProvider = provider.GetServices<IDbContextDrivenProvider>()?.FirstOrDefault(o => o.GetType().GetAttribute<DatabaseTypeAttribute>()?.DatabaseType == databaseType);


        
            if (drivenProvider == null)
            {
                MessageBox.Show($"没有找到{databaseType}类型的驱动");

            }
            DestinyContextOptionsBuilder optionsBuilder1 = new DestinyContextOptionsBuilder();
            optionsBuilder1.MigrationsAssemblyName = dbOption.MigrationsAssemblyName;
            var connectionString = dbOption.ConnectionString;

            if (dbOption.ConnectionString.IsTxtFile()) //txt文件
            {

                connectionString = provider.GetFileText(dbOption.ConnectionString, $"未找到存放{databaseType.ToDescription()}数据库链接的文件");
            }

            builder = drivenProvider.Builder(builder, connectionString, optionsBuilder1);
            return builder;
        }


        /// <summary>
        /// 添加默认仓储
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultRepository(this IServiceCollection services, ServiceLifetime lifetime= ServiceLifetime.Scoped)
        {
            services.Add(new ServiceDescriptor(typeof(IRepository<,>), typeof(Repository<,>), lifetime));
            return services;
        
        }
    }
}
