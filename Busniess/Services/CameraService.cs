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

		public EmergencyHttpListResult<CameraModel> GetCameraByMasterEvent(int pageIndex, int pageSize, double latitude, double longitude)
		{
			var response = GetCameraDataByMasterEvent(pageIndex, pageSize, latitude, longitude);
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

		private EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>> GetCameraDataByMasterEvent(int pageIndex, int pageSize, double latitude, double longitude)
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

        public EmergencyHttpListResult<CameraModel> GetCameras(int pageIndex, int pageSize)
        {
            var response = GetCamerasData(pageIndex, pageSize);

            if (response == null || response.Code != 1)
            {
                return new EmergencyHttpListResult<CameraModel>();
            }
            else
            {
                return response.Result;
            }
        }

        private EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>> GetCamerasData(int pageIndex, int pageSize)
        {
            try
            {
                string serviceName = ConfigurationManager.AppSettings["getVideoListApi"] ?? "getVideoList";
                Dictionary<string, string> pairs = new Dictionary<string, string>()
                                                    {
                                                        { "pageIndex", pageIndex.ToString() },
                                                        { "pageSize", pageSize.ToString() }
                                                    };
                var result = RequestControl.Request(serviceName, "GET", pairs);
                if (result.StatusCode == 200)
                {
                    Logger.DebugFormat("获取摄像头数据:{0}", result.Html);
                    return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<CameraModel>>>(result.Html);
                }
                else
                {
                    Logger.Fatal(string.Format("获取摄像头数据异常:{0}", result.Html));
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("获取摄像头数据异常", ex);
                return null;
            }
        }

        public bool ImportCameras(string camerasJson)
        {
            try
            {
                string serviceName = ConfigurationManager.AppSettings["cameraImportApi"] ?? "video/import";
                Dictionary<string, string> pairs = new Dictionary<string, string>()
                                                {
                                                    { "videoJson", camerasJson}
                                                };

                Logger.DebugFormat("导入摄像头:{0}", camerasJson);
                var result = RequestControl.Request(serviceName, "POST", pairs);
                if (result.StatusCode != 200)
                {
                    Logger.WarnFormat("导入摄像头失败：{0}", result.Html);
                    return false;
                }
                else
                {
                    var success = RequestControl.DefaultValide(result.Html);
                    if (success)
                    {
                        Logger.InfoFormat("导入摄像头成功:{0}", result.Html);
                    }
                    else
                    {
                        Logger.WarnFormat("导入摄像头失败:{0}", result.Html);
                    }

                    return success;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("导入摄像头失败", ex);
                return false;
            }
        }


        public bool DeleteCamera(List<string> ids)
        {
            try
            {
                if (ids == null || ids.Count == 0)
                {
                    Logger.Warn("摄像头ID列表为空，不能删除摄像头");
                    return true;
                }

                var idstring = string.Join(",", ids.ToArray());
                string serviceName = ConfigurationManager.AppSettings["deleteVideoApi"] ?? "video";
                Dictionary<string, string> pairs = new Dictionary<string, string>()
                                                {
                                                    { "ids", idstring }
                                                };

                Logger.DebugFormat("删除摄像头 -- ID(s):{0}", ids);
                var result = RequestControl.Request(serviceName, "DELETE", pairs);
                if (result.StatusCode != 200)
                {
                    Logger.WarnFormat("删除摄像头失败 -- ID(s):{0}", ids);
                    Logger.WarnFormat(result.Html);
                    return false;
                }
                else
                {
                    var success = RequestControl.DefaultValide(result.Html);
                    if (success)
                    {
                        Logger.InfoFormat("删除摄像头成功 -- ID(s):{0}", ids);
                    }
                    else
                    {
                        Logger.WarnFormat("删除摄像头失败 -- ID(s):{0}", ids);
                        Logger.Warn(result.Html);
                    }

                    return success;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("删除摄像头失败 -- ID(s):{0}", ids), ex);
                return false;
            }
        }
    }
}
