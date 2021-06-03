using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DestinyCore.EntityFrameworkCore
{
    public interface IDapperRepository
    {
        IDbConnection DbConnection { get; }

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param);
    }
}