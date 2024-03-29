﻿using System.ComponentModel;

namespace DestinyCore.Entity
{
    /// <summary>
    /// 实体接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<out TKey> : IEntity
    {
        [Description("主键")]
        TKey Id { get; }
    }



}
