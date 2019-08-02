using KGM.Framework.Domain.Core;
using KGM.Framework.Infrastructure;
using System.Collections.Generic;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// log类，与实体数据库一致
    /// </summary>
    [MappingTable(TableName = "MST_PrintLog")]
    public class PrintLogEntity : AggregateRoot
    {
        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public string cWhCode{get;set;}
        /// <summary>
        /// 物料
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string cBatch { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string WorkCode { get; set; }   
        /// <summary>
        ///  描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 任务分组Id
        /// </summary>
        public string TaskGroupId { get; set; }

        public string Status { get; set; }
    }
}
