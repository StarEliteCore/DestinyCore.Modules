using Microsoft.EntityFrameworkCore;
using System;

namespace DestinyCore.EntityFrameworkCore
{
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder b);

        Type DbContextType { get; }

        /// <summary>
        /// 获取 相应的实体类型
        /// </summary>
        Type EntityType { get; }
    }
}