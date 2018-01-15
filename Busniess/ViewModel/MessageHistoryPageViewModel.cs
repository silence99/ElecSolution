using Busniess.Services;
using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace Busniess.ViewModel
{
	public class MessageHistoryPageViewModel : NotificationObject
	{
		public virtual int PageSize { get; set; }
		public virtual int PageIndex { get; set; }
		public virtual int TotalCount { get; set; }
		public virtual int TotalPage { get; set; }
		public virtual MessageService MessageService { get; set; }

		public virtual ObservableCollection<MessageModel> Data { get; set; }

		public MessageHistoryPageViewModel()
		{
			PageIndex = 1;
			PageSize = GetPageSize();
			TotalCount = 0;
			TotalPage = 0;
			MessageService = new MessageService();
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
			var model = IsAopWapper ? this : this.CreateAopProxy();
			if (model.PageSize <= 0 || model.PageIndex < 0)
			{
				return;
			}
			var data = MessageService.GetMessageLog(PageIndex, PageSize);
			if (data != null && data.Data != null)
			{
				model.Data = new ObservableCollection<MessageModel>(data.Data.Select(item => item.CreateAopProxy()));
				model.PageIndex = data.PageIndex;
				model.PageSize = data.PageSize;
				model.TotalCount = data.Count;
				model.TotalPage = (int)Math.Ceiling((double)data.Count / data.PageSize);
			}
			else
			{
				model.Data = new ObservableCollection<MessageModel>();
				model.PageIndex = 1;
				model.TotalCount = 0;
			}
		}
	}
}
