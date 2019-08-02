using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 表头
    /// </summary>
    [DataContract]
    public class GetDPModel
    {
 
        /// <summary>
        /// 表头
        /// </summary>
        [DataMember]
        public DPMainGetDto info { get; set; }

        /// <summary>
        /// 表体
        /// </summary>
        [DataMember]
        public List<DPDetailGetDto> dInfo { get; set; }
     

    }
}
