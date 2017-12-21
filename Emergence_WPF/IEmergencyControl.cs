using Framework;

namespace Emergence_WPF
{
	interface IEmergencyControl<T>: IEmergencyInit where T : NotificationObject
	{
		string StrategyControllerName { get; }
		T ViewModel { get; set; }
		StrategyController StrategyController { get; set; }
	}
}
