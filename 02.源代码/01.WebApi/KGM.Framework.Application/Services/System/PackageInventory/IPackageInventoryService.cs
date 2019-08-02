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
    /// 仓库包装接口
    /// </summary>
   public interface IPackageInventoryService : IService<PackageInventoryEntity>
    {
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary> 
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns>
        Task<PagerEntity<PackageInventorySingleDto>> QueryPackageIuveutoryByPagesAsync(PagerInfo pager, List<SearchCondition> condition = null);
        /// <summary>
        /// 获取包装档案
        /// </summary>
        /// <returns></returns>
        Task<List<PackageInventorySingleDto>> QueryPackageInventoryAll();
    }
}
