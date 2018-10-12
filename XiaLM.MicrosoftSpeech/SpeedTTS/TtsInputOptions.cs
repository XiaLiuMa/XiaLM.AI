using System;
using System.Collections.Generic;

namespace XiaLM.MicrosoftSpeech.SpeedTTS
{
    public class TtsInputOptions
    {
        private const string NomolVoiceName = "Microsoft Server Speech Text to Speech Voice ({0}, {1})";

        /// <summary>
        /// 通信令牌【不需要用户设置】
        /// </summary>
        public string AuthorizationToken { get; set; }

        /// <summary>
        /// 要合成的文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 语种(默认：中文)【如果构造函数中指定了发音人名称，一定要对应修改这个属性】
        /// </summary>
        public String Locale { get; set; }

        /// <summary>
        /// 发音人名称(默认："Microsoft Server Speech Text to Speech Voice (en-US, Guy24KRUS)")
        /// </summary>
        public string VoiceName { get; set; }

        /// <summary>
        /// 发音人性别(默认：女)
        /// </summary>
        public Gender VoiceType { get; set; } = Gender.Female;

        /// <summary>
        /// 服务请求路径【勿轻易改动】(默认路径：https://westus.tts.speech.microsoft.com/cognitiveservices/v1)
        /// </summary>
        public Uri RequestUri { get; set; } = new Uri("https://speech.platform.bing.com/synthesize");

        /// <summary>
        /// 音频输出格式【勿轻易改动】(默认：riff-24khz-16bit-mono-pcm格式)
        /// </summary>
        public AudioOutputFormat OutputFormat { get; set; } = AudioOutputFormat.Riff24Khz16BitMonoPcm;

        public TtsInputOptions(string txt) : this(txt, "zh-CN", "HuihuiRUS") { }

        /// <summary>
        /// 构造函数【如果指定了发音人名称，一定要核对"Locale"成员是否匹配，是否需要重新指定】
        /// </summary>
        /// <param name="txt">要合成的文本</param>
        /// <param name="voice">发音人名称(例：HuihuiRUS)</param>
        public TtsInputOptions(string txt, string local, string voice)
        {
            this.Text = txt;
            this.Locale = local;
            this.VoiceName = string.Format(NomolVoiceName, Locale, voice);
        }

        /// <summary>
        /// 合成请求的包头
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Headers
        {
            get
            {
                List<KeyValuePair<string, string>> toReturn = new List<KeyValuePair<string, string>>();
                toReturn.Add(new KeyValuePair<string, string>("Content-Type", "application/ssml+xml"));
                string outputFormat = string.Empty;
                switch (this.OutputFormat)
                {
                    case AudioOutputFormat.Raw16Khz16BitMonoPcm:
                        outputFormat = "raw-16khz-16bit-mono-pcm";
                        break;
                    case AudioOutputFormat.Raw8Khz8BitMonoMULaw:
                        outputFormat = "raw-8khz-8bit-mono-mulaw";
                        break;
                    case AudioOutputFormat.Riff16Khz16BitMonoPcm:
                        outputFormat = "riff-16khz-16bit-mono-pcm";
                        break;
                    case AudioOutputFormat.Riff8Khz8BitMonoMULaw:
                        outputFormat = "riff-8khz-8bit-mono-mulaw";
                        break;
                    case AudioOutputFormat.Ssml16Khz16BitMonoSilk:
                        outputFormat = "ssml-16khz-16bit-mono-silk";
                        break;
                    case AudioOutputFormat.Raw16Khz16BitMonoTrueSilk:
                        outputFormat = "raw-16khz-16bit-mono-truesilk";
                        break;
                    case AudioOutputFormat.Ssml16Khz16BitMonoTts:
                        outputFormat = "ssml-16khz-16bit-mono-tts";
                        break;
                    case AudioOutputFormat.Audio16Khz128KBitRateMonoMp3:
                        outputFormat = "audio-16khz-128kbitrate-mono-mp3";
                        break;
                    case AudioOutputFormat.Audio16Khz64KBitRateMonoMp3:
                        outputFormat = "audio-16khz-64kbitrate-mono-mp3";
                        break;
                    case AudioOutputFormat.Audio16Khz32KBitRateMonoMp3:
                        outputFormat = "audio-16khz-32kbitrate-mono-mp3";
                        break;
                    case AudioOutputFormat.Audio16Khz16KbpsMonoSiren:
                        outputFormat = "audio-16khz-16kbps-mono-siren";
                        break;
                    case AudioOutputFormat.Riff16Khz16KbpsMonoSiren:
                        outputFormat = "riff-16khz-16kbps-mono-siren";
                        break;
                    case AudioOutputFormat.Raw24Khz16BitMonoPcm:
                        outputFormat = "raw-24khz-16bit-mono-pcm";
                        break;
                    case AudioOutputFormat.Riff24Khz16BitMonoPcm:
                        outputFormat = "riff-24khz-16bit-mono-pcm";
                        break;
                    case AudioOutputFormat.Audio24Khz48KBitRateMonoMp3:
                        outputFormat = "audio-24khz-48kbitrate-mono-mp3";
                        break;
                    case AudioOutputFormat.Audio24Khz96KBitRateMonoMp3:
                        outputFormat = "audio-24khz-96kbitrate-mono-mp3";
                        break;
                    case AudioOutputFormat.Audio24Khz160KBitRateMonoMp3:
                        outputFormat = "audio-24khz-160kbitrate-mono-mp3";
                        break;
                    default:
                        outputFormat = "riff-16khz-16bit-mono-pcm";
                        break;
                }
                toReturn.Add(new KeyValuePair<string, string>("X-Microsoft-OutputFormat", outputFormat));
                toReturn.Add(new KeyValuePair<string, string>("Authorization", this.AuthorizationToken));
                toReturn.Add(new KeyValuePair<string, string>("X-Search-AppId", "07D3234E49CE426DAA29772419F436CA"));
                toReturn.Add(new KeyValuePair<string, string>("X-Search-ClientID", "1ECFAE91408841A480F00935DC390960"));
                toReturn.Add(new KeyValuePair<string, string>("User-Agent", "TtsClient"));
                return toReturn;
            }
            set
            {
                Headers = value;
            }
        }
    }

