using Newtonsoft.Json;
using System;

namespace KGM.Framework.Application.Dtos
{
    public  class PrintLogGetDto2
    {
        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty(PropertyName = "user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [JsonProperty(PropertyName = "cwhcode", NullValueHandling = NullValueHandling.Ignore)]
        public string cWhCode { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        [JsonProperty(PropertyName = "cinvcode", NullValueHandling = NullValueHandling.Ignore)]
        public string cInvCode { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        [JsonProperty(PropertyName = "taskid", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty(PropertyName = "qty", NullValueHandling = NullValueHandling.Ignore)]
        public int? Qty { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty(PropertyName = "cbatch", NullValueHandling = NullValueHandling.Ignore)]
        public string cBatch { get; set; }
        /// <summary>
        /// 工位
        /// </summary>
        [JsonProperty(PropertyName = "workcode", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkCode { get; set; }
        /// <summary>
        ///  描述
        /// </summary>
        [JsonProperty(PropertyName = "desc", NullValueHandling = NullValueHandling.Ignore)]
        public string Desc { get; set; }
        /// <summary>
        ///  条码
        /// </summary>
        [JsonProperty(PropertyName = "barcode", NullValueHandling = NullValueHandling.Ignore)]
        public string BarCode { get; set; }
        /// <summary>
        ///  任务分组Id
        /// </summary>
        [JsonProperty(PropertyName = "taskgroupid", NullValueHandling = NullValueHandling.Ignore)]
        public string TaskGroupId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
}
