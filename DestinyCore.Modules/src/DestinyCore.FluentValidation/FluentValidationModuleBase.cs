using DestinyCore.Extensions;
using DestinyCore.Modules;
using DestinyCore.Reflection;
using DestinyCore.Validation;
using FluentValidation;

namespace DestinyCore.FluentValidation
{
    public class FluentValidationModuleBase : AppModule
    {

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var assemblyFinder = context.Services.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>();
            assemblyFinder.NotNull(nameof(assemblyFinder));
            context.Services.AddValidatorsFromAssemblies(assemblyFinder.FindAll());
            context.Services.WithFluentValidation();
            context.Services.WithModelValidation();
        }



    }
}
