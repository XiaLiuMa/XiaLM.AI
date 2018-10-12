using System;
using XiaLM.Weather.source;

namespace XiaLM.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("控制台程序已启动，输入Q/q退出程序！");
            string txt = string.Empty;
            while (!(txt = Console.ReadLine()).ToUpper().Equals("Q"))
            {
                if (txt.ToUpper().Equals("A"))
                {
                    WeatherHelper weatherHelper = new WeatherHelper("yzwniowtxrdeejan");
                    //var info = weatherHelper.GetWeather("武汉");
                    var info = weatherHelper.GetWeather("霍尔果斯");
                }
            }
        }
    }
}
