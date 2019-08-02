using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KGM.Package.Models
{
    public class PackageInventoryEntity
    {/// <summary>
     /// 用户Id
     /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string cInvCode { get; set; }
        /// <summary>
        /// 物料mc
        /// </summary>
        public string cInvName { get; set; }
        /// <summary>
        /// 任务数量
        /// </summary>
        public int Qty { get; set; }
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
        /// 描述
        /// </summary>
        public string Desc { get; set; }

    }
}
