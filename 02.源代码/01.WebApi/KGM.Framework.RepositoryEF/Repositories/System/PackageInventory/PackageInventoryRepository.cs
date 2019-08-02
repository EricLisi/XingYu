using KGM.Framework.Domain;
using KGM.Framework.Domain.Model.U8;
using KGM.Framework.Infrastructure;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace KGM.Framework.RepositoryEF.Repositories
{
    public  class PackageInventoryRepository : BaseRepository<PackageInventoryEntity, PackageInventoryContext>, IPackageInventoryRepository
    {
        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PackageInventoryRepository() : base()
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
        public PagerEntity<PackageInventoryEntity> QueryPackageIuveutoryByPagers(PagerInfo pager, List<SearchCondition> condition = null)
        {
            var entity = CurrentDbSet.Where(it => true);
            //动态增加过滤条件
            if (condition != null && condition.Count > 0)
            {
                var parser = new LambdaParser<PackageInventoryEntity>();
                entity = entity.Where(parser.ParserConditions(condition));
            }

            var total = entity.Count();

            entity = pager.Sort == "ASC" ? entity.OrderBy(pager.SortFiled) : entity.OrderByDescending(pager.SortFiled);

            //entity = entity.Skip(pager.PageSize * (pager.PageIndex - 1))
            //                 .Take(pager.PageSize);
            return new PagerEntity<PackageInventoryEntity>
            {
                Entity = entity.ToList(),
                Total = total
            };
        }
        /// <summary>
        /// 获取包装档案记录
        /// </summary>
        /// <returns></returns>
        public List<PackageInventoryEntity> QueryPackageInventoryAll()
        {
            List<PackageInventoryEntity> list = new List<PackageInventoryEntity>();
            List<U8Entity.WarehouseEntity> u8list = new List<U8Entity.WarehouseEntity>();
            using (var packdb = new PackageInventoryContext())
            {
                 list = packdb.CurrentDbSet.Where(it=>true).ToList();
                using (var u8db = new U8Context())
                {
                    u8list=u8db.Warehouse.Where(it=>true).ToList();
                    foreach (var item in list)
                    {
                        item.PutcWhName = u8list.Find(it=>it.CWhCode==item.PutStorage).CWhName;
                        item.OutcWhName = u8list.Find(it => it.CWhCode == item.OutStorage).CWhName;
                    }
                }

            }
            return list;
        }

        #endregion
    }
}
