using Framework;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace Emergence.Business.ViewModel
{
	public class MainWindowViewModel : NotificationObject
	{
		public virtual string UserName { get; set; }
		public virtual double Left { get; set; }
		public virtual double Top { get; set; }
		public virtual double Height { get; set; }
		public virtual double Width { get; set; }
		public virtual WindowState WindowState { get; set; }
		public virtual WindowStyle WindowStyle { get; set; }
		public virtual ResizeMode ResizeMode { get; set; }
		public virtual string UserNameLabel { get { return "用户" + UserName; } }
		public virtual MainPageUiModel MainPageData { get; set; }
		public StatisticsViewModel Statistics { get; set; }

		public virtual DelegateCommand<string> NavigateCommand { get; set; }		
	}
}
