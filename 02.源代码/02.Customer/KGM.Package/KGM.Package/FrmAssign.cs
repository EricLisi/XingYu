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
    public partial class FrmAssign : Form
    {
        public class Taskflag
        {
            public string groupid;
            public int boxflag;
        }


        public FrmAssign()
        {
            InitializeComponent();
        }
        #region 私有
        public string cInvCode = "";
        #endregion

        #region 事件
        private void FrmAssign_Load(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.AutoGenerateColumns = false;//不自动  
                DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>("api/Task/" + cInvCode);
                dt.Columns.Add("muti", typeof(string));
                List<Taskflag> list = new List<Taskflag>();
                int boxflag = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrEmpty(dr["groupid"].ToString()))
                    {
                        dr["muti"] = string.Empty;
                    }
                    else
                    {
                        var entity = list.Find(it => it.groupid.Equals(dr["groupid"].ToString()));
                        if (entity == null)
                        {
                            list.Add(new Taskflag
                            {
                                groupid = dr["groupid"].ToString(),
                                boxflag = boxflag
                            });
                            dr["muti"] = boxflag;
                            boxflag++;
                        }
                        else
                        {
                            dr["muti"] = entity.boxflag;
                        }
                    }
                }


                this.dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show("画面初始化失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //全选
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    int count = dataGridView1.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                        Boolean flag = Convert.ToBoolean(checkCell.Value);
                        if (flag == false)
                        {
                            checkCell.Value = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    int count = dataGridView1.Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                        Boolean flag = Convert.ToBoolean(checkCell.Value);
                        if (flag == true)
                        {
                            checkCell.Value = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int CIndex = e.ColumnIndex;
                if (dataGridView1.SelectedRows.Count > 0)
                { 
                    var boxflag = dataGridView1.SelectedRows[0].Cells[2].Value;
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.SelectedRows[0].Cells[0];
                    bool chkValue = !Convert.ToBoolean(checkCell.Value);
                    if (boxflag.Equals(string.Empty))
                    {

                        checkCell.Value = chkValue;
                    }
                    else
                    {
                        for(int i = 0;i< dataGridView1.RowCount; i++)
                        {
                            if(dataGridView1.Rows[i].Cells[2].Value.Equals(boxflag))
                            {
                                DataGridViewCheckBoxCell checkCell1 = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                                checkCell1.Value = chkValue;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 分配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (dataGridView1.DataSource as DataTable);
                if (dt == null)
                {
                    MessageBox.Show("没有数据，请先生成任务！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int count = 0;
                DataTable dt2 = dt.Clone();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True") //checkbox的是否勾选
                    {
                        count++;
                        DataRow row = dt.Rows[i];
                        dt2.ImportRow(row);
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("请至少选择一条数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    FrmFreeze freeze = new FrmFreeze();
                    freeze.type = "用户";
                    freeze.DtAssign = dt2;
                    if (freeze.ShowDialog() == DialogResult.OK)
                    {
                        FrmAssign_Load(this, new EventArgs());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


    }
}
