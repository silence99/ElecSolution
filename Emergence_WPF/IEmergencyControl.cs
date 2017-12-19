using Framework;

namespace Emergence_WPF
{
	interface IEmergencyControl<T>: IEmergencyInit where T : NotificationObject
	{
		string StrategyControllerName { get; }
		T UiModel { get; set; }
		StrategyController StrategyController { get; set; }
	}
}
