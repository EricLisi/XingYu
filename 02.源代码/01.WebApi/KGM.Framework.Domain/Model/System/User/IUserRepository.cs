using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 用户档案的仓储方法
    /// </summary>
    public interface IUserRepository : IRepository<UserEntity>
    {
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns> 
        PagerEntity<UserEntity> QueryUserByPagers(PagerInfo pager, List<SearchCondition> condition = null);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        int UpdatePwd(string Id, string Pwd);
    }
}
