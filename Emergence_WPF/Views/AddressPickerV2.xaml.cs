using System;
using System.Windows;
using System.Windows.Controls;
using WebBrowserOnTransparentWindow;

namespace Emergence_WPF.Views
{
	/// <summary>
	/// AddressPickerV2.xaml 的交互逻辑
	/// </summary>
	public partial class AddressPickerV2 : Window
	{
		WebBrowser Browser { get; set; }
		public event AddressPickedHandler AddressPickedEvent;
		public AddressPickerV2()
		{
			InitializeComponent();
			WebBrowserOverlay wbo = new WebBrowserOverlay(MapContainer);
			Browser = wbo.WebBrowser;
			Browser.Navigate(new Uri(System.IO.Path.GetFullPath("Views/AddressPicker.html")));
		}

		public string PickedAddress
		{
			get
			{
				return Browser.InvokeScript("getAddress").ToString();
			}
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(PickedAddress))
			{
				AddressPickedEvent?.Invoke(new AddressPickedEventArgs() { Address = PickedAddress });
			}
			this.Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
