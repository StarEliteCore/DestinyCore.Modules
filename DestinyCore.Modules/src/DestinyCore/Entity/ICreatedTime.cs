using DestinyCore.Audit;
using System;

namespace DestinyCore.Entity
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public interface ICreatedTime
    {
        [DisableAuditing]
        DateTime CreatedTime { get; set; }
    }
}