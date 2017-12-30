using Emergence.Common.Model;

namespace Emergence.Common.ViewModel
{
	public class TeamDetailPageViewModel : TeamModel
	{
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }

		public TeamDetailPageViewModel()
		{
			PageIndex = 1;
			PageSize = 10;
			TotalCount = 0;
			TotalPage = 0;
		}
	}
}
