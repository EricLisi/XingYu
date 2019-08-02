using KGM.Framework.Infrastructure;
using System.ComponentModel;

namespace KGM.Framework.Domain
{
    /// <summary>
    /// 权限配置 与数据库结构一致
    /// </summary> 
    [MappingTable(TableName = "Sys_Role_Module")]
    public class RoleModuleEntity : AggregateRoot
    {
        /// <summary>
        /// 角色Id
        /// </summary> 
        public string RoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary> 
        public string ModuleId { get; set; }

      

      
    }
}
