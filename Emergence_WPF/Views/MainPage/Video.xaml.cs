using Busniess.Strategies;
using Emergence.Business.ViewModel;
using Emergence_WPF.Util;
using Framework;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Vlc.DotNet.Wpf;

namespace Emergence_WPF.Views
{
	/// <summary>
	/// Video.xaml 的交互逻辑
	/// </summary>
	public partial class Video : UserControl
	{
		public VideoViewModel ViewModel { get; set; }
		public StrategyController StrategyController { get; set; }
		public Video()
		{
			InitializeComponent();
			VideoPlay.EndInit();
			VideoPlay.BackgroundImage = System.Drawing.Image.FromFile(VideoViewModel.DefaultImageUri);
		}

		private void MediaPlayer_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			VideoPlay.Audio.Volume = 0;
		}

		public void BindingViewModel(VideoViewModel viewModel)
		{
			ViewModel = viewModel;
			//DataContext = ViewModel;
			//StrategyController = ObjectFactory.GetInstance<VideoStrategyController>(Constant.Services.VideoStrategyController);
			//StrategyController.RegisterListener(viewModel, "Width", (sender, args) => { ViewModel.Height = ViewModel.Height; });
		}

		private void MediaPlayer_Click(object sender, EventArgs e)
		{
			var obj = sender as Vlc.DotNet.Forms.VlcControl;
			if (obj != null)
			{
				if (!obj.IsPlaying)
				{
					obj.Play(ViewModel.Uri);
					obj.Audio.Volume = 0;
				}
				else
				{
					obj.Pause();
				}
			}
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

		public void Start(VlcControl player)
		{
			player.MediaPlayer.Play(ViewModel.Uri);
			player.MediaPlayer.Audio.Volume = 0;
		}

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
		{
			if (VideoPlay != null)
			{
				VideoPlay.Dispose();
			}
		}
	}
}
