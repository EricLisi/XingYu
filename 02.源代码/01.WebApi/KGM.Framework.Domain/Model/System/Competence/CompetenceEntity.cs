using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 权限类 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_Competence")]
    public class CompetenceEntity : AggregateRoot
    {

        /// <summary>
        /// 父级
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 层次
        /// </summary>
        public  int? Layers { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public  string EnCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public  string FullName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public  string Icon { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public  string UrlAddress { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public  bool? IsMenu { get; set; }

        /// <summary>
        /// 展开
        /// </summary>
        public  bool? IsExpand { get; set; }

        /// <summary>
        /// 公共
        /// </summary>
        public virtual bool? IsPublic { get; set; }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public virtual bool? AllowEdit { get; set; }

        /// <summary>
        /// 允许删除
        /// </summary>
        public virtual bool? AllowDelete { get; set; }
        /// <summary>
        /// 是否App
        /// </summary>
        public virtual bool? IsApp { get; set; }




    }
}
