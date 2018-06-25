namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnConTest = new System.Windows.Forms.Button();
            this.txtThreadCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnTest2 = new System.Windows.Forms.Button();
            this.btnTest3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(23, 24);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(23, 116);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(663, 366);
            this.txtMsg.TabIndex = 1;
            // 
            // btnConTest
            // 
            this.btnConTest.Location = new System.Drawing.Point(184, 53);
            this.btnConTest.Name = "btnConTest";
            this.btnConTest.Size = new System.Drawing.Size(75, 23);
            this.btnConTest.TabIndex = 2;
            this.btnConTest.Text = "并发测试";
            this.btnConTest.UseVisualStyleBackColor = true;
            this.btnConTest.Click += new System.EventHandler(this.btnConTest_Click);
            // 
            // txtThreadCount
            // 
            this.txtThreadCount.Location = new System.Drawing.Point(68, 53);
            this.txtThreadCount.Name = "txtThreadCount";
            this.txtThreadCount.Size = new System.Drawing.Size(100, 21);
            this.txtThreadCount.TabIndex = 3;
            this.txtThreadCount.Text = "50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "线程数";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(265, 53);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(346, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(21, 91);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(41, 12);
            this.lblMsg.TabIndex = 7;
            this.lblMsg.Text = "label2";
            // 
            // btnTest2
            // 
            this.btnTest2.Location = new System.Drawing.Point(594, 24);
            this.btnTest2.Name = "btnTest2";
            this.btnTest2.Size = new System.Drawing.Size(75, 23);
            this.btnTest2.TabIndex = 8;
            this.btnTest2.Text = "测试 2";
            this.btnTest2.UseVisualStyleBackColor = true;
            this.btnTest2.Click += new System.EventHandler(this.btnTest2_Click);
            // 
            // btnTest3
            // 
            this.btnTest3.Location = new System.Drawing.Point(501, 24);
            this.btnTest3.Name = "btnTest3";
            this.btnTest3.Size = new System.Drawing.Size(75, 23);
            this.btnTest3.TabIndex = 9;
            this.btnTest3.Text = "测试 3";
            this.btnTest3.UseVisualStyleBackColor = true;
            this.btnTest3.Click += new System.EventHandler(this.btnTest3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 505);
            this.Controls.Add(this.btnTest3);
            this.Controls.Add(this.btnTest2);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThreadCount);
            this.Controls.Add(this.btnConTest);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnConTest;
        private System.Windows.Forms.TextBox txtThreadCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnTest2;
        private System.Windows.Forms.Button btnTest3;
    }
}

