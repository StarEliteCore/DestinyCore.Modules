using DestinyCore.Enums;
using DestinyCore.Filter;
using DestinyCore.Helpers;
using DestinyCore.Ui;

namespace DestinyCore.AspNetCore
{
    /// <summary>
    /// Ajax操作结果
    /// </summary>
    public class AjaxResult: AjaxResult<object>, IHasResultType<AjaxResultType>
    {
        public AjaxResult()
        {
        }

        public AjaxResult(AjaxResultType type = AjaxResultType.Success)
            : base("", null, type)
        {
        }

        public AjaxResult(string message, AjaxResultType type = AjaxResultType.Success, object data = null)
            : base(message, data, type)
        {
        }

        public AjaxResult(AjaxResultType type = AjaxResultType.Success, object data = null)
            : base("", data, type)
        {
        }

        public AjaxResult(string message, object data, AjaxResultType type):base(message, data, type)
        {
          
        }

        public AjaxResult(string message, bool success, object data, AjaxResultType type):base(message,success,data,type)
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
    public class AjaxResult<TData> : ResultBase<TData>, IHasResultType<AjaxResultType>
    {
        public AjaxResultType Type
        {
            get;
            set;
        }

        public AjaxResult()
        {
        }

        public AjaxResult(AjaxResultType type = AjaxResultType.Success)
            : this("", default(TData), type)
        {
        }

        public AjaxResult(string message, AjaxResultType type = AjaxResultType.Success, TData data = default(TData))
            : this(message, data, type)
        {
        }

        public AjaxResult(AjaxResultType type = AjaxResultType.Success, TData data = default(TData))
            : this("", data, type)
        {
        }

        public AjaxResult(string message, TData data, AjaxResultType type)
        {
            Message = message;
            Data = data;
            Type = type;
            Success = Succeeded();
        }

        public AjaxResult(string message, bool success, TData data, AjaxResultType type)
        {
            Message = message;
            Data = data;
            Type = type;
            Success = success;
        }

        public bool Succeeded()
        {
            return Type == AjaxResultType.Success;
        }

        public bool Error()
        {
            return Type == AjaxResultType.Error;
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