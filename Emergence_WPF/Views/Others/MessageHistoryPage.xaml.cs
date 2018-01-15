using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Emergence_WPF
{
	/// <summary>
	/// MessageHistoryPage.xaml 的交互逻辑
	/// </summary>
	public partial class MessageHistoryPage : Page
	{
		public MessageHistoryPage()
		{
			InitializeComponent();
		}

        private void NavigateToMessageNotification_Handler(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new MessageNotificationPage(), UriKind.Relative);
        }

		private void Pager_OnPageChanged(object sender, object e)
		{

		}
	}
}
