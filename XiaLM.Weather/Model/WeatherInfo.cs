using System;
using System.Collections.Generic;

namespace XiaLM.Weather.Model
{
    /// <summary>
    /// 天气
    /// </summary>
    public class WeatherInfo
    {
        public List<WeatherResult> results { get; set; }
    }

    public class WeatherResult
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }
        /// <summary>
        /// 位置
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 时区
        /// </summary>
        public string timezone { get; set; }
        /// <summary>
        /// 时区偏移
        /// </summary>
        public string timezone_offset { get; set; }
        /// <summary>
        /// 天气现象文字
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 天气现象【base64字符串】(图标:将图片读到字节数组，然后base64编码，这样前后端都可以使用)
        /// 前端："<img width="900" height="450" src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGMAAAAqCAYAAA...."/>"直接显示。
        /// 后台：base64解码后直接拿到字节数组。
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 温度，单位为c摄氏度或f华氏度
        /// </summary>
        public string temperature { get; set; }
        /// <summary>
        /// 该城市的本地时间
        /// </summary>
        public DateTime last_update { get; set; }
    }
}
