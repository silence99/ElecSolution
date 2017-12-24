using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Configuration;
using System.Reflection;

namespace Busniess.Services.StatisticsSvr
{
	public class TeamStatisticsService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public EmergencyHttpResponse<TeamStatisticsModel> GetTeamStatistics()
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["teamStatisticsApi"] ?? "team/statistics";
				var result = RequestControl.Request(serviceName, "GET", null);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取队伍统计数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<TeamStatisticsModel>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取队伍统计数据异常", ex);
				throw;
			}
		}
	}
}
