using Business.MainPageSvr;
using Emergence.Common;
using Emergence.Common.Mappers;
using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using Framework;

namespace Business.Strategies
{
    public class WeatherStrategyController : StrategyController
    {
        WeatherService service = new WeatherService();
        IMapper<Weather, WeatherUiModel> mapper = new WeatherMapper();

        public WeatherStrategyController()
            : base(null, null)
        {
        }

        public override void InitializationUiModel(NotificationObject uiModel)
        {
			UIModel = uiModel;
            var weather = service.ProcessRequest(null);
            mapper.MapToUiModel(weather, UIModel as WeatherUiModel);
        }
    }
}
