using DestinyCore.Enums;
using DestinyCore.Ui;
using System.Collections.Generic;

namespace DestinyCore.AspNetCore
{
    public class TreeModel<TData> : ResultBase, IListResult<TData>, IHasResultType<ResultType>  //这里到时候要统一
    {
        public TreeModel() : this(new TData[0], "成功返回数据", true)
        {
        }

        public TreeModel(IReadOnlyList<TData> itemList, string message = "成功返回数据", bool success = true)
        {
            ItemList = itemList;
            Message = message;
            Success = success;
        }

        public IReadOnlyList<TData> ItemList { get; set; }
        public ResultType Type { get; set; }
    }

    public class TreeModel<TData, TSelectedType> : TreeModel<TData>  //这里到时候要统一
    {
        public TreeModel() : this(new TData[0], new TSelectedType[0], "成功返回数据", true)
        {
        }

        public TreeModel(IReadOnlyList<TData> itemList, IReadOnlyList<TSelectedType> selectedList, string message = "成功返回数据", bool success = true)
        {
            ItemList = itemList;
            Message = message;
            Success = success;
            SelectedList = selectedList;
        }

        public IReadOnlyList<TSelectedType> SelectedList { get; set; }
    }

    public class TreeModel : TreeModel<object>
    {
    }
}