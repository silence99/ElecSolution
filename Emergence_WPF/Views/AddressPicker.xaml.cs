using Emergence_WPF.Comm;
using System.Windows.Controls;

namespace Emergence_WPF.Views
{
	/// <summary>
	/// AddressPicker.xaml 的交互逻辑
	/// </summary>
	public partial class AddressPicker : UserControl
	{
		public System.Windows.Forms.WebBrowser Browser { get; set; }
		public FormControlHost Host { get; set; }
		public AddressPicker()
		{
			InitializeComponent();

			Browser = new System.Windows.Forms.WebBrowser();
			Browser.DocumentStream = System.IO.File.OpenRead(System.IO.Path.GetFullPath("AddressPicker.html"));
			Host = new FormControlHost(Browser);
			MapContainer.Children.Clear();
			MapContainer.Children.Add(Host);
		}

		public string PickedAddress
		{
			get
			{
				return Browser.Document.InvokeScript("getAddress").ToString();
			}
		}

		private void MapContainer_Unloaded(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!Browser.IsDisposed && !Browser.Disposing)
			{
				Browser.Dispose();
			}
		}

		static AddressPicker()
		{
			WidthProperty.OverrideMetadata(typeof(AddressPicker), new System.Windows.FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var ctl = sender as AddressPicker;
				ctl.MapContainer.Width = ctl.Width;
				ctl.Host.Width = ctl.Width;
				ctl.Browser.Width = (int)ctl.Width;
			}));
			HeightProperty.OverrideMetadata(typeof(AddressPicker), new System.Windows.FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var ctl = sender as AddressPicker;
				ctl.Host.Height = ctl.Height;
				ctl.Browser.Height = (int)ctl.Height;
				ctl.MapContainer.Height = ctl.Height;
			}));
		}
	}

}
