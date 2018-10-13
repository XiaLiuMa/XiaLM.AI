using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaLM.FormTest.XfSpeechSDK.Audio;
using XiaLM.XfSpeech.SDK;
using XiaLM.XfSpeech.SDK.Model;

namespace XiaLM.FormTest.XfSpeechSDK
{
    public partial class MainForm : Form
    {
        private TTSSessionBegin_Param ttsSessionParam;  //语音合成参数
        private List<Speaker> speakers;  //语音合成参数
        public MainForm()
        {
            InitializeComponent();
            InitTTSConfig();
        }

        /// <summary>
        /// 初始化语音合成测试
        /// </summary>
        private void InitTTSConfig()
        {
            ttsSessionParam = new TTSSessionBegin_Param();
            speakers = new List<Speaker>    //初始化发音人列表
            {
                new Speaker(){ Name ="小燕",Language="普通话",Tone="青年女声",Vname="xiaoyan" },
                new Speaker(){ Name ="燕平",Language="普通话",Tone="青年女声",Vname="yanping" },
                new Speaker(){ Name ="晓婧",Language="普通话",Tone="青年女声",Vname="jinger" },
                new Speaker(){ Name ="晓峰",Language="普通话",Tone="青年男声",Vname="xiaofeng" },
                new Speaker(){ Name ="晓琳",Language="台湾普通话",Tone="青年女声",Vname="xiaolin" },
                new Speaker(){ Name ="晓倩",Language="东北话",Tone="青年女声",Vname="xiaoqian" },
                new Speaker(){ Name ="晓蓉",Language="四川话",Tone="青年女声",Vname="xiaorong" },
                new Speaker(){ Name ="小坤",Language="河南话",Tone="青年男声",Vname="xiaokun" },
                new Speaker(){ Name ="小强",Language="湖南话",Tone="青年男声",Vname="xiaoqiang" },
                new Speaker(){ Name ="晓美",Language="粤语",Tone="青年女声",Vname="xiaomei" },
                new Speaker(){ Name ="大龙",Language="粤语",Tone="青年男声",Vname="dalong" }
            };
            this.comboBox1.DataSource = speakers.Select(p => p.Language).Distinct().ToArray();
            this.comboBox1.SelectedItem = speakers.Find(p => p.Vname.Equals(ttsSessionParam.voice_name)).Language;
            this.comboBox2.DataSource = speakers.Where(p => p.Language.Equals(this.comboBox1.SelectedItem)).Select(p => p.Tone).Distinct().ToArray();
            this.comboBox2.SelectedItem = speakers.Find(p => p.Vname.Equals(ttsSessionParam.voice_name)).Tone;
            this.comboBox3.DataSource = speakers.Where(p => p.Language.Equals(this.comboBox1.SelectedItem) && p.Tone.Equals(this.comboBox2.SelectedItem)).Select(p => p.Name).Distinct().ToArray();
            this.comboBox3.SelectedItem = speakers.Find(p => p.Vname.Equals(ttsSessionParam.voice_name)).Name;
            this.comboBox4.DataSource = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            this.comboBox4.SelectedItem = 50;
            this.comboBox5.DataSource = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            this.comboBox5.SelectedItem = 50;
            this.comboBox6.DataSource = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            this.comboBox6.SelectedItem = 50;
        }

        /// <summary>
        /// 切换语种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox2.DataSource = speakers.Where(p => p.Language.Equals(this.comboBox1.SelectedItem)).Select(p => p.Tone).Distinct().ToArray();
            this.comboBox2.SelectedIndex = 0;
        }

