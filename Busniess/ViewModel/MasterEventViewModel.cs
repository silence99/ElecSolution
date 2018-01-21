using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;

namespace Emergence.Business.ViewModel
{
	public class MasterEventViewModel : NotificationObject
	{
		public virtual ObservableCollection<MasterEvent> MasterEvents { get; set; }
		public virtual int Count { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int PageSize { get; set; }
	}
}
