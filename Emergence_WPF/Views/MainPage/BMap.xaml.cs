using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF
{
	/// <summary>
	/// UserControl1.xaml 的交互逻辑
	/// </summary>
	public partial class BMap : UserControl
	{
		public BMap()
		{
			InitializeComponent();
			browser.Navigate(new Uri(Path.GetFullPath(@"Views/MainPage/BMap.html")));
			browser.ScriptErrorsSuppressed = false;

		}

		public void Bind(string jing, string wei)
		{
			object[] objs = new object[2] {
				double.Parse(jing),
				double.Parse(wei)
			};
			browser.Document.InvokeScript("removeOverlay");
			browser.Document.InvokeScript("addMarker", objs);
			objs = new object[2] {
				double.Parse(jing),
				double.Parse(wei)
			};
			browser.Document.InvokeScript("MoveToPoint", objs);
		}

		public void RemoveMarkers()
		{
			browser.Document.InvokeScript("removeOverlay");
		}

		public void MoveToCenter(double longitude, double latitude)
		{
			browser.Document.InvokeScript("MoveToPoint", new object[] { longitude, latitude });
		}

		public void MarkPoint(double jing, double wei, string title, string details)
		{
			browser.Document.InvokeScript("addMarker", new object[] { jing, wei, 0, title, details });
		}

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
		{
			browser.Dispose();
		}
	}
}
