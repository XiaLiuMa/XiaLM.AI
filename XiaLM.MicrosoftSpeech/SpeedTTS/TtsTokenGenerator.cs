using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace XiaLM.MicrosoftSpeech.SpeedTTS
{
    /// <summary>
    /// TTS的Token生成器
    /// </summary>
    public class TtsTokenGenerator
    {
        private string accessToken;
        private Timer accessTokenRenewer;   //定时器
        private string apiKey = string.Empty;   //密钥
        //private string accessUri = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";   //生成Token的地址
        private string accessUri = "https://eastasia.api.cognitive.microsoft.com/sts/v1.0/issueToken";   //生成Token的地址
        private const int RefreshTokenDuration = 9; //令牌每10分钟过期一次。每9分钟更换一次

        public TtsTokenGenerator(string apikey)
        {
            this.apiKey = apikey;
            this.accessToken = HttpPost(this.accessUri, this.apiKey);
            accessTokenRenewer = new Timer(
                new TimerCallback(OnTokenExpiredCallback),
                this,
                TimeSpan.FromMinutes(RefreshTokenDuration),
                TimeSpan.FromMilliseconds(-1));
        }

        public string GetAccessToken()
        {
            return this.accessToken;
        }

        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                try
                {
                    accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        private void RenewAccessToken()
        {
            this.accessToken = HttpPost(this.accessUri, this.apiKey);
        }



        private string HttpPost(string accessUri, string apiKey)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(accessUri);
                webRequest.Method = "POST";
                webRequest.ContentLength = 0;
                webRequest.Headers["Ocp-Apim-Subscription-Key"] = apiKey;

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (Stream stream = webResponse.GetResponseStream())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            byte[] waveBytes = null;
                            int count = 0;
                            do
                            {
                                byte[] buf = new byte[1024];
                                count = stream.Read(buf, 0, 1024);
                                ms.Write(buf, 0, count);
                            } while (stream.CanRead && count > 0);

                            waveBytes = ms.ToArray();
                            return Encoding.UTF8.GetString(waveBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }


}
