using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 权限配置 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_Role_ModuleColumn")]
    public class RoleModuleColumnEntity : AggregateRoot
    {
        /// <summary>
        /// 角色Id
        /// </summary> 
        public string RoleId { get; set; }

        /// <summary>
        /// 列Id
        /// </summary> 
        public string ModuleColumnId { get; set; }

      

      
    }
}
