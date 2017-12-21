using Business.Strategies;
using Emergence.Common.ViewModel;
using Framework;
using System.Windows.Controls;

namespace Emergence_WPF.Views.MainPage
{
	/// <summary>
	/// Weather.xaml 的交互逻辑
	/// </summary>
	public partial class Weather : UserControl
	{
		protected WeatherViewModel UIModel = new WeatherViewModel();
		protected StrategyController strategyController = null;
		public Weather()
		{
			InitializeComponent();
			DataContext = UIModel;
			strategyController = new WeatherStrategyController();
			strategyController.InitializationUiModel(UIModel);
		}
	}
}
