using AutoMapper;
using KGM.Framework.Application.Core;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 包装仓库服务实现
    /// </summary>
    public class PackageInventoryService : BaseService<PackageInventoryEntity>, IPackageInventoryService
    {
        #region 私有成员
        private readonly IPackageInventoryRepository _repository;
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public PackageInventoryService(IPackageInventoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        #endregion
        #region 接口实现
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="condition"></param>
        /// <returns></returns>

        public async Task<PagerEntity<PackageInventorySingleDto>> QueryPackageIuveutoryByPagesAsync(PagerInfo pager, List<SearchCondition> condition = null)
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryPackageIuveutoryByPagers(pager, condition);
                return _mapper.Map<PagerEntity<PackageInventorySingleDto>>(list);
            });
        }
        /// <summary>
        /// 获取包装档案
        /// </summary>
        /// <returns></returns>
        public async Task<List<PackageInventorySingleDto>> QueryPackageInventoryAll()
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryPackageInventoryAll();
                return _mapper.Map<List<PackageInventorySingleDto>>(list);
            });
        }
        #endregion
    }
}
