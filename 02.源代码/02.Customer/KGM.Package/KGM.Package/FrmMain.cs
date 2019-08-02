using KGM.Package.Models;
using KGM.Package.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace KGM.Package
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 窗体事件
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            CommonValue.login.Close();
        }

        private void FrmMain_Load(object sender, System.EventArgs e)
        {
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;//不自动   
                this.lblPosition.Text = CommonValue.userInfo.workstation;
                //非管理员 隐藏分配列
                this.Column1.Visible = CommonValue.userInfo.isadmin;

                this.colPrint.Visible = !CommonValue.userInfo.workstation.Equals("-1");


                //读取用户的仓库信息
                var warehouse = WebAPIUtil.GetAPIByJsonToGeneric<List<U8Warehouse>>(string.Format("api/U8/GetWarehouse?code={0}", CommonValue.userInfo.storage));
                lblWh.Text = warehouse[0].cwhname;
                lblUserName.Text = CommonValue.userInfo.encode;
                BingData();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("画面初始化失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 选择行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                BingData();
                DataTable dt = dataGridView1.DataSource as DataTable;
                if (dt == null)
                {
                    return;
                }
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                this.dataGridView2.AutoGenerateColumns = false;//不自动  
                DataGridViewRow gridrow = dataGridView1.SelectedRows[0];
                DataRowView row_view = (DataRowView)gridrow.DataBoundItem;
                DataTable rowData = row_view.DataView.Table.Clone();//克隆DataTable结构
                rowData.ImportRow(row_view.Row);//复制目标DataRow数据到新建的DataTable中

                //获取详细批次
                DataTable dtS = dataGridView1.Tag as DataTable;
                DataView dv = new DataView(dtS);
                dv.RowFilter = string.Format("CINVCODE = '{0}'", rowData.Rows[0]["cinvcode"].ToString());
                dataGridView2.DataSource = dv.ToTable();


                //DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>("api/U8/QueryInventorysByCode/" + rowData.Rows[0]["cinvcode"].ToString() + "/" + rowData.Rows[0]["cwhcode"].ToString());
                //this.dataGridView2.DataSource = dt;

                int CIndex = e.ColumnIndex;
                //扫描
                if (CIndex == 0)
                {
                    if (rowData.Rows[0]["status"].ToString() == "3")
                    {
                        MessageBox.Show("当前数据已被冻结，不允许操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                 
                    //点击打印按钮
                    FrmScan o = new FrmScan();
                    o.U8.cinvcode = rowData.Rows[0]["cinvcode"].ToString();
                    o.U8.cinvname = rowData.Rows[0]["cinvname"].ToString();
                    o.U8.cwhcode = rowData.Rows[0]["cwhcode"].ToString();
                    o.U8.cwhname = rowData.Rows[0]["cwhname"].ToString();
                    o.U8.boxcount = rowData.Rows[0]["boxcount"].ToString();
                    o.U8.iquantity = rowData.Rows[0]["iquantity"].ToString();
                    o.U8.constantvolume = rowData.Rows[0]["constantvolume"].ToString();
                    o.U8.margin = rowData.Rows[0]["margin"].ToString();
                    o.U8.boxed = rowData.Rows[0]["boxed"].ToString();
                    o.U8.th = rowData.Rows[0]["th"].ToString();
                    o.U8.lr = rowData.Rows[0]["lr"].ToString().ToUpper();
                    o.U8.cinvstd = rowData.Rows[0]["cinvstd"].ToString().ToUpper();
                    o.U8.define3 = rowData.Rows[0]["define3"].ToString().ToUpper();
                    o.U8.desc = rowData.Rows[0]["desc"].ToString().ToUpper();
                    o.BatchDt = dataGridView2.DataSource as DataTable;
                    //o.U8.define3= rowData.Rows[0]["F_Define3"].ToString();
                    //o.U8.desc = rowData.Rows[0]["F_Desc"].ToString();
                    o.TaskDt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>("api/Task/GetDeleteMark?UserId=" + CommonValue.userInfo.id + "&WorkStations=" + lblPosition.Text + "&cInvCode=" + rowData.Rows[0]["cinvcode"].ToString());
                    o.ShowDialog();
                    //if (o.ShowDialog() == DialogResult.OK)
                    //{
                    //    BingData();
                    //}
                }

                //任务分配
                if (CommonValue.userInfo.isadmin && CIndex == 1)
                {
                    if (rowData.Rows[0]["status"].ToString() == "3")
                    {
                        MessageBox.Show("当前数据已被冻结，不允许操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    FrmAssign frm = new FrmAssign();
                    frm.cInvCode = rowData.Rows[0]["cinvcode"].ToString();
                    frm.Show();
                }

                //冻结
                if (CommonValue.userInfo.isadmin && CIndex == 2)
                {

                    if (rowData.Rows[0]["ccheckstate"].ToString() == "操作中")
                    {
                        MessageBox.Show("当前状态为正在进行中，不允许操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (rowData.Rows[0]["ccheckstate"].ToString() == "已完成")
                    {
                        MessageBox.Show("当前状态为已完成，不允许操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else if (rowData.Rows[0]["status"].ToString() == "3")
                    {
                        if (MessageBox.Show("当前状态为已冻结，是否解冻? ", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                        {
                            return;
                        }
                    }
                    FrmFreeze freeze = new FrmFreeze();
                    freeze.cInCode = rowData.Rows[0]["cinvcode"].ToString();
                    freeze.type = "密码";
                    if (freeze.ShowDialog() == DialogResult.OK)
                    {
                        BingData();
                        //dataGridView1.Rows[0].Cells[2]

                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnreLoad_Click(object sender, System.EventArgs e)
        {
            try
            {
                BingData();
            }
            catch
            {

            }
        }

        private void btn_ReLoad_Click(object sender, System.EventArgs e)
        {
            try
            {
                BingData();
            }
            catch
            {

            }
        }

        private void btn_closed_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void btnClose_Click(object sender, System.EventArgs e)
        {
            CommonValue.main.Hide();
            CommonValue.login.Show();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BingData()
        {
            DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>("api/U8/QueryInventorysByWhCode/" + CommonValue.userInfo.storage + "/1/" + CommonValue.userInfo.workstation);
            if (dt == null || dt.Rows.Count == 0)
            {
                //MessageBox.Show("画面初始化失败!原因:没有获取到数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!dt.Columns.Contains("status"))
            {
                dt.Columns.Add("status", typeof(string));
            }

            DataView dv = new DataView(dt);
            DataTable dtSource = dv.ToTable(true, new string[] { "status", "cinvcode", "cinvname", "cinvstd", "ConstantVolume", "cwhcode", "boxed", "ccheckstate", "cwhname", "th", "lr","define3" , "desc" });
            dtSource.Columns.Add("iquantity", typeof(int));//任务数
            dtSource.Columns.Add("boxcount", typeof(int));//箱数
            dtSource.Columns.Add("margin", typeof(int)); //余数 
            dtSource.Columns.Add("workstation", typeof(int));//工位
            dtSource.Columns.Add("freeze", typeof(string));//冻结
            foreach (DataRow dr in dtSource.Rows)
            {
                //取出任务数据
                DataTable taskDt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(
                  string.Format("api/Task/GeByConditionAsync?WorkStations={0}&cInvCode={1}&cWhCode={2}&cBatch={3}&GroupId={4}&Status={5}",
                  CommonValue.userInfo.workstation, dr["cinvcode"].ToString(),
                  dr["cwhcode"].ToString(),
                   string.Empty,
                  string.Empty,
                  "0"));
                int iquantity = Convert.ToInt32(dt.Compute("SUM(iquantity)", string.Format("CINVCODE='{0}'", dr["CINVCODE"].ToString())));
                string margin = iquantity.ToString();
                if (taskDt.Rows.Count > 0 && !string.IsNullOrEmpty(taskDt.Rows[0]["groupid"].ToString()))
                {
                    int sumcount = Convert.ToInt32(margin) - Convert.ToInt32(taskDt.Compute("SUM(count)", "1=1"));
                    margin = (sumcount % Convert.ToInt32(dr["constantvolume"])).ToString();
                }
                else {
                    margin = (Convert.ToInt32(iquantity) % Convert.ToInt32(dr["ConstantVolume"])).ToString();
                }
                dr["iquantity"] = iquantity;
                dr["boxcount"] = iquantity / Convert.ToInt32(dr["ConstantVolume"]);
                dr["margin"] = margin;
                //dr["margin"] = iquantity % Convert.ToInt32(dr["ConstantVolume"]);
                dr["workstation"] = CommonValue.userInfo.workstation;
                DataTable dt2 = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>("api/PackageInventory/GetBycInvCodeAsync?cInvCode=" + dr["cinvcode"].ToString());
                if (dt2.Rows.Count != 0)
                {
                    if (dt2.Rows[0]["freezestatus"].ToString() == "1")
                    {
                        //dtSource.Rows.IndexOf(dr)
                        dr["status"] = 3;
                        dr["ccheckstate"] = "已冻结";
                        dtSource.Rows[dtSource.Rows.IndexOf(dr)]["freeze"] = "解冻";
                    }
                    else
                    {
                        dr["status"] = 0;
                        dtSource.Rows[dtSource.Rows.IndexOf(dr)]["freeze"] = "冻结";
                    }
                }

            }

            //过滤掉不满一箱且批次3天内的
            DataView dv1 = new DataView(dtSource);
            //dv1.RowFilter = " boxcount > 0 ";
            dv1.RowFilter = "iquantity>0";
            dv1.Sort = "status ASC,boxcount desc";
            dt.DefaultView.Sort = "cbatch ASC";
            dt = dt.DefaultView.ToTable();
            this.dataGridView1.DataSource = dv1.ToTable();
            this.dataGridView1.Tag = dt;
            foreach (DataGridViewRow view in dataGridView1.Rows)
            {
                this.dataGridView1.Rows[dataGridView1.Rows.IndexOf(view)].Cells[2].Value = dv1.ToTable().Rows[dataGridView1.Rows.IndexOf(view)]["freeze"].ToString();
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPrintSetting frm = new FrmPrintSetting();
            frm.ShowDialog();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否确认退出系统?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmPrint frm = new FrmPrint();
            frm.formMain = this;
            frm.ShowDialog();
        }
    }
}
