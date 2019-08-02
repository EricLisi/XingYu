/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     2019/7/31 9:34:26                            */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('MST_PackageInventory')
            and   type = 'U')
   drop table MST_PackageInventory
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MST_PositionFiles')
            and   type = 'U')
   drop table MST_PositionFiles
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MST_PrintLog')
            and   type = 'U')
   drop table MST_PrintLog
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MST_Task')
            and   type = 'U')
   drop table MST_Task
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MST_UserProfiles')
            and   type = 'U')
   drop table MST_UserProfiles
go

/*==============================================================*/
/* Table: MST_PackageInventory                                  */
/*==============================================================*/
create table MST_PackageInventory (
   F_Id                 nvarchar(50)         not null,
   F_ClassIfyName       nvarchar(50)         null,
   F_Unit               nvarchar(50)         null,
   F_cInvStd            nvarchar(50)         null,
   F_EnCode             nvarchar(50)         null,
   F_ConstantVolume     int                  null,
   F_cInvName           nvarchar(50)         null,
   F_OutcWhName         nvarchar(50)         null,
   F_PutcWhName         nvarchar(50)         null,
   F_Desc               nvarchar(50)         null,
   F_Packing            int                  null,
   F_OutStorage         nvarchar(50)         null,
   F_PutStorage         nvarchar(50)         null,
   F_SortCode           int                  null,
   F_DeleteMark         bit                  null,
   F_EnabledMark        bit                  null,
   F_Description        nvarchar(255)        null,
   F_CreatorTime        datetime             null,
   F_CreatorUserId      nvarchar(100)        null,
   F_CreateUserName     nvarchar(100)        null,
   F_DeleteTime         datetime             null,
   F_DeleteUserId       nvarchar(100)        null,
   F_LastModifyTime     datetime             null,
   F_LastModifyUserId   nvarchar(100)        null,
   F_Define1            nvarchar(100)        null,
   F_Define2            nvarchar(100)        null,
   F_Define3            nvarchar(100)        null,
   F_Define4            nvarchar(100)        null,
   F_Define5            nvarchar(100)        null,
   F_Define6            nvarchar(100)        null,
   F_Define7            nvarchar(100)        null,
   F_Define8            nvarchar(100)        null,
   F_Define9            nvarchar(100)        null,
   F_Define10           nvarchar(100)        null,
   constraint PK_MST_PACKAGEINVENTORY primary key (F_Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('MST_PackageInventory') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'MST_PackageInventory' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '包装记录信息', 
   'user', @CurrentUser, 'table', 'MST_PackageInventory'
go

/*==============================================================*/
/* Table: MST_PositionFiles                                     */
/*==============================================================*/
create table MST_PositionFiles (
   Id                   nvarchar(50)         null,
   F_cWhCode            nvarchar(50)         null,
   F_PositionCode       nvarchar(50)         null,
   F_SortCode           int                  null,
   F_DeleteMark         bit                  null,
   F_EnabledMark        bit                  null,
   F_Description        nvarchar(255)        null,
   F_CreatorTime        datetime             null,
   F_CreatorUserId      nvarchar(100)        null,
   F_CreateUserName     nvarchar(100)        null,
   F_DeleteTime         datetime             null,
   F_DeleteUserId       nvarchar(100)        null,
   F_LastModifyTime     datetime             null,
   F_LastModifyUserId   nvarchar(100)        null,
   F_Define1            nvarchar(100)        null,
   F_Define2            nvarchar(100)        null,
   F_Define3            nvarchar(100)        null,
   F_Define4            nvarchar(100)        null,
   F_Define5            nvarchar(100)        null,
   F_Define6            nvarchar(100)        null,
   F_Define7            nvarchar(100)        null,
   F_Define8            nvarchar(100)        null,
   F_Define9            nvarchar(100)        null,
   F_Define10           nvarchar(100)        null
)
go

/*==============================================================*/
/* Table: MST_PrintLog                                          */
/*==============================================================*/
create table MST_PrintLog (
   F_Id                 nvarchar(50)         null,
   F_User               nvarchar(50)         null,
   F_Status             nvarchar(10)         null,
   F_cWhCode            nvarchar(50)         null,
   F_cInvCode           nvarchar(50)         null,
   F_TaskId             nvarchar(50)         null,
   F_TaskGroupId        nvarchar(50)         null,
   F_Qty                int                  null,
   F_Address            nvarchar(50)         null,
   F_cBatch             nvarchar(50)         null,
   F_WorkCode           nvarchar(50)         null,
   F_Desc               nvarchar(50)         null,
   F_BarCode            nvarchar(50)         null,
   F_SortCode           int                  null,
   F_DeleteMark         bit                  null,
   F_EnabledMark        bit                  null,
   F_Description        nvarchar(255)        null,
   F_CreatorTime        datetime             null,
   F_CreatorUserId      nvarchar(100)        null,
   F_CreateUserName     nvarchar(100)        null,
   F_DeleteTime         datetime             null,
   F_DeleteUserId       nvarchar(100)        null,
   F_LastModifyTime     datetime             null,
   F_LastModifyUserId   nvarchar(100)        null,
   F_Define1            nvarchar(100)        null,
   F_Define2            nvarchar(100)        null,
   F_Define3            nvarchar(100)        null,
   F_Define4            nvarchar(100)        null,
   F_Define5            nvarchar(100)        null,
   F_Define6            nvarchar(100)        null,
   F_Define7            nvarchar(100)        null,
   F_Define8            nvarchar(100)        null,
   F_Define9            nvarchar(100)        null,
   F_Define10           nvarchar(100)        null
)
go

/*==============================================================*/
/* Table: MST_Task                                              */
/*==============================================================*/
create table MST_Task (
   F_Id                 nvarchar(50)         not null,
   F_TaskId             nvarchar(50)         null,
   F_OperationUser      nvarchar(50)         null,
   F_WorkStations       nvarchar(50)         null,
   F_Status             nvarchar(50)         null,
   F_WareHouse          nvarchar(50)         null,
   F_Count              int                  null,
   F_CinvId             nvarchar(50)         null,
   F_GroupId            nvarchar(50)         null,
   F_Cbatch             nvarchar(50)         null,
   F_SortCode           int                  null,
   F_DeleteMark         bit                  null,
   F_EnabledMark        bit                  null,
   F_Description        nvarchar(255)        null,
   F_CreatorTime        datetime             null,
   F_CreatorUserId      nvarchar(100)        null,
   F_CreateUserName     nvarchar(100)        null,
   F_DeleteTime         datetime             null,
   F_DeleteUserId       nvarchar(100)        null,
   F_LastModifyTime     datetime             null,
   F_LastModifyUserId   nvarchar(100)        null,
   F_Define1            nvarchar(100)        null,
   F_Define2            nvarchar(100)        null,
   F_Define3            nvarchar(100)        null,
   F_Define4            nvarchar(100)        null,
   F_Define5            nvarchar(100)        null,
   F_Define6            nvarchar(100)        null,
   F_Define7            nvarchar(100)        null,
   F_Define8            nvarchar(100)        null,
   F_Define9            nvarchar(100)        null,
   F_Define10           nvarchar(100)        null,
   constraint PK_MST_TASK primary key (F_Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('MST_Task') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'MST_Task' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '任务记录', 
   'user', @CurrentUser, 'table', 'MST_Task'
go

/*==============================================================*/
/* Table: MST_UserProfiles                                      */
/*==============================================================*/
create table MST_UserProfiles (
   F_Id                 nvarchar(50)         not null,
   F_cWhName            nvarchar(50)         null,
   F_NickName           nvarchar(50)         null,
   F_RealName           nvarchar(50)         null,
   F_Profession         nvarchar(50)         null,
   F_WorkStation        nvarchar(50)         null,
   F_EnCode             nvarchar(50)         null,
   F_Account            nvarchar(50)         null,
   F_Password           nvarchar(50)         null,
   F_Secretkey          nvarchar(50)         null,
   F_Storage            nvarchar(50)         null,
   F_IsAdmin            bit                  null,
   F_Status             nvarchar(50)         null,
   F_SortCode           int                  null,
   F_DeleteMark         bit                  null,
   F_EnabledMark        bit                  null,
   F_Description        nvarchar(255)        null,
   F_CreatorTime        datetime             null,
   F_CreatorUserId      nvarchar(100)        null,
   F_CreateUserName     nvarchar(100)        null,
   F_DeleteTime         datetime             null,
   F_DeleteUserId       nvarchar(100)        null,
   F_LastModifyTime     datetime             null,
   F_LastModifyUserId   nvarchar(100)        null,
   F_Define1            nvarchar(100)        null,
   F_Define2            nvarchar(100)        null,
   F_Define3            nvarchar(100)        null,
   F_Define4            nvarchar(100)        null,
   F_Define5            nvarchar(100)        null,
   F_Define6            nvarchar(100)        null,
   F_Define7            nvarchar(100)        null,
   F_Define8            nvarchar(100)        null,
   F_Define9            nvarchar(100)        null,
   F_Define10           nvarchar(100)        null,
   constraint PK_MST_USERPROFILES primary key (F_Id)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('MST_UserProfiles') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'MST_UserProfiles' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '记录用户使用记录', 
   'user', @CurrentUser, 'table', 'MST_UserProfiles'
go

