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
    /// 工位档案服务实现
    /// </summary>
    public class PositionService : BaseService<PositionFilesEntity>, IpositionService
    {
        #region 私有成员
        private readonly IPositionFilesRepository _repository;
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public PositionService(IPositionFilesRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        #endregion
    }
}
