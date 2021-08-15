using DestinyCore.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.EntityFrameworkCore.Repositorys
{
    /// <summary>
    /// DapperRepository扩展
    /// </summary>
    public static class DapperRepositoryExtensions
    {

        /// <summary>
        /// 添加默认Dapper仓储
        /// </summary>
        /// <param name="services">要添加服务</param>
        /// <param name="lifetime">生命周期</param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultDapperRepository(this IServiceCollection services, ServiceLifetime lifetime =ServiceLifetime.Scoped)
        {
            services.NotNull(nameof(services));
            ServiceDescriptor serviceDescriptor = new (typeof(IDapperRepository),typeof(DapperRepository), lifetime);
            services.Add(serviceDescriptor);
            return services;
        }

    }
}
