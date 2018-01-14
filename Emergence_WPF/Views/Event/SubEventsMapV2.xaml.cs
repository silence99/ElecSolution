using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF
{
	/// <summary>
	/// Map.xaml 的交互逻辑
	/// </summary>
	public partial class SubEventsMapV2 : Window
	{
		WebBrowser Browser { get; set; }
		public List<Tuple<Point, string, string>> Data { get; set; }
		public SubEventsMapV2()
		{
			InitializeComponent();
			WebBrowserOverlay wbo = new WebBrowserOverlay(MapContainer);
			Browser = wbo.WebBrowser;
			Browser.Navigate(new Uri(System.IO.Path.GetFullPath("Views/Event/SubEventsMap.html")));
			Browser.LoadCompleted += Browser_LoadCompleted;
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

		public void BindingData(List<Tuple<Point, string, string>> data)
		{
			Data = data;
			if (Data != null && Data.Count > 0)
			{
				foreach (var item in Data)
				{
					Browser.InvokeScript("addMarker", item.Item1.X, item.Item1.Y, 0, item.Item2, item.Item3);
				}
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

	}
}
