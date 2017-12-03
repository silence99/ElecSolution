using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Framework.Http;
using Newtonsoft.Json;

namespace Busniess.Services.EventSvr
{
    public class GetSubEventSvr : RemoteService<SubEventListResponse, string>
    {
        private string _url;
        private string _requestData;

        public string Url { get => _url; }
        public string RequestData { get => _requestData; set => _requestData = value; }

        protected override void InitializeHttpRequest(string url)
        {
            HttpHelper = new HttpHelper();
            _url = url;
            if (string.IsNullOrEmpty(RequestData))
            {
                RequestData = "";
            }
        }

        protected override HttpResult HttpReqeust()
        {
            var hd = new System.Net.WebHeaderCollection();
            string timeSpan = TimeControl.GenerateTimeStamp();
            string signString = timeSpan + "GET/getChildEventList";
            string authorization = AuthorizationControl.GetAuthorization(signString);
            //Add headers
            hd.Add("X-Project-Date", timeSpan);
            hd.Add("Authorization", authorization);
            HttpItem httpItem = new HttpItem()
            {
                Method = "GET",
                URL = Url + RequestData,
                Header = hd
            };
            return HttpHelper.GetHtml(httpItem);
        }

        protected override SubEventListResponse AnalyzeResponse(HttpResult result)
        {
            if(!result.Html.Contains("Request Error"))
            { 
            var response = Utils.JSONHelper.ConvertToObject<SubEventListResponse>(result.Html);
            return response;
            }
            else
            {
                return null;
            }
        }
    }

    public class SubEventListResponse
    {

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public SubEventListResult Result { get; set; }
    }

    public class SubEventListResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public SubEvent[] SubEventList { get; set; }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
