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
            //VideoList.BindingViewModel(svc.GetVideos());
            V0.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            V1.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            V2.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            V3.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            V0.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            V1.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            V3.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            V2.MediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            V0.MediaPlayer.EndInit();
            V1.MediaPlayer.EndInit();
            V2.MediaPlayer.EndInit();
            V3.MediaPlayer.EndInit();

            V0.MediaPlayer.Play(new Uri("http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171012/346652/1_rD6ose3DCyObL_YD2m0kuA_media.mp4"));
            V1.MediaPlayer.Play(new Uri("http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171013/346830/1_gQFJSGJnYGbRtDUelsgYPw_media.mp4"));
            V2.MediaPlayer.Play(new Uri("http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171017/325941/1_7IUPQVtjwGQ5xkGq-1cTQw_media.mp4"));
            V3.MediaPlayer.Play(new Uri("http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171017/346652/1_JXw4NVgv-LyIwCztsMCvuw_media.mp4"));

            V0.MediaPlayer.Audio.Volume = 0;
            V1.MediaPlayer.Audio.Volume = 0;
            V2.MediaPlayer.Audio.Volume = 0;
            V3.MediaPlayer.Audio.Volume = 0;

            var tempWidth = this.map.ActualWidth;
            var tempHeight = this.map.ActualHeight;
            this.MainPageSelectMapPopup.HorizontalOffset = tempWidth  - 250;
            //this.MainPageSelectMapPopup.VerticalOffset = 65;
        }
		#region for temp
		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
			V0.MediaPlayer.Dispose();
			V1.MediaPlayer.Dispose();
			V2.MediaPlayer.Dispose();
			V3.MediaPlayer.Dispose();

		}
		private void OnVlcControlNeedsLibDirectory(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
		{
			var currentAssembly = System.Reflection.Assembly.GetEntryAssembly();
			var currentDirectory = new System.IO.FileInfo(currentAssembly.Location).DirectoryName;
			if (currentDirectory == null)
				return;

			if (IntPtr.Size == 4)
				e.VlcLibDirectory = new System.IO.DirectoryInfo(System.IO.Path.Combine(currentDirectory, @"lib\x86\"));
			else
				e.VlcLibDirectory = new System.IO.DirectoryInfo(System.IO.Path.Combine(currentDirectory, @"lib\x64\"));
		}

		private void MediaPlayer_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			var obj = sender as Vlc.DotNet.Forms.VlcControl;
			obj.Audio.Volume = 0;
		}

		#endregion

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
