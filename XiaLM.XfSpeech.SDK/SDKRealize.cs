using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using XiaLM.Tool450.source.common;
using XiaLM.XfSpeech.SDK.Invoke;
using XiaLM.XfSpeech.SDK.InvokeRealize;
using XiaLM.XfSpeech.SDK.Model;

namespace XiaLM.XfSpeech.SDK
{
    /// <summary>
    /// SDK库的实现
    /// </summary>
    public class SDKRealize
    {
        public bool IsLogin { get; set; } = false;  //是否是登录状态
        public string TTSdir { get; set; } = string.Empty;  //合成音频保存文件夹
        private static readonly object lockObj = new object();
        private static SDKRealize sDKRealize;
        public static SDKRealize GetInitialize()
        {
            if (sDKRealize == null)
            {
                lock (lockObj)
                {
                    if (sDKRealize == null)
                    {
                        sDKRealize = new SDKRealize();
                    }
                }
            }
            return sDKRealize;
        }
        public SDKRealize()
        {

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Wav"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Wav");
            }
            TTSdir = AppDomain.CurrentDomain.BaseDirectory + @"Wav\";
        }

        /// <summary>
        /// 初始化msc，用户登录
        /// </summary>
        /// <returns></returns>
        public bool MSPLogin()
        {
            try
            {
                string usr = string.Empty;
                string pwd = string.Empty;
                string param = "appid = 5b065027, work_dir = .";    //登录参数,appid与msc库绑定,请勿随意改动
                var code = MSP_CMN_DLL.MSPLogin(usr, pwd, param);
                if (code == 0)
                {
                    IsLogin = true;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// TTS退出登录
        /// </summary>
        /// <returns></returns>
        public bool MSPLogout()
        {
            var code = MSP_CMN_DLL.MSPLogout();
            if (code == 0)
            {
                IsLogin = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 开始一次语音合成，分配语音合成资源。
        /// </summary>
        /// <param name="param"></param>
        /// <param name="eCode"></param>
        /// <returns>sessionId</returns>
        public string TTSSessionBegin(TTSSessionBegin_Param param, ref int eCode)
        {
            try
            {
                var paramJson = SerializeHelper.SerializeObjectToJson(param);
                var paramStr = paramJson.Replace(':', '=').Replace('"', ' ').Replace('{', ' ').Replace('}', ' ').Trim();
                var intptr = TTS_DLL.QTTSSessionBegin(paramStr, ref eCode);
                string sessioiId = ConvertHelper.IntPtrToString(intptr);
                return sessioiId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 写入要合成的文本
        /// </summary>
        /// <param name="sessionId">会话id</param>
        /// <param name="txt">要合成的文本</param>
        /// <param name="txtLen">文本的字节长度,最大支持8192个字节（不含’\0’）</param>
        /// <param name="param">目前为空</param>
        /// <returns></returns>
        public bool TTSTextPut(string sessionId, string txt, uint txtLen, string param)
        {
            var code = TTS_DLL.QTTSTextPut(sessionId, txt, txtLen, null);
            if (code == 0) return true;
            return false;
        }

        /// <summary>
        /// 获取合成的音频
        /// </summary>
        /// <param name="sessionID">会话id</param>
        /// <param name="audioLen">合成的音频字节长度</param>
        /// <param name="synthStatus">合成音频状态，可能的值如下：1：音频还没取完，还有后继的音频；2：音频已经取完</param>
        /// <param name="errorCode">返回的错误码</param>
        /// <returns></returns>
        public byte[] TTSAudioGet(string sessionID, ref uint audioLen, ref int synthStatus, ref int errorCode)
        {
            byte[] result = null;
            var head = TTSRealize.GetInitialize().GetWavHead();  //音频头
            try
            {
                do
                {
                    var intPtr = TTS_DLL.QTTSAudioGet(sessionID, ref audioLen, ref synthStatus, ref errorCode);
                    if (errorCode != 0) break;  //错误时，跳出循环
                    if (intPtr != null && intPtr != IntPtr.Zero)
                    {
                        head.DATA_Size += (int)audioLen;
                        var source = ConvertHelper.IntPtrToBytes(intPtr, (int)audioLen);
                        if (result == null)
                        {
                            result = new byte[audioLen];
                            source.CopyTo(result, 0);
                        }
                        else
                        {
                            var tempBytes = result;
                            result = new byte[tempBytes.Length + audioLen];
                            tempBytes.CopyTo(result, 0);
                            source.CopyTo(result, tempBytes.Length);
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
                while (synthStatus != 2);
                head.File_Size += head.DATA_Size + (Marshal.SizeOf(head) - 8);
                if (result != null)
                {
                    var tempBytes = result;
                    byte[] headBytes = ConvertHelper.StructToBytes(head);
                    result = new byte[headBytes.Length + tempBytes.Length];
                    headBytes.CopyTo(result, 0);
                    tempBytes.CopyTo(result, headBytes.Length);
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 结束合成会话
        /// </summary>
        /// <param name="sessionID">会话id</param>
        /// <param name="hints">结束会话的原因</param>
        /// <returns></returns>
        public bool TTSSessionEnd(string sessionId, string hints)
        {
            var code = TTS_DLL.QTTSSessionEnd(sessionId, null);
            if (code == 0) return true;
            return false;
        }
    }
}
