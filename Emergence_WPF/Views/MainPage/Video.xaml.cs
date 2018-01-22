using Emergence.Business.ViewModel;
using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF.Views
{
	/// <summary>
	/// Video.xaml 的交互逻辑
	/// </summary>
	public partial class Video : UserControl
	{
		private static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static readonly DependencyProperty UrlProperty = DependencyProperty.Register("Url",
			typeof(string),
			typeof(Video),
			new PropertyMetadata("", OnUrlPropertyChanged));

		public string Url
		{
			get { return GetValue(UrlProperty) as string; }
			set { SetValue(UrlProperty, value); }
		}

		static Video()
		{
			HeightProperty.OverrideMetadata(typeof(Video), new FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var obj = sender as Video;
				obj.VideoPlay.Height = (int)obj.Height;
			}));
			WidthProperty.OverrideMetadata(typeof(Video), new FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var obj = sender as Video;
				obj.VideoPlay.Width = (int)obj.Width;
			}));
		}

		private static void OnUrlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e != null && e.NewValue != null)
			{
				Logger.DebugFormat("play video \"{0}\"", e.NewValue.ToString());
				var obj = d as Video;
				obj.Play(e.NewValue.ToString());
			}
		}

		public void Play(string url)
		{
			try
			{
				VideoPlay.Play(new Uri(url));
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("{0} 地址不能播放");
				Logger.Error(string.Format("play video failed: \"{0}\"", url), ex);
			}
		}

		public Video()
		{
			InitializeComponent();
			VideoPlay.EndInit();
		}

		private void MediaPlayer_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			VideoPlay.Audio.Volume = 0;
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

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
		{
			if (VideoPlay != null)
			{
				VideoPlay.Dispose();
			}
		}
	}
}
