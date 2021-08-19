using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace DestinyCore.Extensions
{
    public static partial class ControllerExtensions
    {
        public static bool IsController(this Type type)
        {
            return IsController(type.GetTypeInfo());
        }
        /// <summary>
        /// 是否控制器
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>

        public static bool IsController(this TypeInfo typeInfo)
        {

            return typeInfo.IsClass && !typeInfo.IsAbstract && typeInfo.IsPublic && (typeInfo.IsDefined(typeof(ControllerAttribute)) && typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
        }
    }
}
