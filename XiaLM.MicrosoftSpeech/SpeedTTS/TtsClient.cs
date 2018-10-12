using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace XiaLM.MicrosoftSpeech.SpeedTTS
{
    /// <summary>
    /// 语音合成客户端
    /// </summary>
    public class TtsClient
    {
        private string apiKey = string.Empty;   //密钥
        private static string ttsToken;    //当前语音合成Token
        /// <summary>
        /// 合成完成返回事件
        /// </summary>
        public Action<byte[]> SynthesizeSuccessEvent;
        /// <summary>
        /// 合成错误返回事件
        /// </summary>
        public Action<Exception> SynthesizeErrorEvent;

        public TtsClient(string apikey)
        {
            this.apiKey = apikey;
        }

        public async Task SyntheticAudio(TtsInputOptions inputOptions, Prosody prosody)
        {
            ttsToken = new TtsTokenGenerator(this.apiKey).GetAccessToken();
            inputOptions.AuthorizationToken = "Bearer " + ttsToken;
            TtsSynthesize synthesize = new TtsSynthesize();
            synthesize.OnAudioAvailable += SuccessHandler;
            synthesize.OnError += ErrorHandler;
            try
            {
                await synthesize.Speak(CancellationToken.None, inputOptions, prosody);
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 合成完成后处理
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">音频流</param>
        private void SuccessHandler(object sender, GenericEventArgs<Stream> args)
        {
            List<byte> list = new List<byte>();
            int bint = 0;
            while ((bint = args.EventData.ReadByte()) != -1)
            {
                list.Add((byte)bint);
            }
            SynthesizeSuccessEvent(list.ToArray());
            args.EventData.Dispose();
        }

        /// <summary>
        /// 合成异常后处理
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">异常信息</param>
        private void ErrorHandler(object sender, GenericEventArgs<Exception> ex)
        {
            SynthesizeErrorEvent(ex.EventData);
        }
    }
}
