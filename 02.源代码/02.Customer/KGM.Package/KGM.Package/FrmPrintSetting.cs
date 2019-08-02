using KGM.Package.Utils;
using System;
using System.Windows.Forms;

namespace KGM.Package
{
    public partial class FrmPrintSetting : Form
    {
        public FrmPrintSetting()
        {
            InitializeComponent();
        }

        private void FrmPrintSetting_Load(object sender, EventArgs e)
        {
            try
            {
                AppUtil.BindPrinter(ref cmbLeftPrinter, CommonValue.LeftPrinter);
                AppUtil.BindPrinter(ref cmbRightPrinter, CommonValue.RightPrinter);
            }
            catch (Exception ex)
            {
                MessageBox.Show("画面初始化失败！原因:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AppUtil.SetConfigValue("lprinter", cmbLeftPrinter.Text);
                AppUtil.SetConfigValue("rprinter", cmbRightPrinter.Text);

                CommonValue.LeftPrinter = cmbLeftPrinter.Text;
                CommonValue.RightPrinter = cmbRightPrinter.Text;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败！原因:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
