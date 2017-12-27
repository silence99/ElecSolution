using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Mappers
{
	public class TeamStatisticsModelMapper : IMapper<TeamStatisticsModel, TeamStatisticsViewModel>
	{
		public void MapToDataModel(TeamStatisticsViewModel viewModel, TeamStatisticsModel model)
		{
			model.TeamMemberTotal = viewModel.TeamMemberTotal;
			model.TeamMemberUseTotal = viewModel.TeamMemberUseTotal;
			model.TeamTotal = viewModel.TeamTotal;
			model.TeamUseTotal = viewModel.TeamUseTotal;
		}

		public void MapToViewModel(TeamStatisticsModel model, TeamStatisticsViewModel viewModel)
		{
			viewModel.TeamMemberTotal = model.TeamMemberTotal;
			viewModel.TeamMemberUseTotal = model.TeamMemberUseTotal;
			viewModel.TeamTotal = model.TeamTotal;
			viewModel.TeamUseTotal = model.TeamUseTotal;
			int personPer = viewModel.TeamMemberTotal == 0 ?
							0 :
							(int)(100 * ((double)viewModel.TeamMemberUseTotal / viewModel.TeamMemberTotal));
			int teamPer = viewModel.TeamTotal == 0 ?
							0 :
							(int)(100 * ((double)viewModel.TeamUseTotal / viewModel.TeamTotal));

			personPer = 50;
			teamPer = 40;
			if (viewModel.Statistics == null)
			{
				viewModel.Statistics = new System.Collections.ObjectModel.ObservableCollection<KeyValuePair<string, int>>();
			}
			viewModel.Statistics.Add(new KeyValuePair<string, int>("已用人数/总人数", personPer));
			viewModel.Statistics.Add(new KeyValuePair<string, int>("已用队伍/总队伍", teamPer));
		}
	}
}
