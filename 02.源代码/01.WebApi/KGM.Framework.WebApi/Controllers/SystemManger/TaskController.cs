using KGM.Framework.Application.Dtos;
using KGM.Framework.Application.Dtos.U8;
using KGM.Framework.Application.Services;
using KGM.Framework.Infrastructure;
using KGM.Framework.WebApi.Model.Condition;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using KGM.Framework.WebApi.Model.TaskModel;

namespace KGM.Framework.WebApi.Controllers
{
    /// <summary>
    /// 任务管理接口
    /// </summary>
    public class TaskController : Base.AppBaseController
    {
        #region 依赖注入
        ITaskService _service;
        IU8Service _u8service;
        IUserProfilesService _UserProfilesService;
        IPrintLogService _PrintLogService;
        IPackageInventoryService _packservice;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="service">任务服务</param> 
        /// <param name="u8Service">u8服务</param> 
        /// <param name="userProfilesService">用户服务</param> 
        /// <param name="printLogService">log服务</param> 
        /// <param name="packservice">物料档案服务</param> 
        public TaskController(IPackageInventoryService packservice, ITaskService service, IU8Service u8Service, IUserProfilesService userProfilesService, IPrintLogService printLogService)
        {
            this._service = service;
            this._u8service = u8Service;
            this._UserProfilesService = userProfilesService;
            this._PrintLogService = printLogService;
            this._packservice = packservice;
        }
        #endregion

