﻿


using DestinyCore.EntityFrameworkCore.DbDrivens;
using DestinyCore.Events;
using DestinyCore.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DestinyCore.EntityFrameworkCore
{
    [DependsOn(
      typeof(MediatorAppModule)

   )]
    public abstract class EntityFrameworkCoreModuleBase : AppModule
    {
                        
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            this.AddDbDriven(context.Services);
            this.AddDbContextWithUnitOfWork(context.Services);
            this.AddRepository(context.Services);
        }

        /// <summary>
        /// 添加仓储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IServiceCollection AddRepository(IServiceCollection services)
        {

            services.AddDefaultRepository();
            return services;
        }

        /// <summary>
        /// 添加DB驱动
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IServiceCollection AddDbDriven(IServiceCollection services)
        {

            services.AddSingleton<IDbContextDrivenProvider, MySqlDbContextDrivenProvider>();
            services.AddSingleton<IDbContextDrivenProvider, SqlServerDbContextDrivenProvider>();
            return services;
        }

        /// <summary>
        /// 添加上下文工作单元
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected abstract IServiceCollection AddDbContextWithUnitOfWork(IServiceCollection services);

     

    }
}