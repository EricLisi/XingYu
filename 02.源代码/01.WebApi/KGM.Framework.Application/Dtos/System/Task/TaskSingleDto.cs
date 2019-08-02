using Newtonsoft.Json;
using System;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 任务记录Dto
    /// </summary>
   public class TaskSingleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [JsonProperty(PropertyName = "taskid", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskId { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        [JsonProperty(PropertyName = "operationuser", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationUser { get; set; }
        /// <summary>
        /// 工位
        /// </summary>
        [JsonProperty(PropertyName = "workstations", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkStations { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [JsonProperty(PropertyName = "warehouse", NullValueHandling = NullValueHandling.Ignore)]
        public string WareHouse { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
        /// <summary>
        /// 任务数量
        /// </summary>
        [JsonProperty(PropertyName = "count", NullValueHandling = NullValueHandling.Ignore)]
        public int? Count { get; set; }
        /// <summary>
        /// 物料编码 
        /// </summary>
        [JsonProperty(PropertyName = "cinvcode", NullValueHandling = NullValueHandling.Ignore)]
        public string CinvCode { get; set; }
        /// <summary>
        /// 分组编码
        /// </summary>
        [JsonProperty(PropertyName = "groupid", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupId { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty(PropertyName = "cbatch", NullValueHandling = NullValueHandling.Ignore)]
        public string Cbatch { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [JsonProperty(PropertyName = "creatoruserid", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty(PropertyName = "creatortime", NullValueHandling = NullValueHandling.Ignore)]
        public  DateTime CreatorTime { get; set; }
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

    }
}
