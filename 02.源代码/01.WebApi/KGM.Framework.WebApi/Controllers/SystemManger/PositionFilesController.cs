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
    /// 工位档案接口
    /// </summary>
    public class PositionFilesController : Base.AppBaseController
    {
        #region 依赖注入
        IpositionService _service;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">工位档案服务</param> 
        public PositionFilesController(IpositionService service)
        {
            this._service = service;
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
            return Ok(await _service.QueryAsync<PositionFilesSingleDto>());
        }
        /// <summary>
        /// 根据条件获取工位档案
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GeByConditionAsync")]
        public async Task<IActionResult> GeByConditionAsync(string cWhCode, string Position)
        {
            var condList = new List<SearchCondition>();
            if (!string.IsNullOrEmpty(cWhCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "cWhCode",
                    Value = cWhCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(Position))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "PositionCode",
                    Value = Position,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
      
            return Ok(await _service.QueryAsync<PositionFilesSingleDto>(condList));
        }
        #endregion
    }
}