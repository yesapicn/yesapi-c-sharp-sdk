namespace OkayApiAdmin
{
    partial class MultiImportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiImportForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label_csv = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_model = new System.Windows.Forms.TextBox();
            this.progressBar_import = new System.Windows.Forms.ProgressBar();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_import_now = new System.Windows.Forms.Button();
            this.richTextBox_detail = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "模型名称";
            // 
            // label_csv
            // 
            this.label_csv.AutoSize = true;
            this.label_csv.Location = new System.Drawing.Point(111, 72);
            this.label_csv.Name = "label_csv";
            this.label_csv.Size = new System.Drawing.Size(473, 12);
            this.label_csv.TabIndex = 1;
            this.label_csv.Text = "请选择将要批量导入的CSV文件（Excel文件太难搞，可以把Excel另存为CSV文件再导入）";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "导入数据";
            // 
            // textBox_model
            // 
            this.textBox_model.Location = new System.Drawing.Point(113, 28);
            this.textBox_model.Name = "textBox_model";
            this.textBox_model.Size = new System.Drawing.Size(518, 21);
            this.textBox_model.TabIndex = 3;
            // 
            // progressBar_import
            // 
            this.progressBar_import.Location = new System.Drawing.Point(113, 117);
            this.progressBar_import.Name = "progressBar_import";
            this.progressBar_import.Size = new System.Drawing.Size(629, 23);
            this.progressBar_import.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(653, 31);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(89, 12);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "我有哪些模型？";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "导入进度";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(606, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "选择CSV文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_import_now
            // 
            this.button_import_now.Location = new System.Drawing.Point(298, 161);
            this.button_import_now.Name = "button_import_now";
            this.button_import_now.Size = new System.Drawing.Size(165, 39);
            this.button_import_now.TabIndex = 8;
            this.button_import_now.Text = "开始导入！";
            this.button_import_now.UseVisualStyleBackColor = true;
            this.button_import_now.Click += new System.EventHandler(this.button_import_now_Click);
            // 
            // richTextBox_detail
            // 
            this.richTextBox_detail.Location = new System.Drawing.Point(49, 243);
            this.richTextBox_detail.Name = "richTextBox_detail";
            this.richTextBox_detail.Size = new System.Drawing.Size(693, 173);
            this.richTextBox_detail.TabIndex = 10;
            this.richTextBox_detail.Text = "";
            // 
            // MultiImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox_detail);
            this.Controls.Add(this.button_import_now);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.progressBar_import);
            this.Controls.Add(this.textBox_model);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_csv);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiImportForm";
            this.Text = "批量导入数据 - 小白管理系统";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_csv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_model;
        private System.Windows.Forms.ProgressBar progressBar_import;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_import_now;
        private System.Windows.Forms.RichTextBox richTextBox_detail;
    }
}