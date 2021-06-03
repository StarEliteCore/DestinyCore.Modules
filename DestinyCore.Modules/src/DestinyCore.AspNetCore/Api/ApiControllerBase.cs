using DestinyCore.Dependency;
using Microsoft.AspNetCore.Mvc;

namespace DestinyCore.AspNetCore.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[EnableCors("Destiny.Core.Flow.API")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IocManage IocManage => IocManage.Instance;
    }
}