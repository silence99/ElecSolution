using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Common.Data;
using Busniess.Services;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Business.Services;
using Busniess;

namespace Emergence.Business.ViewModel
{
	public class VM_MasterEventDetail : NotificationObject
	{
		#region [Services]
		public virtual MasterEventService masterEventService { get; set; }
		public virtual SubeventService subEventService { get; set; }
		public virtual TeamService teamService { get; set; }
        public virtual MaterialService materialService { get; set; }
        public virtual SummaryEvaluationService summaryEvaluationService { get; set; }
        #endregion

        #region [Property]
        public virtual MasterEvent MasterEventInfo { get; set; }

		public virtual string SubEventSearchValue { get; set; }
		public virtual bool SubEventDetailBlockStatus { get; set; }

		public virtual ObservableCollection<SubEvent> SubEventList { get; set; }

		public virtual SubEvent SubEventDetail { get; set; }

		public virtual SubEvent SubEventEdit { get; set; }

        public virtual SummaryEvaluationModel SummaryEvaluationEdit { get; set; }


        public virtual ObservableCollection<TeamModel> TeamList { get; set; }
		public virtual ObservableCollection<TeamModel> UnSelectedTeamList { get; set; }
		public virtual ObservableCollection<MaterialModel> MaterialList { get; set; }
		public virtual ObservableCollection<MaterialModel> UnSelectedMaterialList { get; set; }
		public virtual ObservableCollection<DictItem> EventGrades { get; set; }

		public virtual SubEventStatus SBStatus { get; set; }

		public virtual bool PageEnabled { get; set; }

		public virtual PopupModel SubEventEditPopup { get; set; }
		public virtual PopupModel SubEventAmplifyPopup { get; set; }
		public virtual PopupModel TeamSelectPopup { get; set; }
		public virtual PopupModel MaterialSelectPopup { get; set; }
		public virtual PopupModel ThreeWindowPopup { get; set; }
        public virtual PopupModel MaxMapAndVideoPopup { get; set; }
        public virtual PopupModel SummaryEvaluationPopup { get; set; }
        #endregion


        #region [Command]
        public virtual DelegateCommand<string> SearchSubEventListCommand { get; set; }
		public virtual DelegateCommand<string> SelectSubEventCommand { get; set; }
		public virtual DelegateCommand<int?> DeleteSubEventCommand { get; set; }
		public virtual DelegateCommand<string> UpdateSubEventStatusCommand { get; set; }

		public virtual DelegateCommand StartCreateSubEventCommand { get; set; }
		public virtual DelegateCommand<int?> StartUpdateSubEventCommand { get; set; }
		public virtual DelegateCommand CloseSubEventEditCommand { get; set; }
		public virtual DelegateCommand CreateSubEventCommand { get; set; }
		public virtual DelegateCommand UpdateSubEventCommand { get; set; }
		public virtual DelegateCommand StratSelectTeamCommand { get; set; }
		public virtual DelegateCommand CloseSelectTeamCommand { get; set; }
		public virtual DelegateCommand SelectTeamCommand { get; set; }
		public virtual DelegateCommand DeleteTeamCommand { get; set; }
		public virtual DelegateCommand StartSelectMaterialCommand { get; set; }
		public virtual DelegateCommand CloseSelectMaterialCommand { get; set; }
		public virtual DelegateCommand SelectMaterialCommand { get; set; }
		public virtual DelegateCommand DeleteMaterialCommand { get; set; }
		public virtual DelegateCommand StartAmplifySubEventCommand { get; set; }
		public virtual DelegateCommand CloseAmplifySubEventCommand { get; set; }
		public virtual DelegateCommand ThreePopupSelecOpenCommand { get; set; }
		public virtual DelegateCommand ThreePopupSelectCloseComman { get; set; }
		public virtual DelegateCommand OpenFullScreenSubEventCommand { get; set; }
		public virtual DelegateCommand OpenFullScreenMapCommand { get; set; }
		public virtual DelegateCommand OpenFullScreenVideoCommand { get; set; }
        public virtual DelegateCommand MasterEventArchiveCommand { get; set; }
        public virtual DelegateCommand OpenSummaryEvaluation1Command { get; set; }
        public virtual DelegateCommand OpenSummaryEvaluation2Command { get; set; }
        public virtual DelegateCommand CloseSummaryEvaluationCommand { get; set; }
        public virtual DelegateCommand DownloadSummaryEvaluationCommand { get; set; }
        public virtual DelegateCommand SubmitSummaryEvaluationCommand { get; set; }
        public virtual DelegateCommand GetSummaryEvaluationCommand { get; set; }


