using Microsoft.Extensions.DependencyInjection;
using System;

namespace DestinyCore.Modules
{
    public interface IStartupModuleRunner : IModuleApplication
    {
        void ConfigureServices(IServiceCollection services);


        void Initialize(IServiceProvider service);

    }
}

