﻿
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace DestinyCore.Extensions
{
    public static partial class ClaimsIdentityExtensions
    {
        /// <summary>
        /// 得到用户ID
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T GetUesrId<T>(this IIdentity identity, string type = ClaimTypes.NameIdentifier)
        {

            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return default(T);
            }
            string value = claimsIdentity.FindFirst(type)?.Value;
            if (value == null)
            {
                return default(T);
            }
            return value.AsTo<T>();
        }
        /// <summary>
        /// 得到用户ID
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetUesrId(this IIdentity identity, string type = ClaimTypes.NameIdentifier)
        {

            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return null;
            }
            string value = claimsIdentity.FindFirst(type)?.Value;
            return value;
        }
        /// <summary>
        /// 得到用户名
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetUserName(this IIdentity identity, string type = ClaimTypes.Name)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return string.Empty;
            }
            return claimsIdentity.FindFirst(type)?.Value;
        }
        /// <summary>
        /// 获取昵称
        /// </summary>
        public static string GetNickName(this IIdentity identity)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return string.Empty;
            }
            return claimsIdentity.FindFirst(ClaimTypes.GivenName)?.Value;
        }
        /// <summary>
        /// 得到过期时间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetExpiration<T>(this IIdentity identity)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return default(T);
            }
            string value = claimsIdentity.FindFirst(ClaimTypes.Expiration)?.Value;
            if (value == null)
            {
                return default(T);
            }
            return value.AsTo<T>();

        }
        /// <summary>
        /// 得到角色
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetRoles<T>(this IIdentity identity, string type = ClaimTypes.Role)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return new string[0];
            }
            string value = claimsIdentity.FindFirst(type)?.Value;
            return value != null ? value.Split() : new string[0];
        }

        /// <summary>
        /// 获取登陆人Id 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetIdentityServer4SubjectId(this IIdentity identity)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return null;
            }
            return ((identity as ClaimsIdentity).FindFirst("sub") ?? throw new InvalidOperationException("sub claim is missing")).Value;
        }
        /// <summary>
        /// 获取登陆人Id泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetIdentityServer4SubjectId<T>(this IIdentity identity)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return default(T);
            }
            var value = ((identity as ClaimsIdentity).FindFirst("sub") ?? throw new InvalidOperationException("sub claim is missing")).Value;
            if (value == null)
            {
                return default(T);
            }
            return value.AsTo<T>();
        }
        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetTenantId<T>(this IIdentity identity)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return default(T);
            }
            var value = ((identity as ClaimsIdentity).FindFirst("TenantId") ?? throw new InvalidOperationException("TenantId claim is missing")).Value;
            if (value == null)
            {
                return default(T);
            }
            return value.AsTo<T>();
        }

        /// <summary>
        /// 获取租户Id
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetTenantId(this IIdentity identity)
        {
            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity))
            {
                return null;
            }
            var value = ((identity as ClaimsIdentity).FindFirst("TenantId") ?? throw new InvalidOperationException("TenantId claim is missing")).Value;
            return value;
        }

        /// <summary>
        /// 查找对应类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T FindFirst<T>(this IIdentity identity,string type)
        {

            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return default(T);
            }
            string value = claimsIdentity.FindFirst(type)?.Value;
            if (value == null)
            {
                return default(T);
            }
            return value.AsTo<T>();
        }




        /// <summary>
        /// 查找对应类型
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string  FindFirst(this IIdentity identity, string type)
        {

            identity.NotNull(nameof(identity));
            if (!(identity is ClaimsIdentity claimsIdentity))
            {
                return null;
            }
            string value = claimsIdentity.FindFirst(type)?.Value;
            return value;
        }
    }
}
