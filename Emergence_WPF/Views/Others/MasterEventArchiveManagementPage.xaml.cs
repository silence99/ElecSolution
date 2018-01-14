using Busniess.ViewModel;
using Emergence_WPF.Comm;
using Framework;
using System.Windows.Controls;

namespace Emergence_WPF
{
	/// <summary>
	/// MasterEventArchiveManagementPage.xaml 的交互逻辑
	/// </summary>
	public partial class MasterEventArchiveManagementPage : Page
	{
		public MasterEventArchiveManagemenPageViewModel ViewModel { get; set; }
		public MasterEventArchiveManagementPage()
		{
			InitializeComponent();
			ViewModel = new MasterEventArchiveManagemenPageViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			ViewModel.SyncData();
		}
	}
}
