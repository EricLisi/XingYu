using KGM.Package.Models;
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
    public partial class FrmFreeze : Form
    {
        public FrmFreeze()
        {
            InitializeComponent();
        }

        #region 私有成员
        public string cInCode;
        public string type;
        public DataTable DtAssign = new DataTable();
        #endregion

        #region 控件事件

        private void FrmFreeze_Load(object sender, EventArgs e)
        {
            if (type == "密码")//输入密码
            {
                txtPwd.Show();
                comboBox1.Hide();
                lblTips.Text = "请输入密码";
            }
            else if (type == "用户")//选择员工
            {
                comboBox1.Show();
                txtPwd.Hide();
                lblTips.Text = "请选择员工";
                //绑定员工
                var data = WebAPIUtil.GetAPIByJsonToGeneric<List<SelectValues>>(string.Format("api/UserProfiles/GetSelectJson/{0}/{1}",
                    CommonValue.userInfo.storage, CommonValue.userInfo.workstation));
                comboBox1.DataSource = data;
                comboBox1.DisplayMember = "text";
                comboBox1.ValueMember = "id";
            }
        }

        private void btnTrue_Click(object sender, EventArgs e)
        {
            try
            {
                if (type == "密码")
                {
                    if (txtPwd.Text == "51029010")
                    {
                        //冻结
                        KgmApiResultEntity res = WebAPIUtil.PutAPIByJsonToGeneric<KgmApiResultEntity>("api/PackageInventory/UpdateStatus/" + cInCode, WebAPIUtil.ConvertObjToJson(""));
                        if (res.status)
                        {
                            MessageBox.Show(res.message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show(res.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (type == "用户")//选择员工
                {
                    string idArray = "";
                    for (int i = 0; i < DtAssign.Rows.Count; i++)
                    {
                        idArray += DtAssign.Rows[i]["id"].ToString() + ",";
                    }
                    UpdateAssign assign = new UpdateAssign();
                    assign.userid = comboBox1.SelectedValue.ToString();
                    assign.tasks = idArray;

                    KgmApiResultEntity res = WebAPIUtil.PutAPIByJsonToGeneric<KgmApiResultEntity>("api/Task/AllocationTask", WebAPIUtil.ConvertObjToJson(assign));
                    if (res.status)
                    {
                        MessageBox.Show(res.message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(res.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
