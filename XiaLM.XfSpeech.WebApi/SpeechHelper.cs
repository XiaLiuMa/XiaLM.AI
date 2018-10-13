using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XiaLM.Tool450.source.common;
using XiaLM.XfSpeech.WebApi.Model;

namespace XiaLM.XfSpeech.WebApi
{
    /// <summary>
    /// 讯飞WebApi语音助手
    /// </summary>
    public class SpeechHelper
    {

        public string appId { get; set; }   //应用id
        public string iatKey { get; set; }  //在线识别服务密钥
        public string ttsKey { get; set; }  //在线合成服务密钥
        private static readonly object lockObj = new object();
        private static SpeechHelper instance;
        public static SpeechHelper GetInstance()
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new SpeechHelper();
                    }
                }
            }
            return instance;
        }
        public SpeechHelper()
        {
            appId = "5adf1293";
            iatKey = "27ba5964488a5847192d2fa9ac638943";
            ttsKey = "621f7af6891b44f0e0bb395967efb9ef";
        }

        /// <summary>
        /// 讯飞WebAPI语音识别
        /// </summary>
        /// <param name="bArray"></param>
        /// <returns></returns>
        public IatResult XunFeiIAT(byte[] bytes)
        {
            string requestURL = "http://api.xfyun.cn/v1/service/v1/iat";
            HttpClient http = new HttpClient();
            try
            {
                IatParam iatParam = new IatParam
                {
                    engine_type = "sms16k",
                    aue = "raw"
                };
                var iatJson = Base64Helper.ToBase64(SerializeHelper.SerializeObjectToJson(iatParam));
                var curTime = EncryptHelper.Get1970ToNowSeconds().ToString();
                var checkSum = EncryptHelper.Md5Encryp(iatKey + curTime + iatJson);
                var content = new StringContent("audio=" + HttpUtility.UrlEncode(Base64Helper.ToBase64(bytes))); //数据
                content.Headers.Add("X-Appid", appId);
                content.Headers.Add("X-CurTime", curTime);
                content.Headers.Add("X-Param", iatJson);
                content.Headers.Add("X-CheckSum", checkSum);
                content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                using (var response = http.PostAsync(requestURL, content).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string responseStr = response.Content.ReadAsStringAsync().Result;
                    var iatResult = SerializeHelper.SerializeJsonToObject<IatResult>(responseStr);
                    return iatResult;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
            return null;
        }

        /// <summary>
        /// 讯飞WebAPI语音合成
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public byte[] XunFeiTTS(string txt)
        {
            string requestURL = "http://api.xfyun.cn/v1/service/v1/tts";
            HttpClient http = new HttpClient();
            try
            {
                TtsParam ttsParam = new TtsParam
                {
                    auf = "audio/L16;rate=16000",
                    aue = "raw",
                    voice_name = "xiaoyan",
                    speed = "50",
                    volume = "50",
                    pitch = "50",
                    engine_type = "intp65",
                    text_type = "text"
                };
                var ttsJson = Base64Helper.ToBase64(SerializeHelper.SerializeObjectToJson(ttsParam));
                var curTime = EncryptHelper.Get1970ToNowSeconds().ToString();
                var checkSum = EncryptHelper.Md5Encryp(ttsKey + curTime + ttsJson);
                var content = new StringContent("text=" + HttpUtility.UrlEncode(txt));
                content.Headers.Add("X-CurTime", curTime);
                content.Headers.Add("X-Param", ttsJson);
                content.Headers.Add("X-Appid", appId);
                content.Headers.Add("X-CheckSum", checkSum);
                content.Headers.Add("X-Real-Ip", "127.0.0.1");
                content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                using (var response = http.PostAsync(requestURL, content).Result)
                {
                    if (response.Content.Headers.ContentType.MediaType.Equals("audio/mpeg"))
                    {
                        //合成成功
                        response.EnsureSuccessStatusCode();
                        return response.Content.ReadAsByteArrayAsync().Result;
                    }
                    else
                    {
                        //合成失败
                        var str = response.Content.ReadAsStringAsync().Result;
                        var result = SerializeHelper.SerializeJsonToObject<IatResult>(str);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
            return null;
        }
    }
}
