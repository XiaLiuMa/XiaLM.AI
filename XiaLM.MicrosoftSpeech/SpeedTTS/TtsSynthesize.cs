using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XiaLM.MicrosoftSpeech.SpeedTTS
{
    public class TtsSynthesize
    {
        private HttpClient client;
        private HttpClientHandler handler;
        /// <summary>
        /// 当TTS请求完成且音频可用时调用
        /// </summary>
        public event EventHandler<GenericEventArgs<Stream>> OnAudioAvailable;
        /// <summary>
        /// 发生错误时调用,这可能是一个HTTP错误
        /// </summary>
        public event EventHandler<GenericEventArgs<Exception>> OnError;

        public TtsSynthesize()
        {
            handler = new HttpClientHandler() { CookieContainer = new CookieContainer(), UseProxy = false };
            client = new HttpClient(handler);
        }

        ~TtsSynthesize()
        {
            client.Dispose();
            handler.Dispose();
        }

        /// <summary>
        /// 将指定的文本发送到TTS服务，并将响应音频保存到文件中。
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        public Task Speak(CancellationToken cancellationToken, TtsInputOptions inputOptions, Prosody prosody)
        {
            client.DefaultRequestHeaders.Clear();
            foreach (var header in inputOptions.Headers)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
            var genderValue = inputOptions.VoiceType.Equals(Gender.Male) ? "Male" : "Female";
            var request = new HttpRequestMessage(HttpMethod.Post, inputOptions.RequestUri)
            {
                Content = new StringContent(GenerateSsml(inputOptions.Locale, genderValue, inputOptions.VoiceName, inputOptions.Text, prosody))
            };
            var httpTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            var saveTask = httpTask.ContinueWith(
                async (responseMessage, token) =>
                {
                    try
                    {
                        if (responseMessage.IsCompleted && responseMessage.Result != null && responseMessage.Result.IsSuccessStatusCode)
                        {
                            var httpStream = await responseMessage.Result.Content.ReadAsStreamAsync().ConfigureAwait(false);
                            this.AudioAvailable(new GenericEventArgs<Stream>(httpStream));
                        }
                        else
                        {
                            this.Error(new GenericEventArgs<Exception>(new Exception(String.Format("Service returned {0}", responseMessage.Result.StatusCode))));
                        }
                    }
                    catch (Exception e)
                    {
                        this.Error(new GenericEventArgs<Exception>(e.GetBaseException()));
                    }
                    finally
                    {
                        responseMessage.Dispose();
                        request.Dispose();
                    }
                },
                TaskContinuationOptions.AttachedToParent, cancellationToken);

            return saveTask;
        }

        /// <summary>
        /// 生成 SSML.
        /// </summary>
        /// <param name="locale">语种</param>
        /// <param name="gender">性别</param>
        /// <param name="name">发音人</param>
        /// <param name="text">要合成的文本</param>
        /// <param name="prosody">音律相关参数</param>
        private string GenerateSsml(string locale, string gender, string name, string text, Prosody prosody)
        {
            var ssmlDoc = new XDocument(
                new XElement("speak",
                    new XAttribute("version", "1.0"),
                    new XAttribute(XNamespace.Xml + "lang", "en-US"),
                    new XElement("break",
                        new XAttribute("time", prosody.time)
                    ),
                    new XElement("voice",
                        new XAttribute(XNamespace.Xml + "lang", locale),
                        new XAttribute(XNamespace.Xml + "gender", gender),
                        new XAttribute("name", name),
                        new XElement("prosody",
                            new XAttribute("rate", prosody.rate),
                            new XAttribute("volume", prosody.volume),
                            new XAttribute("pitch", prosody.pitch),
                            text
                        ) 
                    )
                )
             );
            string lll = ssmlDoc.ToString();
            return ssmlDoc.ToString();
        }

        /// <summary>
        /// 当成功完成TTS请求并有音频可用时调用。
        /// </summary>
        private void AudioAvailable(GenericEventArgs<Stream> e)
        {
            EventHandler<GenericEventArgs<Stream>> handler = this.OnAudioAvailable;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// 发生错误时调用
        /// </summary>
        /// <param name="e">The exception</param>
        private void Error(GenericEventArgs<Exception> e)
        {
            EventHandler<GenericEventArgs<Exception>> handler = this.OnError;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    /// <summary>
    /// 生成事件数据
    /// </summary>
    /// <typeparam name="T">Any type T</typeparam>
    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs(T eventData)
        {
            this.EventData = eventData;
        }

        public T EventData { get; private set; }
    }

}
