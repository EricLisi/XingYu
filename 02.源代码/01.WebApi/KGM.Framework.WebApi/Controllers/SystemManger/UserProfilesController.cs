using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Application.Services;
using KGM.Framework.Infrastructure;
using KGM.Framework.WebApi.Model.Condition;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KGM.Framework.WebApi.Controllers
{
    /// <summary>
    /// 用户档案接口
    /// </summary> 
    public class UserProfilesController : Base.AppBaseController
    {
        #region 依赖注入
        IUserProfilesService _service;
        ITaskService _taskService;
        IU8Service _u8service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">用户服务</param> 
        /// <param name="u8Service">U8服务</param> 
        /// <param name="taskService">任务服务</param> 
        public UserProfilesController(IUserProfilesService service, IU8Service u8Service, ITaskService taskService)
        {
            this._service = service;
            this._u8service = u8Service;
            this._taskService = taskService;
        }
        #endregion

        #region Get 请求
        /// <summary>
        /// 获取用户档案
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("QueryUserProfilesAll")]
        public async Task<IActionResult> QueryUserProfilesAll()
        {
            return Ok(await _service.QueryUserProfilesAll());
        }
        /// <summary>
        /// 获取Table数据
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("queryAll")]
        public async Task<DataTable> getTable()
        {
            List<UserProfilesSingleDto> list = await _service.QueryAsync<UserProfilesSingleDto>();
            DataTable dt = new DataTable();
            dt = ListToDataTable(list);
            return dt;
        }

        /// <summary>
        /// 绑定员工下拉框
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetSelectJson")]
        public async Task<IActionResult> GetSelectJson()
        {
            var list = await _service.QueryAsync<UserProfilesSingleDto>();

            if (list == null || list.Count == 0)
            {
                return NotFound();
            }

            List<object> data = new List<object>();
            foreach (UserProfilesSingleDto item in list)
            {
                data.Add(new { id = item.Id, text = item.RealName });
            }
            return Ok(data);
        }

        /// <summary>
        /// 绑定员工下拉框
        /// </summary>
        /// <param name="cWhCode">仓库</param>
        /// <param name="position">工位</param>
        /// <returns></returns>
        [HttpGet, Route("GetSelectJson/{cWhCode}/{position}")]
        public async Task<IActionResult> GetSelectJson(string cWhCode, string position)
        {
            List<SearchCondition> conditions = new List<SearchCondition>();
            conditions.Add(new SearchCondition
            {
                Filed = "Storage",
                Value = cWhCode,
                Operation = CommonEnum.ConditionOperation.Equal
            });
            conditions.Add(new SearchCondition
            {
                Filed = "WorkStation",
                Value = position,
                Operation = CommonEnum.ConditionOperation.NotEqual
            });
            var list = await _service.QueryAsync<UserProfilesSingleDto>(conditions);

            if (list == null || list.Count == 0)
            {
                return NotFound();
            }

            List<object> data = new List<object>();
            foreach (UserProfilesSingleDto item in list)
            {
                data.Add(new { id = item.Id, text = item.RealName });
            }
            return Ok(data);
        }

        /// <summary>
        /// 根据Id获取用户档案信息
        /// </summary>
        /// <param name="Id">用户档案Id</param>
        /// <returns></returns>
        [HttpGet, Route("{Id}")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            UserProfilesSingleDto userInfoMation = await _service.QueryByIdAsync<UserProfilesSingleDto>(Id);
            if (userInfoMation != null)
            {
                List<U8Warehouse> u8 = await _u8service.GetAllWarehouse(userInfoMation.Storage);
                if (u8.Count != 0)
                {
                    userInfoMation.cWhName = u8[0].CWhName;
                }
            }

            return Ok(userInfoMation);
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        [HttpGet, Route("GetByPagesAsync")]
        public async Task<IActionResult> GetByPagesAsync([FromQuery]UserProfilesIndexCondition condition)
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
            if (!string.IsNullOrEmpty(condition.Account))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "Account",
                    Value = condition.Account,
                    Operation = CommonEnum.ConditionOperation.Like
                });
            }
            //数据库查询 
            var entity = await _service.QueryUserProfilesByPagesAsync(pager, condList);
            List<U8Warehouse> u8 = await _u8service.GetAllWarehouse("1");
            foreach (var item in entity.Entity)
            {
                item.cWhName = u8.Find(it => it.CWhCode == item.Storage).CWhName;
            }
            //返回分页结果
            return PagerListAction(pager, entity);
        }
        #endregion

        #region Post请求
        /// <summary>
        /// 新增用户档案
        /// </summary>
        /// <param name="entity">用户档案对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]UserProfilesSingleDto entity)
        {
            try
            {
                var users = await _service.QueryAsync<UserProfilesSingleDto>();
                if (users.Find(it => it.EnCode == entity.EnCode || it.Account == entity.Account) != null)
                {
                    return Ok(new { Status = false, Message = "已存在，请重新输入" });
                }
                entity.Password = SecurityHelper.MD5Encrypt32(entity.Password);
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
        /// 更新用户档案
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="entity">用户档案对象</param>
        /// <returns></returns>
        [HttpPut, Route("Update/{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]UserProfilesSingleDto entity)
        {
            try
            {
                entity.Id = Id;
                entity.Password = SecurityHelper.MD5Encrypt32(entity.Password);
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
        #endregion

        #region Delete请求
        /// <summary>
        /// 删除用户档案
        /// </summary>
        /// <param name="Id">用户档案 Id</param>
        /// <returns></returns>
        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                List<SearchCondition> conditions = new List<SearchCondition>();
                conditions.Add(new SearchCondition
                {
                    Filed = "OperationUser",
                    Value = Id,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                conditions.Add(new SearchCondition
                {
                    Filed = "Status",
                    Value = "0",
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                List<TaskSingleDto> taskSingles = await _taskService.QueryAsync<TaskSingleDto>(conditions);//有没有正在作业的任务
                if (taskSingles.Count > 0)
                {
                    return Ok(new { Status = false, Message = "用户正在操作，不允许删除" });
                }

                int rows = await _service.DeleteByKey(Id);//得到影响行数
                if (rows > 0)
                {
                    return NoContentAction();
                }
                return OKAction(false, "删除失败!");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"删除失败!原因:{ex.Message}");
            }
        }
        #endregion
    }
}