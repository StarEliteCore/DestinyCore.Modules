using DestinyCore.Audit;
using System;

namespace DestinyCore.Entity
{
    public interface IModificationTime
    {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisableAuditing]
        DateTime? LastModifionTime { get; set; }
    }
}