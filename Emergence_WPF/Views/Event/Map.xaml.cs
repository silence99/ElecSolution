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
using System.Windows.Threading;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF
{
	/// <summary>
	/// Map.xaml 的交互逻辑
	/// </summary>
	public partial class Map : Window
	{
		WebBrowser Browser { get; set; }
		public List<Tuple<Point, string, string>> Data { get; set; }
		public Map()
		{
			InitializeComponent();
			WebBrowserOverlay wbo = new WebBrowserOverlay(MapContainer);
			Browser = wbo.WebBrowser;
			Browser.Navigate(new Uri(System.IO.Path.GetFullPath("Views/Event/SubEventsMap.html")));
			Browser.LoadCompleted += Browser_LoadCompleted;
			Data = new List<Tuple<Point, string, string>>()
			{
				new Tuple<Point, string, string>(new Point(119.931298, 28.469722), "子事件1详细信息信息", "子事件描述")
			};
		}

		private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			var timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(1000);
			timer.Tick += Timer_Tick;
			timer.Tag = DateTime.Now;
			timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			var iscompleted = (bool)Browser.InvokeScript("IsCompleted");
			if (iscompleted)
			{
				var obj = sender as DispatcherTimer;
				obj.Stop();
				if (Data != null && Data.Count > 0)
				{
					foreach (var item in Data)
					{
						Browser.InvokeScript("addMarker", item.Item1.X, item.Item1.Y, 0, item.Item2, item.Item3);
					}
				}
			}
			else
			{
				var obj = sender as DispatcherTimer;
				var start = (DateTime)obj.Tag;
				if ((DateTime.Now - start).Seconds > 15)
				{
					System.Windows.MessageBox.Show("标注子事件信息超时，请重新进入。");
				}
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	}
}
