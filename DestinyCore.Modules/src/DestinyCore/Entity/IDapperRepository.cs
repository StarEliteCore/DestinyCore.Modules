using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DestinyCore
{
    public interface IDapperRepository: IDisposable
    {
        IDbConnection DbConnection { get; }

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param);
    }
}