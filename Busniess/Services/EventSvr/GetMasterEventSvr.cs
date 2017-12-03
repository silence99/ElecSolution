using Business;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Framework.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess.Services.EventSvr
{
    public class GetMasterEventSvr : RemoteService<MasterEventListResponse, string>
    {
        private string _url;
        private string _requestData;

        public string Url { get => _url; }
        public string RequestData { get => _requestData; set => _requestData = value; }

        protected override void InitializeHttpRequest(string url)
        {
            HttpHelper = new HttpHelper();
            _url = url;
            RequestData = "";
        }

        protected override HttpResult HttpReqeust()
        {
            var hd = new System.Net.WebHeaderCollection();
            string timeSpan = TimeControl.GenerateTimeStamp();
            string signString = timeSpan + "GET/getMainEventList";
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

        protected override MasterEventListResponse AnalyzeResponse(HttpResult result)
        {
            var response = Utils.JSONHelper.ConvertToObject<MasterEventListResponse>(result.Html);
            return response;
        }
    }

    public class MasterEventListResponse
    {

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public MasterEventListResult Result { get; set; }
    }

    public class MasterEventListResult
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("data")]
        public MasterEvent[] MasterEventList { get; set; }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
    }
}
