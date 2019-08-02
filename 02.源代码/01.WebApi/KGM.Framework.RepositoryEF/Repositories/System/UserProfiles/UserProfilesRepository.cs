using KGM.Framework.Domain;
using KGM.Framework.Domain.Model.U8;
using KGM.Framework.Infrastructure;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KGM.Framework.RepositoryEF.Repositories
{
    public class UserProfilesRepository : BaseRepository<UserProfilesEntity, UserProfilesContext>, IUserProfilesRepository
    {
        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserProfilesRepository() : base()
        {
        }
        #endregion
        #region 自身接口实现
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns> 
        public PagerEntity<UserProfilesEntity> QueryUserProfilesByPagers(PagerInfo pager, List<SearchCondition> condition = null)
        {
            var entity = CurrentDbSet.Where(it => true);
            //动态增加过滤条件
            if (condition != null && condition.Count > 0)
            {
                var parser = new LambdaParser<UserProfilesEntity>();
                entity = entity.Where(parser.ParserConditions(condition));
            }

            var total = entity.Count();

            entity = pager.Sort == "ASC" ? entity.OrderBy(pager.SortFiled) : entity.OrderByDescending(pager.SortFiled);

            //entity = entity.Skip(pager.PageSize * (pager.PageIndex - 1))
            //                 .Take(pager.PageSize);
            return new PagerEntity<UserProfilesEntity>
            {
                Entity = entity.ToList(),
                Total = total
            };
        }
        /// <summary>
        /// 获取用户档案记录
        /// </summary>
        /// <returns></returns>
        public List<UserProfilesEntity> QueryUserProfilesAll()
        {
            List<UserProfilesEntity> list = new List<UserProfilesEntity>();
            List<U8Entity.WarehouseEntity> u8list = new List<U8Entity.WarehouseEntity>();
            using (var userdb = new UserProfilesContext())
            {
                list = userdb.CurrentDbSet.Where(it => true).ToList();
                using (var u8db = new U8Context())
                {
                    u8list = u8db.Warehouse.Where(it => true).ToList();
                    foreach (var item in list)
                    {
                        item.cWhName = u8list.Find(it => it.CWhCode == item.Storage).CWhName;
                    }
                }

            }
            return list;
        }
        #endregion
    }
}
