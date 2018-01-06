using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence_WPF.Util;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF
{
	/// <summary>
	/// main page user control
	/// </summary>
	public partial class UserControl_MainPage : UserControl
	{
		private MainWindowViewModel ViewModel { get; set; }
		public UserControl_MainPage()
		{
			InitializeComponent();
			ViewModel = new MainWindowViewModel()
			{
				Statistics = new StatisticsViewModel()
				{
					TeamStatisticsViewModel = new TeamStatisticsViewModel()
					{
						TeamMemberTotal = 10,
						TeamMemberUseTotal = 5,
						TeamTotal = 15,
						TeamUseTotal = 5
					},
					MaterialsStatisticsViewModel = new MaterialsStatisticsViewModel[2]
					{
						 new MaterialsStatisticsViewModel()
						 {
							  MaterialsName = "电缆",
							  TotalQuantity = 100
						 },
						 new MaterialsStatisticsViewModel()
						 {

						 }
					}
				}
			};

			DataContext = ViewModel;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var svc = ServiceManager.Instance.GetService<MasterEventService>(Constant.Services.MasterEventService);
			this.DataCodeing.ItemsSource = svc.GetMasterEventForMainPage();
			VideoList.BindingViewModel(svc.GetVideos());
		}

		private void MasterEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			map.Bind("119.931298", "28.469722");
		}
	}
}
