using AutoMapper;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Domain;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 用户服务实现
    /// </summary>
    public class CompetenceService : BaseService<CompetenceEntity, CompetenceGetDto>, ICompetenceService
    {
        #region 私有成员
        private readonly ICompetenceRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CompetenceService(ICompetenceRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        #endregion
    }
}
