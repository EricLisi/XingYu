using KGM.Framework.Infrastructure;
using Newtonsoft.Json;
using System;

namespace KGM.Framework.WebApi.Model.Condition
{
    /// <summary>
    /// 过滤条件
    /// </summary>
    public class PrintLogCondition
    {

        /// <summary>
        /// id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty(PropertyName = "user", NullValueHandling = NullValueHandling.Ignore)]
        public string User { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatorTime { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string cInvCode { get; set; }

    }
}
