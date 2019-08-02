using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 表头
    /// </summary>
    [DataContract]
    public class QuickShelfModel
    {
 
        /// <summary>
        /// 用户
        /// </summary>
        [DataMember]
        public string User { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        [DataMember]
        public string orderNo { get; set; }


        /// <summary>
        /// 货位Id
        /// </summary>
        [DataMember]
        public string PositionCode { get; set; }
 
    }
}
