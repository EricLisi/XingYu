using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 任务过滤条件
    /// </summary>
    public class TaskCondition
    {
        ///// <summary>
        ///// 用户Id
        ///// </summary>
        //[JsonProperty(PropertyName = "userid", NullValueHandling = NullValueHandling.Ignore)]
        //public string UserId { get; set; }
        /// <summary>
        ///  工位
        /// </summary>
        [JsonProperty(PropertyName = "workstations", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkStations { get; set; }

        /// <summary>
        /// 存货编码
        /// </summary>
        [JsonProperty(PropertyName = "cinvcode", NullValueHandling = NullValueHandling.Ignore)]
        public string cInvCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [JsonProperty(PropertyName = "cwhcode", NullValueHandling = NullValueHandling.Ignore)]
        public string cWhCode { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty(PropertyName = "cbatch", NullValueHandling = NullValueHandling.Ignore)]
        public string cBatch { get; set; }

        /// <summary>
        /// 分组Id
        /// </summary>
        [JsonProperty(PropertyName = "groupid", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
