using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Application.Services;
using KGM.Framework.Infrastructure;
using KGM.Framework.WebApi.Model.Condition;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KGM.Framework.WebApi.Controllers
{
    /// <summary>
    /// 包装档案接口
    /// </summary> 
    public class PackageInventoryController : Base.AppBaseController
    {

        #region 依赖注入
        IPackageInventoryService _service;
        IUserProfilesService _userService;
        ITaskService _taskService;
        IU8Service _u8Service;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">包装服务</param> 
        /// <param name="userService">用户服务</param> 
        /// <param name="taskService">任务服务</param> 
        public PackageInventoryController(IPackageInventoryService service, IUserProfilesService userService, ITaskService taskService, IU8Service u8Service)
        {
            this._service = service;
            this._userService = userService;
            this._taskService = taskService;
            this._u8Service = u8Service;
        }
        #endregion

        #region Get 请求
        /// <summary>
        /// 根据cInvCode获取信息
        /// </summary>
        /// <param name="cInvCode"></param>
        /// <returns></returns>
        [HttpGet, Route("GetBycInvCodeAsync")]
        public async Task<IActionResult> GetBycInvCodeAsync(string cInvCode)
        {
            var condList = new List<SearchCondition>();
            if (!string.IsNullOrEmpty(cInvCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "EnCode",
                    Value = cInvCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            return Ok( await _service.QueryAsync<PackageInventorySingleDto>(condList));
        }
        /// <summary>
        /// 根据Id获取包装仓库信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        [HttpGet, Route("{Id}")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            return Ok(await _service.QueryByIdAsync<PackageInventorySingleDto>(Id));
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        [HttpGet, Route("GetByPages")]
        public async Task<IActionResult> GetByPages([FromQuery]PackageInventoryIndexCondition condition)
        {
            //设置分页对象
            var pager = SetPager(condition.Page, condition.Rows, condition.SIdx, condition.Sord);
            //设置过滤条件
            var condList = new List<SearchCondition>();
            if (!string.IsNullOrEmpty(condition.EnCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "EnCode",
                    Value = condition.EnCode,
                    Operation = CommonEnum.ConditionOperation.Like
                });
            }
            if (!string.IsNullOrEmpty(condition.cInvName))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "cInvName",
                    Value = condition.cInvName,
                    Operation = CommonEnum.ConditionOperation.Like
                });
            }

            //数据库查询 
            var entity = await _service.QueryPackageIuveutoryByPagesAsync(pager, condList);
            List<U8Warehouse> u8 = await _u8Service.GetAllWarehouse("1");
            foreach (var item in entity.Entity)
            {
                item.OutcWhName = u8.Find(it => it.CWhCode == item.OutStorage).CWhName;
                item.PutcWhName = u8.Find(it => it.CWhCode == item.PutStorage).CWhName;
            }
            //返回分页结果
            return PagerListAction(pager, entity);
        }
        /// <summary>
        /// 获取包装档案
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("QueryPackageInventoryAll")]
        public async Task<IActionResult> QueryPackageInventoryAll()
        {
            return Ok(await _service.QueryPackageInventoryAll());
        }
        #endregion

        #region Post请求
        /// <summary>
        /// 获取标签打印信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("GetTaskLable")]
        public async Task<IActionResult> GetTaskLable(PackageInventoryTaskSingleDto entity)
        {
            try
            {
                #region 读取基础信息
                //读取用户绑定仓库
                UserProfilesSingleDto UserProfiles = await _userService.QueryByIdAsync<UserProfilesSingleDto>(entity.UserId);
                if (UserProfiles == null)
                {
                    return Ok(new { state = false, message = "未能读取到用户绑定的仓库信息!" });
                }

                //读取当前物料信息是否存在
                List<SearchCondition> conditions = new List<SearchCondition>
                {
                    new SearchCondition
                    {
                        Filed = "EnCode",
                        Value = entity.cInvCode,
                        Operation = CommonEnum.ConditionOperation.Equal
                    }
                };
                List<PackageInventorySingleDto> PackageInventorys = await _service.QueryAsync<PackageInventorySingleDto>(conditions);
                if (PackageInventorys == null || PackageInventorys.Count == 0)
                {
                    return Ok(new { state = false, message = "未能读取到物料的信息!" });
                }
                var inv = PackageInventorys.FirstOrDefault();
                #endregion

                #region 已有任务处理
                //查找已有未完成任务
                List<SearchCondition> Taskconditions = new List<SearchCondition>
                {
                    new SearchCondition
                    {
                        Filed = "CinvCode",
                        Value = PackageInventorys[0].EnCode,
                        Operation = CommonEnum.ConditionOperation.Equal
                    },
                    new SearchCondition
                    {
                        Filed = "WareHouse",
                        Value = UserProfiles.Storage,
                        Operation = CommonEnum.ConditionOperation.Equal
                    },
                    new SearchCondition
                    {
                        Filed = "WorkStations",
                        Value = entity.WorkStations,
                        Operation = CommonEnum.ConditionOperation.Equal
                    }
                };
                List<TaskSingleDto> Tasks = await _taskService.QueryAsync<TaskSingleDto>(Taskconditions);
                //1:是不是最优先批次
                //if (Tasks.FindAll(it => Convert.ToInt32( it.Cbatch )< Convert.ToInt32(entity.cBatch)&&it.Status=="0").Count > 0)
                //{
                //    return Ok(new { state = false, message = "非最优先批次!" });
                //}

                //判断是否强制打印
                if (Tasks.Count!=0&&Tasks.FindAll(it => it.Status == "1").Count == Tasks.Count)
                {
                    if(entity.Compel)
                    {
                        TaskSingleDto task = new TaskSingleDto();
                        task.Cbatch = entity.cBatch;
                        task.CinvCode = entity.cInvCode;
                        //任务也许0条
                        if (Tasks.Count > 0)
                        {
                            task.TaskId = Tasks[0].TaskId;
                        }
                        else
                        {
                            task.TaskId = Guid.NewGuid().ToString();
                        }
                        task.TaskId = task.TaskId;
                        //task.OperationUser = entity.UserId;
                        task.OperationUser = "";
                        task.WorkStations = UserProfiles.WorkStation;
                        task.Status = "1";
                        task.Count = entity.Qty;
                        task.WareHouse = UserProfiles.Storage;
                        task.Define1 = "1";
                        task.CreatorTime = DateTime.Now;
                        task.CreatorUserId = entity.UserId;
                        int ReturnCount = 0;
                        ReturnCount = await _taskService.Insert(task);
                        if (ReturnCount > 0)
                        {
                            return Ok(new { state = false, message = "强制打印成功......", Compel = 1,taskId=task.TaskId });
                        }
                    }
                    return Ok(new { state = false, message = "数量不足，待拼箱......" });
                }
                ////如果当前分组有已扫描的，qty需要减掉
                //int? Qty = 0;
                //if (entity.GroupId!=""&&Tasks.FindAll(it => it.Status == "1" && it.GroupId == entity.GroupId).Count > 0)
                //{
                //    Qty = Tasks.FindAll(it => it.Status == "1" && it.GroupId == entity.GroupId).Sum(it => it.Count);
                //    entity.Qty = Convert.ToInt32(PackageInventorys[0].ConstantVolume) - Convert.ToInt32(Qty);
                //}
 
                var undoTask = Tasks.FindAll(
                    it => it.Status.Equals("0")
                    && it.Cbatch.Equals(entity.cBatch)
                    && it.Count == entity.Qty);

                var relist = new List<PackageInventoryTaskSingleDto>();
                if (undoTask.Count > 0)
                {
                    //存在未完成的任务，不需要新生成任务
                    var task = undoTask.FirstOrDefault();
                    task.Status = "1";
                    await _taskService.Update(task);

                    if (string.IsNullOrEmpty(task.GroupId))
                    {
                        //整箱
                        relist.Add(new PackageInventoryTaskSingleDto
                        {
                            UserId = entity.UserId,
                            Address = entity.Address,
                            cBatch = entity.cBatch,
                            cInvCode = entity.cInvCode,
                            cInvName = inv.cInvName,
                            Desc = inv.Desc,
                            Qty = entity.Qty,
                            TaskId = task.Id,
                            WorkCode = UserProfiles.EnCode
                        });
                        return Ok(new { state = true, taskId = task.TaskId, groupId = string.Empty, data = relist });
                    }
                    //查询groupid下所有的信息
                    var allGroupTask = Tasks.FindAll(
                                it => it.Status.Equals("0")
                                && !it.Id.Equals(task.Id)
                                && it.GroupId.Equals(task.GroupId));

                    //非整箱 根据groupid 查找是否还存在未0的记录
                    if (allGroupTask.Count == 0)
                    {
                        //所有的拆分的物料已经扫描,可以打印标签
                        var task1 = Tasks.FindAll(it => it.GroupId.Equals(task.GroupId));
                        foreach (var t in task1)
                        {
                            relist.Add(new PackageInventoryTaskSingleDto
                            {
                                UserId = entity.UserId,
                                Address = entity.Address,
                                cBatch = t.Cbatch,
                                cInvCode = entity.cInvCode,
                                cInvName = inv.cInvName,
                                Desc = inv.Desc,
                                Qty = t.Count.Value,
                                TaskId = t.TaskId,
                                WorkCode = UserProfiles.EnCode
                            });
                        }
                        return Ok(new { state = true, taskId = task.TaskId, groupId = task.GroupId, data = relist });
                    }

                    //不需要打标签
                    return Ok(new { state = true, taskId = task.TaskId, groupId = task.GroupId });
                }
                #endregion

                #region 新生成任务
                //读取所有可生成任务的信息
                var list = await _u8Service.QueryUndoList(UserProfiles.Storage, entity.cInvCode);

                string tt = await _taskService.CreateTask(list, entity.UserId, UserProfiles.Storage, UserProfiles.WorkStation, entity.cBatch,entity.Qty);
                if (string.IsNullOrEmpty(tt))
                {
                    //不存在任何任务
                    return Ok(new { state = true, taskId = string.Empty });
                }
                if (tt=="false")
                {
                    return Ok(new { state = false, message = "非最优先批次!" });
                }
                var taskid = tt.Split('|')[0];
                var grpid = tt.Split('|')[1];
                
              
                if (!string.IsNullOrEmpty(grpid))
                {
                    //不打印标签
                    return Ok(new { state = true, taskId = taskid, groupId = grpid});
                }
                else
                {
                    relist.Add(new PackageInventoryTaskSingleDto
                    {
                        UserId = entity.UserId,
                        Address = entity.Address,
                        cBatch = entity.cBatch,
                        cInvCode = entity.cInvCode,
                        cInvName = inv.cInvName,
                        Desc = inv.Desc,
                        Qty = entity.Qty,
                        TaskId = taskid,
                        WorkCode = UserProfiles.EnCode
                    });

                    return Ok(new { state = true, taskId = taskid, groupId = grpid, data = relist });
                }


            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    state = false,
                    message = ex.Message
                });
            }
        }
        #endregion
        /// <summary>
        /// 新增包装仓库数据
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]PackageInventorySingleDto entity)
        {
            try
            {
                var packs = await _service.QueryAsync<PackageInventorySingleDto>();
                if (packs.Find(it => it.EnCode == entity.EnCode) != null)
                {
                    return Ok(new { Status = false, Message = "此编码已存在，请重新输入" });
                }
                int rows = await _service.Insert(entity);//得到影响行数
                if (rows > 0)
                {
                    return CreateAction();
                }

                return OKAction(false, "创建失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"创建失败!原因:{ex.Message}");
            }
        }
        #endregion

        #region Put请求
        /// <summary>
        /// 更新包装仓库
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        [HttpPut, Route("Update/{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]PackageInventorySingleDto entity)
        {
            try
            {
                entity.Id = Id;
                int rows = await _service.Update(entity);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "更新成功");
                }

                return NotFoundAction(false, "更新失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"更新失败!原因:{ex.Message}");
            }
        }
        /// <summary>
        /// 冻结/解冻 物料
        /// </summary>
        /// <param name="cInvCode">物料编号</param>
        /// <returns></returns>
        [HttpPut, Route("UpdateStatus/{cInvCode}")]
        public async Task<IActionResult> UpdateStatus(string cInvCode)
        {
            try
            {
                List<SearchCondition> conditionsPack = new List<SearchCondition>();
                conditionsPack.Add(new SearchCondition
                {
                    Filed = "EnCode",
                    Value = cInvCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                List<PackageInventorySingleDto> packs = await _service.QueryAsync<PackageInventorySingleDto>(conditionsPack);
                PackageInventorySingleDto pack = packs[0];
                //List<Inventorys> u8List = await _u8Service.QueryInventorysByWhCode(pack.OutStorage);
                //Inventorys u8 = u8List.Find(it => it.cInvCode == cInvCode);
                List<SearchCondition> conditions = new List<SearchCondition>();
                conditions.Add(new SearchCondition
                {
                    Filed = "WareHouse",
                    Value = pack.OutStorage,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                conditions.Add(new SearchCondition
                {
                    Filed = "CinvCode",
                    Value = cInvCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                conditions.Add(new SearchCondition
                {
                    Filed = "Status",
                    Value = "1",
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                List<TaskSingleDto> tasksAll = await _taskService.QueryAsync<TaskSingleDto>(conditions);//所有当前仓库任务
                if (tasksAll.Count > 0)//如果认为进行中或已完成不能冻结
                {
                    return Ok(new { Status = false, Message = "无法冻结" });
                }
                string message = "";
                if (pack.FreezeStatus == "0")
                {
                    pack.FreezeStatus = "1";
                    message = "冻结成功";
                }
                else
                {
                    pack.FreezeStatus = "0";
                    message = "解冻成功";
                }
                int rows = await _service.Update(pack);
                if (rows > 0)
                {
                    return Ok(new { Status = true, Message = message });
                }
                return Ok(new { Status = false, Message = "操作失败" });
            }
            catch (Exception ex)
            {

                return OKAction(false, $"操作失败!原因:{ex.Message}");
            }
        }
        #endregion

        #region Delete请求
        /// <summary>
        /// 删除包装仓库
        /// </summary>
        /// <param name="Id">包装Id</param>
        /// <returns></returns>
        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                PackageInventorySingleDto package = await _service.QueryByIdAsync<PackageInventorySingleDto>(Id);
                List<SearchCondition> conditions = new List<SearchCondition>();
                if (package != null)
                {
                    conditions.Add(new SearchCondition
                    {
                        Filed = "CinvCode",
                        Value = package.EnCode,
                        Operation = CommonEnum.ConditionOperation.Equal
                    });
                    conditions.Add(new SearchCondition
                    {
                        Filed = "Status",
                        Value = "0",
                        Operation = CommonEnum.ConditionOperation.Equal
                    });
                    List<TaskSingleDto> taskSingles = await _taskService.QueryAsync<TaskSingleDto>(conditions);//有没有正在作业的任务
                    if (taskSingles.Count > 0)
                    {
                        return Ok(new { Status = false, Message = "物料正被操作，不允许删除" });
                    }
                }
                int rows = await _service.DeleteByKey(Id);//得到影响行数
                if (rows > 0)
                {
                    return NoContentAction();
                }

                return NotFoundAction(false, "删除失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"删除失败!原因:{ex.Message}");
            }
        }
        #endregion
    }
}