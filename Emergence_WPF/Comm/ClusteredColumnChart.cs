using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Emergence_WPF.Comm
{
	public class ClusteredColumnChart : Control
	{
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(double[]), typeof(ClusteredColumnChart), new PropertyMetadata(new double[5], Refresh));
		public double[] ItemsSource
		{
			get { return (double[])GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		public static readonly DependencyProperty ItemsSourceNameProperty = DependencyProperty.Register("ItemsSourceName", typeof(string[]), typeof(ClusteredColumnChart), new PropertyMetadata(new string[5]));
		public string[] ItemsSourceName
		{
			get { return (string[])GetValue(ItemsSourceNameProperty); }
			set { SetValue(ItemsSourceNameProperty, value); }
		}
		public static readonly DependencyPropertyKey VerticalLinesProperty = DependencyProperty.RegisterReadOnly("VerticalLines", typeof(double[]), typeof(ClusteredColumnChart), new PropertyMetadata(new double[5]));
		public double[] VerticalLines
		{
			get { return (double[])GetValue(VerticalLinesProperty.DependencyProperty); }
			set { SetValue(VerticalLinesProperty, value); }
		}
		public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register("LineColor", typeof(Brush), typeof(ClusteredColumnChart), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"))));
		public Brush LineColor
		{
			get { return (Brush)GetValue(LineColorProperty); }
			set { SetValue(LineColorProperty, value); }
		}
		public static readonly DependencyProperty ColumnColorsProperty = DependencyProperty.Register("ColumnColors", typeof(Brush[]), typeof(ClusteredColumnChart), new PropertyMetadata(GetDefaultColumnsBrush()));
		public Brush[] ColumnColors
		{
			get { return (Brush[])GetValue(ColumnColorsProperty); }
			set { SetValue(ColumnColorsProperty, value); }
		}
		public static readonly DependencyPropertyKey ColumnWidthProperty = DependencyProperty.RegisterReadOnly("ColumnWidth", typeof(double), typeof(ClusteredColumnChart), new PropertyMetadata(10.0));
		public double ColumnWidth
		{
			get { return (double)GetValue(ColumnWidthProperty.DependencyProperty); }
			set { SetValue(ColumnWidthProperty, value); }
		}
		public static readonly DependencyPropertyKey ColumnPositionsProperty = DependencyProperty.RegisterReadOnly("ColumnPositions", typeof(Point[]), typeof(ClusteredColumnChart), new PropertyMetadata(new Point[5]));
		public Point[] ColumnPositions
		{
			get { return (Point[])GetValue(ColumnPositionsProperty.DependencyProperty); }
			set { SetValue(ColumnPositionsProperty, value); }
		}
		public static readonly DependencyPropertyKey ColumnHeightsProperty = DependencyProperty.RegisterReadOnly("ColumnHeights", typeof(double[]), typeof(ClusteredColumnChart), new PropertyMetadata(new double[5]));
		public double[] ColumnHeights
		{
			get { return (double[])GetValue(ColumnHeightsProperty.DependencyProperty); }
			set { SetValue(ColumnHeightsProperty, value); }
		}
		public static readonly DependencyProperty ChartWidthProperty = DependencyProperty.Register("ChartWidth", typeof(double), typeof(ClusteredColumnChart), new PropertyMetadata(0.0));
		public double ChartWidth
		{
			get { return (double)GetValue(ChartWidthProperty); }
			set { SetValue(ChartWidthProperty, value); }
		}
		public static readonly DependencyProperty ChartMarginLeftProperty = DependencyProperty.Register("ChartMarginLeft", typeof(double), typeof(ClusteredColumnChart), new PropertyMetadata(20.0, Refresh));
		public double ChartMarginLeft
		{
			get { return (double)GetValue(ChartMarginLeftProperty); }
			set { SetValue(ChartMarginLeftProperty, value); }
		}
		public static readonly DependencyPropertyKey ChartHeightProperty = DependencyProperty.RegisterReadOnly("ChartHeight", typeof(double), typeof(ClusteredColumnChart), new PropertyMetadata(0.0));
		public double ChartHeight
		{
			get { return (double)GetValue(ChartHeightProperty.DependencyProperty); }
			set { SetValue(ChartHeightProperty, value); }
		}
		public static readonly DependencyProperty ChartMarginBottomProperty = DependencyProperty.Register("ChartMarginBottom",
			typeof(double), typeof(ClusteredColumnChart),
			new PropertyMetadata(30.0, Refresh));
		public double ChartMarginBottom
		{
			get { return (double)GetValue(ChartMarginBottomProperty); }
			set { SetValue(ChartMarginBottomProperty, value); }
		}

		static ClusteredColumnChart()
		{
			HeightProperty.OverrideMetadata(typeof(ClusteredColumnChart), new FrameworkPropertyMetadata(10.0, Refresh));
			WidthProperty.OverrideMetadata(typeof(ClusteredColumnChart), new FrameworkPropertyMetadata(10.0, Refresh));
		}
		private static void Refresh(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var obj = d as ClusteredColumnChart;
			obj.ChartHeight = obj.Height - obj.ChartMarginBottom;
			obj.ChartWidth = obj.Width - obj.ChartMarginLeft;
			obj.ColumnWidth = 0;
			var tempHeights = new double[5];
			var tempPoints = new Point[5];
			if (obj.ItemsSource != null && obj.ItemsSource.Length > 0)
			{
				var span = obj.ChartHeight / 6;
				var count = obj.ItemsSource.Length > 5 ? 5 : obj.ItemsSource.Length;
				obj.ColumnWidth = obj.ChartWidth / (2 * count + 1);
				for (int i = 0; i < 5; i++)
				{
					tempHeights[i] = obj.Height - obj.ChartMarginBottom - span * (i + 1);
					tempPoints[i].X = obj.ChartMarginLeft + obj.ColumnWidth * (2 * i + 1);
					tempPoints[i].Y = obj.ChartHeight - tempHeights[i];
				}
				obj.ColumnHeights = tempHeights;
				obj.ColumnPositions = tempPoints;
			}
		}

		private static Brush[] GetDefaultColumnsBrush()
		{
			return new Brush[]
			{
				new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red")),
				new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red")),
				new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red")),
				new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red")),
				new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red")),
			};
		}
	}
}
