using Business.Services;
using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace Emergence.Business.ViewModel
{
    public class VM_MasterEventManagement : NotificationObject
	{
		public virtual string ErrorMessage { get; set; }
		public virtual string WarningMessage { get; set; }
		public virtual string Message { get; set; }
		public virtual MasterEventService MasterEventService { get; set; }
        public virtual event SetPopupHandler SetPopupToFullScreen;
        public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual string MasterEventSearchValue { get; set; }

		public virtual ObservableCollection<MasterEvent> MasterEvents { get; set; }

		public virtual bool IsPopupOpen { get; set; }
		public virtual double PopupOffsetX { get; set; }
		public virtual double PopupOffsetY { get; set; }
        public virtual string PopupWidth { get; set; }
        public virtual string PopupHeight { get; set; }
        public virtual MasterEvent Current { get; set; }
		public virtual ObservableCollection<DictItem> EventTypes { get; set; }
		public virtual ObservableCollection<DictItem> EventGrades { get; set; }

		public virtual DelegateCommand CreateCommand { get; set; }
		public virtual DelegateCommand DeleteCommand { get; set; }
		public virtual DelegateCommand PopupAddCommand { get; set; }
		public virtual DelegateCommand<AnnouncementModel> PopupEditCommand { get; set; }
		public virtual DelegateCommand PopupCloseCommand { get; set; }
		public virtual DelegateCommand<string> SearchMasterEventListCommand { get; set; }
		public virtual DelegateCommand PopupAddressPickerCommand { get; set; }
		public virtual DelegateCommand CloseAddressPickerCommand { get; set; }
		public virtual bool IsAddressPickPopup { get; set; }


		public VM_MasterEventManagement()
		{
			PageSize = GetPageSize();
			PageIndex = 1;
			TotalCount = 0;
			MasterEventService = new MasterEventService();

            PopupWidth = ResolutionService.Width.ToString();
            PopupHeight = ResolutionService.Height.ToString();
            //var popupStartPoint = ResolutionService.GetCenterControlOffset(880, 332);
            //PopupOffsetX = popupStartPoint.X;
            //PopupOffsetY = popupStartPoint.Y;
            MasterEventSearchValue = "";
			GetMasterEventsAction("");

			CreateCommand = new DelegateCommand(CreateAction);
			DeleteCommand = new DelegateCommand(DeleteAction);
			PopupAddCommand = new DelegateCommand(PopupAddAction);
			PopupCloseCommand = new DelegateCommand(PopupCloseAction);
			SearchMasterEventListCommand = new DelegateCommand<string>(GetMasterEventsAction);
			PopupAddressPickerCommand = new DelegateCommand(PopupAddressPickerAction);
			CloseAddressPickerCommand = new DelegateCommand(ClosePopupAddressPickerAction);
			EventTypes = new ObservableCollection<DictItem>(MetaDataService.EventTypes);
			EventGrades = new ObservableCollection<DictItem>(MetaDataService.EventGrades);
		}

		public void SyncData()
		{
			GetMasterEventsAction("");
		}

		private int GetPageSize()
		{
			var size = ConfigurationManager.AppSettings["PageSize"] ?? "10";
			var t = 10;
			if (!int.TryParse(size, out t))
			{
				t = 10;
			}
			return t;
		}

		private void GetMasterEventsAction(string searchValue)
		{
			var viewModel = IsAopWapper ? this : this.CreateAopProxy();
			if (string.IsNullOrEmpty(searchValue))
			{
				searchValue = viewModel.MasterEventSearchValue;
			}
			var masterEvents = MasterEventService.GetMasterEvents(viewModel.PageIndex,
				viewModel.PageSize, searchValue);
			if (masterEvents != null)
			{
				viewModel.MasterEvents = masterEvents.MasterEvents;
				viewModel.TotalCount = masterEvents.Count;
				viewModel.PageIndex = masterEvents.PageIndex;
				viewModel.PageSize = masterEvents.PageSize;
				viewModel.TotalPage = viewModel.TotalCount == 0 ?
					0 :
					(int)Math.Ceiling((double)viewModel.TotalCount / viewModel.PageSize);
			}
		}

		private void CreateAction()
		{
			PopupCloseAction();
			MasterEventService.CreateMasterEvent(Current);
			CleanMessage();			
			GetMasterEventsAction("");
		}

		private void DeleteAction()
		{
			var ids = MasterEvents.Where(item => item.IsChecked).Select(item => (long)item.ID).ToList();
			if (ids == null && ids.Count == 0)
			{
				Warn("没有选择要删除的主事件！");
			}
			else
			{
				MasterEventService.DeleteMasterEvents(ids);
				GetMasterEventsAction("");
			}
		}

		private void PopupAddAction()
		{
			CleanMessage();
			var model = this.CreateAopProxy();
			model.Current = new MasterEvent().CreateAopProxy();
			model.Current.Time = DateTime.Now.ToString();
			model.Current.EventType = EventTypes[0].Value;
			model.Current.EventTypeName = EventTypes[0].Name;
			model.Current.Grade = EventGrades[0].Value;
			model.Current.GradeName = EventGrades[0].Name;
			model.IsPopupOpen = true;
            SetPopupToFullScreen();

        }

		private void PopupCloseAction()
		{
			CleanMessage();
			var model = this.CreateAopProxy();
			model.IsPopupOpen = false;
		}

		private void PopupAddressPickerAction()
		{
			var obj = this.CreateAopProxy();
			obj.IsAddressPickPopup = true;
		}
		private void ClosePopupAddressPickerAction()
		{
			var obj = this.CreateAopProxy();
			obj.IsAddressPickPopup = false;
		}

		private bool ValidateUpdateAction()
		{
			var model = this.CreateAopProxy();
			if (string.IsNullOrEmpty(model.Current.Title))
			{
				Warn("标题为空");
				return false;
			}

			return true;
		}

		private void Warn(string message)
		{
			CleanMessage();
			WarningMessage = "没有选择删除的主事件";
		}

		private void Error(string message)
		{
			CleanMessage();
			ErrorMessage = message;
		}

		private void Info(string message)
		{
			CleanMessage();
			Message = message;
		}

		private void CleanMessage()
		{
			ErrorMessage = string.Empty;
			WarningMessage = string.Empty;
			Message = string.Empty;
		}
	}
}
