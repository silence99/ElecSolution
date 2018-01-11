using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Configuration;
using System.Reflection;

namespace Busniess.Services
{
	public class MaterialsStatisticsService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MaterialsStatisticsModel[] GetMaterialsStatistics()
		{
			var response = GetMaterialsStatisticsData();
			if (response == null || response.Code != 1)
			{
				return new MaterialsStatisticsModel[0];
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<MaterialsStatisticsModel[]> GetMaterialsStatisticsData()
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["materialsStatisticsApi"] ?? "materials/statistics";
				var result = RequestControl.Request(serviceName, "GET", null);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取物资统计数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<MaterialsStatisticsModel[]>>(result.Html);
				}
				else
				{
					Logger.ErrorFormat("获取物资统计数据异常", result.Html);
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取物资统计数据异常", ex);
				return null;
			}
		}
	}
}
