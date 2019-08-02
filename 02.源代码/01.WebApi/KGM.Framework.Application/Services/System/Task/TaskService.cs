using AutoMapper;
using KGM.Framework.Application.Core;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 任务服务实现
    /// </summary>
    public class TaskService : BaseService<TaskEntity>, ITaskService
    {
        #region 私有成员
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;
        #endregion
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public TaskService(ITaskRepository repository, IMapper mapper) : base(repository, mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        #endregion
        #region 接口实现
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<string> CreateTask(List<Inventorys> list, string userId, string warehouse, string workstation, string ScanBatch,int ScanQty)
        { 
            DataTable dt = new DataTable();
            dt.Columns.Add("CINVCODE", typeof(string));
            dt.Columns.Add("CBATCH", typeof(string));
            dt.Columns.Add("IQUANTITY", typeof(int));
            dt.Columns.Add("DR", typeof(int));

            foreach (var inv in list)
            {
                var dr = dt.NewRow();
                dr["CINVCODE"] = inv.cInvCode;
                dr["CBATCH"] = inv.cBatch;
                dr["IQUANTITY"] = inv.iquantity;
                dr["DR"] = inv.ConstantVolume;
                dt.Rows.Add(dr);
            }

            return await Task.Run(() =>
            {
                return _repository.CreateTask(dt, userId, warehouse, workstation,ScanBatch, ScanQty);
            });
        }
        #endregion
    }
}
