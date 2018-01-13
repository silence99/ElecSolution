using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Utils;

namespace Busniess.Services
{
	public class CameraService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public EmergencyHttpListResult<CameraModel> GetCamera(int pageIndex, int pageSize, double latitude, double longitude)
		{
			var response = GetCameraData(pageIndex, pageSize, latitude, longitude);
			if (response == null || response.Code != 1)
			{
				return new EmergencyHttpListResult<CameraModel>()
				{
					Data = new CameraModel[0]
				};
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>> GetCameraData(int pageIndex, int pageSize, double latitude, double longitude)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["mainEventCameraInfo"] ?? "mainEvent/getVideoList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "latitude", latitude.ToString() },
														{ "longitude", longitude.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取摄像头信息:（{0}, {1}）", latitude, longitude);
					return JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>>>(result.Html);
				}
				else
				{
					Logger.ErrorFormat("获取摄像头信息异常:\r\n{0}", result.Html);
					return new EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>>()
					{
						Result = new EmergencyHttpListResult<CameraModel>()
					};
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取摄像头信息异常", ex);
				return new EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>>()
				{
					Result = new EmergencyHttpListResult<CameraModel>()
				};
			}
		}
	}
}
