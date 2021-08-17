using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DestinyCore.Modules
{
    public interface IModuleApplication : IDisposable
    {

        Type StartupModuleType { get; }

        IServiceCollection Services { get; }

        IServiceProvider ServiceProvider { get; }

        IReadOnlyList<IAppModule> Modules { get; }

        List<IAppModule> Source { get;  }
    }
}
