using DestinyCore.Dependency;
using DestinyCore.Events;
using DestinyCore.Extensions;
using DestinyCore.Modules;
using DestinyCore.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace DestinyCore.AspNetCore
{
    [DependsOn(

        typeof(DependencyAppModule),
        typeof(MediatorAppModule)
    )]
    public class DestinyCoreModule : AppModule
    {
    

    }
}
