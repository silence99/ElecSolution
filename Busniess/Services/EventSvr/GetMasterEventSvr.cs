using Business;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using Framework.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml;

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

		public List<Event> GetMasterEventForMainPage()
		{
			List<Event> Events = new List<Event>();
			XmlDocument doc = new XmlDocument();

			doc.Load(@".\xml\Emergence.xml");
			XmlNodeList topM = doc.DocumentElement.ChildNodes;
			foreach (XmlElement item in topM)
			{
				XmlNodeList nodelist = item.ChildNodes;
				var MessageHeader = nodelist[0].InnerText.ToString().Trim();
				var IncidentTime = nodelist[1].InnerText.ToString().Trim();
				var IncidentArea = nodelist[2].InnerText.ToString().Trim();
				var SubmittingUnit = nodelist[3].InnerText.ToString().Trim();
				var EventType = nodelist[4].InnerText.ToString().Trim();
				var EventLevel = nodelist[5].InnerText.ToString().Trim();
				var EventStatus = nodelist[6].InnerText.ToString().Trim();
				var EventSource = nodelist[7].InnerText.ToString().Trim();

				Event me = new Event();
				me.MessageHeader = MessageHeader;
				me.IncidentTime = IncidentTime;
				me.IncidentArea = IncidentArea;
				me.SubmittingUnit = SubmittingUnit;
				me.EventType = EventType;
				me.EventLevel = EventLevel;
				me.EventStatus = EventStatus;
				me.EventSource = EventSource;
				Events.Add(me);
			}

			return Events;
		}

		public List<VideoViewModel> GetVideos()
		{
			return new List<VideoViewModel>()
			{
						 new VideoViewModel()
						 {
							  ImageUri =@"DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri ="DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 }
			};
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
