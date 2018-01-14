﻿using Busniess.Services;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Emergence_WPF.Views.Charts
{
	/// <summary>
	/// MaterialsChart.xaml 的交互逻辑
	/// </summary>
	public partial class MaterialsChart : UserControl
	{
		private MaterialsStatisticsService Service = new MaterialsStatisticsService();
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

			BackgroundProperty.OverrideMetadata(typeof(MaterialsChart), new System.Windows.FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black")), (sender, e) =>
			{
				var ctl = sender as MaterialsChart;
				ctl.Chart.Background = ctl.Background;
			}));

			ForegroundProperty.OverrideMetadata(typeof(MaterialsChart), new System.Windows.FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("White")), (sender, e) =>
			{
				var ctl = sender as MaterialsChart;
				ctl.Chart.Foreground = ctl.Foreground;
			}));
		}

		private void Chart_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			var data = Service.GetMaterialsStatistics();
			var names = new string[5];
			var values = new double[5];
			if (data != null)
			{
				double max = 1;
				for (int i = 0; i < data.Length; i++)
				{
					names[i] = string.Format("{0}({1})", data[i].MaterialsName, data[i].TotalQuantity);
					values[i] = data[i].TotalQuantity;
					max = max >= data[i].TotalQuantity ? max : data[i].TotalQuantity;
				}
				values = values.Select(val => (100 * val / max)).ToArray();
			}
			Chart.ItemsSource = values;
			Chart.ItemsSourceName = names;
		}
	}
}
