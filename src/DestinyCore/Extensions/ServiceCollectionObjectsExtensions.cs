﻿using DestinyCore.Dependency;
using DestinyCore.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DestinyCore.Extensions
{
    public static partial class ServiceCollectionObjectsExtensions
    {
        public static ObjectAccessor<T> TryAddObjectAccessor<T>(this IServiceCollection services)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                return services.GetSingletonInstance<ObjectAccessor<T>>();
            }

            return services.AddObjectAccessor<T>();
        }

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>());
        }

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, T obj)
        {
            return services.AddObjectAccessor(new ObjectAccessor<T>(obj));
        }

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, ObjectAccessor<T> accessor)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                throw new AppException("在类型“{typeof(T).AssemblyQualifiedName)}”之前注册了对象: ");
            }

            //Add to the beginning for fast retrieve
            services.Insert(0, ServiceDescriptor.Singleton(typeof(ObjectAccessor<T>), accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));

            return accessor;
        }

        public static T GetObjectOrNull<T>(this IServiceCollection services)
            where T : class
        {
            return services.GetSingletonInstanceOrNull<IObjectAccessor<T>>()?.Value;
        }

        public static T GetObject<T>(this IServiceCollection services)
            where T : class
        {
            return services.GetObjectOrNull<T>() ?? throw new AppException($"找不到的对象 {typeof(T).AssemblyQualifiedName} 服务。请确保您以前使用过AddObjectAccessor！");
        }
    }
}
