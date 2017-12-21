using Busniess.Services.EventSvr;
using Emergence.Common.ViewModel;
using Framework;
using System;
using System.Collections.Generic;

namespace Business.Strategies
{
	public class MainPageStrategyController : StrategyController
	{
		private GetMasterEventSvr service = new GetMasterEventSvr();
		public MainPageStrategyController() :
			base(null, new List<Strategy>())
		{
		}

		public override void InitializationUiModel(NotificationObject uiModel)
		{
			UIModel = uiModel;
			var model = UIModel as MainPageUiModel;
			//for test
			model.MasterEvents = new List<MasterEventUiModel>()
			{
				new MasterEventUiModel()
				{
					 ID = 0,
					 Describe = "Description",
					 Videos = new List<VideoViewModel>()
					 {
						 new VideoViewModel()
						 {
							  ImageUri =@"DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri ="DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 },
						 new VideoViewModel()
						 {
							  ImageUri = "DebugImage/11.jpg",
							  Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
						 }
					 }
				}
			};

			base.InitializationUiModel(uiModel);
		}
	}
}
