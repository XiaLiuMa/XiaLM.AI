using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaLM.XfSpeech.WebApi.Model
{
    /// <summary>
    /// 语音识别参数
    /// </summary>
    public class IatParam
    {
        public string engine_type { get; set; } //识别引擎，可选值：sms16k（16k采样率普通话音频）、sms8k（8k采样率普通话音频）等
        public string aue { get; set; } //音频编码，可选值：raw（未压缩的pcm或wav格式）、speex（speex格式）、speex-wb（宽频speex格式）
        public string speex_size { get; set; } //speex音频帧率【非必需】
        public string scene { get; set; } //情景模式（main）【非必需】
        public string vad_eos { get; set; } //后端点检测（单位：ms），默认1800【非必需】
    }
}