        #region Get 请求
        /// <summary>
        /// 根据条件获取任务记录
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GeByConditionAsync")]
        public async Task<IActionResult> GeByConditionAsync([FromQuery]TaskCondition condition)
        {
            var condList = new List<SearchCondition>();
            if (!string.IsNullOrEmpty(condition.WorkStations))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "WorkStations",
                    Value = condition.WorkStations,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(condition.cInvCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "CinvCode",
                    Value = condition.cInvCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(condition.cWhCode))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "WareHouse",
                    Value = condition.cWhCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(condition.cBatch))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "Cbatch",
                    Value = condition.cBatch,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(condition.GroupId))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "GroupId",
                    Value = condition.GroupId,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            if (!string.IsNullOrEmpty(condition.Status))
            {
                condList.Add(new SearchCondition
                {
                    Filed = "Status",
                    Value = condition.Status,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
            }
            List<TaskSingleDto> list = await _service.QueryAsync<TaskSingleDto>(condList);
            return Ok(list.OrderByDescending(it => it.Count));
        }
        /// <summary>
        /// 获取待转库任务
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetDeleteMark")]
        public async Task<IActionResult> GetDeleteMark(string UserId, string WorkStations, string cInvCode)
        {
            List<SearchCondition> conditions = new List<SearchCondition>();
            conditions.Add(new SearchCondition
            {
                Filed = "OperationUser",
                Value = UserId,
                Operation = CommonEnum.ConditionOperation.Equal
            });
            conditions.Add(new SearchCondition
            {
                Filed = "WorkStations",
                Value = WorkStations,
                Operation = CommonEnum.ConditionOperation.Equal
            });
            conditions.Add(new SearchCondition
            {
                Filed = "DeleteMark",
                Value = "1",
                Operation = CommonEnum.ConditionOperation.NotEqual
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
            return Ok(await _service.QueryAsync<TaskSingleDto>(conditions));
        }

        /// <summary>
        /// 根据Id获取任务信息
        /// </summary>
        /// <param name="Id">任务Id</param>
        /// <returns></returns>
        [HttpGet, Route("GetByIdAsync/{Id}")]
        public async Task<IActionResult> GetByIdAsync(string Id)
        {
            return Ok(await _service.QueryByIdAsync<TaskSingleDto>(Id));
        }
        /// <summary>
        /// 根据物料编码获取任务信息
        /// </summary>
        /// <param name="cInvCode">物料编码</param>
        /// <returns></returns>
        [HttpGet, Route("{cInvCode}")]
        public async Task<IActionResult> GetBycInvCodeAsync(string cInvCode)
        {
            try
            {
                List<SearchCondition> conditions = new List<SearchCondition>();
                conditions.Add(new SearchCondition
                {
                    Filed = "cInvCode",
                    Value = cInvCode,
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                conditions.Add(new SearchCondition
                {
                    Filed = "Status",
                    Value = "0",
                    Operation = CommonEnum.ConditionOperation.Equal
                });
                List<TaskSingleDto> tasks = await _service.QueryAsync<TaskSingleDto>(conditions);
                List<U8Warehouse> u8List = await _u8service.GetAllWarehouse("1");
                foreach (var item in tasks)
                {
                    //U8Warehouse u8 = u8List.Find(it => it.CWhCode == item.WareHouse);
                    UserProfilesSingleDto user = await _UserProfilesService.QueryByIdAsync<UserProfilesSingleDto>(item.OperationUser);
                    item.UserAccount = user.Account;
                    item.RealName = user.RealName;
                    item.WareHouseName = u8List.Find(it => it.CWhCode == item.WareHouse).CWhName;
                }
                tasks = tasks.OrderByDescending(it => it.GroupId).ToList();
                return Ok(tasks);
            }
            catch (Exception ex)
            {

                return OKAction(false, $"失败 !原因:{ex.Message}");
            }
        }

        /// <summary>
        /// 获取所有任务信息
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {

            return Ok(await _service.QueryAsync<TaskSingleDto>());
        }
        #endregion

        #region Post请求 
        ///// <summary>
        ///// 生成任务
        ///// </summary>
        ///// <param name="model">参数</param>
        ///// <returns></returns>
        //[HttpPut, Route("GenerateTask")]
        //public async Task<IActionResult> GenerateTask(GenerateTaskModel model)
        //{
        //    await _service.CreateTask(null, model.UserId, model.CWhCode, model.WorkStation, model.ScanBatch, model.ScanQty);
        //    return Ok();
        //}



        /// <summary>
        /// 更新任务分配
        /// </summary>
        /// <returns></returns>
        [HttpPut, Route("AllocationTask")]
        public async Task<IActionResult> AllocationTask(AllocationTaskDto allocation)
        {
            int rows = 0;
            try
            {
                var user = await _UserProfilesService.QueryByIdAsync<UserProfilesSingleDto>(allocation.UserId);


                allocation.tasks = allocation.tasks.Substring(0, allocation.tasks.Length - 1);
                string[] taskId = allocation.tasks.Split(",");
                List<TaskSingleDto> task = new List<TaskSingleDto>();
                foreach (var item in taskId)
                {
                    TaskSingleDto taskSingle = await _service.QueryByIdAsync<TaskSingleDto>(item);
                    if (taskSingle != null)
                    {
                        taskSingle.OperationUser = allocation.UserId;
                        taskSingle.WorkStations = user.WorkStation;
                        task.Add(taskSingle);
                    }
                }
                rows = await _service.Update(task);
                if (rows > 0)
                {
                    return Ok(new { Status = true, Message = "分配成功" });
                }
                return Ok(new { Status = false, Message = "分配失败" });
            }
            catch (Exception ex)
            {

                return Ok(new { Status = false, Message = "分配失败:" + ex.Message });
            }
        }
        /// <summary>
        /// 新增任务数据
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="cInvCode"></param>
        /// <returns></returns>
        [HttpGet, Route("{UserId}/{cInvCode}")]
        public async Task<IActionResult> Insert(string UserId, string cInvCode)
        {
            try
            {
                UserProfilesSingleDto userProfiles = await _UserProfilesService.QueryByIdAsync<UserProfilesSingleDto>(UserId);
                List<Inventorys> inventorys = await _u8service.QueryInventorysByCode(cInvCode, userProfiles.Storage);//所有u8数据
                List<TaskSingleDto> tasks = await _service.QueryAsync<TaskSingleDto>();
                List<TaskSingleDto> newTasks = new List<TaskSingleDto>();

                if (inventorys.Count != 0)
                {
                    TaskSingleDto task = null;
                    string taskId = Guid.NewGuid().ToString();
                    int margin = 0;//总余量
                    string groupid = "";//分组标识
                    foreach (var item in inventorys)//循环批次
                    {
                        List<TaskSingleDto> tasks2 = tasks.FindAll(it => it.Cbatch == item.cBatch & it.WareHouse == item.cWhCode & it.CinvCode == item.cInvCode);
                        foreach (var item2 in tasks2)
                        {
                            item.iquantity = item.iquantity - Convert.ToInt32(item2.Count);
                        }
                        if (margin != 0)//上个批次有余量需要拼箱
                        {
                            task = new TaskSingleDto();
                            task.OperationUser = UserId;
                            task.CinvCode = cInvCode;
                            task.WareHouse = item.cWhCode;
                            task.WorkStations = userProfiles.WorkStation;
                            task.Status = "0";//未完成
                            task.TaskId = taskId;
                            task.Cbatch = item.cBatch;
                            task.GroupId = groupid;
                            if (item.iquantity + margin < item.ConstantVolume)// 如果与上个批次相加还是不满1箱
                            {
                                task.Count = item.iquantity;
                                margin = margin + item.iquantity;
                                newTasks.Add(task);
                                continue;
                            }
                            item.iquantity = item.iquantity - (Convert.ToInt32(item.ConstantVolume) - margin);//去掉拼箱后的数量
                            task.Count = Convert.ToInt32(item.ConstantVolume) - margin;
                            margin = 0;
                            groupid = "";
                            newTasks.Add(task);
                        }
                        int newmargin = item.iquantity % Convert.ToInt32(item.ConstantVolume);//当前批次余量
                        int BoxCount = item.iquantity / Convert.ToInt32(item.ConstantVolume);//当前可装满箱数
                        foreach (var g in tasks2.FindAll(it => it.Status == "0" && it.GroupId != "").ToList())
                        {
                            int gcount = Convert.ToInt32(tasks.FindAll(it => it.GroupId == g.GroupId).ToList().Sum(key => key.Count));
                            if (gcount < item.ConstantVolume)
                            {
                                int a = Convert.ToInt32(item.ConstantVolume) - Convert.ToInt32(gcount);
                                groupid = g.GroupId;
                                task = new TaskSingleDto();
                                task.OperationUser = UserId;
                                task.CinvCode = cInvCode;
                                task.WareHouse = item.cWhCode;
                                task.WorkStations = userProfiles.WorkStation;
                                task.Status = "0";//未完成
                                task.TaskId = taskId;
                                if (item.iquantity - a < 0)
                                {
                                    task.Count = item.iquantity;
                                    newmargin = 0;
                                }
                                else
                                {
                                    item.iquantity = item.iquantity - a;
                                    task.Count = a;
                                    newmargin = item.iquantity % Convert.ToInt32(item.ConstantVolume);
                                    BoxCount = item.iquantity / Convert.ToInt32(item.ConstantVolume);
                                }

                                task.GroupId = groupid;
                                task.Cbatch = item.cBatch;
                                newTasks.Add(task);
                                break;
                            }
                        }

                        for (int i = 0; i < BoxCount; i++)//满箱任务ADD
                        {
                            task = new TaskSingleDto();
                            task.OperationUser = UserId;
                            task.CinvCode = cInvCode;
                            task.WareHouse = item.cWhCode;
                            task.WorkStations = userProfiles.WorkStation;
                            task.Count = item.ConstantVolume;
                            task.Status = "0";//未完成
                            task.TaskId = taskId;
                            task.GroupId = "";
                            task.Cbatch = item.cBatch;
                            newTasks.Add(task);
                        }
                        if (newmargin > 0)
                        {

                            groupid = Guid.NewGuid().ToString();
                            margin = newmargin;
                            task = new TaskSingleDto();
                            task.OperationUser = UserId;
                            task.CinvCode = cInvCode;
                            task.WareHouse = item.cWhCode;
                            task.WorkStations = userProfiles.WorkStation;
                            task.Status = "0";//未完成
                            task.TaskId = taskId;
                            task.Count = newmargin;
                            task.GroupId = groupid;
                            task.Cbatch = item.cBatch;
                            newTasks.Add(task);
                        }
                    }
                    newTasks.RemoveAll(it => it.Count == 0);
                    int rows = await _service.Insert(newTasks);//得到影响行数
                    if (rows > 0)
                    {
                        return Ok(new { Status = true, Message = "任务创建成功" });

                    }
                }

                return Ok(new { Status = true, Message = "任务创建失败" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, Message = "任务创建失败！原因：" + ex.Message });

            }
        }

        /// <summary>
        /// 新增任务单条
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, Route("Insert")]
        public async Task<IActionResult> Insert([FromBody]TaskSingleDto entity)
        {
            try
            {
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
        /// 更新任务
        /// </summary>
        /// <param name="Id">主键</param>
        /// <param name="entity">任务对象</param>
        /// <returns></returns>
        [HttpPut, Route("{Id}")]
        public async Task<IActionResult> Update(string Id, [FromBody]TaskSingleDto entity)
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
        /// 更新任务(临时更新)
        /// </summary>
        /// <param name="entity">任务对象</param>
        /// <returns></returns>
        [HttpPut, Route("UpdateTask")]
        public async Task<IActionResult> UpdateTask(TaskSingleDto entity)
        {
            try
            {
                int rows = await _service.Update(entity);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "更新成功");
                }

                return OKAction(false, "更新失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"更新失败!原因:{ex.Message}");
            }
        }
        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskId">主键</param>
        /// <param name="BarCode">条码</param>
        /// <param name="User">用户</param>
        /// <returns></returns>
        [HttpPut, Route("UpdateStatus/{taskId}/{BarCode}/{User}")]
        public async Task<IActionResult> UpdateStatus(string taskId, string BarCode, string User)
        {
            try
            {
                int rows = 0;
                TaskSingleDto task = await _service.QueryByIdAsync<TaskSingleDto>(taskId);
                PrintLogSingleDto printLog = new PrintLogSingleDto();
                List<SearchCondition> Packconditions = new List<SearchCondition>();
                //if (task != null)
                //{
                //    Packconditions.Add(new SearchCondition
                //    {
                //        Filed = "EnCode",
                //        Value = task.CinvCode,
                //        Operation = CommonEnum.ConditionOperation.Equal
                //    });
                //    List<PackageInventorySingleDto> packs = await _packservice.QueryAsync<PackageInventorySingleDto>(Packconditions);
                //    task.Status = "1";
                //     rows = await _service.Update(task);//得到影响行数
                //    printLog.cInvCode = task.CinvCode;
                //    printLog.cWhCode = task.WareHouse;
                //    printLog.Desc = packs[0].Desc;
                //    printLog.Qty = task.Count.ToString();
                //    printLog.TaskId = task.Id;
                //    printLog.User = User;
                //    printLog.WorkCode = task.WorkStations;
                //    printLog.cBatch = task.Cbatch;
                //    printLog.BarCode = BarCode;
                //    printLog.Address = "地址待修改";
                //    printLog.CreatorTime = DateTime.Now.ToLocalTime();
                //    printLog.TaskGroupId = task.TaskId;
                //}
                //else
                //{
                //    List<SearchCondition> conditions = new List<SearchCondition>();
                //    conditions.Add(new SearchCondition
                //    {
                //        Filed = "GroupId",
                //        Value = taskId,
                //        Operation = CommonEnum.ConditionOperation.Equal
                //    });
                //    List<TaskSingleDto> taskGroup = await _service.QueryAsync<TaskSingleDto>(conditions);
                //    Packconditions.Add(new SearchCondition
                //    {
                //        Filed = "EnCode",
                //        Value = taskGroup[0].CinvCode,
                //        Operation = CommonEnum.ConditionOperation.Equal
                //    });
                //    //Packconditions.Add(new SearchCondition
                //    //{
                //    //    Filed = "OutStorage",
                //    //    Value = taskGroup[0].WareHouse,
                //    //    Operation = CommonEnum.ConditionOperation.Equal
                //    //});
                //    List<PackageInventorySingleDto> packs = await _packservice.QueryAsync<PackageInventorySingleDto>(Packconditions);
                //    taskGroup[0].Status = "1";
                //    printLog.cInvCode = taskGroup[0].CinvCode;
                //    printLog.cWhCode = taskGroup[0].WareHouse;
                //    printLog.Desc = packs[0].Desc;
                //    printLog.TaskId = taskGroup[0].Id;
                //    printLog.User = User;
                //    printLog.WorkCode = taskGroup[0].WorkStations;
                //    printLog.cBatch = taskGroup[0].Cbatch;
                //    printLog.BarCode = BarCode;
                //    printLog.Qty = taskGroup[0].Count.ToString();
                //    printLog.Address = "地址待修改";
                //    printLog.CreatorTime = DateTime.Now.ToLocalTime();
                //    printLog.TaskGroupId = taskGroup[0].TaskId;
                //    for (int i = 1; i < taskGroup.Count; i++)
                //    {
                //        printLog.cBatch +="/"+ taskGroup[i].Cbatch;
                //        printLog.Qty +="/"+taskGroup[i].Count.ToString();
                //        taskGroup[i].Status = "1";
                //    }
                //     rows = await _service.Update(taskGroup);
                //}

                if (rows > 0)
                {
                    int a = await _PrintLogService.Insert(printLog);
                    return Ok(new { Status = true, Message = "更新成功" });
                }

                return NotFoundAction(true, "更新失败!原因:影响行数为0");
            }
            catch (Exception ex)
            {
                return OKAction(false, $"更新失败!原因:{ex.Message}");
            }
        }

        #endregion

        #region Delete请求
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Id">任务Id</param>
        /// <returns></returns>
        [HttpDelete, Route("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                int rows = await _service.DeleteByKey(Id);//得到影响行数
                if (rows > 0)
                {
                    return OKAction(true, "删除成功");
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