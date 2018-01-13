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
			//ViewModel.Cameras = new System.Collections.ObjectModel.ObservableCollection<Emergence.Common.Model.CameraModel>()
			//{
			//	new Emergence.Common.Model.CameraModel()
			//	{
			//		 Url = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi",
			//		 VideoNumber = "001"
			//	},
			//	new Emergence.Common.Model.CameraModel()
			//	{
			//		 Url = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi",
			//		 VideoNumber = "002"
			//	},
			//	new Emergence.Common.Model.CameraModel()
			//	{
			//		 Url = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi",
			//		 VideoNumber = "003"
			//	}
			//};
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
		}
		private void Video2SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc1.Play(new Uri(combox.SelectedValue.ToString()));
		}
		private void Video3SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc2.Play(new Uri(combox.SelectedValue.ToString()));
		}
		private void Video4SelectChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			var combox = sender as ComboBox;
			if (string.IsNullOrEmpty(combox.SelectedValue.ToString()))
			{
				return;
			}
			Vlc3.Play(new Uri(combox.SelectedValue.ToString()));
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
