using KGM.Package.Models;
using KGM.Package.Utils;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace KGM.Package
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
           
            InitializeComponent();
        }

        public void login()
        {
            CommonValue.WebAPIUri = new Uri(string.Format("http://{0}/", ConfigurationManager.AppSettings["api"]));
            Application.DoEvents();

            LoginModel model = new LoginModel();
            model.Account = txtUser.Text;
            model.Password = txtPwd.Text;
            LoginApiResultEntity result = WebAPIUtil.PostAPIByJsonToGeneric<LoginApiResultEntity>("api/Login/LoginSystem", WebAPIUtil.ConvertObjToJson(model));
            if (!result.status)
            {
                MessageBox.Show(result.message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
                txtPwd.Text = string.Empty;
                return;
            }
            CommonValue.token = result.message;
            CommonValue.userInfo = result.user;

            if (CommonValue.userInfo == null)
            {
                MessageBox.Show("未能读取到用户信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
                return;
            }
            if (string.IsNullOrEmpty(CommonValue.userInfo.storage))
            {
                MessageBox.Show("用户没有绑定操作仓库,请维护用户信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
                return;
            }
            //工位为空可不输入工位或输入任意档案内工位
            if (string.IsNullOrEmpty(CommonValue.userInfo.workstation))
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    CommonValue.userInfo.workstation = "-1";
                }
                else
                {
                    
                    DataTable dt = WebAPIUtil.GetAPIByJsonToGeneric<DataTable>(
                    string.Format("api/PositionFiles/GeByConditionAsync?cWhCode={0}&Position={1}",
                    CommonValue.userInfo.storage,
                    textBox1.Text));
                    if (dt.Rows.Count > 0)
                    {
                        CommonValue.userInfo.workstation = textBox1.Text.ToString();
                    }
                    else
                    {
                        MessageBox.Show("请输入空工位，或任意档案内工位!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = string.Empty;
                        textBox1.Focus();
                        return;
                    }
                }
            }
            else//有工位的用户需输入准确工位
            {
                if (CommonValue.userInfo.workstation != textBox1.Text)
                {
                    MessageBox.Show("请输入准确工位!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = string.Empty;
                    textBox1.Focus();
                    return;
                }
            }

            ////初始化api
            //CommonValue.WebAPIUri = new Uri(string.Format("http://{0}/", ConfigurationManager.AppSettings["api"]));
            CommonValue.login.Hide();
            CommonValue.main = new FrmMain();
            CommonValue.main.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                login();
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败!原因:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw ex;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            button1_Click(null, null);

        }
    }
}
