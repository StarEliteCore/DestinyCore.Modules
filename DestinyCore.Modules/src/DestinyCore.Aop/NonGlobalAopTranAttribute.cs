using AspectCore.DynamicProxy;
using DestinyCore.Entity;
using System;
using System.Threading.Tasks;

namespace DestinyCore.Aop
{
    public class NonGlobalAopTranAttribute : AbstractInterceptorAttribute
    {
        //[FromServiceContext]
        //private IUnitOfWork _unitOfWork { get; set; }
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var _unitOfWork = context.ServiceProvider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            try
            {
                _unitOfWork.BeginTransaction();
                Console.WriteLine("代理方法执行前");
                await next(context);
                Console.WriteLine("代理方法执行后");
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }

        }
    }
}
