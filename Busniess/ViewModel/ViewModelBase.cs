using Framework;

namespace Busniess.ViewModel
{
	public class ViewModelBase : NotificationObject
	{
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
	}
}
