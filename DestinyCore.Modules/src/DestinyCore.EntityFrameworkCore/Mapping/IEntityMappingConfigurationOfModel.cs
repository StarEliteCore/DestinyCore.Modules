
using DestinyCore.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DestinyCore.EntityFrameworkCore
{
    public interface IEntityMappingConfiguration<TEntity, TKey> : IEntityMappingConfiguration where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        void Map(EntityTypeBuilder<TEntity> builder);
    }
}