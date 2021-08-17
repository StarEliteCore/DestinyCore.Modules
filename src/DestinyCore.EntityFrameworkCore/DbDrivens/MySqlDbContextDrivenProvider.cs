using DestinyCore.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DestinyCore.EntityFrameworkCore.DbDrivens
{

    /// <summary>
    /// MYsql驱动提供者
    /// </summary>
    [DatabaseType(DataBaseType.MySql)]
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
