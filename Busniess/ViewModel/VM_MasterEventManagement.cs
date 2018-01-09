using Business.Services;
using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
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
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual string TxtSerialNumber { get; set; }
		public virtual string TxtTitle { get; set; }
		public virtual string TxtTime { get; set; }
		public virtual string TxtSubmitParty { get; set; }
		public virtual string TxtLocate { get; set; }

		public virtual ObservableCollection<MasterEvent> MasterEvents { get; set; }

		public virtual bool IsPopupOpen { get; set; }
		public virtual double PopupOffsetX { get; set; }
		public virtual double PopupOffsetY { get; set; }
		public virtual MasterEvent Current { get; set; }

		public virtual DelegateCommand UpdateCommand { get; set; }
		public virtual DelegateCommand DeleteCommand { get; set; }
		public virtual DelegateCommand PopupAddCommand { get; set; }
		public virtual DelegateCommand<AnnouncementModel> PopupEditCommand { get; set; }
		public virtual DelegateCommand PopupCloseCommand { get; set; }

		public VM_MasterEventManagement()
		{
			PageSize = GetPageSize();
			PageIndex = 1;
			TotalCount = 0;
			MasterEventService = new MasterEventService();

			var popupStartPoint = ResolutionService.GetCenterControlOffset(880, 332);
			PopupOffsetX = popupStartPoint.X;
			PopupOffsetY = popupStartPoint.Y;
			GetMasterEventsAction();

			UpdateCommand = new DelegateCommand(UpdateAction);
			DeleteCommand = new DelegateCommand(DeleteAction);
			PopupAddCommand = new DelegateCommand(PopupAddAction);
			PopupEditCommand = new DelegateCommand<AnnouncementModel>(PopupEditAction);
			PopupCloseCommand = new DelegateCommand(PopupCloseAction);
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

		private void GetMasterEventsAction()
		{
			var viewModel = IsAopWapper ? this : this.CreateAopProxy();
			var masterEvents = MasterEventService.GetMasterEvents(viewModel.PageIndex,
				viewModel.PageSize, TxtTitle,
				default(DateTime), default(DateTime),
				string.Empty);
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

		private void UpdateAction()
		{
			//MasterEventService.CreateMasterEvent(Current);
			CleanMessage();
			PopupCloseAction();
			GetMasterEventsAction();
		}

		private void DeleteAction()
		{
			var ids = MasterEvents.Where(item => item.IsChecked).Select(item => item.ID.ToString()).ToList();
			if (ids == null && ids.Count == 0)
			{
				Warn("没有选择删除的公告");
			}
			else
			{
				//MasterEventService(ids);
				GetMasterEventsAction();
			}
		}

		private void PopupAddAction()
		{
			CleanMessage();
			var model = this.CreateAopProxy();
			//model.Current = new AnnouncementModel().CreateAopProxy();
			model.Current.Time = DateTime.Now;
			//model.IsAdding = true;
			model.IsPopupOpen = true;
		}

		public void PopupEditAction(AnnouncementModel model)
		{
			CleanMessage();
			//Current = model;
			//IsAdding = false;
			IsPopupOpen = true;
		}

		private void PopupCloseAction()
		{
			CleanMessage();
			var model = this.CreateAopProxy();
			model.IsPopupOpen = false;
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
			WarningMessage = "没有选择删除的公告";
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
