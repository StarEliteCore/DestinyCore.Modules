using DestinyCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.CodeGenerator
{
    /// <summary>
    /// 
    /// </summary>
    public  class CodeGeneratorModeule : AppModule
    {

        public override void ConfigureServices(ConfigureServicesContext context)
        {

            context.Services.AddSingleton<ICodeGenerator, RazorCodeGenerator>();
            context.Services.AddSingleton<ICodeGeneratorService, CodeGeneratorService>();
        }
    }
}
