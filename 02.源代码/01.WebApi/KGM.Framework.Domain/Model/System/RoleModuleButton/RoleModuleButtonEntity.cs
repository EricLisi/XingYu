using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 权限配置 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_Role_ModuleButton")]
    public class RoleModuleButtonEntity : AggregateRoot
    {
        /// <summary>
        /// 角色Id
        /// </summary> 
        public string RoleId { get; set; }

        /// <summary>
        /// 按钮Id
        /// </summary> 
        public string ModuleButtonId { get; set; }

      

      
    }
}
