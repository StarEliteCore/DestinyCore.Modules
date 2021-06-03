using DestinyCore.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.EntityFrameworkCore
{

    /// <summary>
    /// 上下文驱动提供者
    /// </summary>
    public interface IDbContextDrivenProvider
    {


        /// <summary>
        /// 构建数据库驱动
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>

        DbContextOptionsBuilder Builder(DbContextOptionsBuilder builder,string connectionString, DestinyContextOptionsBuilder optionsBuilder);


    }
}
