using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

		private void ClusteredColumnChart_Loaded(object sender, RoutedEventArgs e)
		{
			Chart.Width = ActualWidth;
			Chart.Height = ActualHeight;
		}
	}
}
