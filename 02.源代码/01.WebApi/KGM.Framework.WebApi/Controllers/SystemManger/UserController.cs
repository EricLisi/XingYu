using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Services;
using KGM.Framework.Infrastructure;
using KGM.Framework.WebApi.Model.Condition;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KGM.Framework.WebApi.Controllers
{
    /// <summary>
    /// 用户管理接口
    /// </summary> 
    public class UserController : Base.AppBaseController
    {
        #region 依赖注入
        IUserService _service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">模块服务</param> 
        public UserController(IUserService service)
        {
            this._service = service;
        }
        #endregion 

        #region Get 请求
        /// <summary>
        /// 根据Id获取菜单信息和他的按钮信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        [HttpGet, Route("{Id}")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            return Ok(await _service.QueryByIdAsync<UserSingleDto>(Id));
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        [HttpGet, Route("GetByPages")]
        public async Task<IActionResult> GetByPagesAsync([FromQuery]UserIndexCondition condition)
        {
            //设置分页对象
            var pager = SetPager(condition.Page, condition.Rows, condition.SIdx, condition.Sord);
            //设置过滤条件
            var condList = new List<SearchCondition>();
            if (!string.IsNullOrEmpty(condition.EnCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "EnCode",
                    Value = condition.EnCode,
                    Operation = CommonEnum.ConditionOperation.Like
                });
            }
            if (!string.IsNullOrEmpty(condition.RealName))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "RealName",
                    Value = condition.RealName,
                    Operation = CommonEnum.ConditionOperation.Like
                });
            }

            if (!string.IsNullOrEmpty(condition.CompanyId))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "CompanyId",
                    Value = condition.CompanyId,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }

            if (!string.IsNullOrEmpty(condition.DepartmentId))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "DepartmentId",
                    Value = condition.DepartmentId,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }

            //数据库查询 
            var entity = await _service.QueryUserByPagesAsync(pager, condList); 
            //返回分页结果
            return PagerListAction(pager, entity);
        }
        #endregion

        #region Post请求
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]UserSingleDto entity)
        {
            try
            {
                int rows = await _service.Insert(entity);//得到影响行数
                if (rows > 0)
                {
                    return CreateAction();
                }

                return OKAction(false, "创建失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"创建失败!原因:{ex.Message}");
            }
        }
        #endregion

        #region Put请求
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        [HttpPut, Route("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]UserSingleDto entity)
        {
            try
            {
                entity.Id = Id;
                int rows = await _service.Update(entity);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "更新成功");
                }

                return NotFoundAction(false, "更新失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"更新失败!原因:{ex.Message}");
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPut, Route("UpdatePwd/{Id}/{pwd}")]
        public async Task<IActionResult> UpdatePwd(string Id, string pwd)
        {
            try
            {
                int rows = await _service.UpdatePwd(Id,pwd);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "更新成功");
                }

                return NotFoundAction(false, "更新失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"更新失败!原因:{ex.Message}");
            }
        }
        #endregion

        #region Delete请求
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                int rows = await _service.DeleteByKey(Id);//得到影响行数
                if (rows > 0)
                {
                    return NoContentAction();
                }

                return NotFoundAction(false, "删除失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"删除失败!原因:{ex.Message}");
            }
        }
        #endregion
    }
}