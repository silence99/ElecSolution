﻿using Busniess.Services;
using Busniess.Services.TeamSvr;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF.Views.Others
{
	/// <summary>
	/// TeamListPage.xaml 的交互逻辑
	/// </summary>
	public partial class TeamDetailPage : Page
	{
		TeamDetailPageViewModel ViewModel { get; set; }
		PersonService PersonService = new PersonService();
		TeamService TeamService = new TeamService();
		public TeamDetailPage(TeamModel team)
		{
			InitializeComponent();
			InitViewModel(team);
		}

		private void InitViewModel(TeamModel team)
		{
			ViewModel = new TeamDetailPageViewModel();
			ViewModel.ID = team.ID;
			ViewModel.PersonCharge = team.PersonCharge;
			ViewModel.PersonChargePhone = team.PersonChargePhone;
			ViewModel.TeamDept = team.TeamDept;
			ViewModel.TeamName = team.TeamName;
			ViewModel.TotalNumber = team.TotalNumber;

			SyncTeamMembers();
		}

		private void Delete_Handler(object sender, RoutedEventArgs e)
		{
			TeamService.DeleteTeamMembers(ViewModel.Members.Where(mbr => mbr.IsChecked).Select(mbr => mbr.ID.ToString()).ToList());
		}

		private void ClosePopup_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.ClosePopup();
		}

		private void Import_Handler(object sender, RoutedEventArgs e)
		{

		}

		private void Add_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.CurrentPerson = new PersonModel();
			ViewModel.PopupTeamEdit();
		}

		private void Edit_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.CurrentPerson = (sender as Image).DataContext as PersonModel;
			ViewModel.PopupTeamEdit();
		}

		private void Click_SearchTextBox(object sender, RoutedEventArgs e)
		{
			SyncTeamMembers();
		}

		private void GridTeamList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ViewModel.CurrentPerson = GridTeamMembersList.SelectedItem as PersonModel;
			ViewModel.PopupTeamEdit();
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			SyncTeamMembers();
		}

		private void SyncTeamMembers()
		{
			var result = PersonService.GetTeamDetails(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.ID);
			ViewModel.PageIndex = result.PageIndex;
			ViewModel.PageSize = result.PageSize;
			ViewModel.TotalCount = result.Count;
			ViewModel.TotalPage = (int)Math.Ceiling((double)ViewModel.TotalCount / ViewModel.PageSize);
			ViewModel.Members = new System.Collections.ObjectModel.ObservableCollection<PersonModel>(result.Data);
		}

		private void EditTeam_Handler(object sender, RoutedEventArgs e)
		{
			if (ViewModel.IsTeamEditModel)
			{
				CancelEditMode();
				TeamService.UpdateTeam(ViewModel.ID, ViewModel.TeamName, ViewModel.PersonCharge, ViewModel.PersonChargePhone, ViewModel.TeamDept);
			}
			else
			{
				GotoEditModel();
			}

		}

		private void CancelEdit_Handler(object sender, RoutedEventArgs e)
		{
			CancelEditMode();
		}

		private void CancelEditMode()
		{
			ViewModel.CancelEditButtonVisibility = Visibility.Hidden;
			ViewModel.IsTeamEditModel = false;
			ViewModel.EditTeamButtonLabel = "编辑";
		}

		private void GotoEditModel()
		{
			ViewModel.IsTeamEditModel = true;
			ViewModel.CancelEditButtonVisibility = Visibility.Visible;
			ViewModel.EditTeamButtonLabel = "更新";
		}
	}
}
