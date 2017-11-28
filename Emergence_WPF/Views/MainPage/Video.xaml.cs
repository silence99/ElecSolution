using Emergence.Common.ViewModel;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Vlc.DotNet.Wpf;

namespace Emergence_WPF.Views
{
    /// <summary>
    /// Video.xaml 的交互逻辑
    /// </summary>
    public partial class Video : UserControl
    {
        public Video()
        {
            InitializeComponent();
            VideoPlay.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;
            VideoPlay.MediaPlayer.EndInit();

            VideoPlay.MediaPlayer.Click += MediaPlayer_Click;
        }

        private void MediaPlayer_Click(object sender, EventArgs e)
        {
            Stop(VideoPlay);
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

        public static readonly DependencyProperty UiModelProperty =
           DependencyProperty.Register("UiModel", typeof(VideoUiModel),
           typeof(Video),
           new PropertyMetadata(new VideoUiModel(), new PropertyChangedCallback(OnViewModelChanged)));

        public static readonly DependencyProperty VideoStatusProperty =
           DependencyProperty.Register("State", typeof(VideoStatus),
           typeof(Video),
           new PropertyMetadata(VideoStatus.Stop, new PropertyChangedCallback(OnViewStateChanged)));

        public VideoUiModel UiModel
        {
            get { return (VideoUiModel)GetValue(UiModelProperty); }

            set
            {
                SetValue(UiModelProperty, value);
                if (UiModel.ImageUri != null)
                {
                    var source = new BitmapImage(); ;
                    source.BeginInit();

                    source.UriSource = new Uri(Path.GetFullPath(UiModel.ImageUri));
                    source.EndInit();
                    VideoImage.Source = source;
                }
            }
        }

        public VideoStatus State
        {
            get { return (VideoStatus)GetValue(VideoStatusProperty); }
            set { SetValue(VideoStatusProperty, value); }
        }

        public void OnImageClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                var img = sender as Image;
                img.Visibility = Visibility.Hidden;
                VideoPlay.Visibility = Visibility.Visible;
                Start(VideoPlay);
            }
        }


        static void OnViewModelChanged(object sender, DependencyPropertyChangedEventArgs args)
        {

        }

        static void OnViewStateChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var videoCtl = sender as Video;
            if (videoCtl != null)
            {
                videoCtl.Start(videoCtl.VideoPlay);
            }
        }

        public void Start(VlcControl player)
        {
            player.MediaPlayer.Play(UiModel.Uri);
        }

        private void Stop(VlcControl player)
        {
            player.MediaPlayer.Stop();
        }

        private void Pause(VlcControl player)
        {
            player.MediaPlayer.Pause();
        }

        private void Resume(VlcControl player)
        {
            player.MediaPlayer.Play();
        }
    }
}
