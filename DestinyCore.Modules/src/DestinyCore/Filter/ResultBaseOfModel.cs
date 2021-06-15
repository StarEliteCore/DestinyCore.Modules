using DestinyCore.Ui;

namespace DestinyCore.Filter
{
    public abstract class ResultBase<TData> : ResultBase, IResultData<TData>
    {
        public virtual TData Data { get; set; }


        /// <summary>
        /// 得到数据
        /// </summary>
        /// <returns></returns>
        public virtual TData GetData() => this.Data;

    }
}
