namespace Emergence_WPF.Views
{
	public class AddressPickedEventArgs
	{
		public string Address { get; set; }
	}
	public delegate void AddressPickedHandler(AddressPickedEventArgs e);
}
