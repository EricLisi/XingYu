using KGM.Framework.Domain;
using KGM.Framework.Infrastructure;
using KGM.Framework.RepositoryEF.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace KGM.Framework.RepositoryEF.Repositories
{
    public class TaskRepository : BaseRepository<TaskEntity, TaskContext>, ITaskRepository
    {
        #region 构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TaskRepository() : base()
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
        public PagerEntity<TaskEntity> QueryTaskByPagers(PagerInfo pager, List<SearchCondition> condition = null)
        {

            var entity = CurrentDbSet.Where(it => true);

            //动态增加过滤条件
            if (condition != null && condition.Count > 0)
            {
                var parser = new LambdaParser<TaskEntity>();
                entity = entity.Where(parser.ParserConditions(condition));
            }

            var total = entity.Count();

            entity = pager.Sort == "ASC" ? entity.OrderBy(pager.SortFiled) : entity.OrderByDescending(pager.SortFiled);

            entity = entity.Skip(pager.PageSize * (pager.PageIndex - 1))
                             .Take(pager.PageSize);
            return new PagerEntity<TaskEntity>
            {
                Entity = entity.ToList(),
                Total = total
            };
        }


        public string CreateTask(DataTable dt, string userId, string warehouse, string workstation, string ScanBatch, int ScanQty)
        {
            int taskcount = 0;
            string grpid = string.Empty;
            string tkid = string.Empty;
            string result = string.Empty;
           
            using (var uow = new EFUnitOfWork<TaskContext>(CurrentDbContext))
            {
                string groupid = string.Empty;
                int retqty = 0;
                string taskId = Guid.NewGuid().ToString();
                tkid = taskId;
                int iRowNo = 0;
                if (ScanBatch != dt.Rows[0]["CBATCH"].ToString())
                {
                    result = "false";
                    return result;
                }
                //添加一列用来排序
                //dt.Columns.Add("order", typeof(int));
                //foreach (DataRow dr in dt.Rows)
                //{
                //    dr["order"] = Convert.ToInt32(dr["cbatch"].ToString());
                //}
                dt.DefaultView.Sort = "CBATCH ASC";
                dt = dt.DefaultView.ToTable();

                foreach (DataRow dr in dt.Rows)
                {
                    int dingrong = dr["DR"].ToInt();//定容
                    int iquantity = dr["IQUANTITY"].ToInt();//库存数
                 
                    if (retqty != 0)
                    {
                        if (iquantity < (dingrong - retqty))
                        {
                            //还是散件
                            uow.RegisterNew(new TaskEntity
                            {
                                //TaskId = Guid.NewGuid().ToString(),
                                TaskId = taskId,
                                Cbatch = dr["CBATCH"].ToString(),
                                CinvCode = dr["CINVCODE"].ToString(),
                                Count = iquantity,
                                CreatorTime = DateTime.Now,
                                CreatorUserId = userId,
                                GroupId = groupid,
                                OperationUser = "",
                                //OperationUser = userId,
                                Status = iquantity == ScanQty && dr["CBATCH"].ToString() == ScanBatch ? "1" : "0",
                                WareHouse = warehouse,
                                WorkStations = workstation
                            });

                            //retqty -= iquantity;
                            retqty += iquantity;
                            continue;
                        }
                        else
                        {
                            taskcount = 1;
                            //当groupid为空时，表示从来没有过整箱 整箱时 grpid = -1
                            if (string.IsNullOrEmpty(grpid))
                            {
                                grpid = groupid;
                            }
                            //可以组箱
                            uow.RegisterNew(new TaskEntity
                            {
                                //TaskId = Guid.NewGuid().ToString(),
                                TaskId = taskId,
                                Cbatch = dr["CBATCH"].ToString(),
                                CinvCode = dr["CINVCODE"].ToString(),
                                Count = (dingrong - retqty),
                                CreatorTime = DateTime.Now,
                                CreatorUserId = userId,
                                GroupId = groupid,
                                //OperationUser = userId,
                                OperationUser = "",
                                Status = (dingrong - retqty) == ScanQty && dr["CBATCH"].ToString() == ScanBatch ? "1" : "0",
                                WareHouse = warehouse,
                                WorkStations = workstation
                            });

                            iquantity -= (dingrong - retqty);
                            retqty = 0;
                        }
                    }
                    #region 整箱定容
                    //库存数大于定容的时候 才会有整箱
                    if (iquantity >= dingrong)
                    {
                        //有整箱表示至少有有ige任务 taskcount = 1
                        taskcount = 1;
                        //有整箱表示没有groupid 用-1来标识
                        if (string.IsNullOrEmpty(grpid))
                        {
                            grpid = "-1";
                        }
                        for (int i = 0; i < iquantity / dingrong; i++)
                        {
                            groupid = string.Empty;
                            var entity = new TaskEntity
                            {
                                TaskId = taskId,
                                Cbatch = dr["CBATCH"].ToString(),
                                CinvCode = dr["CINVCODE"].ToString(),
                                Count = dingrong,
                                CreatorTime = DateTime.Now,
                                CreatorUserId = userId,
                                GroupId = groupid,
                                OperationUser = "",
                                //OperationUser = userId,
                                Status = i == 0 && dr["CBATCH"].ToString() == ScanBatch && dingrong == ScanQty ? "1" : "0",
                                WareHouse = warehouse,
                                WorkStations = workstation
                            };
                            uow.RegisterNew(entity);


                            if (i == 0)
                            {
                                result = entity.Id;
                            }

                        }
                    }
                    #endregion
                    #region 散件余数
                    retqty = iquantity % dingrong;
                    if (retqty > 0)
                    {
                        groupid = Guid.NewGuid().ToString();
                        uow.RegisterNew(new TaskEntity
                        {
                            //TaskId = Guid.NewGuid().ToString(),
                            TaskId = taskId,
                            Cbatch = dr["CBATCH"].ToString(),
                            CinvCode = dr["CINVCODE"].ToString(),
                            Count = retqty,
                            CreatorTime = DateTime.Now,
                            CreatorUserId = userId,
                            GroupId = groupid,
                            //OperationUser = userId,
                            OperationUser = "",
                            Status = iRowNo == 0 && iquantity < dingrong ? "1" : "0",
                            WareHouse = warehouse,
                            WorkStations = workstation
                        });
                    }
                    #endregion

                    iRowNo++;
                }
                //if (retqty > 0)
                //{

                //    List<ExcuteSqlEntity> list = new List<ExcuteSqlEntity>();
                //    list.Add(new ExcuteSqlEntity
                //    {
                //        SqlStr = "DELETE FROM MST_TASK WHERE F_GROUPID = @GROUPID",
                //        ParmList = new List<SqlParameter>
                //        {
                //             new SqlParameter("@GROUPID",groupid)
                //        }
                //    });
                //    //删除groupid
                //    base.ExcuteNoneQuery(uow.DbContext, list);
                //}
                uow.Commit();
                //
                if (retqty > 0)
                {
                    List<SearchCondition> Taskconditions = new List<SearchCondition>
                {
                    new SearchCondition
                    {
                        Filed = "GroupId",
                        Value = groupid,
                        Operation = CommonEnum.ConditionOperation.Equal
                    }
                };
                    int rows = base.Delete(Taskconditions);
                }
                //
            }


            if (taskcount == 0)
            {
                result = string.Empty;
            }
            else
            {
                result = string.Empty;
                result += tkid+"|" + (grpid.Equals("-1") ? string.Empty : grpid);
            }
            return result;
        }
        #endregion

    }
}
