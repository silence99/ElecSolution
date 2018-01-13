using Business.Services;
using Emergence.Common.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace Emergence.Business.ViewModel
{
	public class TeamDetailPageViewModel : TeamModel
	{
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual ObservableCollection<PersonModel> Members { get; set; }

		public virtual double PopupWidth { get; set; }
		public virtual double PopupHeight { get; set; }
		public virtual bool IsPopoupOpen { get; set; }
		public virtual bool IsPageEnabled { get; set; }
		public virtual PersonModel CurrentPerson { get; set; }
		public virtual bool IsTeamEditModel { get; set; }
		public virtual Visibility CancelEditButtonVisibility { get; set; }
		public virtual string EditTeamButtonLabel { get; set; }
		public virtual bool IsAddMember { get; set; }

		public virtual ObservableCollection<DictItem> Places { get; set; }

		public void PopupTeamEdit()
		{
			IsPopoupOpen = true;
			IsPageEnabled = false;
            PopupHeight = ResolutionService.Height;
            PopupWidth = ResolutionService.Width;
        }

		public void ClosePopup()
		{
			IsPopoupOpen = false;
			IsPageEnabled = true;
		}

		public TeamDetailPageViewModel()
		{
			PageIndex = 1;
			PageSize = 10;
			TotalCount = 0;
			TotalPage = 0;
			IsPopoupOpen = false;
			IsPageEnabled = true;
			EditTeamButtonLabel = "编辑"; //1.编辑 2.更新
			CancelEditButtonVisibility = Visibility.Hidden;
			IsTeamEditModel = false;

			Places = new ObservableCollection<DictItem>(MetaDataService.TeamMemberPlaces);
		}
	}
}
