using Emergence.Business.CommonControl;
using Framework.Http;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Reflection;

namespace Busniess.CommonControl
{
	public class RequestControl
	{
		private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static string GetUrl(string serviceName, Dictionary<string, string> param)
		{
			var version = ConfigurationManager.AppSettings["Version"] ?? "v1.0";
			var baseUrl = ConfigurationManager.AppSettings["BaseURL"] ?? "http://106.14.215.193/test";
			return GetUrl(baseUrl, serviceName, param, version);
		}
		public static string GetUrl(string baseUrl, string serviceName, Dictionary<string, string> param, string version)
		{
			var pairs = GetKeyValuePairs(param);
			var url = string.Format("{0}/{1}?v={2}", baseUrl, serviceName, version);
			if (string.IsNullOrEmpty(pairs))
			{
				return url;
			}
			else
			{
				return string.Format("{0}&{1}", url, pairs);
			}
		}

		public static WebHeaderCollection GetCommonHead(string serviceName, string method)
		{
			var hd = new WebHeaderCollection();
			string timeSpan = TimeControl.GenerateTimeStamp();
			string signString = timeSpan + string.Format("{0}/{1}", method, serviceName);
			string authorization = AuthorizationControl.GetAuthorization(signString);
			hd.Add("X-Project-Date", timeSpan);
			hd.Add("Authorization", authorization);
			return hd;
		}

		public static string GetKeyValuePairs(Dictionary<string, string> pairs)
		{
			if (pairs != null)
			{
				string ret = string.Empty;
				foreach (var item in pairs)
				{
					if (!string.IsNullOrEmpty(item.Value))
					{
						var exp = string.Format("{0}={1}", item.Key, item.Value);
						if (ret != string.Empty)
						{
							ret = string.Format("{0}&{1}", exp, ret);
						}
						else
						{
							ret = exp;
						}
					}
				}
				return ret;
			}

			return string.Empty;
		}

		public static HttpResult Request(string serviceName, string method, Dictionary<string, string> param, bool useCommonHead = true)
		{
			return Request(serviceName, method, param, null, useCommonHead, null);
		}

		public static HttpResult Request(string serviceName, string method, Dictionary<string, string> param, Dictionary<string, string> extendHead, bool useCommonHead, Action<HttpItem> initHttpItem)
		{
			HttpHelper helper = new HttpHelper();
			var head = useCommonHead ? GetCommonHead(serviceName, method) : new WebHeaderCollection();
			var isGet = method.Trim().ToUpper() != "POST";
			var url = isGet ? GetUrl(serviceName, param) : GetUrl(serviceName, null);
			if (extendHead != null)
			{
				foreach (var item in extendHead)
				{
					head.Add(item.Key, item.Value);
				}
			}
			HttpItem requestDat = new HttpItem()
			{
				Header = head,
				URL = url,
				Method = method.Trim().ToUpper()
			};
			initHttpItem?.Invoke(requestDat);
			if (!isGet)
			{
				requestDat.Postdata = GetKeyValuePairs(param);
			}
			return helper.GetHtml(requestDat);
		}

		public static bool DefaultValide(string html)
		{
			var response = Utils.JSONHelper.ConvertToObject<EmergencyCommonResponse>(html);
			if (response.Code == 1)
			{
				Logger.Debug("HTTP请求返回成功信息");
				return true;
			}
			else
			{
				Logger.WarnFormat("HTTP请求返回失败信息，{0}", response.Message);
				return false;
			}
		}

		public class EmergencyCommonResponse
		{
			[JsonProperty("code")]
			public int Code { get; set; }

			[JsonProperty("message")]
			public string Message { get; set; }
		}
	}
}
