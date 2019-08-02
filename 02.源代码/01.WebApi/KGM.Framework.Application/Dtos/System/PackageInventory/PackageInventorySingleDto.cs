using Newtonsoft.Json;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 包装仓库Dto
    /// </summary>
    public class PackageInventorySingleDto
    {
        /// <summary>
        /// 规格
        /// </summary>
        [JsonProperty(PropertyName = "cinvstd", NullValueHandling = NullValueHandling.Include)]
        public string cInvStd { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [JsonProperty(PropertyName = "unit", NullValueHandling = NullValueHandling.Include)]
        public string Unit { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        [JsonProperty(PropertyName = "classifyname", NullValueHandling = NullValueHandling.Include)]
        public string ClassIfyName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary> 
        [JsonProperty(PropertyName = "cinvcode", NullValueHandling = NullValueHandling.Include)]
        public string cInvCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary> 
        [JsonProperty(PropertyName = "cinvname", NullValueHandling = NullValueHandling.Include)]
        public string cInvName { get; set; }
        /// <summary>
        /// 定容
        /// </summary> 
        [JsonProperty(PropertyName = "constantvolume", NullValueHandling = NullValueHandling.Include)]
        public double? ConstantVolume { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Include)]
        public string Id { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        [JsonProperty(PropertyName = "encode", NullValueHandling = NullValueHandling.Include)]
        public string EnCode { get; set; }
        /// <summary>
        /// 入库名称
        /// </summary>
        [JsonProperty(PropertyName = "putcwhname", NullValueHandling = NullValueHandling.Include)]
        public string PutcWhName { get; set; }
        /// <summary>
        /// 出库名称
        /// </summary>
        [JsonProperty(PropertyName = "outcwhname", NullValueHandling = NullValueHandling.Include)]
        public string OutcWhName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty(PropertyName = "desc", NullValueHandling = NullValueHandling.Include)]
        public string Desc { get; set; }
        /// <summary>
        /// 装箱数
        /// </summary>
        [JsonProperty(PropertyName = "packing", NullValueHandling = NullValueHandling.Include)]
        public int Packing { get; set; }
        /// <summary>
        /// 调出仓库
        /// </summary>
        [JsonProperty(PropertyName = "outstorage", NullValueHandling = NullValueHandling.Include)]
        public string OutStorage { get; set; }
        /// <summary>
        /// 调入仓库
        /// </summary>
        [JsonProperty(PropertyName = "putstorage", NullValueHandling = NullValueHandling.Include)]
        public string PutStorage { get; set; }
        /// <summary>
        /// 冻结状态
        /// </summary>
        [JsonProperty(PropertyName = "freezestatus", NullValueHandling = NullValueHandling.Include)]
        public string FreezeStatus { get; set; }

        /// <summary>
        /// 左右灯
        /// </summary>
        [JsonProperty(PropertyName = "define1", NullValueHandling = NullValueHandling.Include)]
        public string Define1 { get; set; }

        /// <summary>
        /// 工位
        /// </summary>
        [JsonProperty(PropertyName = "define2", NullValueHandling = NullValueHandling.Include)]
        public string Define2 { get; set; }

        /// <summary>
        /// 工位
        /// </summary>
        [JsonProperty(PropertyName = "define3", NullValueHandling = NullValueHandling.Include)]
        public string Define3 { get; set; }

    }
}
