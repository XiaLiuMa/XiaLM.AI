using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaLM.XfSpeech.WebApi.Model
{
    /// <summary>
    /// 语音识别结果
    /// </summary>
    public class IatResult
    {
        /// <summary>
        /// 结果码(具体见错误码)
        /// 0：成功
        /// 10105：没有权限
        /// 10106：无效参数
        /// 10107：非法参数值
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 语音识别后文本结果
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// sid
        /// </summary>
        public string sid { get; set; }
    }
}
