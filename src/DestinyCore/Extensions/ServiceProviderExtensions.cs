﻿using DestinyCore.Dependency;
using DestinyCore.Exceptions;
using DestinyCore.Helpers;
using DestinyCore.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.Extensions
{
    public static partial class ServiceProviderExtensions
    {

        /// <summary>
        /// 获取指定类型的日志对象
        /// </summary>
        /// <typeparam name="T">非静态强类型</typeparam>
        /// <returns>日志对象</returns>
        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }
        /// <summary>
        /// 获取日志记录器
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        /// <summary>
        /// 获取日志记录器
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILogger GetLogger(this ILazyServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.LazyGetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }


        public static AppOptionSettings GetAppSettings(this IServiceProvider provider)
        {
            provider.NotNull(nameof(provider));
            return provider.GetService<IOptions<AppOptionSettings>>()?.Value;
        }
        public static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
            }

            return descriptor.ImplementationFactory(provider);
        }

        public static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
        }

        public static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
        {
            return ActivatorUtilities.CreateInstance(provider, type, arguments);
        }

        public static void GetService<T>(this IServiceProvider provider, Action<T> action)
        {
            action.NotNull(nameof(action));
            var t = provider.GetService<T>();
            action(t);
        }


        /// <summary>
        ///创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static void CreateScoped<T, TS>(this IServiceProvider provider, Action<TS, T> callback)
        {
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();

            callback(service, scope.ServiceProvider.GetRequiredService<T>());
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static void CreateScoped<TS>(this IServiceProvider provider, Action<TS> callback)
        {
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            callback(service);
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static T CreateScoped<T, TS>(this IServiceProvider provider, Func<TS, T> callback)
        {
            using var scope = provider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            return callback(service);
        }

        public static void CreateScoped(this IServiceProvider provider, Action<IServiceProvider> callback)
        {
            using var scope = provider.CreateScope();
            callback(scope.ServiceProvider);
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static async Task CreateScopedAsync<T, TS>(this IServiceProvider provider, Func<TS, T, Task> callback)
        {
            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();

            await callback(service, scope.ServiceProvider.GetRequiredService<T>());
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// 创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static async Task CreateScopedAsync<TS>(this IServiceProvider provider, Func<TS, Task> callback)
        {
            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            await callback(service);
            if (service is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        ///创建一个IServiceScope，其中包含一个IServiceProvider，用于从新创建的作用域解析依赖项，然后运行关联的回调。
        /// </summary>
        public static async Task<T> CreateScopedAsync<T, TS>(this IServiceProvider provider, Func<TS, Task<T>> callback)
        {
            using var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TS>();
            return await callback(service);
        }

        public static async Task<TResult> CreateScopedAsync<TResult>(this IServiceProvider provider, Func<IServiceProvider, Task<TResult>> callback)
        {
            using var scope = provider.CreateScope();
            return await callback(scope.ServiceProvider);
        }

        public static async Task CreateScopedAsync(this IServiceProvider provider, Func<IServiceProvider, Task> callback)
        {
            using var scope = provider.CreateScope();
            await callback(scope.ServiceProvider);
        }



        /// <summary>
        /// 根据配置得到文件内容
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="sectionKey">分区键</param>
        /// <param name="fileNotExistsMsg">文件不存提示信息</param>
        /// <returns>返回文件中的文件</returns>
        public static string GetFileByConfiguration(this IServiceProvider provider, string sectionKey, string fileNotExistsMsg)
        {


            sectionKey.NotNullOrEmpty(nameof(sectionKey));
            var configuration = provider.GetService<IConfiguration>();
            var value = configuration?.GetSection(sectionKey)?.Value;
            return provider.GetFileText(value, fileNotExistsMsg);

        }

        /// <summary>
        /// 得到文件容器
        /// </summary>
        /// <param name="provider">服务接口</param>
        /// <param name="fileName">文件名+后缀名</param>
        /// <param name="fileNotExistsMsg">文件不存提示信息</param>
        /// <returns>返回文件中的文件</returns>
        public static string GetFileText(this IServiceProvider provider, string fileName, string fileNotExistsMsg)
        {
            fileName.NotNullOrEmpty(nameof(fileName));
            var fileProvider = provider.GetService<IFileProvider>();
            fileProvider.NotNull(nameof(fileProvider));


        
            var fileInfo = fileProvider.GetFileInfo(fileName);

            if (!fileInfo.Exists)
            {

                if (!fileNotExistsMsg.IsNullOrEmpty())
                {
                    throw new AppException(fileNotExistsMsg);
                }

            }
            var text = FileInfoHelper.ReadAllText(fileInfo);
            if (text.IsNullOrEmpty())
            {
                throw new AppException("文件内容不存在");
            }
            return text;
        }

 

    }
}
