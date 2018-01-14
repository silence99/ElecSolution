using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Busniess.Services
{
	public class SubeventService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public EmergencyHttpResponse<EmergencyHttpListResult<SubEvent>> GetSubevents(int pageIndex, int pageSize, int mainEventId)
		{
			return GetSubevents(pageIndex, pageSize, mainEventId, string.Empty);
		}

		public EmergencyHttpResponse<EmergencyHttpListResult<SubEvent>> GetSubevents(int pageIndex, int pageSize, int mainEventId, string title)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["subEventListApi"] ?? "getChildEventList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "mainEventId", mainEventId.ToString() },
														{ "searchInfo", title }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("Get Sub events service response:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<SubEvent>>>(result.Html);
				}
				else
				{
					Logger.ErrorFormat("Get Sub events error: {0}", result.StatusCode);
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Get Sub events error", ex);
				return null;
			}
		}

		public bool CreateChildEvent(string mainEventId, string title, string location, string grade, string remark, string latitude, string longitude, string personLiable)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["childEventApi"] ?? "childEvent";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "childTitle", title },
														{ "mainEventId", mainEventId },
														{ "childLocale", location },
														{ "childGrade", grade },
														{ "childRemarks", remark },
														{ "childLongitude", longitude.ToString() },
														{ "childLatitude", latitude.ToString() },
														{ "personLiable", personLiable }
													};

				Logger.DebugFormat("创建子事件 -- {0}", title);
				var result = RequestControl.Request(serviceName, "POST", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("创建子事件失败 -- {0}", title);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("创建子事件成功 -- {0}", title);
					}
					else
					{
						Logger.WarnFormat("创建子事件失败 -- {0}", title);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Fatal(string.Format("创建子事件失败 -- {0}", title), ex);
				return false;
			}
		}

		public bool CreateChildEvent(string mainEventId, SubEvent subEvent)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["childEventApi"] ?? "childEvent";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "childTitle", subEvent.ChildTitle },
														{ "mainEventId", mainEventId },
														{ "childLocale", subEvent.ChildLocale },
														{ "childGrade", subEvent.ChildGrade },
														{ "childRemarks", subEvent.ChildRemarks },
														{ "childLongitude", subEvent.ChildLongitude.ToString() },
														{ "childLatitude", subEvent.ChildLatitude.ToString() },
														{ "personLiable", subEvent.PersonLiable }
													};

				Logger.DebugFormat("创建子事件 -- {0}", subEvent.ChildTitle);
				var result = RequestControl.Request(serviceName, "POST", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("创建子事件失败 -- {0}", subEvent.ChildTitle);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("创建子事件成功 -- {0}", subEvent.ChildTitle);
					}
					else
					{
						Logger.WarnFormat("创建子事件失败 -- {0}", subEvent.ChildTitle);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Fatal(string.Format("创建子事件失败 -- {0}", subEvent.ChildTitle), ex);
				return false;
			}
		}

		public bool UpdateChildEvent(long id, string mainEventId, string title, string location, string grade, string remark, string latitude, string longitude, string personLiable)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["childEventApi"] ?? "childEvent";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{"id", id.ToString() },
				{ "childTitle", title },
				{ "mainEventId", mainEventId },
				{ "childLocale", location },
				{ "childGrade", grade },
				{ "childRemarks", remark },
				{ "childLongitude", longitude.ToString() },
				{ "childLatitude", latitude.ToString() },
				{ "personLiable", personLiable }
			};

				Logger.DebugFormat("更新子事件 -- {0}", title);
				var result = RequestControl.Request(serviceName, "PUT", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("更新子事件失败 -- {0}", title);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("更新子事件成功 -- {0}", title);
					}
					else
					{
						Logger.WarnFormat("更新子事件失败 -- {0}", title);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Fatal(string.Format("更新子事件失败 -- {0}", title), ex);
				return false;
			}
		}
		public bool UpdateChildEvent(string mainEventId, SubEvent subEvent)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["childEventApi"] ?? "childEvent";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{"id", subEvent.Id.ToString() },
														{ "childTitle", subEvent.ChildTitle },
														{ "mainEventId", mainEventId },
														{ "childLocale", subEvent.ChildLocale },
														{ "childGrade", subEvent.ChildGrade },
														{ "childRemarks", subEvent.ChildRemarks },
														{ "childLongitude", subEvent.ChildLongitude.ToString() },
														{ "childLatitude", subEvent.ChildLatitude.ToString() },
														{ "personLiable", subEvent.PersonLiable }
													};

				Logger.DebugFormat("更新子事件 -- {0}", subEvent.ChildTitle);
				var result = RequestControl.Request(serviceName, "PUT", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("更新子事件失败 -- {0}", subEvent.ChildTitle);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("更新子事件成功 -- {0}", subEvent.ChildTitle);
					}
					else
					{
						Logger.WarnFormat("更新子事件失败 -- {0}", subEvent.ChildTitle);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Fatal(string.Format("更新子事件失败 -- {0}", subEvent.ChildTitle), ex);
				return false;
			}
		}


		public bool UpdateChildeEventState(List<string> ids, int state)
		{
			try
			{
				if (ids == null || ids.Count == 0)
				{
					Logger.Warn("子事件ID列表为空，不能更新子事件状态");
					return true;
				}

				var idstring = string.Join(",", ids.ToArray());

				string serviceName = ConfigurationManager.AppSettings["childEventUpdateApi"] ?? "childEvent/state";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "ids", idstring },
														{ "state", state.ToString() }
													};

				Logger.DebugFormat("更新子事件状态 -- {0}", state);
				var result = RequestControl.Request(serviceName, "PUT", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("更新子事件状态失败 -- {0}", state);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("更新子事件状态成功 -- {0}", state);
					}
					else
					{
						Logger.WarnFormat("更新子事件状态失败 -- {0}", state);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Fatal(string.Format("更新子事件状态失败 -- {0}", state), ex);
				return false;
			}
		}

		public PersonModel[] GetPersonBindingToSubevent(long childEventId)
		{
			var response = GetPersonBindingToSubeventData(childEventId);
			if (response.Code != 1)
			{
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<PersonModel[]> GetPersonBindingToSubeventData(long childEventId)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getPersonBindingToSubeventsApi"] ?? "getChileEventTeamMemberList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "childEventId", childEventId.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取子事件下成员数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<PersonModel[]>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取子事件下成员数据", ex);
				throw;
			}
		}

	}
}
