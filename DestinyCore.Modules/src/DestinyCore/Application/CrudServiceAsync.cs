using DestinyCore.Entity;
using DestinyCore.Ui;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DestinyCore.Application
{
    public abstract class CrudServiceAsync<TEntity, TPrimaryKey> : ICrudServiceAsync<TEntity, TPrimaryKey>
            where TEntity : IEntity<TPrimaryKey>, IEquatable<TPrimaryKey>
    {
        private readonly IServiceProvider _serviceProvider = null;
        private readonly IRepository<TEntity, TPrimaryKey> _repository = null;

        public Task<OperationResponse> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse> DeleteAsync(TPrimaryKey key)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResponse> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}