using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace KGM.Package.Models
{
    public class TaskEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
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
        /// 仓库名
        /// </summary>
        public string WareHouseName { get; set; }
        /// <summary>
        /// 操作用户账户
        /// </summary>
        public string UserAccount { get; set; }
        /// <summary>
        /// 操作用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 临时打印记录
        /// </summary>
        public string Define1 { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
    }
}
