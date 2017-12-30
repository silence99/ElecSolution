using Framework;
using Emergence.Common.Model;
using System.Collections.Generic;

namespace Emergence.Business.ViewModel
{
	public class MainPageUiModel : NotificationObject
	{
		public virtual List<MasterEventViewModel> MasterEvents { get; set; }

		public virtual List<Event> EventsForReview { get; set; }
	}
}
