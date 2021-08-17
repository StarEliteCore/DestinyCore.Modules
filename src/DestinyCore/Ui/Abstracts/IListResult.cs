using System.Collections.Generic;

namespace DestinyCore.Ui
{
    public interface IListResult<T> : IResultBase
    {
        IReadOnlyList<T> ItemList { get; set; }
    }
}
