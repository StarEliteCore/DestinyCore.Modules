using Microsoft.AspNetCore.Mvc.Filters;
using System;
using DestinyCore.Extensions;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DestinyCore.Entity;
using DestinyCore.Exceptions;

namespace DestinyCore.AspNetCore
{
    public class UnitOfWorkAtrrribute : ActionFilterAttribute
    {
        private readonly IServiceProvider _serviceProvider = null;
        private readonly ILogger _logger = null;
        public UnitOfWorkAtrrribute(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
            _logger = serviceProvider.GetLogger<UnitOfWorkAtrrribute>();
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        }

        private readonly IUnitOfWork _unitOfWork = null;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _unitOfWork?.BeginTransaction();
            _unitOfWork?.Push();
        }


        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _unitOfWork?.Pop();
            if (context.Exception != null && context.ExceptionHandled)
            {
                var ex = context.Exception;
                _unitOfWork?.Rollback();
                MessageBox.Show(ex.Message,ex);
            }

            if (context.Result is ObjectResult result)
            {
                if (result.Value is AjaxResult ajax)
                {
                 
                    if (ajax.Success)
                    {
                        _unitOfWork?.Commit();
                    }
                    else
                    {
                        _unitOfWork?.Rollback();
                    }

                    context.Result = new JsonResult(new AjaxResult() { Type = ajax.Type, Message = ajax.Message,Data=ajax.Data,Success=ajax.Success });
                }


            }
        }
    }
}
