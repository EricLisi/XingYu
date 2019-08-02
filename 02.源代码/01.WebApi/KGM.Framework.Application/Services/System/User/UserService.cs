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
    /// 用户服务实现
    /// </summary>
    public class UserService : BaseService<UserEntity>, IUserService
    {
        #region 私有成员
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
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

        public async Task<PagerEntity<UserSingleDto>> QueryUserByPagesAsync(PagerInfo pager, List<SearchCondition> condition = null)
        {
            return await Task.Run(() =>
            {
                var list = _repository.QueryUserByPagers(pager, condition);
                return _mapper.Map<PagerEntity<UserSingleDto>>(list);
            });
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public async Task<int> UpdatePwd(string Id,string Pwd)
        {
            return await Task.Run(() =>
            {
                var list = _repository.UpdatePwd(Id, Pwd);
                return list;
            });
        }
        #endregion
    }
}