        /// <summary>
        /// 切换音色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBox3.DataSource = speakers.Where(p => p.Language.Equals(this.comboBox1.SelectedItem) && p.Tone.Equals(this.comboBox2.SelectedItem)).Select(p => p.Name).Distinct().ToArray();
            this.comboBox3.SelectedIndex = 0;
        }

        /// <summary>
        /// 切换发音人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttsSessionParam.voice_name = speakers.Find(p => p.Name.Equals(this.comboBox3.SelectedItem)).Vname;
        }

        /// <summary>
        /// 切换语速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttsSessionParam.speed = Convert.ToUInt32(this.comboBox4.SelectedItem);
        }

        /// <summary>
        /// 切换音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttsSessionParam.volume = Convert.ToUInt32(this.comboBox5.SelectedItem);
        }

        /// <summary>
        /// 切换语调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttsSessionParam.pitch = Convert.ToUInt32(this.comboBox6.SelectedItem);
        }

        /// <summary>
        /// 合成测试(不保存，合成完立即播放)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (!SDKRealize.GetInitialize().IsLogin)
            {
                SDKRealize.GetInitialize().MSPLogin();
            }
            int eCode = -1; //错误码
            var sessionId = SDKRealize.GetInitialize().TTSSessionBegin(ttsSessionParam, ref eCode);
            if (eCode == 0)
            {
                string text = this.textBox4.Text;
                uint textLen = (uint)Encoding.Default.GetBytes(text).Length;
                bool isSuccess1 = SDKRealize.GetInitialize().TTSTextPut(sessionId, text, textLen, null);
                if (isSuccess1)
                {
                    uint audioLen = 0;  //返回的音频字节长度
                    int synthStatus = 0;    //返回的合成状态
                    int eCode1 = -1;    //返回的合成音频错误码
                    byte[] bytes = SDKRealize.GetInitialize().TTSAudioGet(sessionId, ref audioLen, ref synthStatus, ref eCode1);
                    if (eCode1 == 0)
                    {
                        Naudio.GetInstance().PlayAsBytes(bytes);
                    }
                }
            }
            SDKRealize.GetInitialize().TTSSessionEnd(sessionId, null);
        }

        /// <summary>
        /// 合成到文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            string fileName = SDKRealize.GetInitialize().TTSdir + this.textBox2.Text + ".wav";
            await TTStoFile(text, fileName);
        }

        /// <summary>
        /// 选择批量合成文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();   //显示选择文件对话框 
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox3.Text = openFileDialog1.FileName;     //显示文件路径 
            }
        }

        /// <summary>
        /// 批量语音合成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button3_Click(object sender, EventArgs e)
        {
            string txtFile = this.textBox3.Text;    //文本文件路径
            if (File.Exists(txtFile))
            {
                string ttsDirPath = SDKRealize.GetInitialize().TTSdir + Path.GetFileNameWithoutExtension(txtFile) + "/";
                if (!Directory.Exists(ttsDirPath))
                {
                    Directory.CreateDirectory(ttsDirPath);
                }
                using (StreamReader sr = new StreamReader(txtFile))
                {
                    var tempStr = string.Empty;
                    while (!string.IsNullOrEmpty(tempStr = sr.ReadLine()))
                    {
                        var strArray = tempStr.Split(':');
                        await TTStoFile(strArray[1], ttsDirPath + strArray[0] + ".wav");
                    }
                }
            }
        }

        /// <summary>
        /// 合成语音到文件
        /// </summary>
        /// <param name="txt">要合成的文本</param>
        /// <param name="fName">保存的文件</param>
        private async Task TTStoFile(string text, string fName)
        {
            await Task.Factory.StartNew(() =>
            {
                if (!SDKRealize.GetInitialize().IsLogin)
                {
                    SDKRealize.GetInitialize().MSPLogin();
                }
                int eCode = -1; //错误码
                var sessionId = SDKRealize.GetInitialize().TTSSessionBegin(ttsSessionParam, ref eCode);
                if (eCode == 0)
                {
                    uint textLen = (uint)Encoding.Default.GetBytes(text).Length;
                    bool isSuccess1 = SDKRealize.GetInitialize().TTSTextPut(sessionId, text, textLen, null);
                    if (isSuccess1)
                    {
                        uint audioLen = 0;  //返回的音频字节长度
                        int synthStatus = 0;    //返回的合成状态
                        int eCode1 = -1;    //返回的合成音频错误码
                        byte[] bytes = SDKRealize.GetInitialize().TTSAudioGet(sessionId, ref audioLen, ref synthStatus, ref eCode1);
                        if (eCode1 == 0)
                        {
                            using (FileStream fs = new FileStream(fName, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(bytes, 0, bytes.Length);
                            }
                        }
                    }
                }
                SDKRealize.GetInitialize().TTSSessionEnd(sessionId, null);
            });
        }
    }
}