        #endregion

        #region [Event]

        public virtual event SetPopupHandler SetPopupSubEventEditToFullScreen;
		public virtual event SetPopupHandler SetPopupTeamToFullScreen;
        public virtual event SetPopupHandler SetPopupMaterialToFullScreen;
        public virtual event SetPopupHandler SetPopupSummaryEvaluationToFullScreen;
        public virtual event SetPopupHandler ResetSubEventSelectedValue;
		#endregion

		public VM_MasterEventDetail(MasterEvent mEvent)
		{
			PageEnabled = true;
			masterEventService = new MasterEventService();
			subEventService = new SubeventService();
			teamService = new TeamService();
			materialService = new MaterialService();
            summaryEvaluationService = new SummaryEvaluationService();
            SubEventEdit = new SubEvent().CreateAopProxy();
			SBStatus = new SubEventStatus("").CreateAopProxy();
            SummaryEvaluationEdit = new SummaryEvaluationModel().CreateAopProxy();
            SubEventEditPopup = new PopupModel().CreateAopProxy();
			TeamSelectPopup = new PopupModel().CreateAopProxy();
			MaterialSelectPopup = new PopupModel().CreateAopProxy();
			ThreeWindowPopup = new PopupModel().CreateAopProxy();
			MaxMapAndVideoPopup = new PopupModel().CreateAopProxy();
            SubEventAmplifyPopup = new PopupModel().CreateAopProxy();
            SummaryEvaluationPopup = new PopupModel().CreateAopProxy();
            EventGrades = new ObservableCollection<DictItem>(MetaDataService.EventGrades);
			//this.eventAggregator = eventAggregator;

			SearchSubEventListCommand = new DelegateCommand<string>(new Action<string>(SearchSubEventAction));
			SelectSubEventCommand = new DelegateCommand<string>(new Action<string>(SelectSubEventAction));
			DeleteSubEventCommand = new DelegateCommand<int?>(DeleteSubEventAction);
			UpdateSubEventStatusCommand = new DelegateCommand<string>(new Action<string>(UpdateSubEventStatusAction), new Func<string, bool>(CheckSubEventStatusCanChangeAction));
			StartCreateSubEventCommand = new DelegateCommand(new Action(StartCreateSubEventAction));
			StartUpdateSubEventCommand = new DelegateCommand<int?>(StartUpdateSubEventAction);
			CloseSubEventEditCommand = new DelegateCommand(new Action(CloseSubEventEditAction));
			CreateSubEventCommand = new DelegateCommand(new Action(CreateSubEventAction));
			UpdateSubEventCommand = new DelegateCommand(new Action(UpdateSubEventAction));
			StratSelectTeamCommand = new DelegateCommand(new Action(StartSelectTeamAction));
			CloseSelectTeamCommand = new DelegateCommand(new Action(CloseSelectTeamAction));
			SelectTeamCommand = new DelegateCommand(new Action(SelectTeamAction));
			DeleteTeamCommand = new DelegateCommand(new Action(DeleteTeamAction));
			StartSelectMaterialCommand = new DelegateCommand(new Action(StartSelectMaterialAction));
			CloseSelectMaterialCommand = new DelegateCommand(new Action(CloseSelectMaterialAction));
			SelectMaterialCommand = new DelegateCommand(new Action(SelectMaterialAction));
			DeleteMaterialCommand = new DelegateCommand(new Action(DeleteMaterialAction));
			StartAmplifySubEventCommand = new DelegateCommand(new Action(StartSubEventAmplifyAction));
			CloseAmplifySubEventCommand = new DelegateCommand(new Action(CloseSubEventAmplifyAction));
			ThreePopupSelecOpenCommand = new DelegateCommand(new Action(ThreePopupSelectOpenAction));
			ThreePopupSelectCloseComman = new DelegateCommand(new Action(ThreePopupSelectCloseAction));
			OpenFullScreenSubEventCommand = new DelegateCommand(new Action(OpenFullScreenSubEventAction));
			OpenFullScreenMapCommand = new DelegateCommand(new Action(OpenFullScreenMapAction));
			OpenFullScreenVideoCommand = new DelegateCommand(new Action(OpenFullScreenVideoAction));
            MasterEventArchiveCommand = new DelegateCommand(new Action(MasterEventArchiveAction));
            OpenSummaryEvaluation1Command = new DelegateCommand(new Action(OpenSummaryEvaluation1Action));
            OpenSummaryEvaluation2Command = new DelegateCommand(new Action(OpenSummaryEvaluation2Action));
            CloseSummaryEvaluationCommand = new DelegateCommand(new Action(CloseSummaryEvaluationAction));
            DownloadSummaryEvaluationCommand = new DelegateCommand(new Action(DownloadSummaryEvaluationAction));
            SubmitSummaryEvaluationCommand = new DelegateCommand(new Action(SubmitSummaryEvaluationAction));
            GetSummaryEvaluationCommand = new DelegateCommand(new Action(GetSummaryEvaluationAction));


            if (mEvent != null)
			{
				InitializVM(mEvent);
			}
		}

