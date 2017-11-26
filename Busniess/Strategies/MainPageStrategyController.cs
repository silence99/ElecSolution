using Busniess.MainPageSvr;
using Emergence.Common.ViewModel;
using Framework;
using System;
using System.Collections.Generic;

namespace Busniess.Strategies
{
    public class MainPageStrategyController : StrategyController<MainPageUiModel>
    {
        private MasterEventService service = new MasterEventService();
        public MainPageStrategyController(MainPageUiModel uiModel) :
            base(null, uiModel, new List<Strategy>())
        {
        }

        public override void InitializationUiModel()
        {
            base.InitializationUiModel();

            //for test
            UIModel.MasterEvents = new List<MasterEventUiModel>()
            {
                new MasterEventUiModel()
                {
                     ID = 0,
                     Describe = "Description",
                     Videos = new List<VideoUiModel>()
                     {
                         new VideoUiModel()
                         {
                              ImageUri = "DebugImage/11.jpg",
                              Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
                         },
                         new VideoUiModel()
                         {
                              ImageUri = "DebugImage/11.jpg",
                              Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
                         },
                         new VideoUiModel()
                         {
                              ImageUri = "DebugImage/11.jpg",
                              Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
                         },
                         new VideoUiModel()
                         {
                              ImageUri = "DebugImage/11.jpg",
                              Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
                         },
                         new VideoUiModel()
                         {
                              ImageUri = "DebugImage/11.jpg",
                              Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
                         },
                         new VideoUiModel()
                         {
                              ImageUri = "DebugImage/11.jpg",
                              Uri = new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi")
                         },
                     }
                }
            };

        }


    }
}
