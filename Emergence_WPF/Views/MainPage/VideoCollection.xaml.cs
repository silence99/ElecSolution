using Emergence.Business.ViewModel;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Emergence_WPF.Views.MainPage
{
	/// <summary>
	/// VideoCollection.xaml 的交互逻辑
	/// </summary>
	public partial class VideoCollection : UserControl
	{
		public VideoCollection()
		{
			InitializeComponent();
		}

		public void BindingViewModel(List<VideoViewModel> videos)
		{
			Content.Children.Clear();
			foreach (var item in videos)
			{
				Video video = new Video();
				Content.Children.Add(video);
				video.SetValue(DockPanel.DockProperty, Dock.Top);
				video.Margin = new System.Windows.Thickness(0, 0, 0, 0);
				video.Width = ActualWidth;
				video.BindingViewModel(item);
			}
		}
	}
}
