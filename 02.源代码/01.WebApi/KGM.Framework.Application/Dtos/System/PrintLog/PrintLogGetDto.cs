using Newtonsoft.Json;
using System;

namespace KGM.Framework.Application.Dtos
{
    public class PrintLogGetDto
    {
        public string User { get; set; }
        public string cInvCode { get; set; }
        public string cWhCode { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
}
