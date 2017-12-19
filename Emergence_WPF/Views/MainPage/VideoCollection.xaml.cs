using Emergence.Common.ViewModel;
using Framework;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Emergence_WPF.Views.MainPage
{
	/// <summary>
	/// VideoCollection.xaml 的交互逻辑
	/// </summary>
	public partial class VideoCollection : UserControl, IEmergencyControl<NotificationObject>
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

		NotificationObject IEmergencyControl<NotificationObject>.UiModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public StrategyController StrategyController { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public string StrategyControllerName => throw new NotImplementedException();

		public VideoCollection()
		{
			InitializeComponent();
		}

		private void RefreshVideos()
		{
			Content.Children.Clear();
			StackPanel container = new StackPanel();

			foreach (var uiModel in UiModel)
			{
				Video video = new Video();
				video.UiModel = uiModel;
				container.Children.Add(video);
			}

			Content.Children.Add(container);
		}

		public void BindingUiModel(StrategyController parent, StrategyController strategyController, NotificationObject uiModel)
		{
			throw new NotImplementedException();
		}

		public void InitUiModel()
		{
			throw new NotImplementedException();
		}
	}
}
