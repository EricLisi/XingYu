using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 公司Dto
    /// </summary>
    [DataContract]
    public class TreeGridModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Id { get; set; }//主键
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ParentId { get; set; }//节点
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Text { get; set; }//名称
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool IsLeaf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Expanded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool Loaded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string EntityJson { get; set; }

    }
}
