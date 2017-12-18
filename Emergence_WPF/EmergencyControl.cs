using Framework;
using System.Windows.Controls;

namespace Emergence_WPF
{
	public class EmergencyControl : UserControl
	{
		public NotificationObject UiModel { get; set; }
		public StrategyController StrategyController { get; set; }

		public virtual void BindingUiModel(NotificationObject uiModel, StrategyController parent)
		{

		}
	}
}
