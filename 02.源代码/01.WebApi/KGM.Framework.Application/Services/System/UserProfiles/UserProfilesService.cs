using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KGM.Framework.Application.Core;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 用户档案服务实现
    /// </summary>
  public class UserProfilesService:BaseService<UserProfilesEntity>,IUserProfilesService
    {
        #region 私有成员
        private readonly IUserProfilesRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public UserProfilesService(IUserProfilesRepository repository, IMapper mapper) : base(repository, mapper)
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

        public async Task<PagerEntity<UserProfilesSingleDto>> QueryUserProfilesByPagesAsync(PagerInfo pager, List<SearchCondition> condition = null)
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryUserProfilesByPagers(pager, condition);
                return _mapper.Map<PagerEntity<UserProfilesSingleDto>>(list);
            });
        }
        /// <summary>
        /// 获取用户档案
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserProfilesSingleDto>> QueryUserProfilesAll()
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryUserProfilesAll();
                return _mapper.Map<List<UserProfilesSingleDto>>(list);
            });
        }
        #endregion
    }
}
