using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 用户档案的仓储接口
    /// </summary>
    public interface IUserProfilesRepository : IRepository<UserProfilesEntity>
    {
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns> 
        PagerEntity<UserProfilesEntity> QueryUserProfilesByPagers(PagerInfo pager, List<SearchCondition> condition = null);
        /// <summary>
        /// 获取用户档案信息
        /// </summary>
        /// <returns></returns>
        List<UserProfilesEntity> QueryUserProfilesAll();
    }
}
