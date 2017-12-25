using System.Collections.ObjectModel;

namespace Emergence.Common.ViewModel
{
	public class StatisticsViewModel
	{
		public MaterialsStatisticsViewModel[] MaterialsStatisticsViewModel { get; set; }
		public TeamStatisticsViewModel TeamStatisticsViewModel { get; set; }
		public string PersonStatisticsDisplayName { get { return "已用人数/总人数"; } }
		public string TeamStatisticsDisplayName { get { return "已用队伍/总队伍"; } }
		public ObservableCollection<Statistics> TeamStatistics
		{
			get
			{
				return new ObservableCollection<Statistics>()
				{
					new Statistics()
					{
						Name = TeamStatisticsDisplayName,
						Value =TeamStatisticsViewModel.TeamUseTotal
					},
					new Statistics()
					{
						Name = TeamStatisticsDisplayName,
						Value =TeamStatisticsViewModel.TeamTotal - TeamStatisticsViewModel.TeamUseTotal
					}
				};
			}
		}

		public ObservableCollection<Statistics> PersonStatistics
		{
			get
			{
				return new ObservableCollection<Statistics>()
				{
					new Statistics()
					{
						Name = PersonStatisticsDisplayName,
						Value =TeamStatisticsViewModel.TeamMemberUseTotal
					},
					new Statistics()
					{
						Name = PersonStatisticsDisplayName,
						Value =TeamStatisticsViewModel.TeamMemberTotal - TeamStatisticsViewModel.TeamMemberUseTotal
					}
				};
			}
		}

	}
	public class Statistics
	{
		public string Name { get; set; }
		public int Value { get; set; }
	}
}
