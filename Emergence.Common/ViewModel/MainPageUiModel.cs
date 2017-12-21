using Framework;
using System.Collections.Generic;

namespace Emergence.Common.ViewModel
{
	public class MainPageUiModel : NotificationObject
	{
		public virtual List<MasterEventUiModel> MasterEvents { get; set; }

		public virtual List<Model.Event> EventsForReview { get; set; }
	}
}
