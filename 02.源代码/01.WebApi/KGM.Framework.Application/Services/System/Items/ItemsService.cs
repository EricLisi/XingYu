using AutoMapper;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 角色服务实现
    /// </summary>
    public class ItemsService : BaseService<ItemsEntity, ItemsGetDto>, IItemsService
    {
        #region 私有成员
        private readonly IItemsRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public ItemsService(IItemsRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        #endregion
    }
}
