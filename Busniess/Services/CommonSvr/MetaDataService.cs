﻿using Busniess;
using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Business.Services
{
	public class MetaDataType
	{
		public static string EventType = "event_type";
		public static string EventGrade = "event_grade";
		public static string EventState = "event_state";
		public static string MaterialType = "materials_type";
		public static string TeamDepartment = "team_dept";
		public static string TeamMemberPlace = "team_member_place";

		public static string MaterialDept = "materials_dept";
		public static string TemplateType = "template_type";
		public static string TeamId = "team_id";
	}

	public class MetaDataService
	{
		private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static DictItem[] EventTypes { get { return GetMetaData(MetaDataType.EventType); } }
		public static DictItem[] EventGrades { get { return GetMetaData(MetaDataType.EventGrade); } }
		public static DictItem[] EventStates { get { return GetMetaData(MetaDataType.EventState); } }
		public static DictItem[] MaterialTypes { get { return GetMetaData(MetaDataType.MaterialType); } }
		public static DictItem[] TeamDepartments { get { return GetMetaData(MetaDataType.TeamDepartment); } }
		public static DictItem[] TeamMemberPlaces { get { return GetMetaData(MetaDataType.TeamMemberPlace); } }
		public static DictItem[] MaterialDepts { get { return GetMetaData(MetaDataType.MaterialDept); } }
		public static DictItem[] TemplateTypes { get { return GetMetaData(MetaDataType.TemplateType); } }
		public static DictItem[] TeamIds { get { return GetMetaData(MetaDataType.TeamId); } }

		public static DictItem[] GetMetaData(string type)
		{
			try
			{
				var response = GetMetaDataBase(type);
				if (response.Code != 1)
				{
					Util.ShowError(string.Format("获取基础数据失败：{0}", response.Message));
					return new DictItem[0];
				}
				else
				{
					return response.Result;
				}
			}
			catch (Exception ex)
			{
				Logger.Fatal(string.Format("获取基础数据失败：{0}", type), ex);
				return default(DictItem[]);
			}
		}

		public static EmergencyHttpResponse<DictItem[]> GetMetaDataBase(string type)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["metaDataApi"] ?? "dict";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "type", type },
													};

				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取基础数据数据:{0}", type);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<DictItem[]>>(result.Html);
				}
				else
				{
					return new EmergencyHttpResponse<DictItem[]>()
					{
						Result = new DictItem[0]
					};
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取基础数据数据", ex);
				return new EmergencyHttpResponse<DictItem[]>()
				{
					Result = new DictItem[0]
				};
			}
		}
	}
}
