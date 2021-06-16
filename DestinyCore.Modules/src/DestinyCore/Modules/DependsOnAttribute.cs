using System;

namespace DestinyCore.Modules
{
    /// <summary>
    /// 依赖器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute, IDependedTypesProvider
    {
        /// <summary>
        /// 依赖类型集合
        /// </summary>
        private Type[] DependedTypes { get; }

        public DependsOnAttribute(params Type[] dependedTypes)
        {
            DependedTypes = dependedTypes ?? new Type[0];
        }
        /// <summary>
        ///  得到依赖类型集合
        /// </summary>
        /// <returns></returns>
        public virtual Type[] GetDependedTypes()
        {
            return DependedTypes;
        }

    }
}
