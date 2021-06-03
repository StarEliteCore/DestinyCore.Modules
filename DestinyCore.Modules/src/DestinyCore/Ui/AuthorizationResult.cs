using DestinyCore.Enums;
using DestinyCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.Ui
{
    public class AuthorizationResult : ResultBase, IHasResultType<AuthResultType>
    {

        /// <summary>
        /// 是否成功
        /// </summary>
        public override bool Success => Type == AuthResultType.Success;

        /// <summary>
        /// 类型
        /// </summary>
        public AuthResultType Type { get; set; }


        public static AuthorizationResult Unauthorized()
        {

            return new AuthorizationResult() {
                Message = AuthResultType.Unauthorized.ToDescription(),
                Type= AuthResultType.Unauthorized
            };
        }
    }
}
