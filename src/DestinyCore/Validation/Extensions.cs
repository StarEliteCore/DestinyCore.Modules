using DestinyCore.Validation.Interceptor;
using Microsoft.Extensions.DependencyInjection;

namespace DestinyCore.Validation
{
    public static partial class Extensions
    {

        public static IServiceCollection WithModelValidation(this IServiceCollection services)
        {
            services.AddTransient<MethodInvocationValidator>();
            services.AddTransient<IMethodParameterValidator, ModelValidationMethodParameterValidator>();
            return services;
        }
    }
}
