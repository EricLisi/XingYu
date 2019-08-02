using KGM.Framework.Domain;
using KGM.Framework.Domain.Model.U8;
using KGM.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace KGM.Framework.RepositoryEF.Repositories
{
    // BaseRepository<U8Entity, U8Context>, 
    public class U8Repository : IU8Repository
    {
        /// <summary>
        /// 获取仓库
        /// </summary>
        /// <returns></returns> 
        public List<U8Entity.WarehouseEntity> QueryWarehouse(string Code)
        {
            using (var db = new U8Context())
            {
                if (Code != "1")
                {
                    return db.Warehouse.AsQueryable().Where(it => it.CWhCode == Code).ToList();
                }
                return db.Warehouse.AsQueryable().ToList();
            }
        }
        /// <summary>
        /// 获取存货库存数据
        /// </summary>
        /// <returns></returns>
        public List<U8Entity.InventorysEntity> QueryInventorys()
        {
            using (var db = new U8Context())
            {
                return db.Inventorys.AsQueryable().Select(g => new U8Entity.InventorysEntity
                {
                    cCheckState = g.cCheckState,
                    cInvCode = g.cInvCode,
                    cInvName = g.cInvName,
                    cInvStd = g.cInvStd,
                    cWhCode = g.cWhCode,
                    cWhname = g.cWhname,
                    iquantity = Convert.ToInt32(g.iquantity)
                }).ToList();
            }
        }
        /// <summary>
        /// 根据产品编号/仓库编号查存货库存详细数据
        /// </summary>
        /// <param name="InvCode"></param>
        /// <param name="cWhCode"></param>
        /// <returns></returns>
        public List<U8Entity.InventorysEntity> QueryInventorysByCode(string InvCode, string cWhCode)
        {
            using (var db = new U8Context())
            {
                List<PackageInventoryEntity> packs = new List<PackageInventoryEntity>();
                using (var pack = new PackageInventoryContext())
                {
                    //packs = pack.CurrentDbSet.Where(it => it.OutStorage == cWhCode & it.EnCode == InvCode).ToList();
                    packs = pack.CurrentDbSet.Where(it => it.EnCode == InvCode).ToList();
                }
                List<U8Entity.InventorysEntity> list = db.Inventorys.Where(it => it.cInvCode == InvCode && it.cWhCode == cWhCode).Select(
                    g => new U8Entity.InventorysEntity
                    {
                        cInvCode = g.cInvCode,
                        cbatch = g.cbatch,
                        iquantity = Convert.ToInt32(g.iquantity),
                        cWhCode = cWhCode
                    }
                    ).OrderBy(it => it.cbatch).ToList();

                foreach (var item in list)
                {
                    if (packs.Count == 0)
                    {
                        return null;
                    }
                    item.ConstantVolume = packs[0].ConstantVolume;
                }
                return list;
            }
        }
        /// <summary>
        /// 根据仓库号查存货库存数据
        /// </summary>
        /// <param name="WhCode"></param>
        /// <returns></returns>
        public List<U8Entity.InventorysEntity> QueryInventorysByWhCode(string WhCode, string position)
        {
            string currentDb = string.Empty;

            var config = JSonConfigReader.ReadFile("Config/Database.json");
            var connstr = config[$"ConnectionStrings:{config["ConnectionStrings:DbType"]}Connection"];
            currentDb = connstr.Split(';')[1].Replace("database=", string.Empty);
            string sql = "";

            if (position.Equals("-1"))
            {
                sql = $@"SELECT  A.cInvCode,A.cCheckState, A.cWhCode,'' cPosition,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,'' cInvSN,A.dmdate,A.dvdate,
    A.cmassunit imassunit,A.imassdate,B.cInvName,B.cInvStd,B.cInvAddCode,cInvDefine1,cInvDefine2,
    cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,
    cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,
    CAST(SUM(A.iquantity) - ISNULL(TB.F_Qty,0) AS INT) iquantity,CAST(SUM(A.iNum) AS INT) iNum,'' cPosName,W.cWhname,
    MI.F_ConstantVolume ConstantVolume,ISNULL(Boxed,0) boxed,ISNULL(TT.CSTATUS,'未操作') cCheckState,
    MI.F_Define1 lr,MI.F_cInvCode th,MI.F_define3 define3,MI.F_DESC desct
FROM currentstock AS A
INNER JOIN WareHouse AS W ON A.cWhCode = W.cWhCode
LEFT JOIN Inventory AS B ON A.cInvCode = B.cInvCode
INNER JOIN {currentDb}..MST_PackageInventory AS MI ON A.cInvCode = MI.F_EnCode
OUTER APPLY (
	SELECT F_WareHouse,F_CinvCode,CASE WHEN MAX(F_STATUS) = 1 AND MIN(F_STATUS) = 1 THEN N'已完成'
		WHEN MAX(F_STATUS) <> MIN(F_STATUS)  THEN N'操作中' ELSE N'未操作' END CSTATUS,
		Boxed = (
			SELECT COUNT(*)  
			FROM (
				    SELECT CASE F_GROUPID WHEN '' THEN NEWID() ELSE F_GroupId END G,F_WareHouse,F_CinvCode 
				    FROM {currentDb}..MST_TASK SSS
				    WHERE SSS.F_WareHouse = PPP.F_WareHouse AND SSS.F_CinvCode = PPP.F_CinvCode
				    GROUP BY CASE F_GROUPID WHEN '' THEN NEWID() ELSE F_GroupId END,F_WareHouse,F_CinvCode 
			) AS BBB
		)
	FROM {currentDb}..MST_TASK PPP
	WHERE A.CWHCODE = PPP.F_WareHouse AND A.cInvCode = PPP.F_CinvCode
	GROUP BY F_WareHouse,F_CinvCode
) AS TT
OUTER APPLY (
    SELECT F_cWhCode,F_cInvCode,F_CBatch,SUM(F_Qty) F_Qty
    FROM {currentDb}..MST_PrintLog AS TT
    WHERE F_cWhCode = @CWHCODE AND TT.F_cInvCode = A.cInvCode AND TT.F_CBatch = A.cbatch
    GROUP BY F_cWhCode,F_cInvCode,F_CBatch
) TB
WHERE W.BINCOST = 1 AND A.CWHCODE = @CWHCODE
GROUP BY  A.cInvCode,A.cWhCode,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,MI.F_Define1,MI.F_cInvCode,MI.F_DESC,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,A.dmdate,A.dvdate,ISNULL(TB.F_Qty,0),MI.F_define3,
    A.cmassunit,A.imassdate,B.cInvName,A.cCheckState,B.cInvStd,B.cInvAddCode,cInvDefine1,cInvDefine2,
    cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,ISNULL(TT.CSTATUS,'未操作'),
    cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,W.cWhName,MI.F_ConstantVolume,ISNULL(Boxed,0)
HAVING SUM(A.iquantity) > 0 ";
            }
            else
            {

                sql = $@"SELECT  A.cInvCode,A.cCheckState, A.cWhCode,'' cPosition,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,'' cInvSN,A.dmdate,A.dvdate,
    A.cmassunit imassunit,A.imassdate,B.cInvName,B.cInvStd,B.cInvAddCode,cInvDefine1,cInvDefine2,
    cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,
    cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,
    CAST(SUM(A.iquantity) - ISNULL(TB.F_Qty,0) AS INT) iquantity,CAST(SUM(A.iNum) AS INT) iNum,'' cPosName,W.cWhname,
    MI.F_ConstantVolume ConstantVolume,ISNULL(Boxed,0) boxed,ISNULL(TT.CSTATUS,'未操作') cCheckState,
    MI.F_Define1 lr,MI.F_cInvCode th,MI.F_define3 define3,MI.F_DESC desct
FROM currentstock AS A
INNER JOIN WareHouse AS W ON A.cWhCode = W.cWhCode
LEFT JOIN Inventory AS B ON A.cInvCode = B.cInvCode
INNER JOIN {currentDb}..MST_PackageInventory AS MI ON A.cInvCode = MI.F_EnCode
OUTER APPLY (
	SELECT F_WareHouse,F_CinvCode,CASE WHEN MAX(F_STATUS) = 1 AND MIN(F_STATUS) = 1 THEN N'已完成'
		WHEN MAX(F_STATUS) <> MIN(F_STATUS)  THEN N'操作中' ELSE N'未操作' END CSTATUS,
		Boxed = (
			SELECT COUNT(*)  
			FROM (
				    SELECT CASE F_GROUPID WHEN '' THEN NEWID() ELSE F_GroupId END G,F_WareHouse,F_CinvCode 
				    FROM {currentDb}..MST_TASK SSS
				    WHERE SSS.F_WareHouse = PPP.F_WareHouse AND SSS.F_CinvCode = PPP.F_CinvCode
				    GROUP BY CASE F_GROUPID WHEN '' THEN NEWID() ELSE F_GroupId END,F_WareHouse,F_CinvCode 
			) AS BBB
		)
	FROM {currentDb}..MST_TASK PPP
	WHERE A.CWHCODE = PPP.F_WareHouse AND A.cInvCode = PPP.F_CinvCode
	GROUP BY F_WareHouse,F_CinvCode
) AS TT  
OUTER APPLY (
    SELECT F_cWhCode,F_cInvCode,F_CBatch,SUM(F_Qty) F_Qty
    FROM {currentDb}..MST_PrintLog AS TT
    WHERE F_cWhCode = @CWHCODE AND TT.F_cInvCode = A.cInvCode AND TT.F_CBatch = A.cbatch
    GROUP BY F_cWhCode,F_cInvCode,F_CBatch
) TB
WHERE W.BINCOST = 1 AND A.CWHCODE = @CWHCODE AND MI.F_DEFINE2 = @POSITION
GROUP BY  A.cInvCode,A.cWhCode,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,MI.F_Define1,MI.F_cInvCode,MI.F_DESC,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,A.dmdate,A.dvdate,ISNULL(TB.F_Qty,0),MI.F_define3,
    A.cmassunit,A.imassdate,B.cInvName,A.cCheckState,B.cInvStd,B.cInvAddCode,cInvDefine1,cInvDefine2,
    cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,ISNULL(TT.CSTATUS,'未操作'),
    cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,W.cWhName,MI.F_ConstantVolume,ISNULL(Boxed,0)
HAVING SUM(A.iquantity) > 0
UNION
SELECT  A.cInvCode,A.cCheckState, A.cWhCode,'' cPosition,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,'' cInvSN,A.dmdate,A.dvdate,
    A.imassunit,A.imassdate,B.cInvName,B.cInvStd,B.cInvAddCode,cInvDefine1,cInvDefine2,
    cInvDefine3,cInvDefine4,cInvDefine5,cInvDefine6,cInvDefine7,cInvDefine8,cInvDefine9,
    cInvDefine10,cInvDefine11,cInvDefine12,cInvDefine13,cInvDefine14,cInvDefine15,cInvDefine16,
    TT.F_COUNT iquantity,A.iNum,'' cPosName,W.cWhname,
    MI.F_ConstantVolume ConstantVolume,0 boxed,ISNULL(TT.CSTATUS,'未操作') cCheckState,
    MI.F_Define1 lr,MI.F_cInvCode th,MI.F_define3 define3,MI.F_DESC
FROM (
	SELECT F_CinvCode,F_Cbatch,F_WAREHOUSE,SUM(F_Count) F_COUNT,Boxed = (
				SELECT COUNT(*)  
				FROM (
						SELECT CASE F_GROUPID WHEN '' THEN NEWID() ELSE F_GroupId END G 
						FROM {currentDb}..MST_TASK SSS
						WHERE F_WareHouse = @CWHCODE AND F_WorkStations = @POSITION
						GROUP BY CASE F_GROUPID WHEN '' THEN NEWID() ELSE F_GroupId END 
				) AS BBB
			),CSTATUS = (SELECT CASE WHEN MAX(F_STATUS) = 1 AND MIN(F_STATUS) = 1 THEN N'已完成'
							WHEN MAX(F_STATUS) <> MIN(F_STATUS)  THEN N'操作中' ELSE N'未操作' END
						FROM {currentDb}..MST_TASK SSS
						WHERE F_WareHouse = @CWHCODE AND F_WorkStations = @POSITION)
	FROM {currentDb}..MST_TASK
	WHERE F_WareHouse = @CWHCODE AND F_WorkStations = @POSITION
	group by F_CinvCode,F_Cbatch,F_WAREHOUSE
) AS TT
INNER JOIN WareHouse AS W ON TT.F_WareHouse = W.cWhCode
LEFT JOIN Inventory AS B ON TT.F_CinvCode = B.cInvCode
INNER JOIN {currentDb}..MST_PackageInventory AS MI ON TT.F_CinvCode = MI.F_EnCode AND MI.F_DEFINE2 <> @POSITION
CROSS APPLY (
	SELECT  A.cInvCode,A.cCheckState, A.cWhCode,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,'' cInvSN,A.dmdate,A.dvdate,
    A.cmassunit imassunit,A.imassdate,
    CAST(SUM(A.iquantity) AS INT) iquantity,CAST(SUM(A.iNum) AS INT) iNum
	FROM currentstock AS A 
	WHERE TT.F_CinvCode= A.cInvCode AND A.cBatch = TT.F_Cbatch AND A.cWhCode = TT.F_WareHouse
	GROUP BY A.cInvCode,A.cCheckState, A.cWhCode,A.cfree1,A.cfree2,A.cfree3,A.cfree4,A.cfree5,
    A.cfree6,A.cfree7,A.cfree8,A.cfree9,A.cfree10,A.cbatch,A.dmdate,A.dvdate,
    A.cmassunit,A.imassdate
) AS A
WHERE W.BINCOST = 1  AND TT.CSTATUS <> '已完成'  ";
            }

            SqlParameter[] paramlist = new SqlParameter[] {
                new SqlParameter("@CWHCODE",WhCode),
                new SqlParameter("@POSITION",position)
            };


            using (var db = new U8Context())
            {
                return ExcuteSqlToList<U8Entity.InventorysEntity>(db, sql, paramlist);
            }


            //using (var db = new U8Context())
            //{
            //    List<PackageInventoryEntity> packs = new List<PackageInventoryEntity>();
            //    using (var pack = new PackageInventoryContext()) {
            //       packs= pack.CurrentDbSet.Where(it=>it.OutStorage==WhCode).ToList();
            //    }
            //    List<U8Entity.InventorysEntity> list = new List<U8Entity.InventorysEntity>();
            //    list = db.Inventorys.Where(it => it.cWhCode == WhCode).GroupBy(it => new
            //    {
            //        it.cInvCode,
            //        it.cInvName,
            //        it.cWhCode,
            //        it.cWhname,
            //        it.cInvStd
            //        //it.cCheckState
            //    }).Select(g => new U8Entity.InventorysEntity
            //    {
            //        cInvCode = g.Key.cInvCode,
            //        cInvName = g.Key.cInvName,
            //        cWhCode = g.Key.cWhCode,
            //        cWhname = g.Key.cWhname,
            //        cInvStd = g.Key.cInvStd,
            //        //cCheckState = g.Key.cCheckState,
            //        iquantity = Convert.ToInt32(g.Sum(item => item.iquantity)),//任务总数量
            //    }).ToList();
            //    List<U8Entity.InventorysEntity> resultList = new List<U8Entity.InventorysEntity>();
            //    foreach (var item in packs)
            //    {
            //        List<U8Entity.InventorysEntity> inventorysEntityList = list.FindAll(it=>it.cInvCode==item.EnCode);
            //        if (inventorysEntityList != null)
            //        {
            //            foreach (var item2 in inventorysEntityList)
            //            {
            //                item2.ConstantVolume = item.ConstantVolume;
            //                item2.BoxCount = item2.iquantity / item.ConstantVolume;
            //                item2.Margin = item2.iquantity % item.ConstantVolume;
            //                if (item.FreezeStatus == "1")
            //                {
            //                    item2.status = 3;
            //                }
            //                resultList.Add(item2);
            //            }
            //        }
            //    }
            //    return resultList.OrderByDescending(it=>it.BoxCount).ToList();      
            //}
        }


        /// <summary>   
        /// <summary>
        /// 执行存储过程 返回结果集
        /// </summary>
        /// <typeparam name="TElement">返回结果集对象</typeparam>
        /// <param name="procName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual List<TElement> ExcuteSqlToList<TElement>(DbContext context, string sql, params object[] parameters) where TElement : new()
        {
            using (var connection = context.Database.GetDbConnection())
            {

                using (var cmd = connection.CreateCommand())
                {
                    context.Database.OpenConnection();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddRange(parameters);
                    var dr = cmd.ExecuteReader();
                    var columnSchema = dr.GetColumnSchema();
                    var data = new List<TElement>();

                    while (dr.Read())
                    {
                        TElement item = new TElement();
                        Type type = item.GetType();
                        foreach (var kv in columnSchema)
                        {
                            var propertyInfo = type.GetProperty(kv.ColumnName);
                            if (kv.ColumnOrdinal.HasValue && propertyInfo != null)
                            {
                                //注意需要转换数据库中的DBNull类型
                                var value = dr.IsDBNull(kv.ColumnOrdinal.Value) ? null : dr.GetValue(kv.ColumnOrdinal.Value);
                                //string a = dr.GetValue(kv.ColumnOrdinal.Value).ToString();
                                propertyInfo.SetValue(item, value);
                            }
                        }
                        data.Add(item);
                    }
                    dr.Dispose();

                    return data;
                }
            }
        }

        public List<U8Entity.InventorysEntity> GetUndoList(string cWhCode, string cInvCode)
        {
            string currentDb = string.Empty;

            var config = JSonConfigReader.ReadFile("Config/Database.json");
            var connstr = config[$"ConnectionStrings:{config["ConnectionStrings:DbType"]}Connection"];
            currentDb = connstr.Split(';')[1].Replace("database=", string.Empty);


            string sql = $@"SELECT A.cInvCode,ISNULL(A.cbatch,'') cbatch,cast(SUM(A.iquantity) as int)  - ISNULL(TT.Qty,0) iquantity,MI.F_ConstantVolume ConstantVolume
                            FROM CurrentStock AS A 
                            INNER JOIN WareHouse AS W ON A.cWhCode = W.cWhCode
                            INNER JOIN {currentDb}..MST_PackageInventory AS MI ON A.cInvCode = MI.F_EnCode
                            LEFT JOIN (
	                            SELECT F_WareHouse,F_CinvCode,SUM(F_Count) Qty,F_Cbatch
	                            FROM {currentDb}..MST_TASK
	                            GROUP BY F_WareHouse,F_CinvCode,F_Cbatch,F_Cbatch
                            ) AS TT ON A.CWHCODE = TT.F_WareHouse AND A.cInvCode = TT.F_CinvCode AND A.cBatch = TT.F_Cbatch
                            WHERE W.BINCOST = 1 AND A.CINVCODE = @CINVCODE AND A.CWHCODE = @CWHCODE AND A.cBatch!=''
                            GROUP BY A.cInvCode,A.cBatch,ISNULL(TT.Qty,0),MI.F_ConstantVolume 
                            HAVING SUM(A.iquantity) > 0 ";

            SqlParameter[] paramlist = new SqlParameter[] {
                new SqlParameter("@CWHCODE",cWhCode),
                new SqlParameter("@CINVCODE",cInvCode)
            };

            using (var db = new U8Context())
            {
                return ExcuteSqlToList<U8Entity.InventorysEntity>(db, sql, paramlist);
            }
        }
    }
}
