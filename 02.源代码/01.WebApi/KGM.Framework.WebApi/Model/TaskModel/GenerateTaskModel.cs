using Newtonsoft.Json;
using System.Collections.Generic;

namespace KGM.Framework.WebApi.Model.TaskModel
{
    /// <summary>
    /// 生成任务所需的参数
    /// </summary>
    public class GenerateTaskModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty(PropertyName = "userid", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [JsonProperty(PropertyName = "cwhcode", NullValueHandling = NullValueHandling.Ignore)]
        public string CWhCode { get; set; }

        /// <summary>
        /// 工位
        /// </summary>
        [JsonProperty(PropertyName = "workstation", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkStation { get; set; }

        /// <summary>
        /// 当前扫描数量
        /// </summary>
        [JsonProperty(PropertyName = "scanqty", NullValueHandling = NullValueHandling.Ignore)]
        public int ScanQty { get; set; }

        /// <summary>
        /// 当前扫描的批次
        /// </summary>
        [JsonProperty(PropertyName = "scanbatch", NullValueHandling = NullValueHandling.Ignore)]
        public string ScanBatch { get; set; }
        /// <summary>
        /// 库存信息
        /// </summary>
        [JsonProperty(PropertyName = "stockdata", NullValueHandling = NullValueHandling.Ignore)]
        public List<StockData> DtStock { get; set; }


        /// <summary>
        /// 库存信息
        /// </summary>
        public class StockData
        {
            /// <summary>
            /// 存货编码
            /// </summary>
            [JsonProperty(PropertyName = "cinvcode", NullValueHandling = NullValueHandling.Ignore)]
            public string cInvCode { get; set; }

            /// <summary>
            /// 批次
            /// </summary>
            [JsonProperty(PropertyName = "cbatch", NullValueHandling = NullValueHandling.Ignore)]
            public string CBatch { get; set; }

            /// <summary>
            /// 库存数
            /// </summary>
            [JsonProperty(PropertyName = "iquantity", NullValueHandling = NullValueHandling.Ignore)]
            public decimal IQuantity { get; set; }
        }
    }
}
