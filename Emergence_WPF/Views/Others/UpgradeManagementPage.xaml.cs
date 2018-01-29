using Busniess.ViewModel;
using Emergence_WPF.Util;
using Framework;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Emergence_WPF
{
	/// <summary>
	/// UpgradeManagementPage.xaml 的交互逻辑
	/// </summary>
	public partial class UpgradeManagementPage : Page
	{
		public UpgradeManagementPageViewModel ViewModel { get; set; }

		public UpgradeManagementPage()
		{
			InitializeComponent();
			ViewModel = ObjectFactory.GetInstance<UpgradeManagementPageViewModel>("upgradeManagementPageViewModel").CreateAopProxy();
			DataContext = ViewModel;
		}

		private void OnDownload_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
			DialogResult result = m_Dialog.ShowDialog();

			if (result == System.Windows.Forms.DialogResult.Cancel)
			{
				return;
			}

			ViewModel.DownloadFolder = m_Dialog.SelectedPath.Trim();
			ViewModel.Download();
		}
	}
}
