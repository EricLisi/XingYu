using KGM.Framework.Infrastructure;
using KGM.Framework.WebApi.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace KGM.Framework.WebApi.Controllers.Base
{
    /// <summary>
    /// 父控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AppBaseController : ControllerBase
    {
        #region 内部类
        /// <summary>
        /// 分页返回对象
        /// </summary>
        private class PagerActionEntity<TEntity> where TEntity : class
        {
            /// <summary>
            /// 数据源
            /// </summary>
            [JsonProperty(PropertyName = "rows", NullValueHandling = NullValueHandling.Ignore)]
            public IEnumerable<TEntity> Data;

            /// <summary>
            /// 当前页
            /// </summary>
            [JsonProperty(PropertyName = "page", NullValueHandling = NullValueHandling.Ignore)]
            public int PageIndex;

            /// <summary>
            /// 页数
            /// </summary>
            [JsonProperty(PropertyName = "total", NullValueHandling = NullValueHandling.Ignore)]
            public int PageCount;

            /// <summary>
            /// 总记录数
            /// </summary>
            [JsonProperty(PropertyName = "records", NullValueHandling = NullValueHandling.Ignore)]
            public int Total;


        }
        #endregion
        #region 响应返回
        /// <summary>
        /// 返回分页数据
        /// </summary>
        /// <typeparam name="TEntity">对象</typeparam>
        /// <param name="pager">分页对象</param> 
        /// <param name="data">PagerEntity数据源</param>
        /// <returns></returns>
        protected virtual IActionResult PagerListAction<TEntity>(PagerInfo pager, PagerEntity<TEntity> data)
            where TEntity : class
        {
            int pageCount = data.Total % pager.PageSize == 0 ? (data.Total / pager.PageSize) : (data.Total / pager.PageSize) + 1;
            return Ok(new PagerActionEntity<TEntity>
            {
                Data = data.Entity,
                PageIndex = pager.PageIndex,
                PageCount = pageCount,
                Total = data.Total
            });
        }

        /// <summary>
        /// 返回创建成功的响应
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        protected virtual IActionResult CreateAction(bool state = true, string message = "创建成功")
        {
            return Created(string.Empty, new { state, message });
        }


        /// <summary>
        /// 返回200的响应
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        protected virtual IActionResult OKAction(bool state = false, string message = "")
        {
            return Ok(new { state, message });
        }

        /// <summary>
        /// 返回400的响应
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        protected virtual IActionResult NotFoundAction(bool state = false, string message = "未找到所需的资源")
        {
            return NotFound(new { state, message });
        }

        /// <summary>
        /// 返回无参数的响应
        /// </summary> 
        /// <returns></returns>
        protected virtual IActionResult NoContentAction()
        {
            return NoContent();
        }

        #endregion
        #region 通用操作方法
        /// <summary>
        /// list 转table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable("tablename");
            foreach (var item in list.FirstOrDefault().GetType().GetProperties())
            {
                dt.Columns.Add(item.Name);
            }
            foreach (var item in list) {
                DataRow value = dt.NewRow();
                foreach (DataColumn dtcolumn in dt.Columns) {
                    int i = dt.Columns.IndexOf(dtcolumn);
                    if (value.GetType().IsPrimitive)
                    {
                        value[i] = item.GetType().GetProperty(dtcolumn.ColumnName).GetValue(item);
                    }
                    else {
                        value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtcolumn.ColumnName).GetValue(item));
                    }
                }
                dt.Rows.Add(value);
            }
            return dt;
        }
        /// <summary>
        /// 设置分页对象
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="sortFiled">排序字段 默认 SortCode</param>
        /// <param name="sortType">排序类型 默认 ASC</param>
        /// <returns></returns>
        protected virtual PagerInfo SetPager(int pageIndex, int pageSize, string sortFiled, string sortType)
        {
            return new PagerInfo
            {
                PageIndex = pageIndex == 0 ? 1 : pageIndex,
                PageSize = pageSize == 0 ? 999999 : pageSize,
                SortFiled = string.IsNullOrEmpty(sortFiled) ? "SortCode" : sortFiled,
                Sort = string.IsNullOrEmpty(sortType) ? "ASC" : sortType
            };
        }


        /// <summary>
        /// 递归初始化树
        /// </summary>
        /// <param name="nodes">结果</param>
        /// <param name="parentID">父ID</param>
        /// <param name="sources">数据源</param>
        protected virtual void ListToTreeJson(IList<TreeNode> nodes, string parentID, IList<TreeNode> sources)
        {
            TreeNode tempNode;
            //递归寻找子节点  
            var tempTree = sources.Where(item => item.ParentId == parentID).ToList();
            foreach (TreeNode node in tempTree)
            {
                tempNode = new TreeNode()
                {
                    Id = node.Id,
                    Text = node.Text,
                    ParentId = node.ParentId,
                    Value = node.Value,
                    CheckState = node.CheckState,
                    ShowCheck = node.ShowCheck,
                    Complete = node.Complete,
                    IsExpand = node.IsExpand,
                    HasChildren = node.HasChildren,
                    Img = node.Img,
                    Title = node.Title,
                    Children = new List<TreeNode>()
                };
                nodes.Add(tempNode);
                ListToTreeJson(tempNode.Children, node.Id, sources);
            }
        }
        #endregion
     
    }
}