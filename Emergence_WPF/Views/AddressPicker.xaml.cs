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

namespace Emergence_WPF.Views
{
	/// <summary>
	/// AddressPicker.xaml 的交互逻辑
	/// </summary>
	public partial class AddressPicker : UserControl
	{
		public AddressPicker()
		{
			InitializeComponent();
		}

		public event AddressGotHandler AddressGot;
		public event EventHandler Close;

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			if (AddressGot != null)
			{
				var address = WebBrowser.InvokeScript("getAddress").ToString();
				AddressPickerEventArgs args = new AddressPickerEventArgs()
				{
					Address = address
				};
				AddressGot(this, args);
			}
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close?.Invoke(this, new EventArgs());
		}
	}

	public class AddressPickerEventArgs : EventArgs
	{
		public string Address { get; set; }
	}

	public delegate void AddressGotHandler(object sender, AddressPickerEventArgs e);
}
