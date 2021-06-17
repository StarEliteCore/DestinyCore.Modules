using Microsoft.EntityFrameworkCore;
using System;

namespace DestinyCore.EntityFrameworkCore
{
    public class DefaultDbContext : DbContextBase
    {

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
        
        }
  

    }
}