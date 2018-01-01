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


		public EmergencyHttpListResult<MaterialModel> GetMaterials(int pageIndex, int pageSize, string materialName, string materialNumber, string materialDept, int consumables)
		{
			var response = GetMeterialsData(pageIndex, pageSize, materialName, materialNumber, materialDept, consumables);
			if (response.Code != 1)
			{
				Util.ShowError(string.Format("获取物资失败：{0}", response.Message));
				return null;
			}
			else
			{
				return response.Result;
			}
		}

		private EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>> GetMeterialsData(int pageIndex, int pageSize, string materialName, string materialNumber, string materialDept, int consumables)
		{
			try
			{
				string serviceName = ConfigurationManager.AppSettings["getMaterialsListApi"] ?? "getMaterialsList";
				Dictionary<string, string> pairs = new Dictionary<string, string>()
													{
														{ "pageIndex", pageIndex.ToString() },
														{ "pageSize", pageSize.ToString() },
														{ "materialsName", materialName },
														{ "materialsNumber", materialNumber },
														{ "materialsDept", materialDept},
														{ "consumables", consumables.ToString() }
													};
				var result = RequestControl.Request(serviceName, "GET", pairs);
				if (result.StatusCode == 200)
				{
					Logger.DebugFormat("获取物资数据:{0}", result.Html);
					return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<EmergencyHttpListResult<MaterialModel>>>(result.Html);
				}
				else
				{
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取物资数据异常", ex);
				throw;
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
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取未绑定到事件的物资数据异常", ex);
				throw;
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
					throw new Exception(result.Html);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("获取绑定到事件的物资数据异常", ex);
				throw;
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

		public bool CreateMaterial(string materialName, string materialNumber, string materialType, string materialsDept, int consumables, int big, int total)
		{
			string serviceName = ConfigurationManager.AppSettings["materialUpdateApi"] ?? "materials";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "materialsName", materialName },
				{ "materialsNumber", materialNumber },
				{ "materialsType", materialType },
				{ "materialsDept", materialsDept },
				{ "consumables", consumables.ToString() },
				{ "bigMaterials", big.ToString() },
				{ "totalQuantity", total.ToString() }
			};

			Logger.DebugFormat("创建物资 -- {0}", materialName);
			var result = RequestControl.Request(serviceName, "POST", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("创建物资失败 -- {0}", materialName);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("创建物资成功 -- {0}", materialName);
				}
				else
				{
					Logger.WarnFormat("创建物资失败 -- {0}", materialName);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		public bool UpdateMaterial(string id, string materialName, string materialNumber, string materialType, string materialsDept, int consumables, int big, int total)
		{
			string serviceName = ConfigurationManager.AppSettings["materialUpdateApi"] ?? "materials";
			Dictionary<string, string> pairs = new Dictionary<string, string>()
			{
				{ "id", id },
				{ "materialsName", materialName },
				{ "materialsNumber", materialNumber },
				{ "materialsType", materialType },
				{ "materialsDept", materialsDept },
				{ "consumables", consumables.ToString() },
				{ "bigMaterials", big.ToString() },
				{ "totalQuantity", total.ToString() }
			};

			Logger.DebugFormat("更新物资 -- {0}", materialName);
			var result = RequestControl.Request(serviceName, "PUT", pairs);
			if (result.StatusCode != 200)
			{
				Logger.WarnFormat("更新物资失败 -- {0}", materialName);
				Logger.WarnFormat(result.Html);
				return false;
			}
			else
			{
				var success = RequestControl.DefaultValide(result.Html);
				if (success)
				{
					Logger.InfoFormat("更新物资成功 -- {0}", materialName);
				}
				else
				{
					Logger.WarnFormat("更新物资失败 -- {0}", materialName);
					Logger.Warn(result.Html);
				}

				return success;
			}
		}

		public bool DeleteMaterial(List<string> ids)
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
	}
}
