using Busniess.CommonControl;
using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using Framework.Http;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;
using System.Xml;

namespace Busniess.Services.EventSvr
{
	public class MasterEventService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private HttpHelper HttpHelper = new HttpHelper();

		public MasterEventListResponse GetMasterEvents(int pageIndex, int pageSize)
		{
			return GetMasterEvents(pageIndex, pageSize, string.Empty, default(DateTime), default(DateTime), string.Empty);
		}

		public MasterEventListResponse GetMasterEvents(int pageIndex, int pageSize, string title,
			DateTime start, DateTime end, string loaction)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["GetMainEventListApi"] ?? "/getMainEventList?v=v1.0";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "title", title },
														{ "locale", loaction }
													};
				if (default(DateTime) != start)
				{
					pairs.Add("startTime", start.ToString("yyyyMMdd"));
				}
				if (default(DateTime) != end)
				{
					pairs.Add("endTime", end.ToString("yyyyMMdd"));
				}
				Logger.InfoFormat("Do Service - {0}", serviceName);
				HttpResult result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("Get Master events service response:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<MasterEventListResponse>(result.Html);
				}
				else
				{
					Logger.ErrorFormat("Get Master events error{0}: {1}", result.StatusCode, result.Html);
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
				throw;
			}
		}

		public bool CreateMasterEvent(long id, string title, string eventType, string grade, DateTime time, string description, string location, double longitude, double latitude, Func<string, bool> callback = null)
		{
			return UpdateMasterEvent(-1, title, eventType, grade, time, description, location, longitude, latitude, callback);
		}

		public bool UpdateMasterEvent(long id, string title, string eventType, string grade, DateTime time, string description, string location, double longitude, double latitude, Func<string, bool> callback)
		{
			string serviceName = ConfigurationManager.AppSettings["mainEventApi"] ?? "mainEvent";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "title", title },
				{ "eventType", eventType },
				{ "grade", grade },
				{ "time", time.ToString("yyyyMMdd") },
				{ "describe", description },
				{ "locale", location },
				{ "longitude", longitude.ToString() },
				{ "latitude", latitude.ToString() }
			};
			if (id != -1)
			{
				pairs.Add("id", id.ToString());
			}
			var result = RequestControl.Request(serviceName, "POST", pairs);
			if (result.StatusCode != 200)
			{
				return false;
			}
			else
			{
				if (callback != null)
				{
					return callback.Invoke(result.Html);
				}
				else
				{
					return RequestControl.DefaultValide(result.Html);
				}
			}
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
