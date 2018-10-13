using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaLM.XfSpeech.SDK.Model
{
    /// <summary>
    /// TTS(语音合成)会话参数
    /// </summary>
    public class TTSSessionBegin_Param
    {
        /// <summary>
        /// 引擎类型
        /// cloud：在线引擎，local：离线引擎，默认为cloud
        /// </summary>
        public string engine_type { get; set; } = "cloud";
        /// <summary>
        /// 发音人
        /// 不同的发音人代表了不同的音色，详细请参照《发音人列表》
        /// </summary>
        public string voice_name { get; set; } = "xiaoyan";
        /// <summary>
        /// 语速
        /// 合成音频对应的语速，取值范围：[0,100]，数值越大语速越快。
        /// </summary>
        public uint speed { get; set; } = 50;
        /// <summary>
        /// 音量
        /// 合成音频的音量，取值范围：[0,100]，数值越大音量越大。
        /// </summary>
        public uint volume { get; set; } = 50;
        /// <summary>
        /// 语调
        /// 合成音频的音调，取值范围：[0,100]，数值越大音调越高。
        /// </summary>
        public uint pitch { get; set; } = 50;
        /// <summary>
        /// 数字发音
        /// 合成音频数字发音，支持参数:0 数值优先,1 完全数值,2 完全字符串，3 字符串优先，
        /// </summary>
        public uint rdn { get; set; } = 0;
        /// <summary>
        /// 文本编码格式（必传）
        /// 合成文本编码格式，支持参数，GB2312，GBK，BIG5，UNICODE，GB18030，UTF8
        /// </summary>
        public string text_encoding { get; set; } = "GB2312";
        /// <summary>
        /// 合成音频采样率
        /// 合成音频采样率，支持参数，16000，8000，默认为16000
        /// </summary>
        public uint sample_rate { get; set; } = 16000;

    }

    /// <summary>
    /// 音频头结构体
    /// </summary>
    public struct Wave_Pcm_Head
    {
        public int RIFF_ID;
        public int File_Size;
        public int RIFF_Type;
        public int FMT_ID;
        public int FMT_Size;
        public short FMT_Tag;
        public ushort FMT_Channel;
        public int FMT_SamplesPerSec;
        public int AvgBytesPerSec;
        public ushort BlockAlign;
        public ushort BitsPerSample;
        public int DATA_ID;
        public int DATA_Size;
    }

    /// <summary>
    /// 发音人
    /// </summary>
    public class Speaker
    {
        /// <summary>
        /// 中文名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 语种
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 音色
        /// </summary>
        public string Tone { get; set; }
        /// <summary>
        /// 发音参数名称
        /// </summary>
        public string Vname { get; set; } 
    }
}
