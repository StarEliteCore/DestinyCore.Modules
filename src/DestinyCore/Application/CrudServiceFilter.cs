using DestinyCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.Application
{
  public abstract  class CrudServiceFilter<TPrimaryKey, IEntity, TInputDto, IPagedListDto> 
              where IEntity : class, IEntity<TPrimaryKey>
              where TPrimaryKey : IEquatable<TPrimaryKey>
              where TInputDto : class, IInputDto<TPrimaryKey>, new()
              where IPagedListDto : IPagedListDto<TPrimaryKey>
    {

        /// <summary>
        /// 插入检查
        /// </summary>
        protected Func<TInputDto, Task> InsertCheck { get; set; } = null;

        /// <summary>
        /// 更新检查
        /// </summary>
        protected Func<TInputDto, IEntity, Task> UpdateCheckFunc { get; set; } = null;
    }
}
