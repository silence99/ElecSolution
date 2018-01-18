﻿using Business.Services;
using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;

namespace Emergence.Business.ViewModel
{
	public class TeamListPageViewModel : NotificationObject
	{
		public virtual ObservableCollection<TeamModel> Teams { get; set; }
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual string QueryTeamName { get; set; }
		public virtual string QueryDepartment { get; set; }
        public virtual string QueryChargeName { get; set; }
        public virtual string SerachInfo { get; set; }
        public virtual TeamModel CurrentTeam { get; set; }
		public virtual double PopupWidth { get; set; }
		public virtual double PopupHeight { get; set; }
		public virtual bool IsPopoupOpen { get; set; }
		public virtual bool IsPageEnabled { get; set; }
        public virtual event SetPopupHandler SetPopupEditToFullScreen;

        public virtual ObservableCollection<DictItem> TeamDepts { get; set; }

		public void PopupTeamEdit()
		{
			IsPopoupOpen = true;
			IsPageEnabled = false;
            if (SetPopupEditToFullScreen != null)
            {
                SetPopupEditToFullScreen();
            }
        }

		public void ClosePopup()
		{
			IsPopoupOpen = false;
			IsPageEnabled = true;
		}

		public TeamListPageViewModel()
		{
			PageIndex = 1;
			PageSize = 10;
			TotalCount = 0;
			TotalPage = 0;
			IsPopoupOpen = false;
			IsPageEnabled = true;
            PopupHeight = ResolutionService.Height;
            PopupWidth = ResolutionService.Width;
            TeamDepts = new ObservableCollection<DictItem>(MetaDataService.TeamDepartments);
		}
	}
}
