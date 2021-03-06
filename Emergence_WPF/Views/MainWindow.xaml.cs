﻿using Business.Strategies;
using Emergence.Common.Model;
using Emergence.Business.ViewModel;
using Emergence_WPF.Util;
using Framework;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Emergence_WPF
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWindowViewModel _viewModel;
		public MainWindowViewModel ViewModel
		{
			get
			{
				return _viewModel;
			}
			set
			{
				_viewModel = value == null ? null : (value.IsAopWapper ? value : value.CreateAopProxy());
			}
		}
		private Control CurrentPage { get; set; }

		public string StrategyControllerName
		{
			get { return "mainWindowsStrategyController"; }
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		public void BindingUiModel(StrategyController parent, NotificationObject uiModel)
		{
			ViewModel = uiModel as MainWindowViewModel;
			InitUiModel();
			DataContext = uiModel;
		}

		/// <summary>
		/// create ui model for main window
		/// </summary>
		public void InitUiModel()
		{
			ViewModel.Left = 0.0;
			ViewModel.Top = 0.0;
			ViewModel.Width = SystemParameters.PrimaryScreenWidth;
			ViewModel.Height = SystemParameters.PrimaryScreenHeight;
			ViewModel.ResizeMode = ResizeMode.NoResize;
			ViewModel.WindowState = WindowState.Maximized;
			ViewModel.WindowStyle = WindowStyle.None;
			// main page ui model is empty, filled when showing main page
			ViewModel.MainPageData = new MainPageUiModel();
			ViewModel.NavigateCommand = new Microsoft.Practices.Prism.Commands.DelegateCommand<string>((uri) => frmMain.NavigationService.Navigate(new Uri(uri, UriKind.Relative)));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// show main page first
			ShowMainPage();
		}

		/// <summary>
		/// click event hanlder of home button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HomeBtn_Click(object sender, MouseButtonEventArgs e)
		{
			ShowMainPage();
		}

		private void MasterEventBtn_Click(object sender, MouseButtonEventArgs e)
		{
            MasterEventManagement mem = new MasterEventManagement();
            frmMain.NavigationService.Navigate(new MasterEventManagement());
			//GoToMasterEventMangementPage();
		}

		private void LogoutBtn_Click(object sender, MouseButtonEventArgs e)
		{
			Login login = new Login();
			login.Show();
			Close();
		}

		private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			//maingrid.Children.Clear();
			frmMain.NavigationService.Navigate(new Uri(@".\Views\Others\TeamListPage.xaml", UriKind.Relative));
			frmMain.NavigationService.Refresh();
		}

		private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
		{
			EmergencyCommandCenter center = new EmergencyCommandCenter();
			center.ShowDialog();
		}

		private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
		{
			//maingrid.Children.Clear();
			var report = new ReportCenter();
			var ff = this.ActualWidth - 1000;
			var hh = this.ActualHeight - 630;
			report.bind(1000 + ff, 550 + hh);
			//maingrid.Children.Add(report);
		}

		/// <summary>
		/// show home page(switch to home page)
		/// </summary>
		private void ShowMainPage()
        {
            frmMain.NavigationService.Navigate(new EmergenceMainPage());
            // get panel, binding panel ui model, add to main window
            //var mainPage = ObjectFactory.GetInstance<UserControl_MainPage>("mainPagePanel");
            //maingrid.Children.Clear();
            //maingrid.Children.Add(mainPage);
        }

        private void GoToMasterEventMangementPage()
		{
			//maingrid.Children.Clear();
			//MasterEventManagement information = new MasterEventManagement();
			//CurrentPage = information;
			//information.GoToDetail += GotoMasterEventDetailPage;

			//maingrid.Children.Add(information);
		}


		private void GotoMasterEventDetailPage(MasterEvent master)
		{
			//maingrid.Children.Clear();
			//var cur = CurrentPage as MasterEventManagement;
			//if (cur != null)
			//{
			//	cur.GoToDetail -= GotoMasterEventDetailPage;
			//}
			//MasterEventDetail md = new MasterEventDetail(master);
			//md.GoBack += GoBack_Handler;
			//maingrid.Children.Add(md);
		}

		private void GoBack_Handler(object sender, EventArgs e)
		{
			//var obj = sender as MasterEventDetail;
			//if (obj != null)
			//{
			//	obj.GoBack -= GoBack_Handler;
			//}
			//GoToMasterEventMangementPage();
		}

		private void GraphBtn_Click(object sender, MouseButtonEventArgs e)
		{
			this.frmMain.Navigate(new System.Uri(@".\Views\Event\MasterEventDetail.xaml", UriKind.Relative));

			//maingrid.Children.Clear();
			//var report = new ReportCenter();
			//var ff = this.ActualWidth - 1000;
			//var hh = this.ActualHeight - 630;
			//report.bind(1000 + ff, 550 + hh);
			//maingrid.Children.Add(report);
		}
	}
}
