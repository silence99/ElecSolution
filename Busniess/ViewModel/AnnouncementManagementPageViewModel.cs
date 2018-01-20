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
		public virtual double PopupWidth { get; set; }
		public virtual double PopupHeight { get; set; }
        
        public virtual event SetPopupHandler SetPopupEditToFullScreen;

        public AnnouncementManagementPageViewModel()
		{
			PageSize = GetPageSize();
			Service = new AnnouncementService();
			IsPopupOpen = false;
			PageIndex = 1;
			var popupStartPoint = ResolutionService.GetCenterControlOffset(880, 332);
            PopupHeight = ResolutionService.Height;
            PopupWidth = ResolutionService.Width;
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
				var result = Service.CreateAnnouncement(Current);
                if(result)
                {
                    System.Windows.MessageBox.Show("添加成功！");
                    CleanMessage();
                    PopupCloseAction();
                    GetAnnouncementsAction();
                }
                else
                {
                    System.Windows.MessageBox.Show("添加失败！");
                }
			}
			else
			{
				var result = Service.UpdateAnnouncement(Current);
                if (result)
                {
                    System.Windows.MessageBox.Show("编辑成功！");
                    CleanMessage();
                    PopupCloseAction();
                    GetAnnouncementsAction();
                }
                else
                {
                    System.Windows.MessageBox.Show("编辑失败！");
                }
            }
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
				var result = Service.DeleteAnncoucement(ids);

                if (result)
                {
                    System.Windows.MessageBox.Show("删除成功！");
                    GetAnnouncementsAction();
                }
                else
                {
                    System.Windows.MessageBox.Show("删除失败！");
                }
			}
		}

        private void PopupAddAction()
        {
            CleanMessage();
            var model = this.CreateAopProxy();
            model.Current = new AnnouncementModel().CreateAopProxy();
            model.Current.Time = DateTime.Now;
            model.IsAdding = true;
            model.IsPopupOpen = true;
            if (SetPopupEditToFullScreen != null)
            {
                SetPopupEditToFullScreen();
            }

        }

		public void PopupEditAction(AnnouncementModel model)
		{
			CleanMessage();
			Current = model;
			IsAdding = false;
			IsPopupOpen = true;
            if (SetPopupEditToFullScreen != null)
            {
                SetPopupEditToFullScreen();
            }
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
