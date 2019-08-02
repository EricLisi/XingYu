using KGM.Framework.Domain;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;

namespace KGM.Framework.RepositoryEF
{
    public class UserMapping : EntityMapping<UserEntity>
    {
        #region 接口实现
        public new static void MappingToTable(ModelBuilder modelBuilder, bool bAutoMapping = true)
        {
            //映射用户表
            var user = EntityMapping<UserEntity>.MappingToTable(modelBuilder, bAutoMapping);

        }
        #endregion
    }
}
