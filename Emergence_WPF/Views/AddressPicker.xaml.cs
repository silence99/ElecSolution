using System;
using System.Windows.Controls;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF.Views
{
	/// <summary>
	/// AddressPicker.xaml 的交互逻辑
	/// </summary>
	public partial class AddressPicker : UserControl
	{
		private WebBrowser Browser { get; set; }
		public AddressPicker()
		{
			InitializeComponent();

			Browser = new WebBrowser();
			Browser.Navigate(new Uri(System.IO.Path.GetFullPath("Views/Event/SubEventsMap.html")));
			MapContainer.Child = Browser;
		}

		public string PickedAddress
		{
			get
			{
				return Browser.InvokeScript("getAddress").ToString();
			}
		}
	}

}
