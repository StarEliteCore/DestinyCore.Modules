using System;
using System.Collections.Generic;
using System.Text;

namespace DestinyCore.Caching
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    public interface ICacheManager
    {
        ICache Cache { get; }
    }
}
