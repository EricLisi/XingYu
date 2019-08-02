using KGM.Package.Models;
using KGM.Package.Print;
using KGM.Package.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KGM.Package
{
    public partial class FrmPrint : Form
    {
        public FrmPrint()
        {
            InitializeComponent();
        }
        #region 私有成员
        public FrmMain formMain { get; set; }
        public U8WarehouseModel U8 = new U8WarehouseModel();
        //u8库存
        public DataTable u8dt = new DataTable();
        #endregion

        #region 事件方法
        //价值事件
        private void FrmPrint_Load(object sender, EventArgs e)
        {
            //读取u8库存数据
            u8dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>("api/U8/QueryInventorysByWhCode/" + CommonValue.userInfo.storage + "/1/" + CommonValue.userInfo.workstation);
            
        }
        //继续扫描事件
        private void button3_Click(object sender, EventArgs e)
        {
            // 解析条码
            string[] barArray = null;
            if (txtBarcode.Text.Equals(string.Empty))
            {
                MessageBox.Show("条码为空,请先扫描条码！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                return;
            }
            barArray = BarcodeAnalys(txtBarcode.Text);
            if (barArray == null)
            {
                MessageBox.Show("解析失败，条码规则不符！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                return;
            }
            //读取任务待临时打印的数据
            DataTable dtTask= GetTasks(barArray[3].ToString(), CommonValue.userInfo.storage, barArray[6].ToString(), "1");
            if (dtTask.Rows.Count > 0)//过滤临时打印数据
            {
                dtTask.DefaultView.RowFilter = "Define1=1";
                dtTask = dtTask.DefaultView.ToTable();
            }
            int qty = 0;//当前临时打印余量
            if (dtTask.Rows.Count > 0)
            {
                qty = Convert.ToInt32(dtTask.Rows[0]["count"].ToString());
            }
            else
            {
                MessageBox.Show("该条码无临时打印任务！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                return;
            }
            //过滤u8库存数据
            u8dt.DefaultView.RowFilter = string.Format("cinvcode='{0}'", barArray[3].ToString()) ;
            u8dt = u8dt.DefaultView.ToTable();
            if (u8dt.Rows.Count > 0)//u8库存有数据
            {
                U8.constantvolume = u8dt.Rows[0]["constantvolume"].ToString();//赋值定容
                int u8qty = Convert.ToInt32(u8dt.Compute("SUM(iquantity)", "1=1"));//当前u8库存数量
                if (qty + u8qty >= Convert.ToInt32(U8.constantvolume.ToString()))//如果加起来大于或等于定容即可生成拼箱task
                {
                    int thisqty = Convert.ToInt32(U8.constantvolume.ToString()) - qty;//需要的数量
                    string groupid = Guid.NewGuid().ToString();//定义groupid
                    //开始添加任务并赋值groupid.......
                    foreach (DataRow row in u8dt.Rows)
                    {
                        TaskEntity entity = new TaskEntity();
                        entity.Cbatch = row["cbatch"].ToString();
                        entity.CinvCode = row["cinvcode"].ToString();
                        entity.TaskId = dtTask.Rows[0]["taskid"].ToString();
                        entity.OperationUser = "";
                        entity.WorkStations = CommonValue.userInfo.workstation;
                        entity.Status = "0";
                        entity.WareHouse = row["cwhcode"].ToString();
                        entity.GroupId = groupid;
                        entity.CreatorTime = DateTime.Now;
                        entity.CreatorUserId = CommonValue.userInfo.id;
                        if (Convert.ToInt32(row["iquantity"].ToString()) >= thisqty)//如果第一行批次足够拼箱
                        {
                            //如果是同一批次只需修改批次数量即可
                            if (row["cbatch"].ToString()== dtTask.Rows[0]["cbatch"].ToString())
                            {
                                entity.Count =Convert.ToInt32( U8.constantvolume);
                                entity.Status = "0";
                                entity.GroupId = "";
                                entity.Define1 = "0";
                                entity.Id = dtTask.Rows[0]["id"].ToString();
                                TaskResultEntity taskUpdateResult = WebAPIUtil.PutAPIByJsonToGeneric<TaskResultEntity>("api/Task/UpdateTask", WebAPIUtil.ConvertObjToJson(entity));
                                if (!taskUpdateResult.state)
                                {
                                    MessageBox.Show("临时任务修改失败！原因:" + taskUpdateResult.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtBarcode.Text = string.Empty;
                                    txtBarcode.Focus();
                                    return;
                                }
                                else
                                {
                                    //get打印履历数据
                                    DataTable dtPrint = GetPrintTasks(string.Empty, dtTask.Rows[0]["cinvcode"].ToString(), dtTask.Rows[0]["warehouse"].ToString());
                                    dtPrint.DefaultView.RowFilter = string.Format("TaskGroupId is NULL and qty<{0}", Convert.ToInt32(U8.constantvolume));
                                    if (dtPrint.Rows.Count > 0)
                                    {
                                        //删除log
                                        TaskResultEntity logResult = WebAPIUtil.DeleteAPIByJsonToGeneric<TaskResultEntity>(string.Format("api/PrintLog/Delete?Id={0}", dtPrint.Rows[0]["Id"].ToString()));
                                    }
                                    CommonValue.GROUPID = entity.GroupId.ToString();
                                    this.Close();//回到FrmScan扫描界面
                                    DataGridViewCellEventArgs maine = new DataGridViewCellEventArgs(0, 1);
                                    formMain.dataGridView1_CellClick(sender, maine);
                                    return;
                                }
                            }

                           //不是同一批次则新增group任务
                            entity.Count = thisqty;
                            TaskResultEntity taskResult = WebAPIUtil.PostAPIByJsonToGeneric<TaskResultEntity>("api/Task/Insert", WebAPIUtil.ConvertObjToJson(entity));
                            if (taskResult.state)//添加成功
                            {
                                //修改临时打印的任务数据
                                entity.Id = dtTask.Rows[0]["id"].ToString();
                                entity.Define1 = "0";
                                entity.GroupId = groupid;
                                entity.Count = null;
                                entity.Status = "1";
                                entity.Cbatch= dtTask.Rows[0]["cbatch"].ToString();
                                TaskResultEntity taskResult2 = WebAPIUtil.PutAPIByJsonToGeneric<TaskResultEntity>("api/Task/UpdateTask", WebAPIUtil.ConvertObjToJson(entity));
                                if (!taskResult2.state)
                                {
                                    MessageBox.Show("临时任务修改失败！原因:" + taskResult2.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtBarcode.Text = string.Empty;
                                    txtBarcode.Focus();
                                    return;
                                }
                                //get打印履历数据
                                DataTable dtPrint= GetPrintTasks(string.Empty, dtTask.Rows[0]["cinvcode"].ToString(), dtTask.Rows[0]["warehouse"].ToString());
                                dtPrint.DefaultView.RowFilter =string.Format("TaskGroupId is NULL and qty<{0}", Convert.ToInt32( U8.constantvolume) );
                                if (dtPrint.Rows.Count > 0)
                                {
                                    //给log的gtoupid赋值
                                    PrintModel print = new PrintModel();
                                    print.Id = dtPrint.Rows[0]["Id"].ToString();
                                    print.TaskGroupId = groupid;
                                    TaskResultEntity logResult = WebAPIUtil.PutAPIByJsonToGeneric<TaskResultEntity>("api/PrintLog/UpdatePrintLog", WebAPIUtil.ConvertObjToJson(print));
                                }
                                CommonValue.GROUPID = entity.GroupId.ToString();
                                this.Close();//回到FrmScan扫描界面
                                DataGridViewCellEventArgs maine = new DataGridViewCellEventArgs(0,1);
                                formMain.dataGridView1_CellClick(sender, maine);
                            }
                            else
                            {
                                MessageBox.Show("任务添加失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBarcode.Text = string.Empty;
                                txtBarcode.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(row["iquantity"].ToString()) > 0)//
                            {
                                //如果是同一批次只需修改批次数量即可
                                if (row["cbatch"].ToString() == dtTask.Rows[0]["cbatch"].ToString())
                                {
                                    //当前批次数量加上新增数量
                                    entity.Count = Convert.ToInt32(row["iquantity"].ToString()+Convert.ToInt32(dtTask.Rows[0]["cbatch"].ToString()));
                                    entity.Status = "0";
                                    entity.GroupId = "";
                                    entity.Define1 = "0";
                                    entity.Id = dtTask.Rows[0]["id"].ToString();
                                    TaskResultEntity taskUpdateResult = WebAPIUtil.PutAPIByJsonToGeneric<TaskResultEntity>("api/Task/UpdateTask", WebAPIUtil.ConvertObjToJson(entity));
                                    if (!taskUpdateResult.state)
                                    {
                                        MessageBox.Show("临时任务修改失败！原因:" + taskUpdateResult.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        txtBarcode.Text = string.Empty;
                                        txtBarcode.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        //get打印履历数据
                                        DataTable dtPrint = GetPrintTasks(string.Empty, dtTask.Rows[0]["cinvcode"].ToString(), dtTask.Rows[0]["warehouse"].ToString());
                                        dtPrint.DefaultView.RowFilter = string.Format("TaskGroupId is NULL and qty<{0}", Convert.ToInt32(U8.constantvolume));
                                        if (dtPrint.Rows.Count > 0)
                                        {
                                            //删除log
                                            TaskResultEntity logResult = WebAPIUtil.DeleteAPIByJsonToGeneric<TaskResultEntity>(string.Format("api/PrintLog/Delete?Id={0}", dtPrint.Rows[0]["Id"].ToString()));
                                        }
                                        CommonValue.GROUPID = entity.GroupId.ToString();
                                        this.Close();//回到FrmScan扫描界面
                                        DataGridViewCellEventArgs maine = new DataGridViewCellEventArgs(0, 1);
                                        formMain.dataGridView1_CellClick(sender, maine);
                                        return;
                                    }
                                }
                                //不是同一批次
                                entity.Count = Convert.ToInt32(row["iquantity"].ToString());
                                TaskResultEntity taskResult = WebAPIUtil.PostAPIByJsonToGeneric<TaskResultEntity>("api/Task/Insert", WebAPIUtil.ConvertObjToJson(entity));
                                if (taskResult.state)//添加成功
                                {
                                    thisqty -= Convert.ToInt32(entity.Count);//需要的数量减去新增任务的数量，继续循环
                                }
                                else
                                {
                                    MessageBox.Show("任务添加失败！原因:" + taskResult.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtBarcode.Text = string.Empty;
                                    txtBarcode.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("库存不足 ！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBarcode.Text = string.Empty;
                    txtBarcode.Focus();
                    return;
                }
                
            }
            else
            {
                MessageBox.Show("无库存 ！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                return;
            }
        }
        //打印
        private void button1_Click(object sender, EventArgs e)
        {
            string result= BarcodeKeyDownValidate(txtBarcode.Text);
            if(!string.IsNullOrEmpty(result))
            {
                MessageBox.Show(result, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();
                return;
            }
        }
        //barcode回车
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            button1_Click(sender, e);
        }
        #endregion
        #region 私有方法
        //获取任务数据
        private DataTable GetTasks(string cInvCode,string cWhCode,string cBatch,string Status)
        {
            DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(
              string.Format("api/Task/GeByConditionAsync?WorkStations={0}&cInvCode={1}&cWhCode={2}&cBatch={3}&GroupId={4}&Status={5}",
              string.Empty, cInvCode,
              cWhCode,
              cBatch,
              string.Empty,
              Status));
            return dt;
        }
        //获取打印履历数据
        private DataTable GetPrintTasks(string userid,string cinvcode,string cwhcode)
        {
            DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(string.Format("api/PrintLog/GeByConditionAsync?user={0}&cInvCode={1}&cWhCode={2}", userid, cinvcode, cwhcode));
            return dt;
        }
        /// <summary>
        /// 解析条码
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        private string[] BarcodeAnalys(string barcode)
        {
            string[] barArray = barcode.Split('|');
            if (barArray.Length < 8)
            {
                return null;
            }
            return barArray;
        }
        private string BarcodeKeyDownValidate(string barcode)
        {
            //解析条码
            string[] barArray = null;
            if (barcode.Equals(string.Empty))
            {
                return "条码为空,请先扫描条码！";
            }
            barArray = BarcodeAnalys(barcode);
            if (barArray == null)
            {
                return "解析失败，条码规则不符！";
            }

            if (!barArray[6].ToString().Equals(string.Empty))
            {
                DataTable dt = GetTasks(barArray[3].ToString(), CommonValue.userInfo.storage, barArray[6].ToString(), "1");
                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.RowFilter = "Define1=1";
                    dt = dt.DefaultView.ToTable();
                }
                //如果有临时打印的数据就打印标签
                if (dt.Rows.Count > 0)
                {
                    DataTable dt2 = u8dt;//克隆库存数据过滤
                    dt2.DefaultView.RowFilter = string.Format("cinvcode='{0}'", barArray[3].ToString());
                    dt2 = dt2.DefaultView.ToTable();
                    if (dt2 != null || dt2.Rows.Count != 0)
                    {
                        U8.cinvname = dt2.Rows[0]["cinvname"].ToString();
                        U8.define3 = dt2.Rows[0]["define3"].ToString();
                        U8.desc = dt2.Rows[0]["desc"].ToString();
                        U8.lr = dt2.Rows[0]["lr"].ToString();
                        U8.th = dt2.Rows[0]["th"].ToString();
                        PrintLabel(dt);
                    }
                }
                else
                {
                    return "无该条码临时打印的数据";
                }
            }
            txtBarcode.Text = string.Empty;
            txtBarcode.Focus();
            return string.Empty;
        }
        /// <summary>
        /// 打印箱贴
        /// </summary> 
        /// <param name="dtSource"></param>
        private void PrintLabel(DataTable dtSource)
        {
            StiPrint pr;
            string path;
            //打印信息
            DataTable printdt = new DataTable();
            printdt.Columns.Add("UserId", typeof(string));
            printdt.Columns.Add("TaskId", typeof(string));
            printdt.Columns.Add("cInvCode", typeof(string));
            printdt.Columns.Add("cInvName", typeof(string));
            printdt.Columns.Add("Qty", typeof(int));
            printdt.Columns.Add("Address", typeof(string));
            printdt.Columns.Add("cBatch", typeof(string));
            printdt.Columns.Add("WorkCode", typeof(string));
            printdt.Columns.Add("Desc", typeof(string));
            printdt.Columns.Add("lr", typeof(string));
            printdt.Columns.Add("th", typeof(string));

            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                DataRow printDr = printdt.NewRow();
                printDr["UserId"] = string.Empty; //dtSource.Rows[i]["OperationUser"];
                printDr["TaskId"] = dtSource.Rows[i]["TaskId"];
                printDr["cInvCode"] = dtSource.Rows[i]["CinvCode"];
                printDr["cInvName"] = U8.cinvname;
                printDr["Qty"] = dtSource.Rows[i]["count"];
                printDr["Address"] = U8.define3;
                printDr["cBatch"] = dtSource.Rows[i]["cbatch"];
                printDr["WorkCode"] = CommonValue.userInfo.encode;
                printDr["Desc"] = U8.desc;
                printDr["lr"] = U8.lr;
                printDr["th"] = U8.th;
                printdt.Rows.Add(printDr);
            }
            //拼箱
            if (printdt.Rows.Count > 1)
            {
                printdt.DefaultView.Sort = "cBatch";
                printdt = printdt.DefaultView.ToTable();
                //拼箱
                path = Application.StartupPath + @"\PrintTemp\LCLPrint.mrt";
                pr = new StiPrint(path);

                DataTable dtPrintSource = printdt.Clone();

                dtPrintSource.ImportRow(printdt.Rows[0]);

                for (int i = 0; i < printdt.Rows.Count; i++)
                {
                    var currentdr = dtPrintSource.Rows[0];

                    string barcodefiled = "BarCode" + (i == 0 ? string.Empty : i.ToString());
                    string batchfiled = "cBatch" + (i == 0 ? string.Empty : i.ToString());
                    string qtyfiled = "Qty" + (i == 0 ? string.Empty : i.ToString());

                    dtPrintSource.Columns.Add(barcodefiled, typeof(string));
                    if (i > 0)
                    {
                        dtPrintSource.Columns.Add(batchfiled, typeof(string));
                        dtPrintSource.Columns.Add(qtyfiled, typeof(string));
                    }

                    currentdr[batchfiled] = printdt.Rows[i]["cbatch"];
                    currentdr[qtyfiled] = printdt.Rows[i]["qty"];

                }

                printdt = dtPrintSource;
                CommonValue.GROUPID = "";
            }
            else
            {
                //整箱打印 或者是强制打印
                path = Application.StartupPath + @"\PrintTemp\FULLPrint.mrt";
                pr = new StiPrint(path);
                CommonValue.GROUPID = "";
            }
            pr.Print("DataSource", printdt, 1, U8.lr.Equals("L") ? CommonValue.LeftPrinter : CommonValue.RightPrinter);
            txtBarcode.Text = string.Empty;
            txtBarcode.Focus();
            
        }
        #endregion
    }
}
