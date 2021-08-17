using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.AspNetCore.Api
{
    /// <summary>
    /// 要授权的控制器
    /// </summary>
    [Authorize]
    public abstract class AdminControllerBase : ApiControllerBase
    {
      

    }
}
