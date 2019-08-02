using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KGM.Framework.Application.Dtos.U8
{
    /// <summary>
    /// 仓库对象
    /// </summary>
    public class Inventorys
    {

        /// <summary>
        /// 检验状态码
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 已装箱
        /// </summary>
        public int boxed { get; set; }
        /// <summary>
        /// 定容
        /// </summary>
        public int? ConstantVolume { get; set; }
        /// <summary>
        /// 余量
        /// </summary>        
        public int? Margin { get; set; }
        /// <summary>
        /// 箱数
        /// </summary>        
        public int? BoxCount { get; set; }
        /// <summary>
        /// 存货编码
        /// </summary>
        [JsonProperty(PropertyName = "cinvcode", NullValueHandling = NullValueHandling.Include)]
        public string cInvCode { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary> 
        [JsonProperty(PropertyName = "cwhname", NullValueHandling = NullValueHandling.Include)]
        public string cWhname { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary> 
        [JsonProperty(PropertyName = "cwhcode", NullValueHandling = NullValueHandling.Include)]
        public string cWhCode { get; set; }

        /// <summary>
        /// 批号
        /// </summary> 
        [JsonProperty(PropertyName = "cbatch", NullValueHandling = NullValueHandling.Include)]
        public string cBatch { get; set; }
        /// <summary>
        /// 存货名称
        /// </summary> 
        [JsonProperty(PropertyName = "cinvname", NullValueHandling = NullValueHandling.Include)]
        public string cInvName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary> 
        [JsonProperty(PropertyName = "cinvstd", NullValueHandling = NullValueHandling.Include)]
        public string cInvStd { get; set; }
        /// <summary>
        /// 数量
        /// </summary> 
        [JsonProperty(PropertyName = "iquantity", NullValueHandling = NullValueHandling.Include)]
        public int iquantity { get; set; }
        /// <summary>
        /// 结存铺计数量
        /// </summary> 
        [JsonProperty(PropertyName = "inum", NullValueHandling = NullValueHandling.Include)]
        public double iNum { get; set; }
        /// <summary>
        /// 检验状态
        /// </summary>
        [JsonProperty(PropertyName = "ccheckstate", NullValueHandling = NullValueHandling.Include)] 
        public string cCheckState { get; set; }

        /// <summary>
        /// 图号
        /// </summary>
        [JsonProperty(PropertyName = "th", NullValueHandling = NullValueHandling.Include)]
        public string th { get; set; }

        /// <summary>
        /// 左右灯
        /// </summary>
        [JsonProperty(PropertyName = "lr", NullValueHandling = NullValueHandling.Include)]
        public string lr { get; set; }

        /// <summary>
        /// 左右灯
        /// </summary>
        [JsonProperty(PropertyName = "define3", NullValueHandling = NullValueHandling.Include)]
        public string define3 { get; set; }

        /// <summary>
        /// 左右灯
        /// </summary>
        [JsonProperty(PropertyName = "desc", NullValueHandling = NullValueHandling.Include)]
        public string desct { get; set; }


    }
}
