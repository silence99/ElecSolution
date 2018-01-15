using Business.Strategies;
using Emergence.Business.ViewModel;
using Framework;
using System.Windows.Controls;

namespace Emergence_WPF.Views.MainPage
{
	/// <summary>
	/// Weather.xaml 的交互逻辑
	/// </summary>
	public partial class Weather : UserControl
	{
		protected WeatherViewModel ViewModel = new WeatherViewModel();
		protected StrategyController strategyController = null;
		public Weather()
		{
			InitializeComponent();
			DataContext = ViewModel;
			strategyController = new WeatherStrategyController();
			strategyController.InitializationUiModel(ViewModel);
		}
	}
}
