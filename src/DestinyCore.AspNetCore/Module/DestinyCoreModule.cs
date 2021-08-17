using DestinyCore.Application;
using DestinyCore.Application.Abstractions;
using DestinyCore.Dependency;
using DestinyCore.Events;
using DestinyCore.Modules;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DestinyCore.AspNetCore
{
    [DependsOn(

        typeof(DependencyAppModule),
        typeof(MediatorAppModule)
    )]
    public class DestinyCoreModule : AppModule
    {



        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.TryAddScoped(typeof(ICrudServiceAsync<,,,>),typeof(CrudServiceAsync<,,,>));
        }
    }
}
