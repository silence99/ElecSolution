using Framework;
using Microsoft.Practices.Prism.Commands;

namespace Busniess.ViewModel
{
	public class ManagementPageViewModel : NotificationObject
	{
		public DelegateCommand<string> NavigateCommand { get; set; }
	}
}
