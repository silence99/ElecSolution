using Business.Strategies;
using Emergence.Common.Model;
using Emergence.Business.ViewModel;
using Emergence_WPF.Util;
using Framework;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Business.Services;

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
            ResolutionService.Width = SystemParameters.PrimaryScreenWidth;
            ResolutionService.Height = SystemParameters.PrimaryScreenHeight;

            ViewModel.Left = 0.0;
			ViewModel.Top = 0.0;
			ViewModel.Width = ResolutionService.Width;
			ViewModel.Height = ResolutionService.Height;
			ViewModel.ResizeMode = ResizeMode.CanMinimize;
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

        public void SetUserName(string useName)
        {
            if (!string.IsNullOrEmpty(useName))
            {
                ViewModel.UserName = useName;
            }
            else
            {
                ViewModel.UserName = string.Empty;
            }
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
		}

		private void LogoutBtn_Click(object sender, MouseButtonEventArgs e)
		{
			Login login = new Login();
			login.Show();
			Close();
		}

		private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			frmMain.NavigationService.Navigate(new Uri(@".\Views\Others\MessageNotificationPage.xaml", UriKind.Relative));
		}

		private void Image_MouseLeftButtonDown_PopupMasterEventPage(object sender, MouseButtonEventArgs e)
        {
            MainPagePopupWindow mainPopup = new MainPagePopupWindow();
            mainPopup.Topmost = true;
            mainPopup.Show();
        }
        
		/// <summary>
		/// show home page(switch to home page)
		/// </summary>
		private void ShowMainPage()
        {
            frmMain.NavigationService.Navigate(new EmergenceMainPage());
        }
        
		private void GraphBtn_Click(object sender, MouseButtonEventArgs e)
		{
			this.frmMain.Navigate(new System.Uri(@".\Views\Others\TeamListPage.xaml", UriKind.Relative));
		}

        private void MinimizeBtn_Click(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
