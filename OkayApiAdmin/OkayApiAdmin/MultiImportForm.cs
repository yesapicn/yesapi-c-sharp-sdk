using PhalApiClientSDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkayApiAdmin
{
    public partial class MultiImportForm : Form
    {
        protected String csvPath;

        public MultiImportForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            Process.Start("IExplore", "http://admin.okayapi.com/?r=Data/MyModelsManager&vf=1.0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((csvPath = ChooseExcelFilePath()) != "")
            {
                label_csv.Text = csvPath;
            }
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

        private void button_import_now_Click(object sender, EventArgs e)
        {
            String model = textBox_model.Text.ToString();
            if (model.Length == 0)
            {
                MessageBox.Show("请输入你的模型名称！", "模型未填", MessageBoxButtons.OK);
                return;
            }
            if (csvPath == null || csvPath.Length == 0)
            {
                MessageBox.Show("请选择将要批量导入的CSV文件！", "CSV文件未选", MessageBoxButtons.OK);
                return;
            }

            //DataTable dt = ReadExcel(path);
            DataTable dt = CSVHelper.OpenCSV(csvPath);
            if (dt == null)
            {
                return;
            }

            // 先临时这样取配置
            // 读取配置
            IniFileHelper iniFileHelper = new IniFileHelper();
            StringBuilder sb = new StringBuilder(200);
            iniFileHelper.GetIniString("Product", "host", "", sb, sb.Capacity);
            String host = sb.ToString();
            if (host.Length == 0)
            {
                host = "http://api.okayapi.com/";
            }

            // 重置进度条
            progressBar_import.Maximum = dt.Rows.Count;//设置最大长度值
            progressBar_import.Value = 0;//设置当前值
            progressBar_import.Step = 1;//设置没次增长多少

            foreach (DataRow dr in dt.Rows)
            {   ///遍历所有的行

                Dictionary<String, String> data = new Dictionary<string, string>();

                SetrichTextBox("开始导入……\r\n");

                foreach (DataColumn dc in dt.Columns)
                {   //遍历所有的列
                    Console.WriteLine("{0},   {1},   {2}", dt.TableName, dc.ColumnName, dr[dc]);   //表名,列名,单元格数据
                    SetrichTextBox(String.Format("读取数据：{0}： {1}\r\n", dc.ColumnName, dr[dc]));

                    data.Add(dc.ColumnName, dr[dc].ToString());
                }

                String dataJson = JsonHelper.SerializeDictionaryToJsonString(data);

                // 开始上传数据
                // http://api.okayapi.com/docs.php?service=App.Table.Create&detail=1&type=fold
                Dictionary<String, String> paramsDict = new Dictionary<String, String>();
                paramsDict.Add("model_name", textBox_model.Text.ToString());
                paramsDict.Add("data", dataJson);

                PhalApiClientResponse response = OkayApiClient.instance().go("App.Table.Create", paramsDict);

                if (response.ret == 200)
                {
                    if (response.data.err_code == 0)
                    {
                        SetrichTextBox("导入成功……\r\n");
                    } else
                    {
                        System.Threading.Thread.Sleep(500);//暂停0.5秒
                        SetrichTextBox("导入失败：" + response.data.err_msg + " ……\r\n");
                    }
                }
                else
                {
                    String tmp = "导入失败，请修正后重新导入。错误信息：" + response.msg;
                    SetrichTextBox(tmp + "\r\n");
                    MessageBox.Show(tmp, "导入有失败", MessageBoxButtons.OK);
                    break;
                }

                progressBar_import.Value += progressBar_import.Step;

                SetrichTextBox(">>>>>>>>>>>>>>>>>>>\r\n");
            }
        }

        private void SetrichTextBox(string value)
        {

            if (richTextBox_detail.InvokeRequired)//其它线程调用
            {
                //delInfoList d = new delInfoList(SetrichTextBox);
                //richTextBox_detail.Invoke(d, value);
            }
            else//本线程调用
            {
                if (richTextBox_detail.Lines.Length > 100)
                {
                    richTextBox_detail.Clear();
                }

                richTextBox_detail.Focus(); //让文本框获取焦点 
                richTextBox_detail.Select(richTextBox_detail.TextLength, 0);//设置光标的位置到文本尾
                richTextBox_detail.ScrollToCaret();//滚动到控件光标处 
                richTextBox_detail.AppendText(value);//添加内容
            }
        }
    }
}
