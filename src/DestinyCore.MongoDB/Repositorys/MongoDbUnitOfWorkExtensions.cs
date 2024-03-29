﻿using DestinyCore.Enums;
using DestinyCore.Exceptions;
using DestinyCore.Extensions;
using DestinyCore.MongoDB.Repositorys;
using DestinyCore.Ui;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.MongoDB
{
    public static class MongoDbUnitOfWorkExtensions
    {

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="action">要执行的操作</param>
        /// <returns></returns>
        public static void UseTransaction(this IMongoDbUnitOfWork unitOfWork, Action action)
        {
            action.NotNull(nameof(action));
            if (unitOfWork.HasCommit())
            {
                return;
            }

            
            try
            {
                unitOfWork.StartSession();
                action.Invoke();
                unitOfWork.CommitTransaction();
            }
            catch (ResultException ex)
            {
                unitOfWork.AbortTransaction();
                ResultException.Throw(ex.Message, ex);
            }
            catch (Exception ex)
            {
                unitOfWork.LogError(ex);
                unitOfWork.AbortTransaction();
               
            }
        }

    

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        public static async ValueTask<OperationResponse> UseTransactionAsync(this IMongoDbUnitOfWork unitOfWork, Func<Task<OperationResponse>> func)
        {
            func.NotNull(nameof(func));
            OperationResponse result = new OperationResponse();
            if (unitOfWork.HasCommit())
            {
                result.Type = OperationResponseType.NoChanged;
                result.Message = "事务已提交!!";
                return result;
            }

            try
            {
                await unitOfWork.StartSessionAsync().ConfigureAwait(false);
                result = await func.Invoke().ConfigureAwait(false);
                if (!result.Success)
                {
                    await unitOfWork.AbortTransactionAsync().ConfigureAwait(false);
                    return result;
                }
                await unitOfWork.CommitTransactionAsync().ConfigureAwait(false);
            }
            catch (ResultException ex)
            {
                await unitOfWork.AbortTransactionAsync().ConfigureAwait(false);
                ResultException.Throw(ex.Message, ex);
                unitOfWork?.LogError(ex);
                return result;
            }
            catch (Exception ex)
            {

                await unitOfWork.AbortTransactionAsync().ConfigureAwait(false);
                unitOfWork?.LogError(ex);
                return OperationResponse.Exp(ex.Message);
            }
            return result;
        }

        private static void LogError(this IMongoDbUnitOfWork unitOfWork, Exception exception)
        {
           
            unitOfWork?.GetLogger()?.LogError(exception, exception.Message);
        }

  

        /// <summary>
        /// 开启事务 如果成功提交事务，失败回滚事务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="func"></param>
        /// <returns>返回操作结果</returns>
        public static OperationResponse UseTransaction(this IMongoDbUnitOfWork unitOfWork, Func<OperationResponse> func)
        {
            func.NotNull(nameof(func));
            OperationResponse result = new OperationResponse();
            if (unitOfWork.HasCommit())
            {
                result.Type = OperationResponseType.NoChanged;
                result.Message = "事务已提交!!";
                return result;
            }
            try
            {
                unitOfWork.StartSession();
                result = func.Invoke();
                unitOfWork.CommitTransaction();
                return result;
            }
            catch (ResultException ex)
            {
                unitOfWork.AbortTransaction();
                ResultException.Throw(ex.Message, ex);
                return result;
            }
            catch (Exception ex)
            {

                unitOfWork.AbortTransaction();
                unitOfWork?.LogError(ex);
                return OperationResponse.Exp(ex.Message);
            }
          

        }
    }
}
