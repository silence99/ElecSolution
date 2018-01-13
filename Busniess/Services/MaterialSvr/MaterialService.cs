using Busniess.CommonControl;
using Emergence.Common.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace Busniess.Services
{
	public class MaterialService
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


		public EmergencyHttpListResult<MaterialModel> GetMaterials(int pageIndex, int pageSize, string searchInfo)
		{
			var response = GetMeterialsData(pageIndex, pageSize, searchInfo);

			if (response == null || response.Code != 1)
			{
				return new EmergencyHttpListResult<MaterialModel>();
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>> GetMeterialsData(int pageIndex, int pageSize, string searchInfo)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getMaterialsListApi"] ?? "getMaterialsList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "searchInfo", searchInfo }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取物资数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>>>(result.Html);
				}
				else
				{
					Logger.Fatal(string.Format("获取物资数据异常:{0}", result.Html));
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取物资数据异常", ex);
				return null;
			}
		}

		public EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>> GetUnbindedMaterials(int pageIndex, int pageSize)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getUnBindedMaterialsListApi"] ?? "getBindingMaterialsList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取未绑定到事件的物资数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>>>(result.Html);
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取未绑定到事件的物资数据异常", ex);
				return null;
			}
		}

		public EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>> GetMaterialsBindingToSubevent(int pageIndex, int pageSize, long childEventId)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getMaterialsBindingToSubevent"] ?? "childEvent/getMaterialsList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "childEventId", childEventId.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取绑定到事件的物资数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>>>(result.Html);
				}
				else
				{
					Logger.ErrorFormat("获取绑定到事件的物资数据异常: Code[{0}]", result.StatusCode);
					return null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取绑定到事件的物资数据异常", ex);
				return null;
			}
		}

		public bool BindingMaterialToSubevnt(long subeventId, List<long> ids)
		{
			return BindingUnbindingMaterialToSubevnt(subeventId, ids, "POST");
		}

		public bool UnbindingMaterialFromSubevnt(long subeventId, List<long> ids)
		{
			return BindingUnbindingMaterialToSubevnt(subeventId, ids, "DELETE");
		}

		private bool BindingUnbindingMaterialToSubevnt(long subeventId, List<long> ids, string method)
		{
			var msg = method == "POST" ? "绑定物资到子事件" : "解绑子事件绑定的物资";
			if (ids == null || ids.Count == 0)
			{
				Logger.WarnFormat("物资ID列表为空，不能{0}", msg);
				return true;
			}

			var idstring = string.Join(",", ids.ToArray());
			try
			{
				string serviceName = ConfigurationManager.AppSettings["bindingUnbindingMaterialApi"] ?? "childEvent/materials";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "materialsIds", idstring },
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
			catch (Exception ex)
			{
				Logger.Error(string.Format("{0}失败 -- {1}: ({2})", msg, subeventId, idstring), ex);
				return false;
			}
		}

		public bool CreateMaterial(MaterialModel model)
		{
			try
			{
				if (model.IsEmpty)
				{
					Logger.Warn("Material entity is empty, creating failure");
					return true;
				}
				string serviceName = ConfigurationManager.AppSettings["materialUpdateApi"] ?? "materials";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "materialsName", model.MaterialsName },
														{ "materialsNumber", model.MaterialsNumber },
														{ "materialsType", model.MaterialsType },
														{ "materialsDept", model.MaterialsDept },
														{ "consumables", model.IsConsumable.ToString() },
														{ "bigMaterials", model.IsBigMaterials.ToString() },
														{ "totalQuantity", model.TotalQuantity.ToString() }
													};

				Logger.DebugFormat("创建物资 -- {0}", model.MaterialsName);
				var result = RequestControl.Request(serviceName, "POST", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("创建物资失败 -- {0}", model.MaterialsName);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("创建物资成功 -- {0}", model.MaterialsName);
					}
					else
					{
						Logger.WarnFormat("创建物资失败 -- {0}", model.MaterialsName);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("创建物资失败 -- {0}", model.MaterialsName), ex);
				return false;
			}
		}

		public bool UpdateMaterial(MaterialModel model)
		{
			try
			{
				if (model.IsEmpty)
				{
					Logger.Warn("Material entity is empty, updating failure");
					return true;
				}

				string serviceName = ConfigurationManager.AppSettings["materialUpdateApi"] ?? "materials";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
												{
													{ "id", model.ID.ToString() },
													{ "materialsName", model.MaterialsName },
													{ "materialsNumber", model.MaterialsNumber },
													{ "materialsType", model.MaterialsType },
													{ "materialsDept", model.MaterialsDept },
													{ "consumables", model.IsConsumable.ToString() },
													{ "bigMaterials", model.IsBigMaterials.ToString() },
													{ "totalQuantity", model.TotalQuantity.ToString() }
												};

				Logger.DebugFormat("更新物资 -- {0}", model.MaterialsName);
				var result = RequestControl.Request(serviceName, "PUT", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("更新物资失败 -- {0}", model.MaterialsName);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("更新物资成功 -- {0}", model.MaterialsName);
					}
					else
					{
						Logger.WarnFormat("更新物资失败 -- {0}", model.MaterialsName);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("更新物资失败 -- {0}", model.MaterialsName), ex);
				return false;
			}
		}

		public bool DeleteMaterial(List<string> ids)
		{
			try
			{
				if (ids == null || ids.Count == 0)
				{
					Logger.Warn("物资ID列表为空，不能删除物资");
					return true;
				}

				var idstring = string.Join(",", ids.ToArray());
				string serviceName = ConfigurationManager.AppSettings["materialUpdateApi"] ?? "materials";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
												{
													{ "ids", idstring }
												};

				Logger.DebugFormat("删除物资 -- ID(s):{0}", ids);
				var result = RequestControl.Request(serviceName, "DELETE", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("删除物资失败 -- ID(s):{0}", ids);
					Logger.WarnFormat(result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("删除物资成功 -- ID(s):{0}", ids);
					}
					else
					{
						Logger.WarnFormat("删除物资失败 -- ID(s):{0}", ids);
						Logger.Warn(result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format("删除物资失败 -- ID(s):{0}", ids), ex);
				return false;
			}
		}

		public bool ImportMaterials(string materialsJson)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["materailImportApi"] ?? "materials/import";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
												{
													{ "materialsJson", materialsJson}
												};

				Logger.DebugFormat("导入物资:{0}", materialsJson);
				var result = RequestControl.Request(serviceName, "POST", pairs);
				if (result.StatusCode != 200)
				{
					Logger.WarnFormat("导入物资失败：{0}", result.Html);
					return false;
				}
				else
				{
					var success = RequestControl.DefaultValide(result.Html);
					if (success)
					{
						Logger.InfoFormat("导入物资成功:{0}", result.Html);
					}
					else
					{
						Logger.WarnFormat("导入物资失败:{0}", result.Html);
					}

					return success;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("导入物资失败", ex);
				return false;
			}
		}
	}
}
