using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KGM.Framework.Application.Core;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;

namespace KGM.Framework.Application.Services
{

    /// <summary>
    /// 用户档案接口
    /// </summary>
    public interface IUserProfilesService:IService<UserProfilesEntity>
    {
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary> 
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns>
        Task<PagerEntity<UserProfilesSingleDto>> QueryUserProfilesByPagesAsync(PagerInfo pager, List<SearchCondition> condition = null);
        /// <summary>
        /// 获取用户档案
        /// </summary>
        /// <returns></returns>
        Task<List<UserProfilesSingleDto>> QueryUserProfilesAll();
    }
}
