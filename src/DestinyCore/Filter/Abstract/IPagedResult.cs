using DestinyCore.Enums;
using DestinyCore.Ui;

namespace DestinyCore.Filter.Abstract
{
    public interface IPagedResult<TModel> : IResultBase, IListResult<TModel>, IHasResultType<ResultType>
    {


        int Total { get; }
    }
}
