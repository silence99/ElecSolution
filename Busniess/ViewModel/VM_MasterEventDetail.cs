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

namespace Emergence.Business.ViewModel
{
	public class VM_MasterEventDetail : NotificationObject
	{
		#region [Services]
		public virtual MasterEventService masterEventService { get; set; }
		public virtual SubeventService subEventService { get; set; }
		public virtual TeamService teamService { get; set; }
		public virtual MaterialService materialService { get; set; }
		#endregion

		#region [Property]
		public virtual MasterEvent MasterEventInfo { get; set; }

		public virtual string SubEventSearchValue { get; set; }

		public virtual ObservableCollection<SubEvent> SubEventList { get; set; }

		public virtual SubEvent SubEventDetail { get; set; }

		public virtual SubEvent SubEventEdit { get; set; }

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

		#endregion

		public VM_MasterEventDetail(MasterEvent mEvent)
		{
			PageEnabled = true;
			masterEventService = new MasterEventService();
			subEventService = new SubeventService();
			teamService = new TeamService();
			materialService = new MaterialService();
			SBStatus = new SubEventStatus("").CreateAopProxy();
			SubEventEditPopup = new PopupModel().CreateAopProxy();
			TeamSelectPopup = new PopupModel().CreateAopProxy();
			MaterialSelectPopup = new PopupModel().CreateAopProxy();
			ThreeWindowPopup = new PopupModel().CreateAopProxy();
			MaxMapAndVideoPopup = new PopupModel().CreateAopProxy();
			SubEventAmplifyPopup = new PopupModel().CreateAopProxy();
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
					SBStatus.SetSubEventStatus(status);
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
			if (subEventID != null)
			{
				List<string> ids = new List<string>();
				ids.Add(subEventID.ToString());
				var result = this.UpdateSubEvent(ids, 9);
				if (result)
				{
					ShowMessageBox("删除子事件成功！");
				}
				else
				{
					ShowMessageBox("删除子事件失败！");
				}
				GetSubEventListOb("");
			}
		}

		private void StartCreateSubEventAction()
		{
			SubEventEdit = SubEventEdit ?? new SubEvent()
			{
				ChildGrade = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Value,
				ChildGradeName = EventGrades == null || EventGrades.Count == 0 ? "" : EventGrades[0].Name
			};
			SubEventEditPopup.PopupName = "创建子事件";
			SubEventEditPopup.IsOpen = true;
			SetPageEnableStatus(false);
		}

		private void StartUpdateSubEventAction(int? subEventID)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.SubEventEditPopup.PopupName = "编辑子事件";
			thisAop.SubEventEdit = this.SubEventDetail;
			thisAop.SubEventEditPopup.IsOpen = true;
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
			if (MasterEventInfo != null && SubEventEdit != null)
			{
				var result = subEventService.CreateChildEvent(MasterEventInfo.ID.ToString(), SubEventEdit);
				if (result)
				{
					SetPageEnableStatus(true);
					ShowMessageBox("创建子事件成功！");
					this.SubEventEditPopup.IsOpen = false;
					this.SubEventEdit = new SubEvent();
				}
				else
				{
					ShowMessageBox("创建子事件失败！");
				}
				GetSubEventListOb("");
			}
		}

		private void UpdateSubEventAction()
		{
			if (SubEventEdit != null)
			{
				var result = subEventService.UpdateChildEvent(MasterEventInfo.ID.ToString(), SubEventEdit);
				if (result)
				{
					ShowMessageBox("编辑子事件成功！");
					SetPageEnableStatus(true);
					this.SubEventEditPopup.IsOpen = false;
					this.SubEventEdit = new SubEvent();
				}
				else
				{
					ShowMessageBox("编辑子事件失败！");
				}
				GetSubEventListOb("");
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
			if (result)
			{
				ShowMessageBox("添加成功！");
				SetPageEnableStatus(true);
				thisAop.SubEventEditPopup.IsOpen = false;
				thisAop.SubEventEdit = new SubEvent();
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
			if (result)
			{
				ShowMessageBox("删除成功！");
				SetPageEnableStatus(true);
				thisAop.SubEventEditPopup.IsOpen = false;
				thisAop.SubEventEdit = new SubEvent();
			}
			else
			{
				ShowMessageBox("删除失败！");
			}
			GetTeamListOb();
		}
		private void StartSelectMaterialAction()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var unbindedMaterialList = materialService.GetUnbindedMaterials(0, 10000).Result.Data;
			if (unbindedMaterialList != null)
			{
				thisAop.UnSelectedMaterialList = new ObservableCollection<MaterialModel>(unbindedMaterialList.Select(a => a.CreateAopProxy()));
			}
			this.MaterialSelectPopup.PopupName = "选择物资";
			this.MaterialSelectPopup.IsOpen = true;
			SetPageEnableStatus(false);
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
			if (result)
			{
				ShowMessageBox("添加成功！");
				SetPageEnableStatus(true);
				thisAop.MaterialSelectPopup.IsOpen = false;
			}
			else
			{
				ShowMessageBox("添加失败！");
			}
			GetMaterialListOb();
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
			this.SubEventAmplifyPopup.PopupName = "选择物资";
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

		#endregion

		#region [Request service Methods]
		private void GetSubEventListOb(string searchCondition)
		{
			var subEvents = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID, searchCondition ?? "").Result;
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
			//SubEventList.Clear();
			//SubEventList.join(result);
		}
		private void GetTeamListOb()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var teams = teamService.GetbindingTeamData(0, 1000, SubEventDetail.Id).Result;
			thisAop.TeamList = new ObservableCollection<TeamModel>(teams.Data.Select(o => o.CreateAopProxy()));
		}
		private void GetMaterialListOb()
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			var materials = materialService.GetMaterialsBindingToSubevent(0, 1000, this.SubEventDetail.Id).Result;
			if (materials != null)
			{
				thisAop.MaterialList = new ObservableCollection<MaterialModel>(materials.Data.Select(o => o.CreateAopProxy()));
			}
		}

		private bool UpdateSubEvent(List<string> subEventIDs, int status)
		{
			if (subEventIDs != null && subEventIDs.Count() > 0 && status >= 0)
			{
				var result = subEventService.UpdateChildeEventState(subEventIDs, status);
				return result;
			}
			return false;
		}
		#endregion

		#region [other method]
		private void SetPageEnableStatus(bool status)
		{
			var thisAop = this.AopWapper as VM_MasterEventDetail;
			thisAop.PageEnabled = status;
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
			var subEvent = this.SubEventList.FirstOrDefault(a => a.Id.ToString() == subEventID);
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
