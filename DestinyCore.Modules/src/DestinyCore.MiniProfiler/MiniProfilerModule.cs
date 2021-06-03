using DestinyCore.Extensions;
using DestinyCore.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DestinyCore.MiniProfiler
{
    public class MiniProfilerModule : AppModule
    {
        private const string _name = "Destiny:IsOpenMiniProfiler";

        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
            var isOpen = context.GetConfiguration()[_name].AsTo<bool>();
            if (isOpen.IsTrue())
            {
                app.UseMiniProfiler();
            }

        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var isOpen = context.GetConfiguration()[_name].AsTo<bool>();
            if (isOpen.IsTrue())
            {
                context.Services.AddMiniProfiler().AddEntityFramework();
            }

        }
    }
}
