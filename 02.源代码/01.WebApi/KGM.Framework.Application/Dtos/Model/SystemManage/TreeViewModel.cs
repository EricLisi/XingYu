using System;
using System.Runtime.Serialization;

namespace KGM.Framework.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class TreeViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string parentId { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string id { get; set; }
        /// <summary>
        /// 文本名称
        /// </summary>
        [DataMember]
        public string text { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public string value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int? checkstate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool showcheck { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool complete { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        [DataMember]
        public bool isexpand { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        [DataMember]
        public bool hasChildren { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public string img { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string title { get; set; }
    }
}
