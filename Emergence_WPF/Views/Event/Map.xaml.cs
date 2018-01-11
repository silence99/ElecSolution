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
using System.Windows.Shapes;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF
{
	/// <summary>
	/// Map.xaml 的交互逻辑
	/// </summary>
	public partial class Map : Window
	{
		public Map()
		{
			InitializeComponent();
			WebBrowserOverlay wbo = new WebBrowserOverlay(MapContainer);
			WebBrowser wb = wbo.WebBrowser;
			wb.Navigate(new Uri(System.IO.Path.GetFullPath("Views/Event/SubEventsMap.html")));
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
