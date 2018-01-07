using Busniess.CommonControl;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Busniess.Services
{
	public class AnnouncementService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public EmergencyHttpListResult<AnnouncementModel> GetAnnouncements(int pageIndex, int pageSize)
		{
			var response = GetAnnounments(pageIndex, pageSize);
			if (response.Code != 1)
			{
				Util.ShowError(string.Format("获取公告信息失败：{0}", response.Message));
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		public bool CreateAnnouncement(AnnouncementModel model)
		{
			return CreateAnnouncement(model.Title, TimeControl.GetMillisecons(model.Time), model.Content);
		}

		public bool UpdateAnnouncement(AnnouncementModel model)
		{
			return UpdateAnnouncement(model.ID, model.Title, TimeControl.GetMillisecons(model.Time), model.Content);
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<AnnouncementModel>> GetAnnounments(int pageIndex, int pageSize)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getNoticesApi"] ?? "getNoticeList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取公告数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<AnnouncementModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取公告数据异常", ex);
				throw;
			}
		}

		private bool CreateAnnouncement(string title, long time, string content)
		{
			string serviceName = ConfigurationManager.AppSettings["UpdateNoticeApi"] ?? "notice";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "title", title },
				{ "time", time.ToString() },
				{ "content", content }
			};

			Logger.DebugFormat("创建公告 -- {0}", title);
			var result = RequestControl.Request(serviceName, "POST", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("创建公告失败 -- {0}", title);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("创建公告成功 -- {0}", title);
				}
				else
				{
					Logger.WarnFormat("创建公告失败 -- {0}", title);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		private bool UpdateAnnouncement(long id, string title, long time, string content)
		{
			string serviceName = ConfigurationManager.AppSettings["UpdateNoticeApi"] ?? "notice";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "id", id.ToString() },
				{ "title", title },
				{ "time", time.ToString() },
				{ "content", content }
			};

			Logger.DebugFormat("修改公告 -- {0}", title);
			var result = RequestControl.Request(serviceName, "PUT", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("修改公告失败 -- {0}", title);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("修改公告成功 -- {0}", title);
				}
				else
				{
					Logger.WarnFormat("修改公告失败 -- {0}", title);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		public bool DeleteAnncoucement(List<string> ids)
		{
			if (ids == null || ids.Count == 0)
			{
				Logger.Warn("公告ID列表为空，不能删除公告");
				return true;
			}

			var idstring = string.Join(",", ids.ToArray());
			string serviceName = ConfigurationManager.AppSettings["UpdateNoticeApi"] ?? "notice";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "ids", idstring }
			};

			Logger.DebugFormat("删除公告 -- ID(s):{0}", ids);
			var result = RequestControl.Request(serviceName, "DELETE", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("删除公告失败 -- ID(s):{0}", ids);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("删除公告成功 -- ID(s):{0}", ids);
				}
				else
				{
					Logger.WarnFormat("删除公告失败 -- ID(s):{0}", ids);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}
	}
}
