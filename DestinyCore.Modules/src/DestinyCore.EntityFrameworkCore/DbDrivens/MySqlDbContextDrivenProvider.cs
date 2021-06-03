using DestinyCore.Dependency;
using DestinyCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.EntityFrameworkCore.DbDrivens
{
    [DatabaseType(DatabaseType.MySql), Dependency(ServiceLifetime.Singleton, AddSelf = true)]

    /// <summary>
    /// MYsql驱动提供者
    /// </summary>
    public class MySqlDbContextDrivenProvider : IDbContextDrivenProvider
    {

        public DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder, string connectionString, DestinyContextOptionsBuilder optionsBuilder)
        {
            //builder.UseMySql(connectionString, options => options.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName));
            builder.UseMySql(connectionString,new MySqlServerVersion(new Version(8,0,21)), options => options.MigrationsAssembly(optionsBuilder.MigrationsAssemblyName));
            return builder;
        }
    }
}
