using Framework;
using System.Collections.Generic;
using System.Windows;

namespace Emergence.Common.ViewModel
{
	public class MainPageUiModel : NotificationObject
	{
		public string UserName
		{
			get
			{
				return "用户" + CommHelp.Name;
			}
		}
		public virtual double Left { get; set; }
		public virtual double Top { get; set; }
		public virtual double Height { get; set; }
		public virtual double Width { get; set; }
		public virtual WindowState WindowState { get; set; }
		public virtual WindowStyle WindowStyle { get; set; }
		public virtual ResizeMode ResizeMode { get; set; }
		public virtual List<MasterEventUiModel> MasterEvents { get; set; }
	}
}
