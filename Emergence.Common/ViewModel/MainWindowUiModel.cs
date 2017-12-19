using Framework;
using System.Windows;

namespace Emergence.Common.ViewModel
{
	public class MainWindowUiModel : NotificationObject
	{
		public string UserName { get; set; }
		public virtual double Left { get; set; }
		public virtual double Top { get; set; }
		public virtual double Height { get; set; }
		public virtual double Width { get; set; }
		public virtual WindowState WindowState { get; set; }
		public virtual WindowStyle WindowStyle { get; set; }
		public virtual ResizeMode ResizeMode { get; set; }
		public string UserNameLabel { get { return "用户" + UserName; } }
		public virtual MainPageUiModel MainPageData { get; set; }
	}
}