    /// <summary>
    /// 发音人性别
    /// </summary>
    public enum Gender
    {
        Female,
        Male
    }

    /// <summary>
    /// 音频输出格式
    /// </summary>
    public enum AudioOutputFormat
    {
        /// <summary>
        /// raw-8khz-8bit-mono-mulaw request output audio format type.
        /// </summary>
        Raw8Khz8BitMonoMULaw,

        /// <summary>
        /// raw-16khz-16bit-mono-pcm request output audio format type.
        /// </summary>
        Raw16Khz16BitMonoPcm,

        /// <summary>
        /// riff-8khz-8bit-mono-mulaw request output audio format type.
        /// </summary>
        Riff8Khz8BitMonoMULaw,

        /// <summary>
        /// riff-16khz-16bit-mono-pcm request output audio format type.
        /// </summary>
        Riff16Khz16BitMonoPcm,

        // <summary>
        /// ssml-16khz-16bit-mono-silk request output audio format type.
        /// It is a SSML with audio segment, with audio compressed by SILK codec
        /// </summary>
        Ssml16Khz16BitMonoSilk,

        /// <summary>
        /// raw-16khz-16bit-mono-truesilk request output audio format type.
        /// Audio compressed by SILK codec
        /// </summary>
        Raw16Khz16BitMonoTrueSilk,

        /// <summary>
        /// ssml-16khz-16bit-mono-tts request output audio format type.
        /// It is a SSML with audio segment, and it needs tts engine to play out
        /// </summary>
        Ssml16Khz16BitMonoTts,

        /// <summary>
        /// audio-16khz-128kbitrate-mono-mp3 request output audio format type.
        /// </summary>
        Audio16Khz128KBitRateMonoMp3,

        /// <summary>
        /// audio-16khz-64kbitrate-mono-mp3 request output audio format type.
        /// </summary>
        Audio16Khz64KBitRateMonoMp3,

        /// <summary>
        /// audio-16khz-32kbitrate-mono-mp3 request output audio format type.
        /// </summary>
        Audio16Khz32KBitRateMonoMp3,

        /// <summary>
        /// audio-16khz-16kbps-mono-siren request output audio format type.
        /// </summary>
        Audio16Khz16KbpsMonoSiren,

        /// <summary>
        /// riff-16khz-16kbps-mono-siren request output audio format type.
        /// </summary>
        Riff16Khz16KbpsMonoSiren,

        /// <summary>
        /// raw-24khz-16bit-mono-truesilk request output audio format type.
        /// </summary>
        Raw24Khz16BitMonoTrueSilk,

        /// <summary>
        /// raw-24khz-16bit-mono-pcm request output audio format type.
        /// </summary>
        Raw24Khz16BitMonoPcm,

        /// <summary>
        /// riff-24khz-16bit-mono-pcm request output audio format type.
        /// </summary>
        Riff24Khz16BitMonoPcm,

        /// <summary>
        /// audio-24khz-48kbitrate-mono-mp3 request output audio format type.
        /// </summary>
        Audio24Khz48KBitRateMonoMp3,

        /// <summary>
        /// audio-24khz-96kbitrate-mono-mp3 request output audio format type.
        /// </summary>
        Audio24Khz96KBitRateMonoMp3,

        /// <summary>
        /// audio-24khz-160kbitrate-mono-mp3 request output audio format type.
        /// </summary>
        Audio24Khz160KBitRateMonoMp3
    }

    /// <summary>
    /// 韵律
    /// </summary>
    public class Prosody
    {
        /// <summary>
        /// 休息时间(例："100ms")
        /// </summary>
        public string time { get; set; } = "100ms";
        /// <summary>
        /// 语速(默认不设置为"default"，可选："x-low","low","medium","high","x-high")
        /// </summary>
        public string rate { get; set; }
        /// <summary>
        /// 加减音量(正负百分比，例："+30.00%")
        /// </summary>
        public string volume { get; set; }
        /// <summary>
        /// 音调(默认不设置为"default"，可选："x-low","low","medium","high","x-high")
        /// </summary>
        public string pitch { get; set; }

    }
}
