using DestinyCore.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DestinyCore.Modules
{
    public abstract class AppModule : IAppModule
    {
        public bool Enable { get; set; } = true;

        public virtual void ApplicationInitialization(ApplicationContext context)
        {

        }

        protected internal ConfigureServicesContext ConfigureServicesContext
        {
            get
            {
                if (_configureServicesContext == null)
                {
                    throw new AppException($"{nameof(ConfigureServicesContext)}仅适用于{nameof(ConfigureServices)}方法。");
                }

                return _configureServicesContext;
            }
            internal set => _configureServicesContext = value;
        }

        private ConfigureServicesContext _configureServicesContext;

        public virtual void ConfigureServices(ConfigureServicesContext context)
        {

        }

        public Type[] GetDependedTypes(Type moduleType = null)
        {
            if (moduleType == null)
            {
                moduleType = GetType();
            }

            var dependedTypes = moduleType.GetCustomAttributes().OfType<IDependedTypesProvider>().ToArray();
            if (dependedTypes.Length == 0)
            {
                return new Type[0];
            }
            List<Type> dependList = new List<Type>();
            foreach (var dependedType in dependedTypes)
            {
                var dependeds = dependedType.GetDependedTypes();
                if (dependeds.Length == 0)
                {
                    continue;
                }
                dependList.AddRange(dependeds);

                foreach (Type type in dependeds)
                {
                    dependList.AddRange(GetDependedTypes(type));
                }

            }
            return dependList.Distinct().ToArray();
        }




        public static bool IsAppModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass &&
                 !typeInfo.IsAbstract &&
                 !typeInfo.IsGenericType &&
                 typeof(IAppModule).GetTypeInfo().IsAssignableFrom(type);
        }

        public void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class
        {
            ConfigureServicesContext.Services.Configure<TOptions>(configureOptions);
        }
    }
}
