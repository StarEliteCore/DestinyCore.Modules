using DestinyCore.Events.EventBus;
using DestinyCore.Extensions;
using DestinyCore.Modules;
using DestinyCore.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DestinyCore.Events
{
    public class MediatorAppModule : AppModule
    {


        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var services = context.Services;
            var assemblys = services.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>()?.FindAll();
            services.AddMediatR(assemblys);
            services.TryAddTransient<IMediatorHandler, InMemoryDefaultBus>();
        }



    }
}
