namespace XiaLM.FormTest.MicrosoftSpeech
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.butStartAsPath = new System.Windows.Forms.Button();
            this.tBoxWavPath = new System.Windows.Forms.TextBox();
            this.butCheckFile = new System.Windows.Forms.Button();
            this.rtBoxResult = new System.Windows.Forms.RichTextBox();
            this.comBoxLanguage = new System.Windows.Forms.ComboBox();
            this.butGenerateWav = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comBoxVoice = new System.Windows.Forms.ComboBox();
            this.tBoxRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tBoxPitch = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // butStartAsPath
            // 
            this.butStartAsPath.Location = new System.Drawing.Point(465, 10);
            this.butStartAsPath.Name = "butStartAsPath";
            this.butStartAsPath.Size = new System.Drawing.Size(85, 23);
            this.butStartAsPath.TabIndex = 0;
            this.butStartAsPath.Text = "wav文件识别";
            this.butStartAsPath.UseVisualStyleBackColor = true;
            this.butStartAsPath.Click += new System.EventHandler(this.butStartAsPath_Click);
            // 
            // tBoxWavPath
            // 
            this.tBoxWavPath.Location = new System.Drawing.Point(12, 12);
            this.tBoxWavPath.Name = "tBoxWavPath";
            this.tBoxWavPath.Size = new System.Drawing.Size(148, 21);
            this.tBoxWavPath.TabIndex = 1;
            // 
            // butCheckFile
            // 
            this.butCheckFile.Location = new System.Drawing.Point(166, 10);
            this.butCheckFile.Name = "butCheckFile";
            this.butCheckFile.Size = new System.Drawing.Size(67, 23);
            this.butCheckFile.TabIndex = 2;
            this.butCheckFile.Text = "选择文件";
            this.butCheckFile.UseVisualStyleBackColor = true;
            this.butCheckFile.Click += new System.EventHandler(this.butCheckFile_Click);
            // 
            // rtBoxResult
            // 
            this.rtBoxResult.Location = new System.Drawing.Point(13, 40);
            this.rtBoxResult.Name = "rtBoxResult";
            this.rtBoxResult.Size = new System.Drawing.Size(537, 182);
            this.rtBoxResult.TabIndex = 4;
            this.rtBoxResult.Text = "";
            // 
            // comBoxLanguage
            // 
            this.comBoxLanguage.FormattingEnabled = true;
            this.comBoxLanguage.Location = new System.Drawing.Point(239, 12);
            this.comBoxLanguage.Name = "comBoxLanguage";
            this.comBoxLanguage.Size = new System.Drawing.Size(220, 20);
            this.comBoxLanguage.TabIndex = 5;
            // 
            // butGenerateWav
            // 
            this.butGenerateWav.Location = new System.Drawing.Point(282, 228);
            this.butGenerateWav.Name = "butGenerateWav";
            this.butGenerateWav.Size = new System.Drawing.Size(268, 49);
            this.butGenerateWav.TabIndex = 6;
            this.butGenerateWav.Text = "合成音频播放";
            this.butGenerateWav.UseVisualStyleBackColor = true;
            this.butGenerateWav.Click += new System.EventHandler(this.butGenerateWav_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "发音人";
            // 
            // comBoxVoice
            // 
            this.comBoxVoice.FormattingEnabled = true;
            this.comBoxVoice.Location = new System.Drawing.Point(59, 230);
            this.comBoxVoice.Name = "comBoxVoice";
            this.comBoxVoice.Size = new System.Drawing.Size(217, 20);
            this.comBoxVoice.TabIndex = 8;
            // 
            // tBoxRate
            // 
            this.tBoxRate.Location = new System.Drawing.Point(59, 256);
            this.tBoxRate.Name = "tBoxRate";
            this.tBoxRate.Size = new System.Drawing.Size(72, 21);
            this.tBoxRate.TabIndex = 9;
            this.tBoxRate.Text = "medium";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "语速";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "音调";
            // 
            // tBoxPitch
            // 
            this.tBoxPitch.Location = new System.Drawing.Point(184, 256);
            this.tBoxPitch.Name = "tBoxPitch";
            this.tBoxPitch.Size = new System.Drawing.Size(92, 21);
            this.tBoxPitch.TabIndex = 12;
            this.tBoxPitch.Text = "medium";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 289);
            this.Controls.Add(this.tBoxPitch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tBoxRate);
            this.Controls.Add(this.comBoxVoice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butGenerateWav);
            this.Controls.Add(this.comBoxLanguage);
            this.Controls.Add(this.rtBoxResult);
            this.Controls.Add(this.butCheckFile);
            this.Controls.Add(this.tBoxWavPath);
            this.Controls.Add(this.butStartAsPath);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butStartAsPath;
        private System.Windows.Forms.TextBox tBoxWavPath;
        private System.Windows.Forms.Button butCheckFile;
        private System.Windows.Forms.RichTextBox rtBoxResult;
        private System.Windows.Forms.ComboBox comBoxLanguage;
        private System.Windows.Forms.Button butGenerateWav;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comBoxVoice;
        private System.Windows.Forms.TextBox tBoxRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tBoxPitch;
    }
}

