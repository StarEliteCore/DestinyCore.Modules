using DestinyCore.Enums;
using DestinyCore.Ui;
using System.Collections.Generic;

namespace DestinyCore.AspNetCore
{
    public class PageList<T> : ResultBase, IHasResultType<AjaxResultType>
    {
        public PageList() : this(new T[0], 0, "查询成功", true)
        {
        }

        public PageList(IEnumerable<T> itemList, int total, string message = "查询成功", bool success = true)
        {
            ItemList = itemList;
            Total = total;
            Success = success;
            this.Message = message;
        }

        public IEnumerable<T> ItemList { get; set; }

        public int Total { get; set; }
        public AjaxResultType Type { get; set; }
    }

    public class PageListDto : PageList<dynamic>
    {
    }
}