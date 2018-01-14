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
	public class MessageService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MessageTemplateModel[] GetTemplates(string templateType)
		{
			var response = GetTemplatesData(templateType);

			if (response == null || response.Code != 1)
			{
				return new MessageTemplateModel[0];
			}
			else
			{
				return response.Result;
			}
		}

		public EmergencyHttpListResult<MessageModel> GetMessageLog(int pageIndex, int pageSize)
		{
			var response = GetMessageData(pageIndex, pageSize);

			if (response == null || response.Code != 1)
			{
				return new EmergencyHttpListResult<MessageModel>();
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<MessageTemplateModel[]> GetTemplatesData(string templateType)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["messageTemplateApi"] ?? "getTemplateList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "templateType", templateType }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取短信模板:{0}", result.Html);
					return JSONHelper.ConvertToObject<EmergencyHttpResponse<MessageTemplateModel[]>>(result.Html);
				}
				else
				{
					Logger.Fatal(string.Format("获取短信模板异常:{0}", result.Html));
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取短信模板异常", ex);
				return null;
			}
		}

		public bool SendMessge(string templateType, long childEventId, string templateId, string sendInfo)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["sendMessageApi"] ?? "send/message";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "templateType", templateType },
														{ "childEventId", childEventId.ToString() },
														{ "templateId", templateId },
														{ "sendInfo", sendInfo }
													};

				Logger.DebugFormat("发送消息");
				var result = RequestControl.Request(serviceName, "POST", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("发送消息失败", result.Html);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("发送消息成功 -- {0}", result.Html);
					}
					else
					{
						Logger.WarnFormat("发送消息失败 -- {0}", result.Html);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("创建物资失败", ex);
				return false;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<MessageModel>> GetMessageData(int pageIndex, int pageSize)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["sendLogApi"] ?? "send/log";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取短信历史记录:{0}", result.Html);
					return JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<MessageModel>>>(result.Html);
				}
				else
				{
					Logger.Fatal(string.Format("获取短信历史记录异常:{0}", result.Html));
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取短信历史记录异常", ex);
				return null;
			}
		}
	}
}
