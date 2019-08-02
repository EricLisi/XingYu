using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Services;
using KGM.Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KGM.Framework.WebApi.Controllers
{
    /// <summary>
    /// 数据字典类型管理接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        #region 依赖注入
        IItemsService _service;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service"></param>
        public ItemsController(IItemsService service)
        {
            this._service = service;
        }
        #endregion


        /// <summary>
        /// 获取全部数据字典类型信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.QueryAsync());
        }

        /// <summary>
        /// 根据Id获取数据字典类型信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet, Route("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            return Ok(await _service.QueryByIdAsync(Id));
        }


        /// <summary>
        /// / 数据字典类型树菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetTreeJson")]
        public async Task<IActionResult> GetTreeJson() {
            return Ok(GetTreeJsonStr(await _service.QueryAsync()));
        }
        /// <summary>
        /// 获取树形结构JSon
        /// </summary>
        /// <param name="data">数据对象集合</param>
        /// <returns></returns>
        protected virtual string GetTreeJsonStr(List<ItemsGetDto> data)
        {
            var treeList = new List<TreeViewModel>();
            foreach (ItemsGetDto item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.FullName;
                tree.value = item.EnCode;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return TreeViewJson(treeList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static string TreeViewJson(List<TreeViewModel> data, string parentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeViewModel> item = data.FindAll(t => t.parentId == parentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeViewModel entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"id\":\"" + entity.id + "\",");
                    strJson.Append("\"text\":\"" + entity.text.Replace("&nbsp;", "") + "\",");
                    strJson.Append("\"value\":\"" + entity.value + "\",");
                    if (entity.title != null && !string.IsNullOrEmpty(entity.title.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"title\":\"" + entity.title.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.img != null && !string.IsNullOrEmpty(entity.img.Replace("&nbsp;", "")))
                    {
                        strJson.Append("\"img\":\"" + entity.img.Replace("&nbsp;", "") + "\",");
                    }
                    if (entity.checkstate != null)
                    {
                        strJson.Append("\"checkstate\":" + entity.checkstate + ",");
                    }
                    if (entity.parentId != null)
                    {
                        strJson.Append("\"parentnodes\":\"" + entity.parentId + "\",");
                    }
                    strJson.Append("\"showcheck\":" + entity.showcheck.ToString().ToLower() + ",");
                    strJson.Append("\"isexpand\":" + entity.isexpand.ToString().ToLower() + ",");
                    if (entity.complete == true)
                    {
                        strJson.Append("\"complete\":" + entity.complete.ToString().ToLower() + ",");
                    }
                    strJson.Append("\"hasChildren\":" + entity.hasChildren.ToString().ToLower() + ",");
                    strJson.Append("\"ChildNodes\":" + TreeViewJson(data, entity.id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}