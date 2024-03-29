﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace DestinyCore.Dependency
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个 <see cref="DependencyAttribute"/> 类型的新实例
        /// </summary>
        public DependencyAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        /// <summary>
        /// 获取 生命周期类型，代替 <see cref="ISingletonDependency"/>, <see cref="IScopedDependency"/>, <see
        /// cref="ITransientDependency"/> 三个接口的作用
        /// </summary>
        public ServiceLifetime Lifetime { get; }

        /// <summary>
        /// 获取或设置 是否注册自身类型，默认没有接口的类型会注册自身，当此属性值为true时，也会注册自身
        /// </summary>
        public bool AddSelf { get; set; }
    }
}