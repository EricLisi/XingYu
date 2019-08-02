using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 生成拣货单对象
    /// </summary>
    [DataContract]
    public class GeneratePackList
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public List<string> Ids { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int OrderType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string User { get; set; }

    }
}
