namespace XiaLM.XfSpeech.SDK.Invoke
{
    /// <summary>
    /// SDK公用接口
    /// </summary>
    public partial class MSP_CMN_DLL
    {
        private const string dllPath = @"\Lib\msc.dll";    //dll路径

        /// <summary>
        /// 初始化msc，用户登录
        /// </summary>
        /// <param name="usr">用户名：此参数保留，传入NULL即可。</param>
        /// <param name="pwd">密码：此参数保留，传入NULL即可。</param>
        /// <param name="param">登录参数,appid与msc库绑定,请勿随意改动</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "MSPLogin")]
        public static extern int MSPLogin(string usr, string pwd, string @params);


        /// <summary>
        /// 用户数据上传
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="data"></param>
        /// <param name="dataLen"></param>
        /// <param name="params"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "MSPUploadData")]
        public static extern string MSPUploadData(string dataName, System.IntPtr data, uint dataLen, string @params, ref int errorCode);


        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "MSPLogout")]
        public static extern int MSPLogout();


        /// <summary>
        /// 参数设置接口、离线引擎初始化接口
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "MSPSetParam")]
        public static extern int MSPSetParam(string paramName, string paramValue);


        /// <summary>
        /// 获取MSC的设置信息
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="valueLen"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "MSPGetParam")]
        public static extern int MSPGetParam(string paramName, System.IntPtr paramValue, ref uint valueLen);


        /// <summary>
        /// 获取MSC或本地引擎版本信息
        /// </summary>
        /// <param name="verName"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImportAttribute(dllPath, EntryPoint = "MSPGetVersion")]
        public static extern System.IntPtr MSPGetVersion(string verName, ref int errorCode);

    }

}
