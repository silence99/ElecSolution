using Busniess.Services.EventSvr;
using Emergence_WPF.Util;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF
{
	/// <summary>
	/// main page user control
	/// </summary>
	public partial class UserControl_MainPage : UserControl
	{
		public UserControl_MainPage()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var svc = ServiceManager.Instance.GetService<MasterEventService>(Constant.Services.MasterEventService);
			this.DataCodeing.ItemsSource = svc.GetMasterEventForMainPage();
			VideoList.BindingViewModel(svc.GetVideos());
		}

		private void MasterEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			map.Bind("119.931298", "28.469722");
		}
	}
}
