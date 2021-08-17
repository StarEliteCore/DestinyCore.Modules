using DestinyCore.Events.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DestinyCore.Events
{
    public static class EventBusExtensions
    {

        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
          
            return services;
        }

    }
}
