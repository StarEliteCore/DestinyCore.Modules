using DestinyCore.Extensions;
using DestinyCore.Options;

namespace DestinyCore.Modules
{
    public static class ConfigureServicesContextExtenstions
    {

        public static AppOptionSettings GetAppSettings(this ConfigureServicesContext services)
        {
            return services.Services.GetObject<AppOptionSettings>();
        }
    }
}
