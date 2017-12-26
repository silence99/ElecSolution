using Emergence.Common.Model;

namespace Emergence.Common.ViewModel
{
	public class TeamStatisticsViewModel : TeamStatisticsModel
	{
		public string PersonStatisticsLabel { get { return "已用人数/总人数"; } }
		public int PersonStatisticsValue { get { return 100 * (TeamMemberUseTotal / TeamMemberTotal); } }

		public string TeamStatisticsLabel { get { return "已用队伍/总队伍"; } }
		public int TeamStatisticsValue { get { return 100 * (TeamUseTotal / TeamTotal); } }
	}
}
