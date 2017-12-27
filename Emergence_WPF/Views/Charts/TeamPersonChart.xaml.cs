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
			//default 
			DataContext = new TeamStatisticsViewModel()
			{
				Statistics = new System.Collections.ObjectModel.ObservableCollection<System.Collections.Generic.KeyValuePair<string, int>>()
				{
					new System.Collections.Generic.KeyValuePair<string, int>("已用人数/总人数", 0),
					new System.Collections.Generic.KeyValuePair<string, int>("已用队伍/总队伍", 0)
				}
			};
		}

		private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			try
			{
				var service = ServiceManager.Instance.GetService<TeamStatisticsService>(Constant.Services.TeamStatisticsService);
				ViewModel = service.GetTeamStatistics();
				DataContext = ViewModel;
			}
			catch
			{

			}
		}
	}
}
