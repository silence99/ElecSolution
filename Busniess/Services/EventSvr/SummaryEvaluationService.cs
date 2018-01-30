using Busniess.CommonControl;
using Emergence.Common.Model;
using Framework.Http;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;


namespace Busniess.Services
{
    public class SummaryEvaluationService
    {
        private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public bool UpdateSummaryEvaluation(SummaryEvaluationModel summaryEvaluation, Func<string, bool> callback = null)
        {
            try
            {
                string serviceName = ConfigurationManager.AppSettings["summaryApi"] ?? "mainEvent/summary";
                Dictionary<string, string> pairs = new Dictionary<string, string>()
                                                    {
                                                        { "organAssessment", summaryEvaluation.OrganAssessment },
                                                        { "impleAssessment", summaryEvaluation.ImpleAssessment },
                                                        { "summary", summaryEvaluation.Summary },
                                                        { "mainEventId", summaryEvaluation.MainEventId.ToString() },
                                                        { "type", summaryEvaluation.Type.ToString() },
                                                        { "id", summaryEvaluation.Id.ToString() }
                                                    };
                Logger.InfoFormat("创建或更新总结评估：{0}", summaryEvaluation.MainEventId);
                var result = RequestControl.Request(serviceName, "POST", pairs);
                if (result.StatusCode != 200)
                {
                    Logger.WarnFormat("创建或更新总结评估失败 -- {0}", summaryEvaluation.MainEventId);
                    return false;
                }
                else
                {
                    if (callback != null)
                    {
                        return callback.Invoke(result.Html);
                    }
                    else
                    {
                        return RequestControl.DefaultValide(result.Html);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal(string.Format("创建或更新总结评估失败 -- {0}", summaryEvaluation.MainEventId), ex);
                return false;
            }
        }

        public EmergencyHttpResponse<SummaryEvaluationModel> GetSummaryEvaluationDataByMasterEvent(long masterEventID, int type)
        {
            try
            {
                string serviceName = ConfigurationManager.AppSettings["summaryApi"] ?? "mainEvent/summary";
                Dictionary<string, string> pairs = new Dictionary<string, string>()
                                                    {
                                                        { "mainEventId", masterEventID.ToString() },
                                                        { "type", type.ToString() },
                                                    };
                var result = RequestControl.Request(serviceName, "GET", pairs);
                if (result.StatusCode == 200)
                {
                    Logger.DebugFormat("获取总结评估:{0}", result.Html);
                    return Utils.JSONHelper.ConvertToObject<EmergencyHttpResponse<SummaryEvaluationModel>>(result.Html);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("获取总结评估异常", ex);
                return null;
            }
        }


        public MemoryStream GetSummaryEvaluationFileByMasterEvent(long masterEventID, int type)
        {
            try
            {
                string serviceName = ConfigurationManager.AppSettings["summaryDownloadApi"] ?? "mainEvent/summary/download";
                Dictionary<string, string> pairs = new Dictionary<string, string>()
                                                    {
                                                        { "mainEventId", masterEventID.ToString() },
                                                        { "type", type.ToString() },
                                                    };
                var result = RequestControl.RequestStream(serviceName, "GET", pairs);
                if (result != null)
                {
                    Logger.DebugFormat("获取总结评估:{0}", result.Length);
                    return result;
                }
                else
                {
                    Logger.DebugFormat("获取总结评估文件失败:{0}", result.Length);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("获取总结评估异常", ex);
                return null;
            }
        }
    }
}
