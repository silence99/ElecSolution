using Business.Services;
using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace Busniess.ViewModel
{
	public class AnnouncementManagementPageViewModel : NotificationObject
	{
		public virtual string ErrorMessage { get; set; }
		public virtual string WarningMessage { get; set; }
		public virtual string Message { get; set; }
		protected virtual AnnouncementService Service { get; set; }

		public virtual ObservableCollection<AnnouncementModel> Data { get; set; }
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual AnnouncementModel Current { get; set; }
		public virtual DelegateCommand UpdateCommand { get; set; }
		public virtual DelegateCommand DeleteCommand { get; set; }
		public virtual DelegateCommand PopupAddCommand { get; set; }
		public virtual DelegateCommand<AnnouncementModel> PopupEditCommand { get; set; }
		public virtual DelegateCommand PopupCloseCommand { get; set; }

		public virtual bool IsAdding { get; set; }
		public virtual bool IsPopupOpen { get; set; }
		public virtual double PopupOffsetX { get; set; }
		public virtual double PopupOffsetY { get; set; }

		public AnnouncementManagementPageViewModel()
		{
			PageSize = GetPageSize();
			Service = new AnnouncementService();
			IsPopupOpen = false;
			PageIndex = 1;
			var popupStartPoint = ResolutionService.GetCenterControlOffset(880, 332);
			PopupOffsetX = popupStartPoint.X;
			PopupOffsetY = popupStartPoint.Y;
			UpdateCommand = new DelegateCommand(UpdateAction);
			DeleteCommand = new DelegateCommand(DeleteAction);
			PopupAddCommand = new DelegateCommand(PopupAddAction);
			PopupEditCommand = new DelegateCommand<AnnouncementModel>(PopupEditAction);
			PopupCloseCommand = new DelegateCommand(PopupCloseAction);

			GetAnnouncementsAction();
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

		private void GetAnnouncementsAction()
		{
			if (PageSize <= 0 || PageIndex < 0)
			{
				return;
			}
			var data = Service.GetAnnouncements(PageIndex, PageSize);
			var model = IsAopWapper ? this : this.CreateAopProxy();
			model.Data = new ObservableCollection<AnnouncementModel>(data.Data.Select(item => item.CreateAopProxy()));
			model.PageIndex = data.PageIndex;
			model.PageSize = data.PageSize;
			model.TotalCount = data.Count;
			model.TotalPage = (int)Math.Ceiling((double)data.Count / data.PageSize);
		}

		private void UpdateAction()
		{
			if (IsAdding)
			{
				Service.CreateAnnouncement(Current);
			}
			else
			{
				Service.UpdateAnnouncement(Current);
			}
			CleanMessage();
			PopupCloseAction();
			GetAnnouncementsAction();
		}

		private void DeleteAction()
		{
			var ids = Data.Where(item => item.IsChecked).Select(item => item.ID.ToString()).ToList();
			if (ids == null && ids.Count == 0)
			{
				Warn("没有选择删除的公告");
			}
			else
			{
				Service.DeleteAnncoucement(ids);
				GetAnnouncementsAction();
			}
		}

		private void PopupAddAction()
		{
			CleanMessage();
			var model = this.CreateAopProxy();
			model.Current = new AnnouncementModel().CreateAopProxy();
			model.IsAdding = true;
			model.IsPopupOpen = true;
		}

		public void PopupEditAction(AnnouncementModel model)
		{
			CleanMessage();
			Current = model;
			IsAdding = false;
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
