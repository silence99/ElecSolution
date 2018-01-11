using Emergence.Common.Model;
using Framework.Http;
using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace Business.MainPageSvr
{
	public class WeatherService
	{
		private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private string _weatherData = "weather.json";
		protected string WeatherData
		{
			get { return _weatherData; }
			set { _weatherData = value; }
		}

		public Weather GetWeather()
		{
			var data = string.Empty;
			if (!LocalDataValid())
			{
				try
				{
					var http = new HttpHelper();
					HttpItem httpItem = new HttpItem()
					{
						Method = "GET",
						URL = "http://jisutianqi.market.alicloudapi.com/weather/query?city=安顺",
						Header = new System.Net.WebHeaderCollection()
					};
					httpItem.Header["Authorization"] = "APPCODE 845c52bbefba41829dacc4642147fd58";
					var result = http.GetHtml(httpItem);
					data = result.Html;
					File.WriteAllText(WeatherData, data);
				}
				catch (Exception ex)
				{
					System.Windows.MessageBox.Show("获取天气信息出错，请联系管理员。");
					Logger.Fatal("获取天气信息出错，请联系管理员。", ex);
					if (File.Exists(WeatherData))
					{
						data = File.ReadAllText(WeatherData);
					}
				}
			}
			else
			{
				if (File.Exists(WeatherData))
				{
					data = File.ReadAllText(WeatherData);
				}
			}

			var response = Utils.JSONHelper.ConvertToObject<WeatherApiModel>(data);
			return response.Weather;
		}

		private bool LocalDataValid()
		{
			var info = new FileInfo(WeatherData);
			var span = DateTime.Now - info.LastWriteTime;

			return span.Minutes <= 14;
		}
	}

	public class WeatherApiModel
	{

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("msg")]
		public string Msg { get; set; }

		[JsonProperty("result")]
		public Weather Weather { get; set; }
	}
}
