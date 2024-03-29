﻿using DestinyCore.Enums;
using DestinyCore.Extensions;
using DestinyCore.Ui;
using System;
using System.Threading.Tasks;

namespace DestinyCore.AspNetCore
{
    public static class AjaxResultExtensions
    {
        public static AjaxResult ToAjaxResult(this OperationResponse operationResponse)
        {
            var (message, type) = operationResponse.GetMessageWithAjaxType(operationResponse.Type);
            return new AjaxResult(message, type, operationResponse.Data) { Success = operationResponse.Success };
        }

        public static async Task<AjaxResult> ToAjaxResult(this Task<OperationResponse> operationResponse)
        {

            var response = await operationResponse;
            return response.ToAjaxResult();
        }


        public static AjaxResult ToAjaxResult<T>(this OperationResponse<T> operationResponse)
        {
            var (message, type) = operationResponse.GetMessageWithAjaxType(operationResponse.Type);
            return new AjaxResult(message, type, operationResponse.Data) { Success = operationResponse.Success };
        }


        public static async Task<AjaxResult<T>> ToAjaxResult<T>(this Task<OperationResponse<T>> operationResponse)
        {
            var response = await operationResponse.ConfigureAwait(false);
            var (message,type) = response.GetMessageWithAjaxType(response.Type);
            return new AjaxResult<T>(message, type, response.Data) { Success = response.Success };

        }

        /// <summary>
        /// 得到消息与结果类型
        /// </summary>
        /// <param name="resultBase"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static (string message, ResultType type) GetMessageWithAjaxType(this ResultBase resultBase, OperationResponseType type)
        {
            var message = resultBase.Message ?? type.ToDescription();
            return (message, type.ToResultType());
           
        }
        public static AjaxResultType ToAjaxResultType(this OperationResponseType responseType)
        {
            return responseType switch
            {
                OperationResponseType.Success => AjaxResultType.Success,
                OperationResponseType.NoChanged => AjaxResultType.Info,
                _ => AjaxResultType.Error,
            };
        }

        public static ResultType ToResultType(this OperationResponseType responseType)
        {
            return responseType switch
            {
                OperationResponseType.Success => ResultType.Success,
                OperationResponseType.NoChanged => ResultType.Info,
                _ => ResultType.Error,
            };
        }


        public static AjaxResultType ToAjaxResultType(this AuthResultType type)
        {
            return type switch
            {
                AuthResultType.Success => AjaxResultType.Success,
                AuthResultType.Unauthorized => AjaxResultType.Unauthorized,
                AuthResultType.NoFound => AjaxResultType.NoFound,
                AuthResultType.Uncertified => AjaxResultType.Uncertified,
                _ => AjaxResultType.Uncertified,
            };
        }

        public static ResultType ToResultType(this AuthResultType type)
        {
            return type switch
            {
                AuthResultType.Success => ResultType.Success,
                AuthResultType.Unauthorized => ResultType.Unauthorized,
                AuthResultType.NoFound => ResultType.NoFound,
                AuthResultType.Uncertified => ResultType.Uncertified,
                _ => ResultType.Uncertified,
            };

        }

        /// <summary>
        /// 转成Ajax结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static AjaxResult ToAjaxResult(this AuthorizationResult result)
        {

            var message = result.Message ?? result.Type.ToDescription();
            var type = result.Type.ToResultType();
            return new AjaxResult(message, type) { Success = result.Success };
        }
    }
}