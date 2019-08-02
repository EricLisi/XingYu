using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 下拉框
    /// </summary>
    [DataContract]
    public class SelectModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Id { get; set; }//ID
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Text { get; set; }//名称
      

    }
}
