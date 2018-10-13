using System;
using System.Runtime.InteropServices;
using System.Threading;
using XiaLM.Tool450.source.common;
using XiaLM.XfSpeech.SDK.Invoke;
using XiaLM.XfSpeech.SDK.Model;

namespace XiaLM.XfSpeech.SDK.InvokeRealize
{
    public class TTSRealize
    {
        private static readonly object lockObj = new object();
        private static TTSRealize tTSRealize;
        public static TTSRealize GetInitialize()
        {
            if (tTSRealize == null)
            {
                lock (lockObj)
                {
                    if (tTSRealize == null)
                    {
                        tTSRealize = new TTSRealize();
                    }
                }
            }
            return tTSRealize;
        }

        /// <summary>
        /// 获取文件头
        /// </summary>
        /// <returns></returns>
        public Wave_Pcm_Head GetWavHead()
        {
            Wave_Pcm_Head wav = new Wave_Pcm_Head()
            {
                RIFF_ID = ConvertHelper.StringToInt("RIFF"),
                File_Size = 0,
                RIFF_Type = ConvertHelper.StringToInt("WAVE"),
                FMT_ID = ConvertHelper.StringToInt("fmt "),
                FMT_Size = 16,
                FMT_Tag = 1,
                FMT_Channel = 1,
                FMT_SamplesPerSec = 16000,
                AvgBytesPerSec = 32000,
                BlockAlign = 2,
                BitsPerSample = 16,
                DATA_ID = ConvertHelper.StringToInt("data"),
                DATA_Size = 0
            };
            return wav;
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
