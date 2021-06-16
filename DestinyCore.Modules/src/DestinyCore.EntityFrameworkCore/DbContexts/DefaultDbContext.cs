using Microsoft.EntityFrameworkCore;

namespace DestinyCore.EntityFrameworkCore
{
    public class DefaultDbContext : DbContextBase
    {

        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
          : base(options)
        {
        
        }
  

    }
}