		#region [Public Method]
		public void InitializVM(MasterEvent masterEvent)
		{
			this.MasterEventInfo = masterEvent;
			var subEvents = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID).Result;
			if (subEvents != null)
			{
				SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
			}
		}

		public void CloseAllPopupWindow()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.MaterialSelectPopup.IsOpen = false;
			thisAop.SubEventAmplifyPopup.IsOpen = false;
			thisAop.MaxMapAndVideoPopup.IsOpen = false;
			thisAop.SubEventEditPopup.IsOpen = false;
			thisAop.TeamSelectPopup.IsOpen = false;
		}

		public void SetSubEventDetailBlockStatus(bool status)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.SubEventDetailBlockStatus = status;
		}
		#endregion



		#region [Private Method]
		#region [Command Action]
		private void SearchSubEventAction(string searchCondition)
		{
			if (!string.IsNullOrEmpty(searchCondition))
			{
				GetSubEventListOb(searchCondition);
			}
		}
		public void SelectSubEventAction(string subEventID)
		{
			RefershSubEventDetailBlock(subEventID);
		}

		private void UpdateSubEventStatusAction(string status)
		{
			if (!string.IsNullOrEmpty(status))
			{
				List<string> ids = new List<string>();
				var st = Enum.Parse(typeof(Enumerator.SubEventStatus), status).GetHashCode();
				ids.Add(SubEventDetail.Id.ToString());
				var result = this.UpdateSubEvent(ids, st);
				if (result)
				{
					string str = string.Empty;
					switch (st)
					{
						case 0:
							str = "登记";
							break;
						case 1:
							str = "发布";
							break;
						case 2:
							str = "接受";
							break;
						case 3:
							str = "执行";
							break;
						case 5:
							str = "完成";
							break;
						default: break;
					}
					SBStatus.SetSubEventStatus(status);
					ShowMessageBox(str + "成功！");
				}
			}
		}
		private bool CheckSubEventStatusCanChangeAction(string status)
		{
			if (string.IsNullOrEmpty(status))
			{
				return false;
			}
			else
			{
				return SBStatus.StatusColorList[status] == Enumerator.EllipseUnselectedColor;
			}
		}
		private void DeleteSubEventAction(int? subEventID)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;

			if (subEventID != null)
			{
				List<string> ids = new List<string>();
				ids.Add(subEventID.ToString());
				var result = this.UpdateSubEvent(ids, 9);
				if (result)
				{
					ShowMessageBox("删除子事件成功！");
					//thisAop.SubEventList.Remove(thisAop.SubEventList.Where(a => a.Id == subEventID).FirstOrDefault());
					if (ResetSubEventSelectedValue != null)
					{
                        if (SubEventList.Count <= 1)
                        {
                            CleanSubEventDetailBlock();
                            SetSubEventDetailBlockStatus(false);
                        }
                        //else
                        //{
                        //    ResetSubEventSelectedValue();
                        //}
                    }
                }
				else
				{
					ShowMessageBox("删除子事件失败！");
				}
				GetSubEventListOb("");
				//if (ResetSubEventSelectedValue != null)
				//{
				//    if (SubEventList.Count <= 0)
				//    {
				//        CleanSubEventDetailBlock();
				//    }
				//    else
				//    {
				//        ResetSubEventSelectedValue();
				//    }
				//}
			}
		}

		private void StartCreateSubEventAction()
		{
			SubEventEdit = SubEventEdit ?? new SubEvent().CreateAopProxy();
			SubEventEdit.ChildGrade = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Value;
			SubEventEdit.ChildGradeName = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Name;

			//         {
			//	ChildGrade = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Value,
			//	ChildGradeName = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Name
			//};
			SubEventEditPopup.PopupName = "创建子事件";
			SubEventEditPopup.IsOpen = true;
			if (SetPopupSubEventEditToFullScreen != null)
			{
				SetPopupSubEventEditToFullScreen();
			}
			SetPageEnableStatus(false);
		}

		private void StartUpdateSubEventAction(int? subEventID)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.SubEventEditPopup.PopupName = "编辑子事件";
			thisAop.SubEventEdit = this.SubEventDetail;
			thisAop.SubEventEditPopup.IsOpen = true;
			if (SetPopupSubEventEditToFullScreen != null)
			{
				SetPopupSubEventEditToFullScreen();
			}
			SetPageEnableStatus(false);
		}

		private void CloseSubEventEditAction()
		{
			SetPageEnableStatus(true);
			this.SubEventEditPopup.IsOpen = false;
			this.SubEventEdit = new SubEvent()
			{
				ChildGrade = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Value,
				ChildGradeName = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Name
			};
		}

		private void CreateSubEventAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;

			if (MasterEventInfo != null && SubEventEdit != null)
			{
				var result = subEventService.CreateChildEvent(MasterEventInfo.ID.ToString(), SubEventEdit);
				if (result)
				{
					this.SubEventEditPopup.IsOpen = false;
					SetPageEnableStatus(true);
					ShowMessageBox("创建子事件成功！");
					//RefershSubEventDetailBlock(thisAop.SubEventEdit.Id.ToString());
					thisAop.SubEventEdit = new SubEvent().CreateAopProxy();
				}
				else
				{
					ShowMessageBox("创建子事件失败！");
				}
				GetSubEventListOb("");
				ResetSubEventSelectedValue();
			}
		}

		private void UpdateSubEventAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;

			if (SubEventEdit != null)
			{
				var result = subEventService.UpdateChildEvent(MasterEventInfo.ID.ToString(), SubEventEdit);
				if (result)
				{
					thisAop.SubEventEditPopup.IsOpen = false;
					thisAop.SubEventDetail = thisAop.SubEventEdit;
					ShowMessageBox("编辑子事件成功！");
					SetPageEnableStatus(true);
					thisAop.SubEventEdit = new SubEvent().CreateAopProxy();
				}
				else
				{
					ShowMessageBox("编辑子事件失败！");
				}
				GetSubEventListOb("");
				if (thisAop.SubEventList.Count <= 0)
				{
					CleanSubEventDetailBlock();
				}
				else
				{

				}
			}
		}
		private void StartSelectTeamAction()
		{
			var thisAop = this.CreateAopProxy();
			var unbindedTeamList = teamService.GetUnbindedTeamData(0, 10000).Result.Data;
			if (unbindedTeamList != null)
			{
				thisAop.UnSelectedTeamList = new ObservableCollection<TeamModel>(unbindedTeamList.Select(a => a.CreateAopProxy()));
			}
			this.TeamSelectPopup.PopupName = "选择队伍";
			this.TeamSelectPopup.IsOpen = true;
			if (SetPopupTeamToFullScreen != null)
			{
				SetPopupTeamToFullScreen();
			}
			SetPageEnableStatus(false);
		}
		private void CloseSelectTeamAction()
		{
			SetPageEnableStatus(true);
			this.TeamSelectPopup.IsOpen = false;
			this.UnSelectedTeamList = new ObservableCollection<TeamModel>();
		}
		private void SelectTeamAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var ids = UnSelectedTeamList.Where(a => a.IsChecked).Select(a => (long)a.ID).ToList();
			var result = teamService.BindingUnbindingTeamToSubevnt(SubEventDetail.Id, ids, "POST");
			GetTeamListOb();
			if (result)
			{
				SetPageEnableStatus(true);
				thisAop.TeamSelectPopup.IsOpen = false;
				//thisAop.SubEventEdit = new SubEvent();
				ShowMessageBox("添加成功！");
			}
			else
			{
				ShowMessageBox("添加失败！");
			}
			GetTeamListOb();
		}
		private void DeleteTeamAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var ids = TeamList.Where(a => a.IsChecked).Select(a => (long)a.ID).ToList();
			var result = teamService.BindingUnbindingTeamToSubevnt(SubEventDetail.Id, ids, "DELETE");
			GetTeamListOb();
			if (result)
			{
				SetPageEnableStatus(true);
				thisAop.SubEventEditPopup.IsOpen = false;
				thisAop.SubEventEdit = new SubEvent();
				ShowMessageBox("删除成功！");
			}
			else
			{
				ShowMessageBox("删除失败！");
			}
		}
		private void StartSelectMaterialAction()
		{
			try
			{
				var thisAop = this.AopWapper as VM_MasterEventDetail;
				var data = materialService.GetUnbindedMaterials(0, 10000);
				if (data != null && data.Result != null)
				{
					var unbindedMaterialList = data.Result.Data;
					if (unbindedMaterialList != null)
					{
						thisAop.UnSelectedMaterialList = new ObservableCollection<MaterialModel>(unbindedMaterialList.Select(a => a.CreateAopProxy()));
					}
				}
				else
				{
					thisAop.UnSelectedMaterialList = new ObservableCollection<MaterialModel>();
				}
				this.MaterialSelectPopup.PopupName = "选择物资";
				this.MaterialSelectPopup.IsOpen = true;
				if (SetPopupMaterialToFullScreen != null)
				{
					SetPopupMaterialToFullScreen();
				}
				SetPageEnableStatus(false);
			}
			catch (Exception ex)
			{
				Logger.Error("Add materials errors", ex);
			}
		}
		private void CloseSelectMaterialAction()
		{
			SetPageEnableStatus(true);
			this.MaterialSelectPopup.IsOpen = false;
			this.UnSelectedMaterialList = new ObservableCollection<MaterialModel>();
		}
		private void SelectMaterialAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var ids = UnSelectedMaterialList.Where(a => a.IsChecked).Select(a => (long)a.ID).ToList();
			var result = materialService.BindingMaterialToSubevnt(SubEventDetail.Id, ids);
			GetMaterialListOb();
			if (result)
			{
				SetPageEnableStatus(true);
				thisAop.MaterialSelectPopup.IsOpen = false;
				ShowMessageBox("添加成功！");
			}
			else
			{
				ShowMessageBox("添加失败！");
			}
		}
		private void DeleteMaterialAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var ids = UnSelectedMaterialList.Where(a => a.IsChecked).Select(a => (long)a.ID).ToList();
			var result = materialService.UnbindingMaterialFromSubevnt(SubEventDetail.Id, ids);
			if (result)
			{
				ShowMessageBox("删除成功！");
				SetPageEnableStatus(true);
				thisAop.MaterialSelectPopup.IsOpen = false;
			}
			else
			{
				ShowMessageBox("删除失败！");
			}
			GetMaterialListOb();
		}
		private void StartSubEventAmplifyAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			this.SubEventAmplifyPopup.PopupName = "";
			this.SubEventAmplifyPopup.IsOpen = true;
			SetPageEnableStatus(false);
		}
		private void CloseSubEventAmplifyAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			SetPageEnableStatus(true);
			thisAop.SubEventAmplifyPopup.IsOpen = false;
		}

		public void ThreePopupSelectOpenAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.ThreeWindowPopup.IsOpen = true;
			thisAop.ThreeWindowPopup.PopupWidth = ResolutionService.Width.ToString();
			thisAop.ThreeWindowPopup.PopupHeight = ResolutionService.Height.ToString();
		}
		public void ThreePopupSelectCloseAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.ThreeWindowPopup.IsOpen = false;
		}
		public void OpenFullScreenSubEventAction()
		{

		}
		public void OpenFullScreenMapAction()
		{

		}
		public void OpenFullScreenVideoAction()
		{

		}

		private void MasterEventArchiveAction()
		{
			var unComplatedSubEvents = SubEventList.Where(a => a.State != "5").ToList();
			if (unComplatedSubEvents.Count > 0)
			{
				var titleList = string.Join("\n", unComplatedSubEvents.Select(a => a.ChildTitle).ToArray());
				string message = "以下子事件未完成，不能归档：" + titleList;
				System.Windows.MessageBox.Show(message);
			}
			else
			{
				List<long> masterEventIDs = new List<long>();
				masterEventIDs.Add(MasterEventInfo.ID);
				var result = masterEventService.UpdateMasterEventState(masterEventIDs, 1);
				if (result)
				{
					System.Windows.MessageBox.Show("归档成功");
				}
				else
				{
					System.Windows.MessageBox.Show("归档失败，请联系管理员！");
				}
			}
		}

        private void OpenSummaryEvaluation1Action()
        {
            var thisAop = this.AopWapper as VM_MasterEventDetail;
            thisAop.SummaryEvaluationPopup.PopupName = "总结评估";
            thisAop.SummaryEvaluationEdit = new SummaryEvaluationModel().CreateAopProxy();
            thisAop.SummaryEvaluationEdit.Type = 1;
            thisAop.SummaryEvaluationEdit.MainEventId = MasterEventInfo.ID;
            thisAop.SummaryEvaluationPopup.IsOpen = true;
            if (thisAop.SetPopupSummaryEvaluationToFullScreen != null)
            {
                thisAop.SetPopupSummaryEvaluationToFullScreen();
            }
        }
        private void OpenSummaryEvaluation2Action()
        {
            var thisAop = this.AopWapper as VM_MasterEventDetail;
            thisAop.SummaryEvaluationPopup.PopupName = "信息汇总";
            thisAop.SummaryEvaluationEdit = new SummaryEvaluationModel().CreateAopProxy();
            thisAop.SummaryEvaluationEdit.Type = 2;
            thisAop.SummaryEvaluationEdit.MainEventId = thisAop.MasterEventInfo.ID;
            thisAop.SummaryEvaluationPopup.IsOpen = true;
            if (thisAop.SetPopupSummaryEvaluationToFullScreen != null)
            {
                thisAop.SetPopupSummaryEvaluationToFullScreen();
            }
        }
        private void CloseSummaryEvaluationAction()
        {
            var thisAop = this.AopWapper as VM_MasterEventDetail;
            thisAop.SummaryEvaluationPopup.IsOpen = false;
            thisAop.SummaryEvaluationEdit = new SummaryEvaluationModel().CreateAopProxy();
        }
        private void DownloadSummaryEvaluationAction()
        {
            var thisAop = this.AopWapper as VM_MasterEventDetail;

            thisAop.SummaryEvaluationPopup.IsOpen = false;
            ShowMessageBox("下载成功！");
        }
        private void SubmitSummaryEvaluationAction()
        {
            var thisAop = this.AopWapper as VM_MasterEventDetail;
            var result = summaryEvaluationService.UpdateSummaryEvaluation(thisAop.SummaryEvaluationEdit);

            if (result)
            {
                thisAop.SummaryEvaluationPopup.IsOpen = false;
                ShowMessageBox("提交成功！");
            }
            else
            {
                thisAop.SummaryEvaluationPopup.IsOpen = false;
                ShowMessageBox("提交失败！");
            }

        }
        private void GetSummaryEvaluationAction()
        {
            var thisAop = this.AopWapper as VM_MasterEventDetail;
            EmergencyHttpResponse<SummaryEvaluationModel> result = summaryEvaluationService.GetSummaryEvaluationDataByMasterEvent(thisAop.MasterEventInfo.ID, thisAop.SummaryEvaluationEdit.Type);
            if (result != null)
            {
                //ShowMessageBox("获取成功！");
                thisAop.SummaryEvaluationEdit.Id = result.Result.Id;
                thisAop.SummaryEvaluationEdit.ImpleAssessment = result.Result.ImpleAssessment;
                thisAop.SummaryEvaluationEdit.Summary = result.Result.Summary;
                thisAop.SummaryEvaluationEdit.OrganAssessment = result.Result.OrganAssessment;
                // = result.Result.CreateAopProxy();
            }
            else
            {
                //ShowMessageBox("获取失败，没有历史数据！");
            }
        }
        #endregion

        #region [Request service Methods]
        private void GetSubEventListOb(string searchCondition)
		{
			try
			{
				var data = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID, searchCondition ?? "");
				var subEvents = (data == null || data.Result == null || data.Result.Data == null) ? new Busniess.EmergencyHttpListResult<SubEvent>() { Data = new SubEvent[0] } : data.Result;
				var thisAop = this.AopWapper as VM_MasterEventDetail;
				thisAop.SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
			}
			catch (Exception ex)
			{
				Logger.Error("获取子事件数据异常", ex);
			}
			//SubEventList.Clear();
			//SubEventList.join(result);
		}
		private void GetTeamListOb()
		{
			try
			{
				var thisAop = this.AopWapper as VM_MasterEventDetail;
				var data = teamService.GetbindingTeamData(0, 1000, SubEventDetail.Id);
				if (data != null)
				{
					var teams = data.Result ?? new Busniess.EmergencyHttpListResult<TeamModel>();
					thisAop.TeamList = new ObservableCollection<TeamModel>(teams.Data.Select(o => o.CreateAopProxy()));
				}
				else
				{
					thisAop.TeamList = new ObservableCollection<TeamModel>();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Load teams error", ex);
			}
		}
		private void GetMaterialListOb()
		{
			try
			{
				var thisAop = this.AopWapper as VM_MasterEventDetail;
				var data = materialService.GetMaterialsBindingToSubevent(0, 1000, this.SubEventDetail.Id);
				if (data != null)
				{
					var materials = data.Result ?? new Busniess.EmergencyHttpListResult<MaterialModel>();
					if (materials != null)
					{
						thisAop.MaterialList = new ObservableCollection<MaterialModel>(materials.Data.Select(o => o.CreateAopProxy()));
					}
				}
				else
				{
					thisAop.MaterialList = new ObservableCollection<MaterialModel>();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Load materials error", ex);
			}
		}

		private bool UpdateSubEvent(List<string> subEventIDs, int status)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			if (subEventIDs != null && subEventIDs.Count() > 0 && status >= 0)
			{
				var result = subEventService.UpdateChildeEventState(subEventIDs, status);
				if (result)
				{
					var tempSubEventList = thisAop.SubEventList.Where(a => subEventIDs.Contains(a.Id.ToString())).ToList();
					if (tempSubEventList.Count > 0)
					{
						foreach (var tempSE in tempSubEventList)
						{
							tempSE.State = status.ToString();
						}
					}
				}
				return result;
			}
			return false;
		}
		#endregion

		#region [other method]
		private void SetPageEnableStatus(bool status)
		{
			//var thisAop = this.AopWapper as VM_MasterEventDetail;
			//thisAop.PageEnabled = status;
		}
		private void GetSelectedSubEventInfo(string subEventID)
		{
			if (!string.IsNullOrEmpty(subEventID))
			{
				int tempID = -1;
				if (int.TryParse(subEventID, out tempID))
				{
					var subEvent = this.SubEventList.FirstOrDefault(a => a.Id == tempID);
					RefershSubEventDetailBlock(subEvent.Id.ToString());
				}
			}
		}

		private void RefershSubEventDetailBlock(string subEventID)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;

			if (!string.IsNullOrEmpty(subEventID) && thisAop.SubEventList.Count > 0)
			{
				var subEvent = thisAop.SubEventList.FirstOrDefault(a => a.Id.ToString() == subEventID);
				if (subEvent != null)
				{
					SubEventDetail = subEvent;
					GetTeamListOb();
					GetMaterialListOb();
					SBStatus.ResetSubEventStatus();
					int sbStatus = -1;
					if (int.TryParse(SubEventDetail.State, out sbStatus))
					{
						Enum.GetName(typeof(Enumerator.SubEventStatus), sbStatus);
						SBStatus.SetSubEventStatus(SubEventDetail.State);
					}
					SetSubEventDetailBlockStatus(true);
				}
			}
		}
		public void CleanSubEventDetailBlock()
		{
			var aopVapper = this.IsAopWapper ? this : this.AopWapper as VM_MasterEventDetail;
			aopVapper.SubEventDetail = new SubEvent().CreateAopProxy();
			aopVapper.TeamList = new ObservableCollection<TeamModel>();
			aopVapper.MaterialList = new ObservableCollection<MaterialModel>();
			SBStatus.ResetSubEventStatus();
            aopVapper.SetSubEventDetailBlockStatus(false);

        }
		private void ShowMessageBox(string value)
		{
			System.Windows.MessageBox.Show(value);
		}
		#endregion
		#endregion
	}

	public class SubEventStatus : NotificationObject
	{
		public virtual string Registed { get; set; }
		public virtual string Published { get; set; }
		public virtual string Accepted { get; set; }
		public virtual string Executed { get; set; }
		public virtual string Completed { get; set; }

		public virtual string selectedColor { get; set; }
		public virtual string unSelectedColor { get; set; }

		public virtual Dictionary<string, string> StatusColorList { get; set; }

		public SubEventStatus(string status)
		{
			selectedColor = Enumerator.EllipseSelectedColor;
			unSelectedColor = Enumerator.EllipseUnselectedColor;

			Registed = unSelectedColor;
			Published = unSelectedColor;
			Accepted = unSelectedColor;
			Executed = unSelectedColor;
			Completed = unSelectedColor;
			StatusColorList = new Dictionary<string, string>();
			StatusColorList.Add("Registed", unSelectedColor);
			StatusColorList.Add("Published", unSelectedColor);
			StatusColorList.Add("Accepted", unSelectedColor);
			StatusColorList.Add("Executed", unSelectedColor);
			StatusColorList.Add("Completed", unSelectedColor);
			SetSubEventStatus(status);
		}

		public void ResetSubEventStatus()
		{
			Registed = unSelectedColor;
			Published = unSelectedColor;
			Accepted = unSelectedColor;
			Executed = unSelectedColor;
			Completed = unSelectedColor;
			StatusColorList["Registed"] = unSelectedColor;
			StatusColorList["Published"] = unSelectedColor;
			StatusColorList["Accepted"] = unSelectedColor;
			StatusColorList["Executed"] = unSelectedColor;
			StatusColorList["Completed"] = unSelectedColor;
		}
		public void SetSubEventStatus(string status)
		{
			if (!string.IsNullOrEmpty(status))
			{
				var statusValue = (int)Enum.Parse(typeof(Enumerator.SubEventStatus), status);
				switch (statusValue)
				{
					case (int)Enumerator.SubEventStatus.Registed:
						this.Registed = selectedColor;
						this.StatusColorList["Registed"] = selectedColor;
						break;
					case (int)Enumerator.SubEventStatus.Published:
						this.Registed = selectedColor;
						this.Published = selectedColor;
						this.StatusColorList["Registed"] = selectedColor;
						this.StatusColorList["Published"] = selectedColor;
						break;
					case (int)Enumerator.SubEventStatus.Accepted:
						this.Registed = selectedColor;
						this.Published = selectedColor;
						this.Accepted = selectedColor;
						this.StatusColorList["Registed"] = selectedColor;
						this.StatusColorList["Published"] = selectedColor;
						this.StatusColorList["Accepted"] = selectedColor;
						break;
					case (int)Enumerator.SubEventStatus.Executed:
						this.Registed = selectedColor;
						this.Published = selectedColor;
						this.Accepted = selectedColor;
						this.Executed = selectedColor;
						this.StatusColorList["Registed"] = selectedColor;
						this.StatusColorList["Published"] = selectedColor;
						this.StatusColorList["Accepted"] = selectedColor;
						this.StatusColorList["Executed"] = selectedColor;
						break;
					case (int)Enumerator.SubEventStatus.Completed:
						this.Registed = selectedColor;
						this.Published = selectedColor;
						this.Accepted = selectedColor;
						this.Executed = selectedColor;
						this.Completed = selectedColor;
						this.StatusColorList["Registed"] = selectedColor;
						this.StatusColorList["Published"] = selectedColor;
						this.StatusColorList["Accepted"] = selectedColor;
						this.StatusColorList["Executed"] = selectedColor;
						this.StatusColorList["Completed"] = selectedColor;
						break;
					default:
						break;
				}
			}
		}
	}
}
