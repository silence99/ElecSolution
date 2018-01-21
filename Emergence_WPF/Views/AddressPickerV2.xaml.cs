using System;
using System.Windows;

namespace Emergence_WPF
{
	/// <summary>
	/// AddressPickerV2.xaml 的交互逻辑
	/// </summary>
	public partial class AddressPickerV2 : Window
	{
		public event AddressPickedHandler AddressPickedEvent;
		public AddressPickerV2()
		{
			InitializeComponent();
			Browser.Navigate(new Uri(System.IO.Path.GetFullPath("Views/AddressPicker.html")));
		}

		public string PickedAddress
		{
			get
			{
				var obj = Browser.InvokeScript("getAddress");
				return obj == null ? "" : obj.ToString();
			}
		}

		public Point PickedCoordinate
		{
			get
			{
				double x = 0;
				double y = 0;
				var xVar = Browser.InvokeScript("getCoordinate_X");
				var yVar = Browser.InvokeScript("getCoordinate_Y");
				x = double.TryParse(xVar == null ? "0.0" : xVar.ToString(), out x) ? x : 0;
				y = double.TryParse(yVar == null ? "0.0" : yVar.ToString(), out y) ? y : 0;
				return new Point(x, y);
			}
		}

		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
            if (!string.IsNullOrEmpty(PickedAddress))
            {
                AddressPickedEvent?.Invoke(new AddressPickedEventArgs() { Address = PickedAddress, Coordinate = PickedCoordinate });
                this.Close();

            }
            else
            {
                System.Windows.MessageBox.Show("请选择地址！");
            }
        }

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
