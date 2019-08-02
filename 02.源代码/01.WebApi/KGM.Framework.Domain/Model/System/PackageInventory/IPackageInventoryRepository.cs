using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 包装仓库的仓储接口
    /// </summary>
    public interface IPackageInventoryRepository : IRepository<PackageInventoryEntity>
    {
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns> 
        PagerEntity<PackageInventoryEntity> QueryPackageIuveutoryByPagers(PagerInfo pager, List<SearchCondition> condition = null);
        /// <summary>
        /// 获取包装档案信息
        /// </summary>
        /// <returns></returns>
        List<PackageInventoryEntity> QueryPackageInventoryAll();
    }
}
