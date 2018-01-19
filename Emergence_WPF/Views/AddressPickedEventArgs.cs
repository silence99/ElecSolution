using System.Windows;

namespace Emergence_WPF.Views
{
	public class AddressPickedEventArgs
	{
		public string Address { get; set; }
		public Point Coordinate { get; set; }
	}
	public delegate void AddressPickedHandler(AddressPickedEventArgs e);
}
