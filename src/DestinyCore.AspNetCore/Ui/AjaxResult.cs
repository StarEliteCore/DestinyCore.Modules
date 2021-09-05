using DestinyCore.Enums;
using DestinyCore.Filter;
using DestinyCore.Helpers;
using DestinyCore.Ui;

namespace DestinyCore.AspNetCore
{
    /// <summary>
    /// Ajax操作结果
    /// </summary>
    public class AjaxResult: AjaxResult<object>, IHasResultType<ResultType>
    {
        public AjaxResult()
        {
        }

        public AjaxResult(ResultType type = ResultType.Success)
            : base("", null, type)
        {
        }

        public AjaxResult(string message, ResultType type = ResultType.Success, object data = null)
            : base(message, data, type)
        {
        }

        public AjaxResult(ResultType type = ResultType.Success, object data = null)
            : base("", data, type)
        {
        }

        public AjaxResult(string message, object data, ResultType type):base(message, data, type)
        {
          
        }

        public AjaxResult(string message, bool success, object data, ResultType type):base(message,success,data,type)
        {
        
        }

        /// <summary>
        /// 只得到数据
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public TData GetData<TData>()
        {

            return (TData)this.Data;
        }
    }


    /// <summary>
    /// Ajax操作结果
    /// </summary>
    public class AjaxResult<TData> : ResultBase<TData>, IHasResultType<ResultType>
    {
        public ResultType Type
        {
            get;
            set;
        }

        public AjaxResult()
        {
        }

        public AjaxResult(ResultType type = ResultType.Success)
            : this("", default(TData), type)
        {
        }

        public AjaxResult(string message, ResultType type = ResultType.Success, TData data = default(TData))
            : this(message, data, type)
        {
        }

        public AjaxResult(ResultType type = ResultType.Success, TData data = default(TData))
            : this("", data, type)
        {
        }

        public AjaxResult(string message, TData data, ResultType type)
        {
            Message = message;
            Data = data;
            Type = type;
            Success = Succeeded();
        }

        public AjaxResult(string message, bool success, TData data, ResultType type)
        {
            Message = message;
            Data = data;
            Type = type;
            Success = success;
        }

        public bool Succeeded()
        {
            return Type == ResultType.Success;
        }

        public bool Error()
        {
            return Type == ResultType.Error;
        }

        public virtual object ToObject()
        {
            return new
            {
                Data,
                Message,
                Success,
                Type
            };
        }

        public virtual string ToJson()
        {
            return ToObject().ToJson();
        }
    }
}