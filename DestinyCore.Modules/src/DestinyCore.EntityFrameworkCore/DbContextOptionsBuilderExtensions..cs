using DestinyCore.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.EntityFrameworkCore
{
   public static class DbContextOptionsBuilderExtenions
    {


        public static void MigrationsAssembly(this DbContextOptionsBuilder optionsBuilder, string migrationsAssemblyName)
        {
            if (!migrationsAssemblyName.IsNullOrEmpty())
            {
                optionsBuilder.MigrationsAssembly(migrationsAssemblyName);

            }
        }
    }
}
