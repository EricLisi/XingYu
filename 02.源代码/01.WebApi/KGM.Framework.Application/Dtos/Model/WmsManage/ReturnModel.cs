using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 表头
    /// </summary>
    [DataContract]
    public class ReturnModel
    {
 
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public int r { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [DataMember]
        public string msg { get; set; }
     

    }
}
