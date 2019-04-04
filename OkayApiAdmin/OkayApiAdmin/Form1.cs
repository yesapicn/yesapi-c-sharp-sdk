using PhalApiClientSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkayApiAdmin
{
    public partial class Form1 : Form
    {
        public String host;
        public String app_key;
        public String app_secrect;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_save_cfg_Click(object sender, EventArgs e)
        {
            String host = textBox_host.Text.ToString();
            String appKey = textBox_app_key.Text.ToString();
            String appSecrect = textBox_app_secrect.Text.ToString();

            if (host.Length == 0 || appKey.Length == 0 || appSecrect.Length == 0)
            {
                MessageBox.Show("配置不能为空，请补全", "提示", MessageBoxButtons.OK);
                return;
            }

            Dictionary<String, String> paramsDict = new Dictionary<String, String>();
            paramsDict.Add("name", "dogstar");

            PhalApiClientResponse response = OkayApiClient.instance().go("Hello.World", paramsDict);

            /**
            PhalApiClientResponse response = PhalApiClient.create()
               .withHost(host)
               .withService("Site.Index")
               .withParams("name", "dogstar")
               .withTimeout(3000)
               .request();
            */

            Console.WriteLine("response ret", response.ret + "");
            if (response.ret == 200)
            {
                OkayApiClient.instance().updateConfig(host, appKey, appSecrect);

                /**
                bool tmpWrite = false;
                IniFileHelper iniFileHelper = new IniFileHelper();
                tmpWrite = iniFileHelper.WriteIniString("Product", "host", host);
                tmpWrite = iniFileHelper.WriteIniString("Product", "app_key", appKey);
                tmpWrite = iniFileHelper.WriteIniString("Product", "app_secrect", appSecrect);
                */

                MessageBox.Show("配置检测通过！更新成功" , "小白套餐", MessageBoxButtons.OK);
            } else
            {
                MessageBox.Show("配置检测不正确，错误信息：" + response.msg, "小白套餐有误", MessageBoxButtons.OK);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            Process.Start("IExplore", "http://admin.okayapi.com/?vf=1.0");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;
            Process.Start("IExplore", "http://admin.okayapi.com/?r=App/Mine&vf=1.0");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox_host.Text = OkayApiClient.instance().host;
            textBox_app_key.Text = OkayApiClient.instance().app_key;
            textBox_app_secrect.Text = OkayApiClient.instance().app_secrect;

            /**
            // 读取配置
            IniFileHelper iniFileHelper = new IniFileHelper();
            StringBuilder sb = new StringBuilder(200);
            iniFileHelper.GetIniString("Product", "host", "", sb, sb.Capacity);
            host = sb.ToString();
            if (host.Length == 0)
            {
                host = "http://api.okayapi.com/";
            }
            textBox_host.Text = host;

            iniFileHelper.GetIniString("Product", "app_key", "", sb, sb.Capacity);
            app_key = sb.ToString();
            textBox_app_key.Text = app_key;

            iniFileHelper.GetIniString("Product", "app_secrect", "", sb, sb.Capacity);
            app_secrect = sb.ToString();
            textBox_app_secrect.Text = app_secrect;
    */
        }

        private void button_multi_import_Click(object sender, EventArgs e)
        {
            MultiImportForm form = new MultiImportForm();
            form.Show();
        }

        /// <summary>
        /// 对话框形式选择Excel文件路径
        /// </summary>
        /// <returns>返回所选择的Excel文件路径</returns>
        private string ChooseExcelFilePath()
        {
            OpenFileDialog opd = new OpenFileDialog();
            opd.Filter = @"CSV文件 (*.csv)|*.csv";
            opd.FilterIndex = 1;
            opd.RestoreDirectory = true;
            if (opd.ShowDialog() == DialogResult.OK)
            {
                return opd.FileName;
            }
            else
            {
                return "";
            }
        }

        public static DataTable ReadExcel(string filePath)
        {
            try
            {
                string strConn;
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
                strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", filePath);
                OleDbConnection OleConn = new OleDbConnection(strConn);
                OleConn.Open();
                String sql = "SELECT * FROM  [Sheet1$]";//可是更改Sheet名称，比如sheet2，等等

                OleDbDataAdapter OleDaExcel = new OleDbDataAdapter(sql, OleConn);
                DataSet OleDsExcle = new DataSet();
                OleDaExcel.Fill(OleDsExcle, "Sheet1");
                OleConn.Close();

                return OleDsExcle.Tables["Sheet1"];
            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
    }
}
