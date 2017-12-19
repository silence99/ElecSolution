using Business.Strategies;
using Emergence.Common.ViewModel;
using Emergence_WPF.Util;
using Framework;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Emergence_WPF
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window, IEmergencyControl<MainWindowUiModel>
	{
		private MainWindowUiModel _uiModel;
		public StrategyController StrategyController { get; set; }
		public MainWindowUiModel UiModel
		{
			get
			{
				return _uiModel;
			}
			set
			{
				_uiModel = value == null ? null : (value.IsAopWapper ? value : value.CreateAopProxy());
			}
		}

		public string StrategyControllerName
		{
			get { return "mainWindowsStrategyController"; }
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		public void BindingUiModel(StrategyController parent, StrategyController strategyController, NotificationObject uiModel)
		{
			UiModel = uiModel as MainWindowUiModel;
			InitUiModel();
			StrategyController = strategyController;
			DataContext = uiModel;
		}

		/// <summary>
		/// create ui model for main window
		/// </summary>
		public void InitUiModel()
		{
			UiModel.Left = 0.0;
			UiModel.Top = 0.0;
			UiModel.Width = SystemParameters.PrimaryScreenWidth;
			UiModel.Height = SystemParameters.PrimaryScreenHeight;
			UiModel.ResizeMode = ResizeMode.CanResize;
			UiModel.WindowState = WindowState.Normal;
			UiModel.WindowStyle = WindowStyle.SingleBorderWindow;
			// main page ui model is empty, filled when showing main page
			UiModel.MainPageData = new MainPageUiModel();
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
			maingrid.Children.Clear();
			MasterEventManagement information = new MasterEventManagement();
			maingrid.Children.Add(information);
		}

		private void LogoutBtn_Click(object sender, MouseButtonEventArgs e)
		{
			Login login = new Login();
			login.Show();
			Close();
		}

		private void Image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			maingrid.Children.Clear();
			var Navigation = new NotificationNavigation();
			var ff = this.ActualWidth - 1000;
			var hh = this.ActualHeight - 630;
			Navigation.bind(1000 + ff, 550 + hh);
			maingrid.Children.Add(Navigation);
		}

		private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
		{
			EmergencyCommandCenter center = new EmergencyCommandCenter();
			center.ShowDialog();
		}

		private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
		{
			maingrid.Children.Clear();
			var report = new ReportCenter();
			var ff = this.ActualWidth - 1000;
			var hh = this.ActualHeight - 630;
			report.bind(1000 + ff, 550 + hh);
			maingrid.Children.Add(report);
		}

		/// <summary>
		/// show home page(switch to home page)
		/// </summary>
		private void ShowMainPage()
		{
			// get panel, binding panel ui model, add to main window
			var mainPage = ObjectFactory.GetInstance<UserControl_MainPage>("mainPagePanel");
			mainPage.BindingUiModel(StrategyController, ObjectFactory.GetInstance<MainPageStrategyController>(mainPage.StrategyControllerName), UiModel.MainPageData.CreateAopProxy());
			maingrid.Children.Clear();
			maingrid.Children.Add(mainPage);
		}
	}
}
