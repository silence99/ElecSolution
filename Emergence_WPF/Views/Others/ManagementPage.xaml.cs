using System.Windows.Controls;
using System.Windows.Media;

namespace Emergence_WPF
{
	/// <summary>
	/// ManagementPage.xaml 的交互逻辑
	/// </summary>
	public partial class ManagementPage : Page
	{
		public ManagementPage()
		{
			InitializeComponent();
			InitPage();
		}

		private void InitPage()
		{
			ChangeStyle(Menu1);
			ManagementPagesFrame.Navigate(new CameraManagementPage());
		}

		private void SwitchToCameroPage_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ChangeStyle(sender as Label);
			ManagementPagesFrame.Navigate(new CameraManagementPage());
		}

		private void SwitchToAnnouncementPage_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ChangeStyle(sender as Label);
			ManagementPagesFrame.Navigate(new AnnouncementManagementPage());
		}

		private void SwitchToArchivePage_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ChangeStyle(sender as Label);
			ManagementPagesFrame.Navigate(new MasterEventArchiveManagementPage());
		}

		private void SwitchToUpgradePage_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			ChangeStyle(sender as Label);
			ManagementPagesFrame.Navigate(new UpgradeManagementPage());
		}

		private void ChangeStyle(Label label)
		{
			var brushWhite = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
			var brushSeleted = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#006f69"));
			Menu1.Background = brushWhite;
			Menu2.Background = brushWhite;
			Menu3.Background = brushWhite;
			Menu4.Background = brushWhite;
			label.Background = brushSeleted;
		}
	}
}
