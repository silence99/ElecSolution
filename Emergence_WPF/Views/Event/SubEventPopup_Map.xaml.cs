using Emergence_WPF.Comm;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for SubEventPopup_Map.xaml
	/// </summary>
	public partial class SubEventPopup_Map : Page
	{
		public event EventHandler Close;

		public Dictionary<Point, string> ViewModel { get; set; }

		public SubEventPopup_Map()
		{
			InitializeComponent();
			//WebBrowserOverlay wbo = new WebBrowserOverlay(Map);
			//WebBrowser wb = wbo.WebBrowser;
			//wb.Navigate(new Uri(System.IO.Path.GetFullPath(@"Views/Event/SubEventsMap.html")));
			var browser = new System.Windows.Forms.WebBrowser();
			FormControlHost webbrowser = new FormControlHost(browser);

			browser.Navigate(new Uri(System.IO.Path.GetFullPath(@"Views/Event/SubEventsMap.html")));
			Map.Child = webbrowser;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close?.Invoke(this, new EventArgs());
		}

		private void WebBrowser_Loaded(object sender, RoutedEventArgs e)
		{

		}
	}
}
