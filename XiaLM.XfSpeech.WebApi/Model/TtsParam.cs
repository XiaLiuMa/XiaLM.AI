using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaLM.XfSpeech.WebApi.Model
{
    /// <summary>
    /// 语音合成参数
    /// </summary>
    public class TtsParam
    {
        public string auf { get; set; } //音频采样率，可选值：audio/L8;rate=8000，audio/L16;rate=16000
        public string aue { get; set; } //音频编码，可选值：raw（未压缩的pcm或wav格式），lame（mp3格式）
        public string voice_name { get; set; }  //发音人
        public string speed { get; set; }   //语速，可选值：[0-100]，默认为50
        public string volume { get; set; }  //音量，可选值：[0-100]，默认为50
        public string pitch { get; set; }   //音高，可选值：[0-100]，默认为50
        public string engine_type { get; set; } //合成类型,可选值:aisound(普通效果),intp65(中文),intp65_en(英文),mtts(小语种,需配合小语种发音人使用),x(优化效果),默认为inpt65
        public string text_type { get; set; }   //文本类型，可选值：text（普通格式文本），默认为text
    }
}
