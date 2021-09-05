using DestinyCore.Enums;
using DestinyCore.Filter.Abstract;
using DestinyCore.Ui;
using System.Collections.Generic;

namespace DestinyCore.Filter
{
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T">动态类型</typeparam>
    public class PageResult<T> : ResultBase, IPagedResult<T>, IHasResultType<ResultType>
    {

        public PageResult() : this(new T[0], 0, "查询成功", true)
        {

        }
        public PageResult(IReadOnlyList<T> itemList, int total, string message = "查询成功", bool success = true)
        {
            ItemList = itemList;
            Total = total;
            Success = success;
            this.Message = message;
            Type = success ? ResultType.Success : ResultType.Error;
        }


        /// <summary>
        /// 总数
        /// </summary>

        public int Total { get; set; }

        public IReadOnlyList<T> ItemList { get; set; }

        public ResultType Type { get; set; }
    }
}
