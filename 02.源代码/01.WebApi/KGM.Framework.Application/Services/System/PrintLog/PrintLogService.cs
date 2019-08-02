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
    /// log服务实现
    /// </summary>
    public class PrintLogService : BaseService<PrintLogEntity>, IPrintLogService
    {
        #region 私有成员
        private readonly IPrintLogRepository _repository;
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public PrintLogService(IPrintLogRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }


        #endregion
    }
}
