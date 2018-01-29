using Busniess.CommonControl;
using Emergence.Common.Model;
using Framework.Http;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Busniess.Services
{
	public class UpdateService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public ClientVersionModel GetCurrentVersionInfo()
		{
			var response = GetCurrentVersionInfoData();
			if (response == null || response.Code != 1)
			{
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		public HttpResult Download(string url)
		{
			return RequestControl.Request(url);
		}

		private EmergencyHttpResponse<ClientVersionModel> GetCurrentVersionInfoData()
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["downloadClienApi"] ?? "client/version";
				Dictionary<string, string> pairs = new Dictionary<string, string>();
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取最新版本信息");
					return JSONHelper.ConvertToObject<EmergencyHttpResponse<ClientVersionModel>>(result.Html);
				}
				else
				{
					Logger.ErrorFormat("获取最新版本信息异常:\r\n{0}", result.Html);
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取最新版本信息异常", ex);
				return null;
			}
		}
	}
}
