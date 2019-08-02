using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 表头
    /// </summary>
    [DataContract]
    public class GetDataHead
    {
 
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string label { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string name { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [DataMember]
        public string width { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        [DataMember]
        public string align { get; set; }
   

    }
}
