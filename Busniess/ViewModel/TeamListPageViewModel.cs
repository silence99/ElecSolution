﻿using Business.Services;
using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.ObjectModel;

namespace Emergence.Business.ViewModel
{
	public class TeamListPageViewModel : NotificationObject
	{
		public virtual ObservableCollection<TeamMemberModel> Teams { get; set; }
        public virtual TeamService teamService { get; set; }
        public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual string QueryTeamName { get; set; }
		public virtual string QueryDepartment { get; set; }
        public virtual string QueryChargeName { get; set; }
        public virtual string SerachInfo { get; set; }
        public virtual TeamMemberModel CurrentTeam { get; set; }
		public virtual double PopupWidth { get; set; }
		public virtual double PopupHeight { get; set; }
		public virtual bool IsPopoupOpen { get; set; }
		public virtual bool IsPageEnabled { get; set; }
        public virtual string CanSelectCaptain { get; set; }
        public virtual string PopupHeader { get; set; }
        public virtual ObservableCollection<PersonModel> Members { get; set; }

        public virtual event SetPopupHandler SetPopupEditToFullScreen;

        public virtual ObservableCollection<DictItem> TeamDepts { get; set; }
        public virtual ObservableCollection<DictItem> TeamList { get; set; }
        public virtual ObservableCollection<DictItem> TeamMemberPlace { get; set; }
        public virtual ObservableCollection<DictItem> TeamMembers { get; set; }


        public void PopupTeamEdit()
        {
            var aopWapper = this.IsAopWapper ? this : this.AopWapper as TeamListPageViewModel;

            aopWapper.TeamMembers = new ObservableCollection<DictItem>();

            SyncTeamMembers(CurrentTeam.TeamId);

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
			PageSize = 1000;
			TotalCount = 0;
			TotalPage = 0;
			IsPopoupOpen = false;
			IsPageEnabled = true;
            PopupHeight = ResolutionService.Height;
            PopupWidth = ResolutionService.Width;
            TeamDepts = new ObservableCollection<DictItem>(MetaDataService.TeamDepartments);
            TeamList = new ObservableCollection<DictItem>(MetaDataService.TeamIds);
            TeamMemberPlace = new ObservableCollection<DictItem>();
            TeamMemberPlace.Add(new DictItem { Name = "队长", Value = "place_1" });
            TeamMemberPlace.Add(new DictItem { Name = "队员", Value = "place_2" });

            TeamMembers = new ObservableCollection<DictItem>(MetaDataService.TeamDepartments);
            TeamMembers = new ObservableCollection<DictItem>();
            Members = new ObservableCollection<PersonModel>();
            teamService = new TeamService();
        }
        private void SyncTeamMembers(string teamID)
        {
            var aopWapper = this.IsAopWapper ? this : this.AopWapper as TeamListPageViewModel;

            var result = teamService.GetTeamPersons(aopWapper.PageIndex, aopWapper.PageSize, teamID);
            //ViewModel.PageIndex = result.PageIndex;
            //ViewModel.PageSize = result.PageSize;
            //ViewModel.TotalCount = result.Count;
            //ViewModel.TotalPage = (int)Math.Ceiling((double)ViewModel.TotalCount / ViewModel.PageSize);
            if (result.Data != null && result.Data.Length > 0)
            {
                aopWapper.Members = new System.Collections.ObjectModel.ObservableCollection<PersonModel>(result.Data);
                foreach (PersonModel pm in aopWapper.Members)
                {
                    aopWapper.TeamMembers.Add(new DictItem { Name = pm.Name, Value = pm.ID.ToString() });
                }
            }
        }
    }
}
