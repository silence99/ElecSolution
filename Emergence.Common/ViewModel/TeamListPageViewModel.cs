using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;

namespace Emergence.Common.ViewModel
{
	public class TeamListPageViewModel : NotificationObject
	{
		public virtual ObservableCollection<TeamModel> MasterEvents { get; set; }
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }

		public TeamListPageViewModel()
		{
			PageIndex = 1;
			PageSize = 10;
			TotalCount = 0;
			TotalPage = 0;
		}
	}
}
