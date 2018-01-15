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
using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence_WPF.Util;
using Emergence.Common.Model;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for EmergenceMainPage.xaml
	/// </summary>
	public partial class EmergenceMainPage : Page
	{
		private MainWindowViewModel ViewModel { get; set; }
		public EmergenceMainPage()
		{
			InitializeComponent();

			ViewModel = new MainWindowViewModel();
			DataContext = ViewModel;
		}

		public EmergenceMainPage(bool isShowMaxPop) : this()
		{
			ViewModel.MainWindowSubTitleVisible = isShowMaxPop ? "Visible" : "Collapsed";
			if (DataGrid_MasterEvent.Items.Count > 0)
			{
				DataGridRow row = (DataGridRow)DataGrid_MasterEvent.ItemContainerGenerator.ContainerFromIndex(0);
				row.IsSelected = true;
			}
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var svc = ServiceManager.Instance.GetService<MasterEventService>(Constant.Services.MasterEventService);
			//this.DataCodeing.ItemsSource = svc.GetMasterEventForMainPage();
			VideoList.BindingViewModel(svc.GetVideos());
		}

		private void MasterEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var masterEvent = (sender as DataGrid).SelectedItem as MasterEvent;
			if (masterEvent != null)
			{
				var subeventService = new SubeventService();
				var cameraService = new CameraService();
				// for show to customers, remove if release
				masterEvent.Longitude = 119.931298.ToString();
				masterEvent.Latitude = 28.469722.ToString();

				map.RemoveMarkers();
				map.MarkPoint(double.Parse(masterEvent.Longitude), double.Parse(masterEvent.Latitude), string.Format("主事件:{0}", masterEvent.Title), masterEvent.Remarks);
				var subevents = subeventService.GetAllSubevents(masterEvent.ID);
				double latitude = 28.469722;
				double longitude = 119.931298;
				var camerasData = cameraService.GetCameraByMasterEvent(1, 10000, latitude, longitude);
				CameraModel[] cameras = new CameraModel[0];
				if (camerasData != null && camerasData.Data != null)
				{
					cameras = camerasData.Data;
				}

				foreach (var item in subevents)
				{
					double childLongitude = 0;
					double childLatitude = 0;
					if (double.TryParse(item.ChildLongitude, out childLongitude)) { childLongitude = 119.931298; }
					if (double.TryParse(item.ChildLongitude, out childLongitude)) { childLatitude = 28.469722; }
					map.MarkPoint(childLongitude, childLatitude, string.Format("子事件:{0}", item.ChildTitle), item.ChildRemarks);
				}

				foreach (var item in cameras)
				{
					map.MarkPoint(item.Longitude, item.Latitude, string.Format("摄像头:{0}", item.VideoNumber), item.Remarks);
				}
			}
		}
	}
}
