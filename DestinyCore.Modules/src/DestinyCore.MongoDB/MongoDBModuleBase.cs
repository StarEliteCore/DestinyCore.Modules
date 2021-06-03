using DestinyCore.Modules;
using DestinyCore.MongoDB.Repositorys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DestinyCore.MongoDB
{
    public abstract class MongoDBModuleBase : AppModule
    {

        public override void ConfigureServices(ConfigureServicesContext context)
        {

            AddDbContext(context.Services);
            AddRepository(context.Services);
        }





        public virtual void AddRepository(IServiceCollection services)
        {

            services.TryAddScoped(typeof(IMongoDBRepository<,>), typeof(MongoDBRepository<,>));
        }



        protected abstract void AddDbContext(IServiceCollection services);
    }
}
