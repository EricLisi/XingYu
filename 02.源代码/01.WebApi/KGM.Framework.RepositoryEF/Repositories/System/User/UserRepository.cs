using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;
using KGM.Framework.RepositoryEF.Core;
using System.Collections.Generic;
using System.Linq;

namespace KGM.Framework.RepositoryEF.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, UserContext>, IUserRepository
    {
        public UserRepository() : base()
        {

        }
        #region 私有方法

        /// <summary>
        /// 查找指定主键的实体记录
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        /// <param name="key">指定主键</param>
        /// <returns></returns>
        private UserEntity GetByKey(UserContext dbContext, object key)
        {
            return dbContext.CurrentDbSet
               .Where(it => it.Id.Equals(key))
               .FirstOrDefault();
        }
        #endregion
        #region 自身接口实现
        /// <summary>
        /// 分页查询 + 条件查询 + 排序
        /// </summary>
        /// <param name="pager">分页对象</param>
        /// <param name="condition">过滤条件</param>
        /// <returns></returns> 
        public PagerEntity<UserEntity> QueryUserByPagers(PagerInfo pager, List<SearchCondition> condition = null)
        {
            var entity = CurrentDbSet.Where(it => true);
            //动态增加过滤条件
            if (condition != null && condition.Count > 0)
            {
                var parser = new LambdaParser<UserEntity>();
                entity = entity.Where(parser.ParserConditions(condition));
            }

            var total = entity.Count();

            entity = pager.Sort == "ASC" ? entity.OrderBy(pager.SortFiled) : entity.OrderByDescending(pager.SortFiled);

            entity = entity.Skip(pager.PageSize * (pager.PageIndex - 1))
                             .Take(pager.PageSize);
            return new PagerEntity<UserEntity>
            {
                Entity = entity.ToList(),
                Total = total
            };
        }
       /// <summary>
       /// 修改密码
       /// </summary>
       /// <param name="Id"></param>
       /// <param name="Pwd"></param>
       /// <returns></returns>
        public int UpdatePwd(string Id, string Pwd)
        {
            using (var uow = new EFUnitOfWork<UserContext>(CurrentDbContext))
            {
                //设置级联删除以后,取对象必须先InClude
                var entity = GetByKey(uow.DbContext, Id);
                entity.Password = Pwd;
                uow.RegisterModified(entity, true);

                return uow.Commit();
            }
        }
        #endregion
    }
}
