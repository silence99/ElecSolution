using Business.MainPageSvr;
using Emergence.Common;
using Emergence.Common.Mappers;
using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using Framework;

namespace Business.Strategies
{
    public class WeatherStrategyController : StrategyController<WeatherUiModel>
    {
        WeatherService service = new WeatherService();
        IMapper<Weather, WeatherUiModel> mapper = new WeatherMapper();

        public WeatherStrategyController(WeatherUiModel uiModel)
            : base(null, uiModel, null)
        {
        }

        public override void InitializationUiModel()
        {
            var weather = service.ProcessRequest(null);
            mapper.MapToUiModel(weather, UIModel);
        }
    }
}
