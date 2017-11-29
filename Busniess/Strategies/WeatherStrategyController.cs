using Busniess.MainPageSvr;
using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busniess.Strategies
{
    public class WeatherStrategyController : StrategyController<Weather>
    {
        WeatherService service = new WeatherService();

        public WeatherStrategyController(Weather uiModel)
            : base(null, uiModel, null)
        {
        }

        public override void InitializationUiModel()
        {
            UIModel = service.ProcessRequest(null);
        }
    }
}
