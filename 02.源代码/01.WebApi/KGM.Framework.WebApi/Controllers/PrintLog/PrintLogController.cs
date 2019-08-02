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
    /// log接口
    /// </summary>
    public class PrintLogController : Base.AppBaseController
    {
        #region 依赖注入
        IPrintLogService _service;
        ITaskService _taskService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">log服务</param> 
        /// <param name="taskService">任务服务</param> 
        public PrintLogController(IPrintLogService service, ITaskService taskService)
        {
            this._service = service;
            this._taskService = taskService;
        }
        #endregion
        #region GET请求
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.QueryAsync<PrintLogSingleDto>());
        }
        /// <summary>
        /// 根据条件获取打印记录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GeByConditionAsync")]
        public async Task<IActionResult> GeByConditionAsync(string User, string cInvCode, string cWhCode)
        {
            var condList = new List<SearchCondition>();
            if (!string.IsNullOrEmpty(User))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "User",
                    Value = User,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(cInvCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "cInvCode",
                    Value = cInvCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(cWhCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "cWhCode",
                    Value = cWhCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            return Ok(await _service.QueryAsync<PrintLogSingleDto>(condList, "CreatorTime", false));
        }
        #endregion
        #region POST请求
        /// <summary>
        /// 添加打印记录 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost, Route("Insert")]
        public async Task<IActionResult> Insert(PrintLogSingleDto dto)
        {
            try
            {

                 int rows = await _service.Insert(dto);//得到影响行数
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
        /// 更新打印履历(临时更新)
        /// </summary>
        /// <returns></returns>
        [HttpPut, Route("UpdatePrintLog")]
        public async Task<IActionResult> UpdatePrintLog(PrintLogSingleDto entity)
        {
            try
            {
                PrintLogSingleDto dto = await _service.QueryByIdAsync<PrintLogSingleDto>(entity.Id);
                dto.TaskGroupId = entity.TaskGroupId;
                int rows = await _service.Update(dto);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "更新成功");
                }

                return OKAction(false, "更新失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"更新失败!原因:{ex.Message}");
            }
        }
        #endregion
        #region Delete请求
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Id">任务Id</param>
        /// <returns></returns>
        [HttpDelete, Route("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                int rows = await _service.DeleteByKey(Id);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "删除成功");
                }

                return OKAction(false, "删除失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"删除失败!原因:{ex.Message}");
            }
        }
        #endregion
    }
}