using Busniess.CommonControl;
using Framework.Http;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Business.MainPageSvr
{
	public class LoginServices
	{
		public bool LogIn(string userName, string password, Func<string, bool> callback)
		{
			string serviceName = ConfigurationManager.AppSettings["LoginApi"] ?? "login";
			List<HeaderInfo> headers = new List<HeaderInfo>();
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{"name", userName },
				{"pwd", password }
			};

			HttpResult hr = RequestControl.Request(serviceName, "POST", pairs, null, false, (item) =>
			{
				item.ContentType = "application/x-www-form-urlencoded";
				item.ResultType = ResultType.String;
			});
			if (callback != null)
			{
				return callback(hr.Html);
			}

			return true;
		}
	}
}
