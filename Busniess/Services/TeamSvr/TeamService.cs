using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Busniess.Services
{
	public class TeamService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public EmergencyHttpListResult<TeamModel> GetTeam(int pageIndex, int pageSize)
		{
			return GetTeam(pageIndex, pageSize, string.Empty);
		}

		public EmergencyHttpListResult<TeamModel> GetTeam(int pageIndex, int pageSize, string searchInfo)
		{
			var response = GetTeamData(pageIndex, pageSize, searchInfo);
			if (response.Code != 1)
			{
				Util.ShowError(string.Format("获取统计信息失败：{0}", response.Message));
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<TeamModel>> GetTeamData(int pageIndex, int pageSize, string searchInfo)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getTeamListApi"] ?? "getTeamList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "searchInfo", searchInfo }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取队伍数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<TeamModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取队伍数据异常", ex);
				throw;
			}
		}

		public EmergencyHttpResponse<EmergencyHttpListResult<TeamModel>> GetUnbindedTeamData(int pageIndex, int pageSize)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getTeamsUnbindingApi"] ?? "getBindingTeamList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取未绑定到事件的队伍数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<TeamModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取未绑定到事件的队伍数据异常", ex);
				throw;
			}
		}

		public EmergencyHttpResponse<EmergencyHttpListResult<TeamModel>> GetbindingTeamData(int pageIndex, int pageSize, long childEventId)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getTeamsBindingToSubevent"] ?? "childEvent/getTeamList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "childEventId", childEventId.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取绑定到事件的队伍数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<TeamModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取绑定到事件的队伍数据异常", ex);
				throw;
			}
		}

		public bool BindgTeamToSubevent(long subeventId, List<long> ids)
		{
			return BindingUnbindingTeamToSubevnt(subeventId, ids, "POST");
		}

		public bool UnbindingTeamFromSubevent(long subevent, List<long> ids)
		{
			return BindingUnbindingTeamToSubevnt(subevent, ids, "DELETE");
		}

		public bool BindingUnbindingTeamToSubevnt(long subeventId, List<long> ids, string method)
		{
			var msg = method == "POST" ? "绑定队伍到子事件" : "解绑子事件绑定的队伍";
			if (ids == null || ids.Count == 0)
			{
				Logger.WarnFormat("队伍ID列表为空，不能{0}", msg);
				return true;
			}

			var idstring = string.Join(",", ids.ToArray());

			string serviceName = ConfigurationManager.AppSettings["bindingTeamToSubEveentApi"] ?? "childEvent/team";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "teamIds", idstring },
				{ "childEventId", subeventId.ToString() }
			};

			Logger.Debug(msg);
			var result = RequestControl.Request(serviceName, method, pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("{0}失败 -- {1}: ({2})", msg, subeventId, idstring);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("{0}成功 -- {1}:({2})", msg, subeventId, idstring);
				}
				else
				{
					Logger.WarnFormat("{0}失败 -- {1}: ({2})", msg, subeventId, idstring);
				}

				return success;
			}
		}

		public bool CreateTeam(string teamName, string charge, string chargePhone, string teamDept, string teamDeptLocale)
		{
			string serviceName = ConfigurationManager.AppSettings["teamApi"] ?? "team";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "teamName", teamName },
				{ "personCharge", charge },
				{ "personChargePhone", chargePhone },
				{ "teamDept", teamDept },
                { "teamDeptLocale", teamDeptLocale}
			};

			Logger.DebugFormat("创建队伍 -- {0}", teamName);
			var result = RequestControl.Request(serviceName, "POST", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("创建队伍失败 -- {0}", teamName);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("创建队伍成功 -- {0}", teamName);
				}
				else
				{
					Logger.WarnFormat("创建队伍失败 -- {0}", teamName);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		public bool UpdateTeam(long id, string teamName, string charge, string chargePhone, string teamDept,string teamDeptLocale, long teamMemberID)
		{
			string serviceName = ConfigurationManager.AppSettings["teamApi"] ?? "team";
            Dictionary<string, string> pairs = new Dictionary<string, string>()
            {
                { "id", id.ToString() },
                { "teamName", teamName },
                { "personCharge", charge },
                { "personChargePhone", chargePhone },
                { "teamDept", teamDept },
                { "teamMemberId", teamMemberID.ToString() },
                { "teamDeptLocale",teamDeptLocale}
            };

			Logger.DebugFormat("更新队伍 -- {0}", teamName);
			var result = RequestControl.Request(serviceName, "PUT", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("更新队伍失败 -- {0}", teamName);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("更新队伍成功 -- {0}", teamName);
				}
				else
				{
					Logger.WarnFormat("更新队伍失败 -- {0}", teamName);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		/// <summary>
		/// delete team
		/// </summary>
		/// <param name="ids">删除队伍的ID列表</param>
		/// <returns></returns>
		public bool DeleteTeam(List<string> ids)
		{
			if (ids == null || ids.Count == 0)
			{
				Logger.Warn("队伍ID列表为空，不能删除队伍");
				return true;
			}

			var idstring = string.Join(",", ids.ToArray());
			string serviceName = ConfigurationManager.AppSettings["teamApi"] ?? "team";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "ids", idstring }
			};

			Logger.DebugFormat("删除队伍 -- ID(s):{0}", ids);
			var result = RequestControl.Request(serviceName, "DELETE", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("删除队伍失败 -- ID(s):{0}", ids);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("删除队伍成功 -- ID(s):{0}", ids);
				}
				else
				{
					Logger.WarnFormat("删除队伍失败 -- ID(s):{0}", ids);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		public EmergencyHttpListResult<PersonModel> GetTeamPersons(int pageIndex, int pageSize, long teamId, string Name = "")
		{
			var response = GetTeamMembersData(pageIndex, pageSize, teamId, Name);
			if (response.Code != 1)
			{
				Util.ShowError(string.Format("获取统计信息失败：{0}", response.Message));
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		public EmergencyHttpListResult<TeamMemberModel> GetTeamPersonsV2(int pageIndex, int pageSize)
		{
			var response = GetTeamMembersDataV2(pageIndex, pageSize);
			if (response.Code != 1)
			{
				Util.ShowError(string.Format("获取统计信息失败：{0}", response.Message));
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		public bool CreateTeamMember(string teamId, string name, string phoneNumber, string place)
		{
			string serviceName = ConfigurationManager.AppSettings["updataTeamMemberApi"] ?? "team/teamMember";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "teamId", teamId },
				{ "name", name },
				{ "phoneNumber", phoneNumber },
				{ "place", place }
			};

			Logger.DebugFormat("创建队伍成员 -- {0}", name);
			var result = RequestControl.Request(serviceName, "POST", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("创建更新队伍成员失败 -- {0}", name);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("创建更新队伍成员成功 -- {0}", name);
				}
				else
				{
					Logger.WarnFormat("创建更新队伍成员失败 -- {0}", name);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		private bool TeamMemberApi(PersonModel person, TeamModel team, string httpMethod)
		{
			string serviceName = ConfigurationManager.AppSettings["updataTeamMemberApi2"] ?? "team/teamMemberInfo";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "teamId", team.ID.ToString() },
				{ "teamDept", team.TeamDept.ToString() },
				{ "teamMemberId", person.ID.ToString() },
				{ "phoneNumber", person.PhoneNumber },
				{ "place", person.Place },
				{ "teamMemberName", person.Name },
			};

			return DoRequest(serviceName, httpMethod, pairs);
		}
		public bool CreateTeamMemberV2(PersonModel person, TeamModel team)
		{
			Logger.DebugFormat("创建队伍成员 -- {0}", person.Name);
			var success = TeamMemberApi(person, team, "POST");
			if (success)
			{
				Logger.InfoFormat("创建更新队伍成员成功 -- {0}", person.Name);
			}
			else
			{
				Logger.WarnFormat("创建更新队伍成员失败 -- {0}", person.Name);
			}
			return success;
		}

		public bool UpdateTeamMemberV2(PersonModel person, TeamModel team)
		{
			Logger.DebugFormat("更新队伍成员 -- {0}", person.Name);
			var success = TeamMemberApi(person, team, "PUT");
			if (success)
			{
				Logger.InfoFormat("更新队伍成员成功 -- {0}", person.Name);
			}
			else
			{
				Logger.WarnFormat("更新队伍成员失败 -- {0}", person.Name);
			}
			return success;
		}

		public bool UpdateTeamMember(long id, string teamId, string name, string phoneNumber, string place)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["updataTeamMemberApi"] ?? "team/teamMember";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "id", id.ToString() },
				{ "teamId", teamId },
				{ "name", name },
				{ "phoneNumber", phoneNumber },
				{ "place", place }
			};

				Logger.DebugFormat("更新队伍成员 -- {0}", name);
				var result = RequestControl.Request(serviceName, "PUT", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("更新队伍成员失败 -- {0}", name);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("更新队伍成员成功 -- {0}", name);
					}
					else
					{
						Logger.WarnFormat("更新队伍成员失败 -- {0}", name);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("更新队伍成员失败 -- {0}", name), ex);
				return false;
			}
		}

		public bool DeleteTeamMembers(List<string> ids)
		{
			try
			{
				if (ids == null || ids.Count == 0)
				{
					Logger.Warn("队伍成员ID列表为空，不能删除队伍成员");
					return true;
				}

				var idstring = string.Join(",", ids.ToArray());
				string serviceName = ConfigurationManager.AppSettings["updataTeamMemberApi"] ?? "team/teamMember";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "ids", idstring }
													};

				Logger.DebugFormat("删除队伍成员 -- ID(s):{0}", ids);
				var result = RequestControl.Request(serviceName, "DELETE", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("删除队伍成员失败 -- ID(s):{0}", ids);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("删除队伍成员成功 -- ID(s):{0}", ids);
					}
					else
					{
						Logger.WarnFormat("删除队伍成员失败 -- ID(s):{0}", ids);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("删除队伍成员失败 -- ID(s):{0}", ids), ex);
				return false;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<PersonModel>> GetTeamMembersData(int pageIndex, int pageSize, long teamId, string memberName)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getTeamMemberListApi"] ?? "getTeamMemberList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "teamId", teamId.ToString() },
														{ "name", memberName },
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取队伍成员数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<PersonModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取队伍成员数据异常", ex);
				throw;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<TeamMemberModel>> GetTeamMembersDataV2(int pageIndex, int pageSize)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getTeamMemberListApi2"] ?? "team/memberInfoList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取队伍成员数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<TeamMemberModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取队伍成员数据异常", ex);
				throw;
			}
		}

		public bool ImportTeamMembers(long teamId, string teamMemberJson)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["teamMemberImport"] ?? "teamMember/import";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
												{
													{ "teamId", teamId.ToString() },
													{ "teamMemberJson", teamMemberJson}
												};

				Logger.DebugFormat("导入队伍成员:{0}", teamId);
				var result = RequestControl.Request(serviceName, "POST", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("导入队伍成员:{0}失败", teamId);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("导入队伍成员成功:{0}", teamId);
					}
					else
					{
						Logger.WarnFormat("导入队伍成员失败:{0}", teamId);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("导入队伍成员失败:{0}", teamId), ex);
				return false;
			}
		}

		public bool ImportTeamMembersV2(string json)
		{
			string serviceName = ConfigurationManager.AppSettings["importTeamMemberApi"] ?? "team/import";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{"teamInfoJson", json }
			};

			Logger.DebugFormat("导入队伍成员:\r\n{0}", json);
			var success = DoRequest(serviceName, "POST", pairs);
			if (success)
			{
				Logger.Info("导入队伍成员成功");
			}
			else
			{
				Logger.Info("导入队伍成员失败");
			}
			return success;
		}

		private bool DoRequest(string serviceName, string method, Dictionary<string, string> pairs)
		{
			var result = RequestControl.Request(serviceName, method, pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (!success)
				{
					Logger.Warn(result.Html);
				}

				return success;
			}
		}
	}
}
