using Busniess.ViewModel;
using Framework;
using System;
using System.Windows;
using System.Windows.Input;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for AddMasterEvent.xaml
	/// </summary>
	public partial class AddMasterEvent : Window
	{
		public bool isclose = false;
		AddMasterEventViewModel ViewModel { get; set; }
		public AddMasterEvent()
		{
			InitializeComponent();
			ViewModel = new AddMasterEventViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		private void DP_CreateMasterEvent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			isclose = true;
			this.Close();
		}
	}
}
