using DestinyCore.Application.Abstractions;
using DestinyCore.Entity;
using DestinyCore.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyCore.AspNetCore.Api
{




    [Authorize]
    public abstract class CrudAdminControllerBase<TPrimaryKey, TEntity, IInputDto, IPagedListDto> : AdminControllerBase
              where TEntity : class, IEntity<TPrimaryKey>
              where TPrimaryKey : IEquatable<TPrimaryKey>
             where IInputDto : IInputDto<TPrimaryKey>
             where IPagedListDto : IOutputDto<TPrimaryKey>
    {


        protected ICrudServiceAsync<TPrimaryKey, TEntity, IInputDto, IPagedListDto> CrudServiceAsync { get; set; }
        public CrudAdminControllerBase(ICrudServiceAsync<TPrimaryKey, TEntity, IInputDto, IPagedListDto> crudServiceAsync)
        {

            this.CrudServiceAsync = crudServiceAsync;
        }


        //[Description($"异步创建{Alias??string.Empty}")]
        [HttpPost]
        protected virtual async Task<AjaxResult> CreateAsync([FromBody] IInputDto dto)
        {
            
            return (await CrudServiceAsync.CreateAsync(dto)).ToAjaxResult();
        }


        [HttpPost]
        protected virtual async Task<AjaxResult> UpdateAsync([FromBody] IInputDto dto)
        {

            return (await CrudServiceAsync.UpdateAsync(dto)).ToAjaxResult();
        }

        [HttpDelete]
        protected virtual async Task<AjaxResult> DeleteAsync(TPrimaryKey id)
        {

            return (await CrudServiceAsync.DeleteAsync(id)).ToAjaxResult();

        }

 
        [HttpPost]
        public async Task<PageList<IPagedListDto>> GetPageAsync([FromBody] PageRequest request)
        {
            return (await CrudServiceAsync.GetPageAsync(request)).ToPageList();

        }
    }
}
