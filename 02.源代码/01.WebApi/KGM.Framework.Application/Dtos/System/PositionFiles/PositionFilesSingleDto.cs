using Newtonsoft.Json;
using System;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 工位档案
    /// </summary>
    public class PositionFilesSingleDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        /// <summary>
        /// 工位号
        /// </summary>
        [JsonProperty(PropertyName = "positioncode", NullValueHandling = NullValueHandling.Ignore)]
        public string PositionCode { get; set; }
        /// <summary>
        /// 仓库号
        /// </summary>
        [JsonProperty(PropertyName = "cwhcode", NullValueHandling = NullValueHandling.Ignore)]
        public string cWhCode { get; set; }
    }
}
