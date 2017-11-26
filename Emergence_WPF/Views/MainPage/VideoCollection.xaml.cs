using Emergence.Common.ViewModel;
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

namespace Emergence_WPF.Views.MainPage
{
    /// <summary>
    /// VideoCollection.xaml 的交互逻辑
    /// </summary>
    public partial class VideoCollection : UserControl
    {
        List<VideoUiModel> _uiMode = null;
        public List<VideoUiModel> UiModel
        {
            get
            {
                return _uiMode;
            }
            set
            {
                _uiMode = value;
                RefreshVideos();
            }
        }

        public VideoCollection()
        {
            InitializeComponent();
        }

        private void RefreshVideos()
        {
            StackPanel container = new StackPanel();

            foreach (var uiModel in UiModel)
            {
                Video video = new Video();
                video.UiModel = uiModel;
                container.Children.Add(video);
            }

            VideoScroll.Content = container;
        }
    }
}
