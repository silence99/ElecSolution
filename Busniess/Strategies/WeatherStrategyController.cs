using Business.MainPageSvr;
using Emergence.Business.Mappers;
using Emergence.Common.Model;
using Emergence.Business;
using Emergence.Business.ViewModel;
using Framework;

namespace Business.Strategies
{
	public class WeatherStrategyController : StrategyController
	{
		WeatherService service = new WeatherService();
		IMapper<Weather, WeatherViewModel> mapper = new WeatherMapper();

		public WeatherStrategyController()
			: base(null, null)
		{
		}

		public override void InitializationUiModel(NotificationObject uiModel)
		{
			UIModel = uiModel;
			var weather = service.GetWeather();
			mapper.MapToViewModel(weather, UIModel as WeatherViewModel);
		}
	}
}
