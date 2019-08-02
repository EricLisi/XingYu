using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 数据字典具体
    /// </summary> 
    [MappingTable(TableName = "Sys_ItemsDetail")]
    public class ItemsDetailEntity : AggregateRoot
    {

        /// <summary>
        /// 类型
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        ///编码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        ///名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        ///SimpleSpelling
        /// </summary>
        public string SimpleSpelling { get; set; }

        /// <summary>
        ///IsDefault
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        ///层
        /// </summary>
        public int? Layers { get; set; }


        /// <summary>
        /// 排序码
        /// </summary>
        public override int? SortCode { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public override bool? EnabledMark { get; set; }

    }
}
