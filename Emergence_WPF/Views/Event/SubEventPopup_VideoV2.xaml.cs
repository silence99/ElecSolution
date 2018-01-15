using Busniess.ViewModel;
using Framework;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Vlc.DotNet.Forms;

namespace Emergence_WPF.Views.Event
{
	/// <summary>
	/// SubEventPopup_VideoV2.xaml 的交互逻辑
	/// </summary>
	public partial class SubEventPopup_VideoV2 : Window
	{
		public SubEventPopupVideoViewModel ViewModel { get; set; }
		public SubEventPopup_VideoV2()
		{
			InitializeComponent();
			Vlc0.EndInit();
			Vlc1.EndInit();
			Vlc2.EndInit();
			Vlc3.EndInit();
			ViewModel = new SubEventPopupVideoViewModel().CreateAopProxy();
			DataContext = ViewModel;
		}

		/// <summary>
		/// called outside to init data
		/// </summary>
		/// <param name="latitude"></param>
		/// <param name="longitude"></param>
		public void SetCoordinate(double latitude, double longitude)
		{
			ViewModel.SetCoordinate(latitude, longitude);

			//for test
			ViewModel.Cameras = new System.Collections.ObjectModel.ObservableCollection<Emergence.Common.Model.CameraModel>()
			{
				new Emergence.Common.Model.CameraModel()
				{
					 Url = "http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171012/346652/1_rD6ose3DCyObL_YD2m0kuA_media.mp4",
					 VideoNumber = "无人机一号编组01号"
				},
				new Emergence.Common.Model.CameraModel()
				{
					 Url = "http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171013/346830/1_gQFJSGJnYGbRtDUelsgYPw_media.mp4",
					 VideoNumber = "无人机一号编组02号"
				},
				new Emergence.Common.Model.CameraModel()
				{
					 Url = "http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171017/325941/1_7IUPQVtjwGQ5xkGq-1cTQw_media.mp4",
					 VideoNumber = "无人机一号编组03号"
				},
				new Emergence.Common.Model.CameraModel()
				{
					Url = "http://cdn.cnbj2.fds.api.mi-img.com/sportscamera/sportssns/20171017/346652/1_JXw4NVgv-LyIwCztsMCvuw_media.mp4",
					VideoNumber = "无人机一号编组04号"
				}
			};
		}

		private void MediaPlayer_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			var obj = sender as VlcControl;
			obj.Audio.Volume = 0;
		}

		private void OnVlcControlNeedsLibDirectory(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
		{
			var currentAssembly = Assembly.GetEntryAssembly();
			var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
			if (currentDirectory == null)
				return;

			if (IntPtr.Size == 4)
				e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"lib\x86\"));
			else
				e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"lib\x64\"));
		}

		private void Video1SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc0.Play(new Uri(combox.SelectedValue.ToString()));
			Vlc0.Audio.Volume = 0;
		}
		private void Video2SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc1.Play(new Uri(combox.SelectedValue.ToString()));
			Vlc1.Audio.Volume = 0;
		}
		private void Video3SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc2.Play(new Uri(combox.SelectedValue.ToString()));
			Vlc2.Audio.Volume = 0;
		}
		private void Video4SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc3.Play(new Uri(combox.SelectedValue.ToString()));
			Vlc3.Audio.Volume = 0;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Vlc0.Dispose();
			Vlc1.Dispose();
			Vlc2.Dispose();
			Vlc3.Dispose();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
