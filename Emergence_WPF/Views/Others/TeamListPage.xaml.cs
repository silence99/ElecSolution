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
using System.Windows.Media;
using Business.Services;

namespace Emergence_WPF
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
				var result = TeamService.DeleteTeam(teams.Select(t => t.ID.ToString()).ToList());

                if (result)
                {
                    System.Windows.MessageBox.Show("删除成功！");
                    GetTeams();
                }
                else
                {
                    System.Windows.MessageBox.Show("删除失败！");
                }
            }
			else
			{
				System.Windows.MessageBox.Show("没有选择要删除的队伍");
			}
		}

		private void Import_Handler(object sender, RoutedEventArgs e)
		{

		}

		private void Add_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.CurrentTeam = new TeamModel().CreateAopProxy();
			ViewModel.CurrentTeam.TeamDept = ViewModel.TeamDepts == null || ViewModel.TeamDepts.Count == 0 ? "" : ViewModel.TeamDepts[0].Value;
            DependencyObject parent = this.PopupEditTeam.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
            ViewModel.CanSelectCaptain = "Hidden";
            ViewModel.PopupHeader = "新增队伍";
            ViewModel.PopupTeamEdit();
            FullPageEditPopup();
        }
        
        private void Search_Handler(object sender, RoutedEventArgs e)
		{
			ViewModel.PageIndex = 1;
			GetTeams();
		}

		private void GetTeams()
		{
			var teams = TeamService.GetTeam(ViewModel.PageIndex, ViewModel.PageSize, ViewModel.SerachInfo);
            if (teams != null)
            {
                ViewModel.Teams = new ObservableCollection<TeamModel>(teams.Data.Select(o => o.CreateAopProxy()));
                ViewModel.PageIndex = teams.PageIndex;
                ViewModel.PageSize = teams.PageSize;
                ViewModel.TotalCount = teams.Count;
                ViewModel.TotalPage = teams.PageSize == 0 ? 0 : (int)Math.Ceiling((double)teams.Count / teams.PageSize);
            }
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
                ViewModel.SetPopupEditToFullScreen += this.FullPageEditPopup;
            }
			GetTeams();
		}

		private void Edit_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ViewModel.CurrentTeam = (sender as Image).DataContext as TeamModel;
            DependencyObject parent = this.PopupEditTeam.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
            ViewModel.CanSelectCaptain = "Visible";
            ViewModel.PopupHeader = "编辑队伍";
            ViewModel.PopupTeamEdit();
            ViewModel.CurrentTeam.TeamMemberId = (ViewModel.CurrentTeam.TeamMemberId < 0 && ViewModel.Members != null && ViewModel.Members.Count != 0) ? ViewModel.Members[0].ID : ViewModel.CurrentTeam.TeamMemberId;
            FullPageEditPopup();
        }

		private void NavigateToMaterialPage_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			NavigationService.Navigate(new Uri("./Views/Others/MaterialListPage.xaml", UriKind.Relative));
		}

		private void Update_Handler(object sender, RoutedEventArgs e)
        {
            var resultValidation = false;
            foreach (var item in TLPPopupBindingGroup.BindingExpressions)
            {
                item.UpdateSource();
                if (item.HasValidationError)
                {
                    resultValidation = true;
                }
            }
            if (!resultValidation)
            {
                if (ViewModel.CurrentTeam.ID == 0)
                {
                    var result = TeamService.CreateTeam(ViewModel.CurrentTeam.TeamName, ViewModel.CurrentTeam.PersonCharge, ViewModel.CurrentTeam.PersonChargePhone, ViewModel.CurrentTeam.TeamDept);
                    if (result)
                    {
                        ViewModel.ClosePopup();
                        System.Windows.MessageBox.Show("添加成功！");
                        GetTeams();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("添加失败！");
                    }
                }
                else
                {
                    var captain = ViewModel.Members.First(a => a.ID == ViewModel.CurrentTeam.TeamMemberId);
                    if(captain != null)
                    {
                        ViewModel.CurrentTeam.PersonCharge = captain.Name;
                        ViewModel.CurrentTeam.PersonChargePhone = captain.PhoneNumber;
                    }
                    var result = TeamService.UpdateTeam(ViewModel.CurrentTeam.ID, ViewModel.CurrentTeam.TeamName, ViewModel.CurrentTeam.PersonCharge, ViewModel.CurrentTeam.PersonChargePhone, ViewModel.CurrentTeam.TeamDept, ViewModel.CurrentTeam.TeamMemberId);
                    if (result)
                    {
                        ViewModel.ClosePopup();
                        System.Windows.MessageBox.Show("编辑成功！");
                        GetTeams();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("编辑失败！");
                    }
                }
            }
            
		}

		private void NavigateToTeamDetailPage_Handler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var team = GridTeamList.SelectedItem as TeamModel;
			if (team != null)
			{
				NavigationService.Navigate(new TeamDetailPage(team));
			}
		}

        private void ClosePopup_Handler(object sender, RoutedEventArgs e)
        {
            ViewModel.ClosePopup();
        }
        private void FullPageEditPopup()
        {
            DependencyObject parent = this.PopupEditTeam.Child;
            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;
                    element.Height = ResolutionService.Height;
                    element.Width = ResolutionService.Width;
                    break;
                }
            }
            while (parent != null);
        }

    }
}
