using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Framework.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DictSvr : RemoteService<List<DictItem>, string>
    {
        private string _url;
        private string _requestData;

        public string Url { get => _url; }
        public string RequestData { get => _requestData; set => _requestData = value; }

        protected override void InitializeHttpRequest(string url)
        {
            HttpHelper = new HttpHelper();
            _url = "http://106.14.215.193/test/dict?v=v1.0";
            RequestData = "";
        }

        protected override HttpResult HttpReqeust()
        {
            var hd = new System.Net.WebHeaderCollection();
            string timeSpan = TimeControl.GenerateTimeStamp();
            string signString = timeSpan + "GET/dict";
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

        protected override List<DictItem> AnalyzeResponse(HttpResult result)
        {
            var response = Utils.JSONHelper.ConvertToObject<List<DictItem>>(result.Html);
            return response;
        }
    }
}
