using AutoMapper;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 角色服务实现
    /// </summary>
    public class ItemsDetailService : BaseService<ItemsDetailEntity, ItemsDetailGetDto>, IItemsDetailService
    {
        #region 私有成员
        private readonly IItemsDetailRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ItemsDetailService(IItemsDetailRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        #endregion
    }
}
