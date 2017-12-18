using Business.Strategies;
using Emergence.Common.ViewModel;
using Emergence_WPF.Comm;
using Emergence_WPF.Views;
using System.Windows;
using System.Windows.Input;
using Framework;

namespace Emergence_WPF
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window, IEmergencyInit, INotifyPropertyChangedEx
	{
		private MainPageUiModel _uiModel;
		public event PropertyChangedHandlerEx PropertyChangedEx;
		public MainPageStrategyController StrategyController { get; set; }
		public MainPageUiModel UiModel
		{
			get
			{
				return _uiModel;
			}
			set
			{
				PropertyChangedEx?.Invoke(UiModel, new PropertyChangedEventArgsEx("UIModel")
				{
					OldValue = _uiModel,
					NewValue = value,
				});
				_uiModel = value;
			}
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
			UiModel = new MainPageUiModel()
			{
				Left = 0.0,
				Top = 0.0,
				Width = SystemParameters.PrimaryScreenWidth,
				Heigh = SystemParameters.PrimaryScreenHeight,
				WindowState = WindowState.Normal,
				WindowStyle = WindowStyle.SingleBorderWindow,
				ResizeMode = ResizeMode.CanResize
			};
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
			var Maincontrol = new UserControl_MainPage();
			maingrid.Children.Add(Maincontrol);
			Maincontrol.UiModel = UiModel;
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
