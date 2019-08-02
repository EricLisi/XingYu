using System;
using System.Data;
using System.Windows.Forms;
using KGM.Package.Models;
using KGM.Package.Print;
using KGM.Package.Utils;

namespace KGM.Package
{
    public partial class FrmScan : Form
    {
        public FrmScan()
        {
            InitializeComponent();
        }
        #region 私有成员
        public U8WarehouseModel U8 = new U8WarehouseModel();

        //详细批次信息
        public DataTable BatchDt = new DataTable();

        //task任务 deleteMark <> 1
        public DataTable TaskDt = new DataTable();
        //task打印
        public DataTable TaskPrint = new DataTable();

        //打印成功信息
        public DataTable dtPrinted = new DataTable();
        #endregion

        #region 控件事件
        private void FrmScan_Load(object sender, EventArgs e)
        {

            try
            {
                CommonValue.GROUPID = string.Empty;

                this.lblWh.Text = U8.cwhname;
                this.lblCinvName.Text = U8.cinvname;
                this.lblCinvCode.Text = U8.cinvcode;
                this.lblBoxNum.Text = U8.boxcount;
                this.lblCount.Text = U8.iquantity;
                this.lblDR.Text = U8.constantvolume;
                this.lblSQty.Text = U8.margin;
                this.lblYiBoxNum.Text = U8.boxed;
                this.lblCinvStd.Text = U8.cinvstd;
                this.lblWorkStation.Text = CommonValue.userInfo.workstation;
                this.txtBarcode.AutoSize = false;

                this.dataGridView1.AutoGenerateColumns = false;
                BindGrid();


            }
            catch (Exception ex)
            {
                MessageBox.Show("画面初始化失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult = DialogResult.OK;
            //this.Hide();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }

                string[] barArray = null;
                DataRow[] drStock = null;
                var validateMsg = BarcodeKeyDownValidate(txtBarcode.Text, out barArray, out drStock);

                if (!string.IsNullOrEmpty(validateMsg))
                {
                    MessageBox.Show(validateMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBarcode.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    txtBarcode.Focus();
                    return;
                }
                DataTable dt2 = this.dataGridView1.DataSource as DataTable;


                DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(
                string.Format("api/Task/GeByConditionAsync?WorkStations={0}&cInvCode={1}&cWhCode={2}&cBatch={3}&GroupId={4}&Status={5}",
                CommonValue.userInfo.workstation, barArray[3].ToString(),
                CommonValue.userInfo.storage,
                barArray[6].ToString(),
                CommonValue.GROUPID.ToString(),
                "0"
                ));
                //读取当前有没有任务 

                if (dt == null || dt.Rows.Count == 0)
                {
                    //库存数或者箱规数取小
                    txtQty.Text = Convert.ToDecimal(drStock[0]["iquantity"]) < Convert.ToDecimal(lblDR.Text) ? Convert.ToDecimal(drStock[0]["iquantity"]).ToString() : lblDR.Text;
                }
                else
                {
                    dt.DefaultView.RowFilter = "status=0";
                    dt = dt.DefaultView.ToTable();
                    //取Task Count
                    txtQty.Text = dt.Rows[0]["Count"].ToString();
                }
                CommonValue.qty = txtQty.Text;
                txtQty.Focus();
                txtQty.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //数量
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter)
                {
                    return;
                }

                string[] barArray = null;
                DataRow[] drStock = null;
                bool Compel = false;//是否强制打印

                string validateMsg = QtyKeyDownValidate(out barArray, out drStock);
                if (!string.IsNullOrEmpty(validateMsg))
                {
                    if (validateMsg == "margin")
                    {
                        if (MessageBox.Show("是否强制打印? ", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                        {
                            txtQty.Text = string.Empty;
                            txtBarcode.Text = string.Empty;
                            txtBarcode.Focus(); 
                            return;
                        }
                        Compel = true;
                    }
                    else
                    {
                        MessageBox.Show(validateMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQty.SelectAll();
                        return;
                    }
                }
                //生成/更新 任务
                PrintApiModel model = new PrintApiModel();
                model.UserId = CommonValue.userInfo.id;
                model.cInvCode = lblCinvCode.Text;
                model.Qty = txtQty.Text.Trim();
                model.cBatch = barArray[6];
                model.GroupId = CommonValue.GROUPID;
                model.Compel = Compel;
                model.WorkStations= CommonValue.userInfo.workstation;
                TaskResultEntity taskEntity = WebAPIUtil.PostAPIByJsonToGeneric<TaskResultEntity>("api/PackageInventory/GetTaskLable", WebAPIUtil.ConvertObjToJson(model));
                CommonValue.GROUPID = taskEntity.groupid;

                //写入PrintLog 
                PrintLogEntity printLog = new PrintLogEntity();
                printLog.User = CommonValue.userInfo.id;
                printLog.cWhCode = CommonValue.userInfo.storage;
                printLog.TaskId = taskEntity.taskid;
                printLog.TaskGroupId = taskEntity.groupid;
                printLog.cInvCode = model.cInvCode;
                printLog.Qty = model.Qty;
                printLog.cBatch = model.cBatch;
                printLog.WorkCode = CommonValue.userInfo.workstation;
                printLog.BarCode = txtBarcode.Text;
                printLog.CreatorTime = DateTime.Now;
                printLog.Status = "0";
                TaskResultEntity printEntity = WebAPIUtil.PostAPIByJsonToGeneric<TaskResultEntity>("api/PrintLog/Insert", WebAPIUtil.ConvertObjToJson(printLog));

                //绑定列表
                BindGrid();
                //如果已经满箱 打印标签

                DataTable dt = dataGridView1.DataSource as DataTable;
                DataView dv = new DataView(dt);
                 dv.RowFilter = string.Format("TaskGroupId = '{0}'", CommonValue.GROUPID);
                int boxedqty = 0;
                if (dv.ToTable().Rows.Count > 0)
                {
                     boxedqty = Convert.ToInt32(dv.ToTable().Compute("SUM(qty)", "1=1"));
                }
                if (string.IsNullOrEmpty(printLog.TaskGroupId)
                    || Convert.ToInt32(lblDR.Text) == boxedqty)
                {
                    string groupid = dt.Rows[0]["TaskGroupId"].ToString();
                    if (string.IsNullOrEmpty(groupid))
                    {
                        DataTable dtSource = dt.Clone();
                        dtSource.ImportRow(dt.Rows[0]);
                        PrintLabel(dtSource);
                    }
                    else
                    {
                        dv = new DataView(dt);
                        dv.RowFilter = string.Format("TaskGroupId = '{0}'", groupid);
                        PrintLabel(dv.ToTable());
                    }
                }
                //减去已扫描数量
                DataRow[] arrRows = BatchDt.Select(" cbatch = '" + model.cBatch.ToString() + "'");
                foreach (DataRow row in arrRows)
                {
                    row["iquantity"] = Convert.ToInt32(row["iquantity"].ToString()) - Convert.ToInt32(txtQty.Text.ToString());
                }
                txtBarcode.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //正常打印
        private void printBarcode(object sender, EventArgs e, bool isCompel)
        {
            try
            {
                string path;
                StiPrint pr;
                //打印成功数据
                if (!dtPrinted.Columns.Contains("cinvcode"))
                {
                    //添加打印数据
                    dtPrinted.Columns.Add("cinvcode");
                    dtPrinted.Columns.Add("cbatch");
                    dtPrinted.Columns.Add("qty");
                    dtPrinted.Columns.Add("barcode");
                    dtPrinted.Columns.Add("taskid");
                    dtPrinted.Columns.Add("groupid");
                    dtPrinted.Columns.Add("creatortime");
                }

                string barcode = this.txtBarcode.Text.ToString();
                string[] barArray = barcode.Split('|');
                if (barArray.Length < 8)
                {
                    MessageBox.Show("解析失败，条码规则不符！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBarcode.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    txtBarcode.Focus();
                    return;
                }
                if (barArray[3] != lblCinvCode.Text || barArray[6] == "")
                {
                    MessageBox.Show("解析失败，条码规则不符！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBarcode.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    txtBarcode.Focus();
                    return;
                }
                //打印数据源
                PrintApiModel model = new PrintApiModel();
                model.UserId = CommonValue.userInfo.id;
                model.cInvCode = lblCinvCode.Text;
                model.Qty = txtQty.Text.Trim();
                model.cBatch = barArray[6];
                model.GroupId = CommonValue.GROUPID;
                model.Compel = isCompel;

                TaskResultEntity taskEntity = WebAPIUtil.PostAPIByJsonToGeneric<TaskResultEntity>("api/PackageInventory/GetTaskLable", WebAPIUtil.ConvertObjToJson(model));
                string taskId = "";
                string groupid = "";
                //打印履历
                PrintLogEntity printLog = new PrintLogEntity();
                printLog.User = CommonValue.userInfo.id;
                printLog.cWhCode = CommonValue.userInfo.storage;
                printLog.cInvCode = model.cInvCode;
                printLog.TaskId = taskId;
                printLog.Qty = model.Qty;
                printLog.Address = "地址待修改";
                printLog.cBatch = model.cBatch;
                printLog.WorkCode = CommonValue.userInfo.workstation;
                printLog.BarCode = barcode;
                printLog.CreatorTime = DateTime.Now;
                printLog.Status = "0";
                if (!taskEntity.state)
                {
                    if (taskEntity.Compel == 1)
                    {
                        taskId = taskEntity.taskid;
                        //减去已扫描数量
                        DataRow[] arrRows = BatchDt.Select(" cbatch = '" + barArray[6].ToString() + "'");
                        foreach (DataRow row in arrRows)
                        {
                            row["iquantity"] = Convert.ToInt32(row["iquantity"].ToString()) - Convert.ToInt32(txtQty.Text.ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show(taskEntity.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (taskEntity.state && taskEntity.data == null) //拼箱不用打印标签
                {
                    if (string.IsNullOrEmpty(taskEntity.taskid) && string.IsNullOrEmpty(taskEntity.groupid))
                    {
                        MessageBox.Show("条码已经扫描！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBarcode.Text = string.Empty;
                        txtQty.Text = string.Empty;
                        txtBarcode.Focus();
                        return;
                    }
                    DataRow dr = dtPrinted.NewRow();
                    dr["cinvcode"] = lblCinvCode.Text;
                    dr["cbatch"] = barArray[6];
                    dr["qty"] = txtQty.Text;
                    dr["barcode"] = txtBarcode.Text;
                    dr["taskid"] = taskEntity.taskid;
                    dr["groupid"] = taskEntity.groupid;
                    dr["creatortime"] = DateTime.Now.ToString();

                    dtPrinted.Rows.Add(dr);
                    taskId = taskEntity.taskid;
                    groupid = taskEntity.groupid;

                    CommonValue.GROUPID = groupid;
                    //减去已扫描数量
                    DataRow[] arrRows = BatchDt.Select(" cbatch = '" + barArray[6].ToString() + "'");
                    foreach (DataRow row in arrRows)
                    {
                        row["iquantity"] = Convert.ToInt32(row["iquantity"].ToString()) - Convert.ToInt32(txtQty.Text.ToString());
                    }
                    printLog.TaskGroupId = CommonValue.GROUPID;

                }
                else if (taskEntity.state && taskEntity.data.Count > 0 || isCompel)
                {
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

                    for (int i = 0; i < taskEntity.data.Count; i++)
                    {
                        DataRow printDr = printdt.NewRow();
                        printDr["UserId"] = taskEntity.data[i].UserId;
                        printDr["TaskId"] = taskEntity.data[i].TaskId;
                        printDr["cInvCode"] = taskEntity.data[i].cInvCode;
                        printDr["cInvName"] = taskEntity.data[i].cInvName;
                        printDr["Qty"] = taskEntity.data[i].Qty;
                        printDr["Address"] = taskEntity.data[i].Address;
                        printDr["cBatch"] = taskEntity.data[i].cBatch;
                        printDr["WorkCode"] = taskEntity.data[i].WorkCode;
                        printDr["Desc"] = taskEntity.data[i].Desc;
                        printDr["lr"] = U8.lr;
                        printDr["th"] = U8.th;
                        printdt.Rows.Add(printDr);
                    }
                    //添加已打印信息
                    DataRow dr = dtPrinted.NewRow();
                    dr["cinvcode"] = lblCinvCode.Text;
                    dr["cbatch"] = barArray[6];
                    //if (model.GroupId != "")
                    //{
                    //    dr["qty"] = taskEntity.data.Find(it=>it.gr);
                    //}
                    if (taskEntity.data.Count > 0)
                    {
                        if (taskEntity.data.Find(it => it.cBatch == dr["cbatch"].ToString()) != null)
                        {

                            model.Qty = taskEntity.data.Find(it => it.cBatch == dr["cbatch"].ToString()).Qty.ToString();
                        }

                    }
                    dr["qty"] = model.Qty;
                    dr["barcode"] = txtBarcode.Text;
                    dr["taskid"] = taskEntity.taskid;
                    dr["creatortime"] = DateTime.Now.ToString();
                    dr["groupid"] = taskEntity.groupid;
                    dtPrinted.Rows.Add(dr);

                    //减去已扫描数量
                    DataRow[] arrRows = BatchDt.Select(" cbatch = '" + barArray[6].ToString() + "'");
                    foreach (DataRow row in arrRows)
                    {
                        row["iquantity"] = Convert.ToInt32(row["iquantity"].ToString()) - Convert.ToInt32(dr["qty"]);
                        //row["iquantity"] = Convert.ToInt32(row["iquantity"].ToString()) - Convert.ToInt32(txtQty.Text.ToString());
                    }

                    taskId = taskEntity.taskid;
                    groupid = taskEntity.groupid;
                    CommonValue.GROUPID = groupid;

                    //拼箱
                    if (printdt.Rows.Count > 1)
                    {
                        printdt.DefaultView.Sort = "cBatch asc";
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

                    }
                    else
                    {
                        //整箱打印 或者是强制打印
                        path = Application.StartupPath + @"\PrintTemp\FULLPrint.mrt";
                        pr = new StiPrint(path);
                    }
                    pr.Print("DataSource", printdt, 1, U8.lr.Equals("L") ? CommonValue.LeftPrinter : CommonValue.RightPrinter);
                    printLog.TaskGroupId = CommonValue.GROUPID;
                    CommonValue.GROUPID = "";
                }
                TaskResultEntity printEntity = WebAPIUtil.PostAPIByJsonToGeneric<TaskResultEntity>("api/PrintLog/Insert", WebAPIUtil.ConvertObjToJson(printLog));
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtBarcode.Text = txtQty.Text = string.Empty;
                txtBarcode.Focus();
            }
        }
        /// <summary>
        /// 强制打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ////解析条码
                //string[] barArray = null;
                //DataRow[] drStock = null;
                //var validateMsg = BarcodeKeyDownValidate(txtBarcode.Text, out barArray, out drStock);
                //if (!string.IsNullOrEmpty(validateMsg))
                //{
                //    MessageBox.Show(validateMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtBarcode.Text = string.Empty;
                //    txtQty.Text = string.Empty;
                //    txtBarcode.Focus();
                //    return;
                //}

                var rows = dataGridView1.SelectedRows;
                if (rows == null || rows.Count == 0)
                {
                    MessageBox.Show("请至少选择一行记录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string groupid = rows[0].Cells["GroupId"].Value.ToString();
                string id = rows[0].Cells["id"].Value.ToString();
                DataTable dt = dataGridView1.DataSource as DataTable;
                if (string.IsNullOrEmpty(groupid))
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = string.Format("id = '{0}'", id);
                    PrintLabel(dv.ToTable());
                }
                else
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = string.Format("TaskGroupId = '{0}'", groupid);
                    PrintLabel(dv.ToTable());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //选中打印标签
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //string qty = dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString();//数量
            //string groupid = dataGridView1.Rows[e.RowIndex].Cells["GroupId"].Value.ToString();//分组id
            //string cbatch = dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value.ToString();//批次
            //string cinvcode = dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();//物料
            ////如果groupid!=""拿整个分组的任务
            //if (groupid != "")
            //{
            //    cbatch = "";
            //}
            ////获取任务
            //TaskPrint = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(string.Format("api/Task/GeByConditionAsync?User={0}&cInvCode={1}&cWhCode={2}&cBatch={3}&GroupId={4}", CommonValue.userInfo.id, cinvcode, CommonValue.userInfo.storage, cbatch, groupid));
        }

        private void FrmScan_Activated(object sender, EventArgs e)
        {
            txtBarcode.Focus();
            txtBarcode.SelectAll();
        }
        #endregion

        #region 私有方法
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

        /// <summary>
        /// 条码回车后的数据校验
        /// </summary>
        /// <param name="barcode">条码</param>
        /// <returns></returns>
        private string BarcodeKeyDownValidate(string barcode, out string[] barArray, out DataRow[] drStock)
        {
            //解析条码
            barArray = null;
            drStock = null;

            if (barcode.Equals(string.Empty))
            {
                return "条码为空,请先扫描条码！";
            }
            barArray = BarcodeAnalys(barcode);
            if (barArray == null)
            {
                return "解析失败，条码规则不符！";
            }
            if (barArray[3] != lblCinvCode.Text)
            {
                return "条码对应的存货编码不正确！";
            }
            //库存是否足够
            drStock = BatchDt.Select(" cbatch = '" + barArray[6].ToString() + "' and iquantity >  0");
            if (drStock.Length == 0)
            {
                return "当前条码没有库存！";
            }

            if (!barArray[6].ToString().Equals(string.Empty))
            {
                //是否最早批次
                DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(
                string.Format("api/Task/GeByConditionAsync?WorkStations={0}&cInvCode={1}&cWhCode={2}&cBatch={3}&GroupId={4}&Status={5}",
                CommonValue.userInfo.workstation, barArray[3].ToString(),
                CommonValue.userInfo.storage,
                string.Empty,
                string.Empty,
                "0"));
                string minbatch = "";
                if (dt.Rows.Count > 0)
                {
                    minbatch = dt.Compute("MIN(cbatch)", "1=1").ToString();
                }
                else
                {
                    minbatch = BatchDt.Compute("MIN(CBATCH)", "iquantity>0").ToString();
                }
                if (!barArray[6].ToString().Equals(minbatch))
                {
                    return string.Format("当前条码对应的批次为[{0}],库存内最早批次为[{1}]！", barArray[6].ToString(), minbatch);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 数量回车校验
        /// </summary>
        /// <returns></returns>
        private string QtyKeyDownValidate(out string[] barArray, out DataRow[] drStock)
        {
            //条码是否正确
            barArray = null;
            drStock = null;
            var validateMsg = BarcodeKeyDownValidate(txtBarcode.Text, out barArray, out drStock);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return validateMsg;
            }
            //数量是否为空
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                return "数量不允许为空";
            }
            //数量是否合法
            try
            {
                if (Convert.ToInt32(txtQty.Text) <= 0)
                {
                    return "输入的数量不合法,必须为正数且大于0";
                }
            }
            catch
            {
                return "输入的数量不合法,必须为整数类型";
            }
            //数量是否正确
            //如果是有任务的情况下 输入的数量应该是已经扫描的任务+当前输入的数量
            int qty = Convert.ToInt32(txtQty.Text);
            int boxedqty = 0;//已经装箱的数量
            if (!string.IsNullOrEmpty(CommonValue.GROUPID))
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                DataView dv = new DataView(dt);
                dv.RowFilter = string.Format("TaskGroupId = '{0}'", CommonValue.GROUPID);
                boxedqty = Convert.ToInt32(dv.ToTable().Compute("SUM(qty)", "1=1"));
            }
            if (qty + boxedqty > Convert.ToInt32(lblDR.Text))
            {
                return string.Format("输入的数量不合法,数量不允许超过定容!已装箱[{0}]当次装箱[{1}]定容[{2}]",
                    boxedqty.ToString(),
                    txtQty.Text,
                    lblDR.Text);
            }
            if (qty.ToString() !=CommonValue.qty)
            {
                //DataView dv = new DataView(BatchDt);
                //string margin = dv.ToTable().Compute("SUM(iquantity)", "1=1").ToString();
                //if (margin == U8.margin)//打印余量的话可以小于
                //{
                //    return "margin";
                //}
                //else
                //{
                    return string.Format("输入的数量不合法!",
                      boxedqty.ToString(),
                      txtQty.Text,
                      lblDR.Text);
                //}
            }
            //if (qty + boxedqty < Convert.ToInt32(lblDR.Text))
            //{

            //    return string.Format("输入的数量不合法,数量小于定容!当次装箱[{1}]定容[{2}]",
            //    boxedqty.ToString(),
            //    txtQty.Text,
            //    lblDR.Text);
            //}
            //不允许超过库存数
            if (qty > Convert.ToInt32(drStock[0]["iquantity"]))
            {
                return "输入的数量不合法,数量不允许超过库存数,当前库存为:" + Convert.ToInt32(drStock[0]["iquantity"]).ToString();
            }
            DataView dv2 = new DataView(BatchDt);
            string margin = dv2.ToTable().Compute("SUM(iquantity)", "1=1").ToString();
            margin =( Convert.ToInt32(margin)+boxedqty).ToString();
            if (margin == U8.margin)//打印余量的话可以小于
            {
                return "margin";
            } 
            return string.Empty;
        }

        private void BindGrid()
        {
            DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(string.Format("api/PrintLog/GeByConditionAsync?user={0}&cInvCode={1}&cWhCode={2}", CommonValue.userInfo.id, U8.cinvcode, U8.cwhcode));
            dt.Columns.Add("CinvName", typeof(string));
            dt.Columns.Add("CinvStd", typeof(string));
            foreach (DataRow dataRow in dt.Rows)
            {
                dataRow["CinvName"] = U8.cinvname.ToString();
                dataRow["CinvStd"] = U8.cinvstd.ToString();
            }
            if (dt.Rows.Count > 0)
            {
                DataTable ndt = new DataTable();
                ndt.Columns.AddRange(new DataColumn[] { new DataColumn("taskgroupid", typeof(string)),
                                        new DataColumn("count", typeof(int)),
                                        new DataColumn("nowCount", typeof(int)),
                                        new DataColumn("qty", typeof(int)) });
                DataTable dtResult = dt.Clone();
                DataTable dtName = dt.DefaultView.ToTable(true, "taskgroupid");
                for (int i = 0; i < dtName.Rows.Count; i++)
                {
                    DataRow[] rows = dt.Select("taskgroupid='" + dtName.Rows[i][0] + "'");
                    //temp用来存储筛选出来的数据
                    DataTable temp = dtResult.Clone();
                    int sumcount = 0;
                    foreach (DataRow row in rows)
                    {
                        sumcount +=Convert.ToInt32(row["qty"].ToString());
                        temp.Rows.Add(row.ItemArray);
                    }
                    DataRow dr = ndt.NewRow();
                    dr[0] = dtName.Rows[i][0].ToString();
                    dr[1] = temp.Compute("count(taskgroupid)", "");
                    dr[2] = 0;
                    dr[3] = sumcount;
                    ndt.Rows.Add(dr);
                }
                DataView ndtView = new DataView(ndt);
                ndtView.RowFilter = string.Format("count = '{0}' or qty<{1}", 1,Convert.ToInt32( U8.constantvolume));
                if (ndtView.ToTable().Rows.Count > 0)
                {
                    CommonValue.GROUPID = ndtView.ToTable().Rows[0]["taskgroupid"].ToString();
                }
            }
           
            this.dataGridView1.DataSource = dt;
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
                printDr["Qty"] = dtSource.Rows[i]["qty"];
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
                //if (printdt.Rows.Count == 3)
                //{
                //    path = Application.StartupPath + @"\PrintTemp\model3.mrt";
                //}
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

        }
        #endregion


    }
}
