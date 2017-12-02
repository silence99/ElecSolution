using Emergence.Common.Model;
using Framework.Http;
using Newtonsoft.Json;

namespace Business.MainPageSvr
{
    public class WeatherService : RemoteService<Weather, object>
    {
        protected override void InitializeHttpRequest(object request)
        {
            HttpHelper = new HttpHelper();
        }

        protected override HttpResult HttpReqeust()
        {

            HttpItem httpItem = new HttpItem()
            {
                Method = "GET",
                URL = "http://jisutianqi.market.alicloudapi.com/weather/query?city=安顺",
                Header = new System.Net.WebHeaderCollection()
            };
            httpItem.Header["Authorization"] = "APPCODE 845c52bbefba41829dacc4642147fd58";
            return HttpHelper.GetHtml(httpItem);
        }

        protected override Weather AnalyzeResponse(HttpResult result)
        {
            var response = Utils.JSONHelper.ConvertToObject<WeatherApiModel>(result.Html);
            return response.Weather;
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
