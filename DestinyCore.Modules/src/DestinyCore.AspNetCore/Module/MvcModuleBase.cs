using DestinyCore.AspNetCore;
using DestinyCore.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyCore.AspNetCore
{

    public abstract class MvcModuleBase : AppModule
    {
        /// <summary>
        /// 前
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PreConfigureServices(ConfigureServicesContext context)
        {


        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            this.PreConfigureServices(context);
            AddCors(context);
            context.Services.AddControllers(o => this.AddMvcOptions(o)).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            this.AfterConfigureServices(context);
        }

        /// <summary>
        /// 后
        /// </summary>
        /// <param name="context"></param>
        protected virtual void AfterConfigureServices(ConfigureServicesContext context)
        { 
        
        
        }



        protected virtual void AddMvcOptions(MvcOptions options)
        {

        }
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app= context.GetApplicationBuilder();
            app.UseRouting();
            UseCors(context);

            app.UseAuthentication(); //认证
            app.UseAuthorization();//授权
        }

        protected virtual void AddCors(ConfigureServicesContext context)
        {


        }


        protected virtual void UseCors(ApplicationContext context)
        {



        }
    }
}
