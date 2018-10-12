using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using XiaLM.Tool450.source.common;
using XiaLM.Weather.Model;
using XiaLM.Weather.Properties;

namespace XiaLM.Weather.source
{
    public class WeatherHelper
    {
        /// <summary>
        /// 密钥
        /// </summary>
        public string apikey { set; private get; }
        /// <summary>
        /// 语种(默认：中文("zh-Hans"))
        /// </summary>
        public string language { set; private get; } = "zh-Hans";
        public WeatherHelper(string _apikey)
        {
            this.apikey = _apikey;
        }

        public WeatherInfo GetWeather(string city)
        {
            WeatherInfo weather = GetSeniverseWeather(city);
            if (weather == null) weather = GetJirenguWeather(city);
            return weather;
        }

        /// <summary>
        /// 获取心知天气
        /// </summary>
        /// <param name="city">城市</param>
        /// <returns></returns>
        private WeatherInfo GetSeniverseWeather(string city)
        {
            if (string.IsNullOrEmpty(city)) return null;
            WeatherInfo weatherInfo = new WeatherInfo() { results = new List<WeatherResult>() };
            try
            {
                string requestUrl = "https://api.seniverse.com/v3/weather/now.json?";
                string param = $"key={apikey}&location={city}&language={language}&unit=c";
                requestUrl += param;
                string response = HttpClientHelper.Get(requestUrl);
                var seniverseWeather = SerializeHelper.SerializeJsonToObject<SeniverseWeather>(response);
                if (seniverseWeather == null) return null;
                if (seniverseWeather.results == null || seniverseWeather.results.Length <= 0) return null;
                foreach (var item in seniverseWeather.results)
                {
                    WeatherResult weatherResult = new WeatherResult();
                    weatherResult.country = item.location.country;
                    weatherResult.id = item.location.id;
                    weatherResult.name = item.location.name;
                    weatherResult.path = item.location.path;
                    weatherResult.timezone = item.location.timezone;
                    weatherResult.timezone_offset = item.location.timezone_offset;
                    weatherResult.last_update = item.last_update;
                    weatherResult.text = item.now.text;
                    weatherResult.temperature = item.now.temperature + "℃";
                    string code = item.now.code;
                    byte[] bytes = FileReadWriteHelper.ReadBytesFromBitmap(GetBitmap(code));
                    weatherResult.icon = Base64Helper.Encoding(bytes);
                    weatherInfo.results.Add(weatherResult);
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteError(ex);
            }
            return weatherInfo;
        }

        /// <summary>
        /// 获取Jirengu天气(备用查询天气方案(地级市也可查询))
        /// </summary>
        /// <param name="city">城市</param>
        /// <returns></returns>
        private WeatherInfo GetJirenguWeather(string city)
        {
            if (string.IsNullOrEmpty(city)) return null;
            WeatherInfo weatherInfo = new WeatherInfo() { results = new List<WeatherResult>() };
            try
            {
                string requestUrl = $"http://api.jirengu.com/getWeather.php?city={city}";
                string response = HttpClientHelper.Get(requestUrl);
                var jirenguWeather = SerializeHelper.SerializeJsonToObject<JirenguWeather>(response);
                if (jirenguWeather == null) return null;
                if (jirenguWeather.results == null || jirenguWeather.results.Length <= 0) return null;
                foreach (var item in jirenguWeather.results)
                {
                    int i = 0;
                    WeatherResult weatherResult = new WeatherResult();
                    weatherResult.path = item.currentCity;
                    weatherResult.text = item.weather_data[i].weather;

                    string tempStr = item.weather_data[i].date;
                    Regex reg = new Regex(@"实时：(.+)℃\)");
                    Match match = reg.Match(tempStr);
                    string temperature = match.Groups[1].Value;
                    weatherResult.temperature = temperature + "℃";
                    string code = GetWeatherCode(item.weather_data[i].weather);
                    byte[] bytes = FileReadWriteHelper.ReadBytesFromBitmap(GetBitmap(code));
                    weatherResult.icon = Base64Helper.Encoding(bytes);
                    weatherInfo.results.Add(weatherResult);
                    i++;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
            }
            return weatherInfo;
        }

        private string GetWeatherCode(string weather)
        {
            if (weather.Equals("晴")) return "0";
            if (weather.Equals("多云")) return "4";
            if (weather.Equals("晴间多云")) return "5";
            if (weather.Equals("大部多云")) return "8";
            if (weather.Equals("阴")) return "9";
            if (weather.Equals("阵雨")) return "10";
            if (weather.Equals("雷阵雨")) return "11";
            if (weather.Equals("雷阵雨伴有冰雹")) return "12";
            if (weather.Equals("小雨")) return "13";
            if (weather.Equals("中雨")) return "14";
            if (weather.Equals("大雨")) return "15";
            if (weather.Equals("暴雨")) return "16";
            if (weather.Equals("大暴雨")) return "17";
            if (weather.Equals("特大暴雨")) return "18";
            if (weather.Equals("冻雨")) return "19";
            if (weather.Equals("雨夹雪")) return "20";
            if (weather.Equals("阵雪")) return "21";
            if (weather.Equals("小雪")) return "22";
            if (weather.Equals("中雪")) return "23";
            if (weather.Equals("大雪")) return "24";
            if (weather.Equals("暴雪")) return "25";
            if (weather.Equals("浮尘")) return "26";
            if (weather.Equals("扬沙")) return "27";
            if (weather.Equals("沙尘暴")) return "28";
            if (weather.Equals("强沙尘暴")) return "29";
            if (weather.Equals("雾")) return "30";
            if (weather.Equals("霾")) return "31";
            if (weather.Equals("风")) return "32";
            if (weather.Equals("大风")) return "33";
            if (weather.Equals("飓风")) return "34";
            if (weather.Equals("热带风暴")) return "35";
            if (weather.Equals("龙卷风")) return "36";
            if (weather.Equals("冷")) return "37";
            if (weather.Equals("热")) return "38";
            return "99";
        }

        private Bitmap GetBitmap(string code)
        {
            Resource rec = new Resource();
            var propertyInfos = rec.GetType().GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo item in propertyInfos)
            {
                if (item.Name.Equals("_" + code))
                {
                    Bitmap b = item.GetValue(rec, null) as Bitmap;
                    return b;
                }
            }
            return null;
        }
    }
}
