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
		}
	}
}
