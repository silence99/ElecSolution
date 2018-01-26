using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence.Common.Model;
using Emergence_WPF.Util;
using Framework;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Emergence_WPF.Views;
using System.Reflection;
using log4net;
using System.Drawing;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for EmergenceMainPage.xaml
	/// </summary>
	public partial class EmergenceMainPage : Page
	{
		private ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private MainWindowViewModel ViewModel { get; set; }

		public EmergenceMainPage()
		{
			InitializeComponent();

			ViewModel = new MainWindowViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		public EmergenceMainPage(bool isShowMaxPop) : this()
		{
			ViewModel.MainWindowSubTitleVisible = isShowMaxPop ? "Visible" : "Collapsed";
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			var svc = ServiceManager.Instance.GetService<MasterEventService>(Constant.Services.MasterEventService);
			VideoContainer.Width = (int)BorderVideoContainer.ActualWidth;
			VideoContainer.Height = (int)BorderVideoContainer.ActualHeight;
			VideoContainer.AutoScroll = true;
			VideoContainer.BackColor = Color.Black;
			//var panel = new System.Windows.Forms.Panel();
			//panel.Width = VideoContainer.Width + 20;
			//panel.Height = VideoContainer.Height + 20;
			//panel.AutoScroll = true;
			//VideoContainer.Controls.Add(panel);			

			var tempWidth = this.map.ActualWidth;
			var tempHeight = this.map.ActualHeight;
			this.MainPageSelectMapPopup.HorizontalOffset = tempWidth - 250;
			//if (DataGrid_MasterEvent.Items.Count > 0)
			//{
			//	DataGridRow row = (DataGridRow)DataGrid_MasterEvent.ItemContainerGenerator.ContainerFromIndex(0);
			//	row.IsSelected = true;
			//}
		}

		#region for temp
		private void Page_Unloaded(object sender, RoutedEventArgs e)
		{
			DisposeVideos();
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
			try
			{
				map.RemoveMarkers();
				var masterEvent = (sender as DataGrid).SelectedItem as MasterEvent;
				if (masterEvent != null)
				{
					var subeventService = new SubeventService();
					var cameraService = new CameraService();
					map.MarkPoint(masterEvent.Longitude, masterEvent.Latitude, string.Format("主事件:{0}", masterEvent.Title), masterEvent.Remarks);

					var subevents = subeventService.GetAllSubevents(masterEvent.ID);
					var camerasData = cameraService.GetCameraByMasterEvent(1, 10000, masterEvent.Latitude, masterEvent.Longitude);
					CameraModel[] cameras = new CameraModel[0];
					if (camerasData != null && camerasData.Data != null)
					{
						cameras = camerasData.Data;
					}

					ViewModel.CurrentMasterEventVideos = new ObservableCollection<CameraModel>(cameras.Select(it => it.CreateAopProxy()));
					DisplayVideos(cameras);

					foreach (var item in subevents)
					{
						map.MarkPoint(item.ChildLongitude, item.ChildLatitude, string.Format("子事件:{0}", item.ChildTitle), item.ChildRemarks);
					}

					foreach (var item in cameras)
					{
						map.MarkPoint(item.Longitude, item.Latitude, string.Format("摄像头:{0}", item.VideoNumber), item.Remarks);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("选择主事件发生异常", ex);
			}
		}

		private void DisplayVideos(CameraModel[] videos)
		{
			DisposeVideos();
			if (videos != null)
			{
				// var panel = VideoContainer.Controls[0] as System.Windows.Forms.Panel;
				var panel = VideoContainer as System.Windows.Forms.Panel;
				if (panel != null)
				{
					int i = 0;
					foreach (var video in videos)
					{
						Vlc.DotNet.Forms.VlcControl vtl = new Vlc.DotNet.Forms.VlcControl();
						vtl.Width = VideoContainer.Width - 20;
						vtl.Height = (int)(vtl.Width * 9 / 16);
						panel.Controls.Add(vtl);
						vtl.Location = new System.Drawing.Point(0, i * (vtl.Height + 10));
						vtl.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
						vtl.TimeChanged += MediaPlayer_TimeChanged;
						
						vtl.EndInit();
						vtl.Play(video.Url);
						vtl.Audio.Volume = 0;
						i++;
					}
				}
			}
		}

		private void DisposeVideos()
		{
			if (VideoContainer.HasChildren)
			{
				// var panel = VideoContainer.Controls[0] as System.Windows.Forms.Panel;
				var panel = VideoContainer as System.Windows.Forms.Panel;
				if (panel != null)
				{
					foreach (System.Windows.Forms.Control ctl in panel.Controls)
					{
						var vd = ctl as Vlc.DotNet.Forms.VlcControl;
						vd.Dispose();
						panel.Controls.Remove(ctl);
					}
				}
			}
		}
	}
}
