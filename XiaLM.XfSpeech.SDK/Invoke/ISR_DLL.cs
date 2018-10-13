namespace XiaLM.XfSpeech.SDK.Invoke
{
    /// <summary>
    /// ISR(语音识别)
    /// </summary>
    public partial class ISR_DLL
    {
        private const string dllPath = @"\Lib\msc.dll";    //dll路径

        /// <summary>
        /// 构建语法回掉函数委托
        /// </summary>
        /// <param name="param0"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public delegate int GrammarCallBack(int param0, string param1, System.IntPtr param2);
        /// <summary>
        /// 更新本地语法词典回掉函数委托
        /// </summary>
        /// <param name="param0"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public delegate int LexiconCallBack(int param0, string param1, System.IntPtr param2);

        /// <summary>
        /// 开始一次语音识别
        /// </summary>
        /// <param name="grammarList"></param>
        /// <param name="params"></param>
        /// <param name="errorCode"></param>
        /// <returns>sessionId</returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRSessionBegin")]
        public static extern string QISRSessionBegin(string grammarList, string @params, ref int errorCode);


        /// <summary>
        /// 写入本次识别的音频
        /// </summary>
        /// <param name="sessionID">由QISRSessionBegin返回的句柄</param>
        /// <param name="waveData">音频数据</param>
        /// <param name="waveLen">音频数据长度,单位字节</param>
        /// <param name="audioStatus">告知音频发送是否完成{1  第一块音频 ,2  还有后继音频  ,4  最后一块音频 }</param>
        /// <param name="epStatus">端点检测器所处的状态{0  还没有检测到音频的前端点 ,1  已经检测到了音频前端点，正在进行正常的音频处理。 3  检测到音频的后端点，后继的音频会被忽略, 4  超时。 5  出现错误。  6  音频过大。  }</param>
        /// <param name="recogStatus">0  识别成功，此时用户可以调用QISRGetResult来获取（部分）结果。 1  识别结束，没有识别结果。 2  正在识别中。  5  识别结束。 </param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRAudioWrite")]
        public static extern int QISRAudioWrite(string sessionID, System.IntPtr waveData, uint waveLen, int audioStatus, ref int epStatus, ref int recogStatus);


        /// <summary>
        /// 获取识别结果
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="rsltStatus">0  识别成功，此时用户可以调用QISRGetResult来获取（部分）结果。 1  识别结束，没有识别结果。 2  正在识别中。  5  识别结束。</param>
        /// <param name="waitTime"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRGetResult")]
        //public static extern System.IntPtr QISRGetResult(string sessionID, ref int rsltStatus, int waitTime, ref int errorCode);
        public static extern string QISRGetResult(string sessionID, ref int rsltStatus, int waitTime, ref int errorCode);


        /// <summary>
        /// 结束本次语音识别
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="hints"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRSessionEnd")]
        public static extern int QISRSessionEnd(string sessionID, string hints);


        /// <summary>
        /// 获取当次语音识别信息，如上行流量、下行流量等
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="valueLen"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRGetParam")]
        public static extern int QISRGetParam(string sessionID, string paramName, System.IntPtr paramValue, ref uint valueLen);


        /// <summary>
        /// 构建语法，生成语法ID
        /// </summary>
        /// <param name="grammarType"></param>
        /// <param name="grammarContent"></param>
        /// <param name="grammarLength"></param>
        /// <param name="params"></param>
        /// <param name="callback"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRBuildGrammar")]
        public static extern int QISRBuildGrammar(string grammarType, string grammarContent, uint grammarLength, string @params, GrammarCallBack callback, System.IntPtr userData);


        /// <summary>
        /// 更新本地语法词典
        /// </summary>
        /// <param name="lexiconName"></param>
        /// <param name="lexiconContent"></param>
        /// <param name="lexiconLength"></param>
        /// <param name="params"></param>
        /// <param name="callback"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "QISRUpdateLexicon")]
        public static extern int QISRUpdateLexicon(string lexiconName, string lexiconContent, uint lexiconLength, string @params, LexiconCallBack callback, System.IntPtr userData);

    }

}
