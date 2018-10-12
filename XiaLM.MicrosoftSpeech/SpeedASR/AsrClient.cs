using Microsoft.Bing.Speech;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace XiaLM.MicrosoftSpeech.SpeedASR
{
    /// <summary>
    /// 语音识别Client
    /// </summary>
    public class AsrClient
    {
        private string apiKey = string.Empty;   //密钥
        private static readonly Uri ShortUrl = new Uri(@"wss://speech.platform.bing.com/api/service/recognition");    //短听写网址
        private static readonly Uri LongUrl = new Uri(@"wss://speech.platform.bing.com/api/service/recognition/continuous");   //长听写网址
        private static readonly Task CompletedTask = Task.FromResult(true); //完成的任务
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        /// <summary>
        /// 部分识别完成返回事件
        /// </summary>
        public Action<string> ReturnPartialResult;
        /// <summary>
        /// 全部识别完成后结果返回事件
        /// </summary>
        public Action<string> ReturnRecognitionResult;

        public AsrClient(string apikey)
        {
            this.apiKey = apikey;
        }

        public async Task RunDiscernAsPath(string audioFile, string locale)
        {
            byte[] audioBytes;  //音频数据
            using (var audioStream = new FileStream(audioFile, FileMode.Open, FileAccess.Read))
            {
                audioBytes = new byte[audioStream.Length];
                audioStream.Read(audioBytes, 0, audioBytes.Length);
            }
            Uri serviceUrl = ShortUrl;
            if (GetWavPlayTime(audioBytes) > 15) serviceUrl = LongUrl;
            var preferences = new Preferences(locale, serviceUrl, new AsrCheckKeyProvider(apiKey));   //参数配置类
            using (var speechClient = new SpeechClient(preferences))    //创建语音客户端
            {
                speechClient.SubscribeToPartialResult(this.OnPartialResult);
                speechClient.SubscribeToRecognitionResult(this.OnRecognitionResult);
                using (Stream stream = new MemoryStream(audioBytes))
                {
                    var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Desktop, NetworkType.Ethernet, OsName.Windows, "1607", "Dell", "T3600");
                    var applicationMetadata = new ApplicationMetadata("SampleApp", "1.0.0");
                    var requestMetadata = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "SampleAppService");
                    try
                    {
                        await speechClient.RecognizeAsync(new SpeechInput(stream, requestMetadata), this.cts.Token).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }

        /// <summary>
        /// 通过语音数据识别语音内容
        /// </summary>
        /// <param name="audioBytes">语音数据</param>
        /// <param name="locale">语种</param>
        /// <returns></returns>
        public async Task RunDiscernAsBytes(byte[] audioBytes, string locale)
        {
            Uri serviceUrl = ShortUrl;
            if (GetWavPlayTime(audioBytes) > 15) serviceUrl = LongUrl;
            var preferences = new Preferences(locale, serviceUrl, new AsrCheckKeyProvider(apiKey));   //参数配置类
            using (var speechClient = new SpeechClient(preferences))    //创建语音客户端
            {
                speechClient.SubscribeToPartialResult(this.OnPartialResult);
                speechClient.SubscribeToRecognitionResult(this.OnRecognitionResult);
                using (Stream stream = new MemoryStream(audioBytes))
                {
                    var deviceMetadata = new DeviceMetadata(DeviceType.Near, DeviceFamily.Desktop, NetworkType.Ethernet, OsName.Windows, "1607", "Dell", "T3600");
                    var applicationMetadata = new ApplicationMetadata("SampleApp", "1.0.0");
                    var requestMetadata = new RequestMetadata(Guid.NewGuid(), deviceMetadata, applicationMetadata, "SampleAppService");
                    try
                    {
                        await speechClient.RecognizeAsync(new SpeechInput(stream, requestMetadata), this.cts.Token).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
        }

        /// <summary>
        /// 部分识别结果返回事件
        /// </summary>
        /// <param name="args">识别结果</param>
        /// <returns></returns>
        private Task OnPartialResult(RecognitionPartialResult args)
        {
            ReturnPartialResult(args.DisplayText);
            return CompletedTask;
        }

        /// <summary>
        /// 全部识别结果事件
        /// </summary>
        /// <param name="args">识别结果</param>
        /// <returns></returns>
        private Task OnRecognitionResult(RecognitionResult args)
        {
            List<string> results = new List<string>();
            var state = args.RecognitionStatus; //识别状态
            if (args.Phrases != null && args.Phrases.Count > 0)
            {
                foreach (var result in args.Phrases)
                {
                    results.Add(result.DisplayText);
                    var confidence = result.Confidence; //可信度
                }
            }
            ReturnRecognitionResult(results.First());
            return CompletedTask;
        }

        /// <summary>
        /// 获取音频播放时长(单位:秒)
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        private float GetWavPlayTime(byte[] bs)
        {
            var bs1 = bs.Skip(28).Take(4).ToArray();
            int ss = BitConverter.ToInt32(bs1, 0);
            bs1 = bs.Skip(40).Take(4).ToArray();
            int ss2 = BitConverter.ToInt32(bs1, 0);
            float pTime = ss2 / (ss * 1.0f);
            pTime = Math.Abs(pTime);
            return pTime;
        }
    }
}
