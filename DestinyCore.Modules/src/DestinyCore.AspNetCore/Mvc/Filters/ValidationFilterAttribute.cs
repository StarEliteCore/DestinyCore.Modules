
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DestinyCore.Validation.Interceptor;
using DestinyCore.Extensions;

namespace DestinyCore.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    //验证特性
    public class ValidationFilterAttribute : ActionFilterAttribute
    {

        private readonly MethodInvocationValidator _validator;
        private readonly ILogger _logger;

        public ValidationFilterAttribute(MethodInvocationValidator validator, ILoggerFactory loggerFactory)
        {
            _validator = validator;
            _logger = loggerFactory.CreateLogger(nameof(ValidationFilterAttribute));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var method = action.MethodInfo;


            var arguments = context.ActionArguments.Values.ToArray();
            _logger.LogInformation($"开启验证:{method.Name}");
            var failures = _validator.Validate(method, arguments);

            if (failures.Any())
            {
                _logger.LogInformation($"验证失败:{method.Name} {failures.Select(o => o.ToString()).ToJoin()}");
                context.Result = new JsonResult( new AjaxResult() { Type = Enums.AjaxResultType.Error, Message = failures.Select(o => o.ToString()).ToJoin() });
                return;
            }

            _logger.LogInformation($"验证成功:{method.Name}");
        }
       

    }
}
