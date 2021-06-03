using DestinyCore.Ui;

namespace DestinyCore.Filter
{
    public abstract class ResultBase<TData> : ResultBase, IResultData<TData>
    {
        public virtual TData Data { get; set; }
    }
}
