using DestinyCore.Dependency;
using System;

namespace DestinyCore.Security.Jwt
{
    /// <summary>
    /// JwtBearer服务
    /// </summary>
    public interface IJwtBearerService: IScopedDependency
    {
        JwtResult CreateToken(Guid userId, string userName);
    }
}
