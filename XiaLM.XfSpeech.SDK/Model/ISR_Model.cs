using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaLM.XfSpeech.SDK.Model
{
    /// <summary>
    /// ISR(语音识别)会话参数
    /// </summary>
    public class ISRSessionBegin_Param
    {
        /// <summary>
        /// 引擎类型
        /// cloud：在线引擎，local：离线引擎，mixed：混合引擎，默认为cloud
        /// </summary>
        public string engine_type { get; set; } = "cloud";
        /// <summary>
        /// 本次识别请求的类型 
        /// iat:连续语音识别,asr:语法、关键词识别。默认为iat 
        /// </summary>
        public string sub { get; set; } = "iat";
        /// <summary>
        /// 语言
        /// 可取值：zh_cn：简体中文，en_us：英文,默认值：zh_cn
        /// </summary>
        public string language { get; set; } = "zh_cn";
        /// <summary>
        /// 领域 
        /// iat：连续语音识别,asr：语法、关键词识别,search：热词,video：视频,poi：地名,music：音乐,默认为iat。
        /// 注意：sub=asr时，domain只能为asr 
        /// </summary>
        public string domain { get; set; } = "iat";
        /// <summary>
        /// 语言区域
        /// mandarin：普通话,cantonese：粤语,lmz：四川话
        /// </summary>
        public string accent { get; set; } = "mandarin";
        /// <summary>
        /// 音频采样率
        /// 音频采样率，支持参数，16000，8000，默认为16000
        /// </summary>
        public int sample_rate { get; set; } = 16000;
        /// <summary>
        /// 识别门限
        /// 离线语法识别结果门限值，设置只返回置信度得分大于此门限值的结果,可取值：0~100，默认值：0 
        /// </summary>
        public int asr_threshold { get; set; } = 0;
        /// <summary>
        /// 是否开启降噪功能
        /// 可取值：0：不开启，1：开启,默认不开启
        /// </summary>
        public int asr_denoise { get; set; } = 0;
        /// <summary>
        /// 离线识别资源路径
        /// 离线识别资源所在路径，格式如下：access_type1|file_info1|[offset1]|[length1];access_type2|file_info2|[offset2]|[length2]
        /// 各字段含义如下：
        /// access_type：文件访问方式，支持路径方式（fo）和文件描述符方式（fd）；
        /// file_info：此字段和access_type 对应，文件路径对应fo，文件描述符对应fd， 
        /// </summary>
        public string asr_res_path { get; set; }
        /// <summary>
        /// 离线语法生成路径
        /// 构建离线语法所生成数据的保存路径（文件夹）
        /// </summary>
        public string grm_build_path { get; set; }
        /// <summary>
        /// 结果格式
        /// 可取值：plain，json,默认值：plain 
        /// </summary>
        public string result_type { get; set; } = "plain";
        /// <summary>
        /// 文本编码格式 
        /// 表示参数中携带的文本编码格式
        /// </summary>
        public string text_encoding { get; set; } = "UTF-8";
        /// <summary>
        /// 离线语法id 
        /// 构建离线语法后获得的语法ID 
        /// </summary>
        public string local_grammar { get; set; }
        /// <summary>
        /// 在线语法id 
        /// 构建在线语法后获得的语法ID 
        /// </summary>
        public string cloud_grammar { get; set; }
        /// <summary>
        /// 混合引擎策略类型(仅engine_type=mixed时生效)
        /// 可取值： realtime：实时，同时使用在线引擎和离线引擎，在在线引擎结果超时的情况下，使用离线引擎结果；
        /// delay：延时，先使用在线引擎，当在线引擎结果超时后自动转向离线引擎。
        /// 默认值：realtime 
        /// </summary>
        public string mixed_type { get; set; } = "realtime";
        /// <summary>
        /// 在线引擎结果超时时间
        /// (仅engine_type=mixed时生效)
        /// 使用混合引擎情况下，在线引擎结果超时时间。默认值：2000，单位：ms 
        /// </summary>
        public int mixed_timeout { get; set; } = 2000;
        /// <summary>
        /// 混合门限(仅engine_type=mixed时生效)
        /// 混合策略为realtime 时使用，当离线引擎给出识别结果大于此门限值时直接给出离线结果，
        /// 否则等待在线结果，若在线结果超时则给出离线结果。
        /// 可取值：0~100，默认值：60 
        /// </summary>
        public int mixed_threshold { get; set; } = 2000;




        /// <summary>
        /// 识别结果字符串所用编码格式 
        /// GB2312;UTF-8;UNICODE,不同的格式支持不同的编码：
        /// plain:UTF-8,GB2312
        /// json:UTF-8 
        /// </summary>
        public string result_encoding { get; set; } = "UTF-8";
    }
}
