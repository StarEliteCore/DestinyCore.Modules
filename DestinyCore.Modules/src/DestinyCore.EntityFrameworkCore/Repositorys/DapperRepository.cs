using Dapper;
using DestinyCore.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DestinyCore.EntityFrameworkCore
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IUnitOfWork _unitOfWork = null;

        public DapperRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            DbConnection = _unitOfWork.GetDbContext().Database.GetDbConnection();
        }

        public IDbConnection DbConnection { get; set; }

        /// <summary>
        /// 显式调用Dispose方法 
        /// </summary>
        ~DapperRepository()
        {
            //为false
            Dispose(false);
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            //必须true
            Dispose(true);
            //不再调用终结器（析构器）
            GC.SuppressFinalize(this);
        }

        protected bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                //告诉GC，不要调用析构函数
                GC.SuppressFinalize(this);
                
            }
            DbConnection?.Dispose();
            _disposed = true;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param)
        {
            return DbConnection.QueryAsync<T>(sql, param);
        }
    }
}