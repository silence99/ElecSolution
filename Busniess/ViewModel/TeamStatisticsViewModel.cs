using Emergence.Common.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Emergence.Business.ViewModel
{
	public class TeamStatisticsViewModel : TeamStatisticsModel
	{
		public ObservableCollection<KeyValuePair<string, int>> Statistics { get; set; }
	}
}
