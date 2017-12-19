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
	public partial class MainWindow : Window, IEmergencyControl<MainPageUiModel>
	{
		private MainPageUiModel _uiModel;
		public StrategyController StrategyController { get; set; }
		public MainPageUiModel UiModel
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
		{//mainwindows don't need call this function
			throw new System.NotImplementedException();
		}

		public void InitUiModel()
		{
			UiModel.Top = 0.0;
			UiModel.Width = SystemParameters.PrimaryScreenWidth;
			UiModel.Height = SystemParameters.PrimaryScreenHeight;
			UiModel.ResizeMode = ResizeMode.CanResize;
			DataContext = UiModel;
			StrategyController = new MainPageStrategyController(UiModel);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			InitUiModel();
			var Maincontrol = new UserControl_MainPage();
			Maincontrol.UiModel = UiModel;
			maingrid.Children.Add(Maincontrol);

		}

		private void HomeBtn_Click(object sender, MouseButtonEventArgs e)
		{
			maingrid.Children.Clear();
			var mainPage = ObjectFactory.GetInstance<UserControl>("mainPagePanel");
			maingrid.Children.Add(mainPage);
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
	}
}
