using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 数据字典
    /// </summary> 
    [MappingTable(TableName = "Sys_Items")]
    public class ItemsEntity : AggregateRoot
    {

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        ///编码
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        ///名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///IsDefault
        /// </summary>
        public bool? IsTree { get; set; }

        /// <summary>
        ///层
        /// </summary>
        public int? Layers { get; set; }




    }
}
