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
    public class GetTaskSvr : RemoteService<TaskListResponse, string>
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
            string signString = timeSpan + "GET/getTaskList";
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

        protected override TaskListResponse AnalyzeResponse(HttpResult result)
        {
            if (!result.Html.Contains("Request Error"))
            {
                var response = Utils.JSONHelper.ConvertToObject<TaskListResponse>(result.Html);
                return response;
            }
            else
            {
                return null;
            }
        }
    }

    public class TaskListResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TaskResult result { get; set; }
    }

    public class TaskResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<EventTask> taskList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
    }
}

