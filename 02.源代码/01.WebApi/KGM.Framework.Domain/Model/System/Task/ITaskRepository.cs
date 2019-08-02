using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;
using System.Data;

namespace KGM.Framework.Domain
{
    public interface ITaskRepository: IRepository<TaskEntity>
    {
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns> 
        PagerEntity<TaskEntity> QueryTaskByPagers(PagerInfo pager, List<SearchCondition> condition = null);

        string CreateTask(DataTable dt, string userId, string warehouse, string workstation, string ScanBatch,int ScanQty);
    }
}
