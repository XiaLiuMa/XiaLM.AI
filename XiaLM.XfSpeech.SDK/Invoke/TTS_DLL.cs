using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaLM.XfSpeech.SDK.Invoke
{
    /// <summary>
    /// TTS(语音合成)
    /// </summary>
    public partial class TTS_DLL
    {
        private const string dllPath = @"\Lib\msc.dll";    //dll路径

        /// <summary>
        /// 开始一次语音合成，分配语音合成资源
        /// </summary>
        /// <param name="params"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QTTSSessionBegin")]
        public static extern System.IntPtr QTTSSessionBegin(string @params, ref int errorCode);

        /// <summary>
        /// 写入要合成的文本
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="textString"></param>
        /// <param name="textLen"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QTTSTextPut")]
        public static extern int QTTSTextPut(string sessionID, string textString, uint textLen, string @params);

        /// <summary>
        /// 获取合成音频
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="audioLen"></param>
        /// <param name="synthStatus"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QTTSAudioGet")]
        public static extern System.IntPtr QTTSAudioGet(string sessionID, ref uint audioLen, ref int synthStatus, ref int errorCode);

        /// <summary>
        /// 获取当前语音合成信息，如当前合成音频对应文本结束位置、上行流量、下行流量等
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="valueLen"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QTTSGetParam")]
        public static extern int QTTSGetParam(string sessionID, string paramName, string paramValue, ref int valueLen);

        /// <summary>
        /// 结束本次语音合成
        /// </summary>
        /// <param name="sessionID">由QTTSSessionBegin返回的句柄</param>
        /// <param name="hints">结束本次语音合成的原因描述，为用户自定义内容</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QTTSSessionEnd")]
        public static extern int QTTSSessionEnd(string sessionID, string hints);

    }
}
