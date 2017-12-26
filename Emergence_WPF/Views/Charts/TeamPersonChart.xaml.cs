using Busniess.Services.StatisticsSvr;
using Emergence.Common.ViewModel;
using Emergence_WPF.Util;
using System.Windows.Controls;

namespace Emergence_WPF.Views.Charts
{
	/// <summary>
	/// TeamPersonChart.xaml 的交互逻辑
	/// </summary>
	public partial class TeamPersonChart : UserControl
	{
		private TeamStatisticsViewModel ViewModel { get; set; }
		public TeamPersonChart()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			var service = ServiceManager.Instance.GetService<TeamStatisticsService>(Constant.Services.TeamStatisticsService);
			ViewModel = service.GetTeamStatistics();
			DataContext = ViewModel;
		}
	}
}
