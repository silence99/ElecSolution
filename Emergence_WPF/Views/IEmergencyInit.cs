using Framework;

namespace Emergence_WPF.Views
{
	interface IEmergencyInit
	{
		void BindingUiModel(StrategyController parent, StrategyController strategyController, NotificationObject uiModel);
		void InitUiModel();
	}
}
