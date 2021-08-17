using System;

namespace DestinyCore.Modules
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IAppModule : IApplicationInitialization
    {
        void ConfigureServices(ConfigureServicesContext context);


        /// <summary>
        /// 配置
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="configureOptions">配置选项</param>
        void Configure<TOptions>(Action<TOptions> configureOptions) where TOptions : class;



        /// <summary>
        /// 得到依赖集合
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Type[] GetDependedTypes(Type moduleType = null);


        bool Enable { get; set; }


    }
}
