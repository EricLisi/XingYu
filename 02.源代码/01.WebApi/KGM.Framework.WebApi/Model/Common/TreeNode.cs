using Newtonsoft.Json;
using System.Collections.Generic;

namespace KGM.Framework.WebApi.Model.Common
{
    /// <summary>
    /// 树形结构节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        [JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }
         
        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty(PropertyName = "ChildNodes", NullValueHandling = NullValueHandling.Ignore)]
        public IList<TreeNode> Children { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [JsonProperty(PropertyName = "parentId", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentId { get; set; }
         
        /// <summary>
        /// 实际值
        /// </summary>
        [JsonProperty(PropertyName = "value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        /// <summary>
        /// 选中状态
        /// </summary>
        [JsonProperty(PropertyName = "checkstate", NullValueHandling = NullValueHandling.Ignore)]
        public int? CheckState { get; set; }
 
        /// <summary>
        /// 显示复选框
        /// </summary>
        [JsonProperty(PropertyName = "showcheck", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCheck { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        [JsonProperty(PropertyName = "complete", NullValueHandling = NullValueHandling.Ignore)]
        public bool Complete { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        [JsonProperty(PropertyName = "isexpand", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsExpand { get; set; }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        [JsonProperty(PropertyName = "hasChildren", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasChildren { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [JsonProperty(PropertyName = "img", NullValueHandling = NullValueHandling.Ignore)]
        public string Img { get; set; }
        
        /// <summary>
        /// title
        /// </summary>
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
    }
}
