using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Application.Services;
using KGM.Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace KGM.Framework.WebApi.Controllers
{
    /// <summary>
    /// U8API
    /// </summary>
    public class U8Controller : Base.AppBaseController
    {
        #region 依赖注入
        IU8Service _service;
        ITaskService _taskService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">u8服务</param> 
        /// <param name="taskService">任务服务</param> 
        public U8Controller(IU8Service service, ITaskService taskService)
        {
            this._service = service;
            this._taskService = taskService;
        }
        #endregion


        #region Get 请求
        /// <summary>
        /// 获取U8仓库
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetWarehouse")]
        public async Task<IActionResult> GetWarehouse(string code)
        {
            return Ok(await _service.GetAllWarehouse(code));
        }
        /// <summary>
        /// 获取U8存货
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetInventorys")]
        public async Task<IActionResult> GetInventorys()
        {
            return Ok(await _service.GetInventorys());
        }
        /// <summary>
        /// 根据产品编号/仓库编号查存货库存详细数据
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <param name="cWhCode"></param>
        /// <returns></returns>
        [HttpGet, Route("QueryInventorysByCode/{cInvCode}/{cWhCode}")]
        public async Task<IActionResult> QueryInventorysByCode(string cInvCode, string cWhCode)
        {
            return Ok(await _service.QueryInventorysByCode(cInvCode, cWhCode));
        }
        /// <summary>
        /// 根据仓库编码获取U8存货
        /// </summary>
        /// <param name="WhCode">仓库号</param>
        /// <param name="cInvCode">物料号</param>
        /// <param name="position">工位号</param>
        /// <returns></returns>
        [HttpGet, Route("QueryInventorysByWhCode/{WhCode}/{cInvCode}/{position}")]
        public async Task<IActionResult> QueryInventorysByWhCode(string WhCode,string  cInvCode,string position)
        {
            List<Inventorys> inventorys = await _service.QueryInventorysByWhCode(WhCode, position);//u8存货list
            if (cInvCode != "1")
            {
                inventorys= inventorys.FindAll(it=>it.cInvCode==cInvCode);
            }
            return Ok(inventorys);
        } 
        #endregion
    }
}