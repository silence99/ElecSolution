using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Emergence.Common.ViewModel
{
	public class VM_MasterEventManagement : NotificationObject
	{
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual string TxtSerialNumber { get; set; }
		public virtual string TxtTitle { get; set; }
		public virtual string TxtTime { get; set; }
		public virtual string TxtSubmitParty { get; set; }
		public virtual string TxtLocate { get; set; }

		public virtual ObservableCollection<MasterEvent> MasterEvents { get; set; }

		public VM_MasterEventManagement()
		{
			PageSize = 5;
			PageIndex = 1;
			TotalCount = 0;
		}
	}
}
