﻿using Busniess.Services;
using Emergence.Common.Model;
using Emergence.Business.ViewModel;
using Emergence_WPF.Comm;
using Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF.Views.Others
{
	/// <summary>
	/// TeamListPage.xaml 的交互逻辑
	/// </summary>
	public partial class TeamListPage : Page
	{
		private TeamListPageViewModel ViewModel { get; set; }
		private TeamService TeamService = new TeamService();
		public TeamListPage()
		{
			InitializeComponent();
		}

		private void Delete_Handler(object sender, RoutedEventArgs e)
		{
			var teams = ViewModel.Teams.Where(team => team.IsChecked).ToList();
			if (teams != null && teams.Count != 0)
			{
				TeamService.DeleteTeam(teams.Select(t => t.ID.ToString()).ToList());
			}
			GetTeams();
		}

		private void Import_Handler(object sender, RoutedEventArgs e)
		{

		}

		private void Add_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.CurrentTeam = new TeamModel();
			ViewModel.PopupTeamEdit();
		}

		private void Search_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.PageIndex = 1;
			GetTeams();
		}

		private void GetTeams()
		{
			var teams = TeamService.GetTeam(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.QueryTeamName, ViewModel.QueryChargeName, ViewModel.QueryDepartment);
			ViewModel.Teams = new ObservableCollection<TeamModel>(teams.Data.Select(o => o.CreateAopProxy()));
			ViewModel.PageIndex = teams.PageIndex;
			ViewModel.PageSize = teams.PageSize;
			ViewModel.TotalCount = teams.Count;
			ViewModel.TotalPage = teams.PageSize == 0 ? 0 : (int)Math.Ceiling((double)teams.Count / teams.PageSize);
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			GetTeams();
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			if (ViewModel == null)
			{
				ViewModel = new TeamListPageViewModel().CreateAopProxy();
				ViewModel.PageIndex = 1;
				ViewModel.PageSize = 10;
				DataContext = ViewModel;
			}
			GetTeams();
		}

		private void Edit_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ViewModel.CurrentTeam = (sender as Image).DataContext as TeamModel;
			ViewModel.PopupTeamEdit();
		}

		private void NavigateToMaterialPage_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new Uri("./Views/Others/MaterialListPage.xaml", UriKind.Relative));
		}

		private void Update_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.ClosePopup();
			if (ViewModel.CurrentTeam.ID == 0)
			{
				TeamService.CreateTeam(ViewModel.CurrentTeam.TeamName, ViewModel.CurrentTeam.PersonCharge, ViewModel.CurrentTeam.PersonChargePhone, ViewModel.CurrentTeam.TeamDept);
			}
			else
			{
				TeamService.UpdateTeam(ViewModel.CurrentTeam.ID, ViewModel.CurrentTeam.TeamName, ViewModel.CurrentTeam.PersonCharge, ViewModel.CurrentTeam.PersonChargePhone, ViewModel.CurrentTeam.TeamDept);
			}
			GetTeams();
		}

		private void ClosePopup_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ViewModel.ClosePopup();
		}

		private void NavigateToTeamDetailPage_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var team = GridTeamList.SelectedItem as TeamModel;
			if (team != null)
			{
				NavigationService.Navigate(new TeamDetailPage(team));
			}
		}
	}
}
