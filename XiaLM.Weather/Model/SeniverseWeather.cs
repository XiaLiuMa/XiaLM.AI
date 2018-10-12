using System;

namespace XiaLM.Weather.Model
{
    /// <summary>
    /// 天气
    /// </summary>
    public class SeniverseWeather
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public Location location { get; set; }
        public Now now { get; set; }
        public DateTime last_update { get; set; }   //该城市的本地时间
    }

    public class Location
    {
        public string id { get; set; }
        public string name { get; set; }    //城市名称
        public string country { get; set; } //国家
        public string path { get; set; }    //位置
        public string timezone { get; set; }    //时区
        public string timezone_offset { get; set; } //时区偏移
    }

    public class Now
    {
        public string text { get; set; }    //天气现象文字
        public string code { get; set; }    //天气现象代码
        public string temperature { get; set; }  //温度，单位为c摄氏度或f华氏度
    }
}
