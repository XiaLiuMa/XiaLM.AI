using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using XiaLM.MicrosoftSpeech.SpeedASR;
using XiaLM.MicrosoftSpeech.SpeedTTS;

namespace XiaLM.FormTest.MicrosoftSpeech
{
    public partial class MainForm : Form
    {
        private Dictionary<string, string> languageDictionary;  //语种字典

        public MainForm()
        {
            InitializeComponent();
            InitFormData();
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitFormData()
        {
            languageDictionary = new Dictionary<string, string>();   //语种字典
            languageDictionary.Add("zh-CN中文(普通话)", "zh-CN");
            languageDictionary.Add("zh-HK中文(香港特别行政区)", "zh-HK");
            languageDictionary.Add("zh-TW中文(台湾地区)", "zh-TW");
            languageDictionary.Add("en-GB英语(英国)", "en-GB");
            languageDictionary.Add("en-US英语(美国)", "en-US");
            languageDictionary.Add("ar-EG阿拉伯语(埃及)", "ar-EG");
            languageDictionary.Add("ca-ES加泰罗尼亚语(西班牙)", "ca-ES");
            languageDictionary.Add("da-DK丹麦语(丹麦)", "da-DK");
            languageDictionary.Add("de-DE德语(德国)", "de-DE");
            languageDictionary.Add("en-AU英语(澳大利亚)", "en-AU");
            languageDictionary.Add("en-CA英语(加拿大)", "en-CA");
            languageDictionary.Add("en-IN英语(印度)", "en-IN");
            languageDictionary.Add("en-NZ英语(新西兰)", "en-NZ");
            languageDictionary.Add("es-ES西班牙语(西班牙)", "es-ES");
            languageDictionary.Add("es-MX西班牙语(墨西哥)", "es-MX");
            languageDictionary.Add("fi-FI芬兰语(芬兰)", "fi-FI");
            languageDictionary.Add("fr-CA法语(加拿大)", "fr-CA");
            languageDictionary.Add("fr-FR法语(法国)", "fr-FR");
            languageDictionary.Add("hi-IN印地语(印度)", "hi-IN");
            languageDictionary.Add("it-IT意大利语(意大利)", "it-IT");
            languageDictionary.Add("ja-JP日语(日本)", "ja-JP");
            languageDictionary.Add("ko-KR韩语(韩国)", "ko-KR");
            languageDictionary.Add("nb-NO书面挪威语(挪威)", "nb-NO");
            languageDictionary.Add("nl-NL荷兰语(荷兰)", "nl-NL");
            languageDictionary.Add("pl-PL波兰语(波兰)", "pl-PL");
            languageDictionary.Add("pt-BR葡萄牙语(巴西)", "pt-BR");
            languageDictionary.Add("pt-PT葡萄牙语(葡萄牙)", "pt-PT");
            languageDictionary.Add("sv-SE俄语(俄罗斯)", "sv-SE");
            languageDictionary.Add("fr-CA瑞典语(瑞典)", "fr-CA");

            foreach (var key in languageDictionary.Keys)
            {
                this.comBoxLanguage.Items.Add(key);
            }
            this.comBoxLanguage.SelectedIndex = 0;

            this.comBoxVoice.Items.AddRange(new string[]
            {
                "zh-CN，HuihuiRUS",
                "zh-CN，Yaoyao, Apollo",
                "zh-CN，Kangkang, Apollo",
                "zh-HK，Tracy, Apollo",
                "zh-HK，TracyRUS",
                "zh-HK，Danny, Apollo",
                "zh-TW，Yating, Apollo",
                "zh-TW，HanHanRUS",
                "zh-TW，Zhiwei, Apollo",
                "ar-EG，Hoda",
                "ar-SA，Naayf",
                "bg-BG，Ivan",
                "ca-ES，HerenaRUS",
                "cs-CZ，Jakub",
                "da-DK，HelleRUS",
                "de-AT，Michael",
                "de-CH，Karsten",
                "de-DE，Hedda",
                "de-DE，HeddaRUS",
                "de-DE，Stefan, Apollo",
                "el-GR，Stefanos",
                "en-AU，Catherine",
                "en-AU，HayleyRUS",
                "en-CA，Linda",
                "en-CA，HeatherRUS",
                "en-GB，Susan, Apollo",
                "en-GB，HazelRUS",
                "en-GB，George, Apollo",
                "en-IE，Sean",
                "en-IN，Heera, Apollo",
                "en-IN，PriyaRUS",
                "en-IN，Ravi, Apollo",
                "en-US，ZiraRUS",
                "en-US，JessaRUS",
                "en-US，BenjaminRUS",
                "es-ES，Laura, Apollo",
                "es-ES，HelenaRUS",
                "es-ES，Pablo, Apollo",
                "es-MX，HildaRUS",
                "es-MX，Raul, Apollo",
                "fi-FI，HeidiRUS",
                "fr-CA，Caroline",
                "fr-CA，HarmonieRUS",
                "fr-CH，Guillaume",
                "fr-FR，Julie, Apollo",
                "fr-FR，HortenseRUS",
                "fr-FR，Paul, Apollo",
                "he-IL，Asaf",
                "hi-IN，Kalpana, Apollo",
                "hi-IN，Kalpana",
                "hi-IN，Hemant",
                "hr-HR，Matej",
                "hu-HU，Szabolcs",
                "id-ID，Andika",
                "it-IT，Cosimo, Apoll",
                "it-IT，LuciaRUS",
                "ja-JP，Ayumi, Apollo",
                "ja-JP，Ichiro, Apollo",
                "ja-JP，HarukaRUS",
                "ko-KR，HeamiRUS",
                "ms-MY，Rizwan",
                "nb-NO，HuldaRUS",
                "nl-NL，HannaRUS",
                "pl-PL，PaulinaRUS",
                "pt-BR，HeloisaRUS",
                "pt-BR，Daniel, Apollo",
                "pt-PT，HeliaRUS",
                "ro-RO，Andrei",
                "ru-RU，Irina, Apollo",
                "ru-RU，Pavel, Apollo",
                "ru-RU，EkaterinaRUS",
                "sk-SK，Filip",
                "sl-SI，Lado",
                "sv-SE，HedvigRUS",
                "ta-IN，Valluvar",
                "th-TH，Pattara",
                "tr-TR，SedaRUS",
                "vi-VN，An"
            });
            this.comBoxVoice.SelectedIndex = 0;
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCheckFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"D:\Test";//初始目录，不赋值也可以
            openFileDialog.Filter = "wav文件(*.wav) | *.wav";//文件类型
            openFileDialog.ShowDialog();//弹出选择框
            this.tBoxWavPath.Text = openFileDialog.FileName;
        }

        /// <summary>
        /// wav文件识别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butStartAsPath_Click(object sender, EventArgs e)
        {
            string filePath = this.tBoxWavPath.Text.Trim();
            if (!File.Exists(filePath)) return;
            string lkey = this.comBoxLanguage.SelectedItem.ToString();
            if (string.IsNullOrEmpty(lkey)) return;
            string lvalue = string.Empty;
            if (languageDictionary.TryGetValue(lkey, out lvalue))
                RunDiscernAsPath(filePath, lvalue);
        }
        private void RunDiscernAsPath(string path, string type)
        {
            Task.Factory.StartNew(async () =>
            {
                AsrClient asrClient = new AsrClient("3a1ec26757b94a84af648ac1f88cb95f");
                asrClient.ReturnPartialResult += (s) => //部份识别完成
                {
                    this.Invoke(new Action(() =>
                    {
                        this.rtBoxResult.Text = s;
                    }));
                };
                asrClient.ReturnRecognitionResult += (s) => //全部识别完成
                {
                    this.Invoke(new Action(() =>
                    {
                        this.rtBoxResult.Text = s;
                    }));
                };
                await asrClient.RunDiscernAsPath(path, type);
            });
        }

        /// <summary>
        /// 生成音频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butGenerateWav_Click(object sender, EventArgs e)
        {
            string txt = this.rtBoxResult.Text.Trim();
            if (string.IsNullOrEmpty(txt)) return;
            string fistStr = this.comBoxVoice.SelectedItem.ToString().Split('，').First();
            string voiceName = this.comBoxVoice.SelectedItem.ToString().Replace(fistStr + "，", " ").Trim();
            string _rate = this.tBoxRate.Text.Trim();
            string _pitch = this.tBoxPitch.Text.Trim();
            Task.Factory.StartNew(async () =>
            {
                TtsClient ttsClient = new TtsClient("3a1ec26757b94a84af648ac1f88cb95f");
                ttsClient.SynthesizeSuccessEvent += (s) =>  //合成成功
                {
                    SoundPlayer player = new SoundPlayer(new MemoryStream(s));
                    player.PlaySync();
                };
                ttsClient.SynthesizeErrorEvent += (s) =>    //合成失败
                {

                };
                await ttsClient.SyntheticAudio(new TtsInputOptions(txt, fistStr, voiceName), new Prosody()
                {
                    rate = _rate, //语速
                    volume = "+20.00%",   //音量
                    pitch = _pitch   //音调
                });
            });
        }
    }
}
