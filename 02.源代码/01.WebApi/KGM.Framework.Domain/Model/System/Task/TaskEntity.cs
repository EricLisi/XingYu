using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;


namespace KGM.Framework.Domain
{
    /// <summary>
    /// 模块类 与数据库结构一致
    /// </summary>
    [MappingTable(TableName = "MST_Task")]
    public class TaskEntity: AggregateRoot
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 操作用户
        /// </summary>
        public string OperationUser { get; set; }
        /// <summary>
        /// 工位
        /// </summary>
        public string WorkStations { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string WareHouse { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 任务数量
        /// </summary>
        public int? Count { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string CinvCode { get; set; }
        /// <summary>
        /// 分组编码
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Cbatch { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string Define1 { get; set; }

    }
}
