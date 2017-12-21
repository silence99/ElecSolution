using Framework;

namespace Emergence_WPF
{
	interface IEmergencyInit
	{
		void BindingUiModel(StrategyController parent, NotificationObject uiModel);
		void InitUiModel();
	}
}
