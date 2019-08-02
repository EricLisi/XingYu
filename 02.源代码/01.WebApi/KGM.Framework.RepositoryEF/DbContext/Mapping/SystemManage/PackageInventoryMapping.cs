using KGM.Framework.Domain;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;

namespace KGM.Framework.RepositoryEF
{ 
    /// <summary>
    /// 包装仓库映射
    /// </summary>
   public class PackageInventoryMapping : EntityMapping<PackageInventoryEntity>
    {
        #region 接口实现
        public new static void MappingToTable(ModelBuilder modelBuilder, bool bAutoMapping = true)
        {
            //映射用户档案表
            var packageiuveutory = EntityMapping<PackageInventoryEntity>.MappingToTable(modelBuilder, bAutoMapping);
        }
        #endregion
    }
}
