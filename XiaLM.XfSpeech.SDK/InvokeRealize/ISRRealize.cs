using System;
using System.Threading;
using XiaLM.Tool450.source.common;
using XiaLM.XfSpeech.SDK.Invoke;
using XiaLM.XfSpeech.SDK.Model;

namespace XiaLM.XfSpeech.SDK.InvokeRealize
{
    public class ISRRealize
    {
        private static readonly object lockObj = new object();
        private static ISRRealize iSRRealize;
        public static ISRRealize GetInitialize()
        {
            if (iSRRealize == null)
            {
                lock (lockObj)
                {
                    if (iSRRealize == null)
                    {
                        iSRRealize = new ISRRealize();
                    }
                }
            }
            return iSRRealize;
        }

        /// <summary>
        /// 开始一次语音识别
        /// </summary>
        /// <param name="param"></param>
        /// <param name="eCode"></param>
        /// <returns>sessionId</returns>
        public string ISRSessionBegin(ISRSessionBegin_Param param, ref int eCode)
        {
            //try
            //{
            //    var paramJson = JsonConvert.SerializeObject(param);
            //    var paramStr = paramJson.Replace(':', '=').Replace('"', ' ').Replace('{', ' ').Replace('}', ' ').Trim();
            //    var intptr = ISR_DLL.QISRSessionBegin(paramStr, ref eCode);
            //    string sessioiId = UtilityConverter.IntPtrToString(intptr);
            //    return sessioiId;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            return null;
        }

        /// <summary>
        /// 写入本次识别的音频
        /// </summary>
        /// <param name="sessionId">由QISRSessionBegin返回的句柄</param>
        /// <param name="bytes">音频数据</param>
        /// <returns></returns>
        public bool ISRAudioWrite(string sessionId, byte[] bytes)
        {
            Log.LogHelper.GetInstance().Debug("123");

            IntPtr intPtr = ConvertHelper.BytesToIntPtr(bytes);
            uint dataLen = (uint)bytes.Length;   //音频数据长度,单位字节
            int aStatus = 2;   //告知音频发送是否完成
            int eStatus = -1;   //端点检测器所处的状态
            int rStatus = -1;   //识别状态
            do
            {
                var code = ISR_DLL.QISRAudioWrite(sessionId, intPtr, dataLen, aStatus, ref eStatus, ref rStatus);
                if (code != 0) break;
                if (eStatus == 3) break;
                Thread.Sleep(TimeSpan.FromSeconds(0.16));
            }
            while (true);

            return false;
        }

        /// <summary>
        /// 获取识别结果
        /// </summary>
        /// <param name="sessionID">由QISRSessionBegin返回的句柄</param>
        /// <returns></returns>
        public string ISRGetResult(string sessionID)
        {
            string resultStr = string.Empty;
            int rsltStatus = -1;    //识别结果的状态,0  识别成功 1  识别结束，没有识别结果。 2  正在识别中。  5  识别结束。
            int waitTime = 0;
            int errorCode = -1;
            try
            {
                do
                {
                    string rStr = ISR_DLL.QISRGetResult(sessionID, ref rsltStatus, waitTime, ref errorCode);
                    if (errorCode != 0) break;  //错误时，跳出循环
                    if (rsltStatus != 2)
                    {
                        switch (rsltStatus)
                        {
                            case 0: //识别成功
                                resultStr = rStr;
                                break;
                            case 1: //识别结束，没有识别结果。
                                break;
                            case 5: //识别结束
                                break;
                        }
                        break;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
                while (true);
            }
            catch (Exception ex)
            {

            }
            return resultStr;
        }

        /// <summary>
        /// 结束识别会话
        /// </summary>
        /// <param name="sessionID">会话id</param>
        /// <param name="hints">结束会话的原因</param>
        /// <returns></returns>
        public bool ISRSessionEnd(string sessionId, string hints)
        {
            var code = ISR_DLL.QISRSessionEnd(sessionId, null);
            if (code == 0) return true;
            return false;
        }
    }
}
