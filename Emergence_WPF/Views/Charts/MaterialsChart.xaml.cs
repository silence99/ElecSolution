using System.Windows.Controls;

namespace Emergence_WPF.Views.Charts
{
	/// <summary>
	/// MaterialsChart.xaml 的交互逻辑
	/// </summary>
	public partial class MaterialsChart : UserControl
	{
		public MaterialsChart()
		{
			InitializeComponent();
		}

		static MaterialsChart()
		{
			WidthProperty.OverrideMetadata(typeof(MaterialsChart), new System.Windows.FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var ctl = sender as MaterialsChart;
				ctl.Chart.Width = ctl.Width;
			}));
			HeightProperty.OverrideMetadata(typeof(MaterialsChart), new System.Windows.FrameworkPropertyMetadata(100.0, (sender, e) =>
			{
				var ctl = sender as MaterialsChart;
				ctl.Chart.Height = ctl.Height;
			}));
		}
	}
}
