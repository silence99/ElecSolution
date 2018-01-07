using Busniess.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Framework;
using System.Windows.Controls;

namespace Emergence_WPF.Views.Others
{
	/// <summary>
	/// AnnouncementManagementPage.xaml 的交互逻辑
	/// </summary>
	public partial class AnnouncementManagementPage : Page
	{
		AnnouncementManagementPageViewModel ViewModel { get; set; }
		public AnnouncementManagementPage()
		{
			InitializeComponent();
			ViewModel = new AnnouncementManagementPageViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
		}

		private void PopupEdit_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var obj = sender as Image;
			if (obj != null)
			{
				var current = obj.Tag as AnnouncementModel;
				if (current != null)
				{
					ViewModel.PopupEditAction(current);
				}
			}
		}
	}
}
