using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KGM.Framework.Application.Core;
using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;

namespace KGM.Framework.Application.Services
{
    /// <summary>
    /// 任务记录接口
    /// </summary>
    public interface ITaskService : IService<TaskEntity>
    {
        /// <summary>
        /// 创建Task
        /// </summary>
        /// <param name="list">U8库存集合</param>
        /// <returns></returns>
        Task<string> CreateTask(List<Inventorys> list, string userId, string warehouse, string workstation,string ScanBatch,int ScanQty);
    }
}
