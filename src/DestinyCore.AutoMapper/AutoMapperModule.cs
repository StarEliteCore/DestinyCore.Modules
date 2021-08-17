using AutoMapper;
using DestinyCore.Extensions;
using DestinyCore.Mapping;
using DestinyCore.Modules;
using DestinyCore.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DestinyCore.AutoMapper
{
    public class AutoMapperModule : AppModule
    {



        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var assemblyFinder = context.Services.GetOrAddSingletonService<IAssemblyFinder, AssemblyFinder>();
            var assemblys = assemblyFinder.FindAll();
            var myAutoMapTypes = assemblys.SelectMany(o => o.GetTypes()).Where(t => t.IsClass && !t.IsAbstract && t.HasAttribute<AutoMappingAttribute>(true)).Distinct().ToArray();
            context.Services.AddAutoMapper(mapper =>
            {

                this.CreateMapping<AutoMappingAttribute>(myAutoMapTypes, mapper);

            }, assemblys, ServiceLifetime.Singleton);
            var mapper = context.Services.GetService<IMapper>();
            DestinyCore.Extensions.Extensions.SetMapper(mapper);
        }


        private void CreateMapping<TAttribute>(Type[] sourceTypes, IMapperConfigurationExpression mapperConfigurationExpression)
     where TAttribute : AutoMappingAttribute
        {
            foreach (var sourceType in sourceTypes)
            {
                var attribute = sourceType.GetCustomAttribute<TAttribute>();
                if (attribute.TargetTypes?.Count() <= 0)
                {
                    return;
                }
                foreach (var tatgetType in attribute.TargetTypes)
                {

                    if (attribute.Direciton.HasFlag(AutoMapDirection.To))
                    {
                        mapperConfigurationExpression.CreateMap(sourceType, tatgetType);
                    }

                    if (attribute.Direciton.HasFlag(AutoMapDirection.From))
                    {
                        mapperConfigurationExpression.CreateMap(tatgetType, sourceType);
                    }

                }
            }

        }
    }
}
