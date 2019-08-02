using KGM.Framework.Domain;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;

namespace KGM.Framework.RepositoryEF
{
  public  class UserProfilesMapping : EntityMapping<UserProfilesEntity>
    {
        #region 接口实现
        public new static void MappingToTable(ModelBuilder modelBuilder,bool bAutoMapping=true)
        {
            //映射用户档案表
            var userprofiles = EntityMapping<UserProfilesEntity>.MappingToTable(modelBuilder, bAutoMapping);
        }
        #endregion
    }
}
