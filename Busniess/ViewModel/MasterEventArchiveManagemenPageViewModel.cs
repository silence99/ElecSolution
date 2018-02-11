using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Busniess.ViewModel
{
	public class MasterEventArchiveManagemenPageViewModel : NotificationObject
	{
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }

		public virtual string SearchInfo { get; set; }
		public virtual ObservableCollection<MasterEvent> Events { get; set; }

		public MasterEventArchiveManagemenPageViewModel()
		{
			PageIndex = 1;
			PageSize = GetPageSize();
			TotalCount = 0;
			TotalPage = 0;
			SyncData();
		}

		private int GetPageSize()
		{
			var size = ConfigurationManager.AppSettings["PageSize"] ?? "10";
			var t = 10;
			if (!int.TryParse(size, out t))
			{
				t = 10;
			}
			return t;
		}

		public void SyncData()
		{
			var masterEventService = new MasterEventService();
			var obj = this.IsAopWapper ? this : this.CreateAopProxy();
			var data = masterEventService.GetAchievedMasterEvents(obj.PageIndex, obj.PageSize, obj.SearchInfo) ?? new EmergencyHttpListResult<MasterEvent>();
			if (data.Data == null)
			{
				data.Data = new MasterEvent[0];
			}

			obj.Events = new ObservableCollection<MasterEvent>(data.Data);
			obj.TotalCount = obj.Events.Count;
			obj.TotalPage = (int)System.Math.Ceiling(((double)obj.TotalCount) / (obj.PageSize == 0 ? 1000 : obj.PageSize));
		}
	}
}
