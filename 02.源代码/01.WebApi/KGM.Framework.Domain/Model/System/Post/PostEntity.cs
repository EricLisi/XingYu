using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 岗位类 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_Post")]
    public class PostEntity : AggregateRoot
    {
        /// <summary>
        /// 节点
        /// </summary> 
        public string ParentId { get; set; }

        /// <summary>
        /// 编号
        /// </summary> 
        public string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary> 
        public string FullName { get; set; }

      
    }
}
