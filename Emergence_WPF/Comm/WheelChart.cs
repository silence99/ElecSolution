using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Emergence_WPF.Comm
{
	public class WheelChart : Control
	{
		public readonly static DependencyProperty RadiusProperty = DependencyProperty.Register("Radius",
			typeof(double),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(1.0, OnSensitivePropertyChanged));
		public double Radius { get { return (double)GetValue(RadiusProperty); } set { SetValue(RadiusProperty, value); } }

		public readonly static DependencyProperty InnerDiameterProperty = DependencyProperty.Register("InnerDiameter",
			typeof(double),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(0.0));
		public double InnerDiameter { get { return (double)GetValue(InnerDiameterProperty); } set { SetValue(InnerDiameterProperty, value); } }

		public readonly static DependencyProperty SweepDirectionProperty = DependencyProperty.Register("SweepDirection ",
			typeof(SweepDirection),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(SweepDirection.Clockwise, OnSensitivePropertyChanged));
		public SweepDirection SweepDirection
		{
			get { return (SweepDirection)GetValue(SweepDirectionProperty); }
			set { SetValue(SweepDirectionProperty, value); }
		}

		public readonly static DependencyProperty IsLargeArcProperty = DependencyProperty.Register("IsLargeArc",
			typeof(bool),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(false));
		public bool IsLargeArc
		{
			get { return (bool)GetValue(IsLargeArcProperty); }
			set { SetValue(IsLargeArcProperty, value); }
		}

		public readonly static DependencyProperty IsRestArcLargeProperty = DependencyProperty.Register("IsRestArcLarge",
			typeof(bool),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(true));
		public bool IsRestArcLarge
		{
			get { return (bool)GetValue(IsRestArcLargeProperty); }
			set { SetValue(IsRestArcLargeProperty, value); }
		}

		public readonly static DependencyProperty InitAngleOffsetProperty = DependencyProperty.Register("InitAngleOffset",
			typeof(double),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(0.0, OnSensitivePropertyChanged));
		public double InitAngleOffset { get { return (double)GetValue(InitAngleOffsetProperty); } set { SetValue(InitAngleOffsetProperty, value); } }

		public readonly static DependencyProperty ValueProperty = DependencyProperty.Register("Value",
			typeof(double),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(0.0, OnSensitivePropertyChanged));
		public double Value { get { return (double)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }

		public readonly static DependencyProperty TotalProperty = DependencyProperty.Register("Total",
			typeof(double),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(0.0, OnSensitivePropertyChanged));
		public double Total { get { return (double)GetValue(TotalProperty); } set { SetValue(TotalProperty, value); } }

		public readonly static DependencyPropertyKey CenterPropertyKey = DependencyProperty.RegisterReadOnly("Center",
			typeof(Point),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(new Point(0, 0)));
		public Point Center
		{
			get { return (Point)GetValue(CenterPropertyKey.DependencyProperty); }
			set { SetValue(CenterPropertyKey, value); }
		}

		public readonly static DependencyPropertyKey ArcStartPropertyKey = DependencyProperty.RegisterReadOnly("ArcStart",
			typeof(Point),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(new Point(0, 0)));
		public Point ArcStart
		{
			get { return (Point)GetValue(ArcStartPropertyKey.DependencyProperty); }
			set { SetValue(ArcStartPropertyKey, value); }
		}

		public readonly static DependencyPropertyKey ArcEndPropertyKey = DependencyProperty.RegisterReadOnly("ArcEnd",
			typeof(Point),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(new Point(0, 0)));
		public Point ArcEnd
		{
			get { return (Point)GetValue(ArcEndPropertyKey.DependencyProperty); }
			set { SetValue(ArcEndPropertyKey, value); }
		}

		public readonly static DependencyPropertyKey ArcSizePropertyKey = DependencyProperty.RegisterReadOnly("ArcSize",
			typeof(Size),
			typeof(WheelChart),
			new FrameworkPropertyMetadata(new Size(10, 10)));
		public Size ArcSize
		{
			get { return (Size)GetValue(ArcSizePropertyKey.DependencyProperty); }
			set { SetValue(ArcSizePropertyKey, value); }
		}

		public readonly static DependencyProperty SectorColorProperty = DependencyProperty.Register("SectorColor",
				typeof(Brush),
				typeof(WheelChart),
				new FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"))));
		public Brush SectorColor { get { return (Brush)GetValue(SectorColorProperty); } set { SetValue(SectorColorProperty, value); } }

		public readonly static DependencyProperty RestSectorColorProperty = DependencyProperty.Register("RestSectorColor",
				typeof(Brush),
				typeof(WheelChart),
				new FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("Green"))));
		public Brush RestSectorColor
		{
			get { return (Brush)GetValue(RestSectorColorProperty); }
			set { SetValue(RestSectorColorProperty, value); }
		}

		public readonly static DependencyProperty InnerCircleColorProperty = DependencyProperty.Register("InnerCircleColor",
				typeof(Brush),
				typeof(WheelChart),
				new FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"))));
		public Brush InnerCircleColor { get { return (Brush)GetValue(InnerCircleColorProperty); } set { SetValue(InnerCircleColorProperty, value); } }

		public readonly static DependencyProperty ContentProperty = DependencyProperty.Register("Content",
				typeof(string),
				typeof(WheelChart),
				new FrameworkPropertyMetadata("0.00%"));
		public string Content { get { return (string)GetValue(ContentProperty); } set { SetValue(ContentProperty, value); } }


		static WheelChart()
		{
			WidthProperty.OverrideMetadata(typeof(WheelChart), new FrameworkPropertyMetadata(40.0, (sender, e) =>
			{
				var obj = sender as WheelChart;
				if (obj.Width != double.NaN)
				{
					obj.Center = new Point(obj.Width / 2, obj.Width / 2);
					obj.Refresh();
				}
			}));
		}

		public WheelChart()
		{
			Radius = Width / 2;
			InnerDiameter = Radius / 2;
			Center = new Point(Radius, Radius);
			InitAngleOffset = 90;
			Total = 1;
			Refresh();
		}

		private Point GetArcStartPoint()
		{
			var point = GetPoint(InitAngleOffset);
			return new Point(Center.X - point.X, Center.Y - point.Y);
		}

		private Point GetArcEndPoint()
		{
			var angle = Total == 0 ? 0 : 360 * Value / Total;
			angle = SweepDirection == SweepDirection.Clockwise ? InitAngleOffset + angle : InitAngleOffset - angle;
			var point = GetPoint(angle);
			return new Point(Center.X - point.X, Center.Y - point.Y);
		}

		private Point GetPoint(double angle)
		{
			return new Point(Radius * Math.Cos(angle * Math.PI / 180), Radius * Math.Sin(angle * Math.PI / 180));
		}

		private static void OnSensitivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var chart = d as WheelChart;
			chart.Refresh();
		}

		private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var chart = d as WheelChart;
			chart.Refresh();
		}

		private static void OnTotalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var chart = d as WheelChart;
			chart.Refresh();
		}

		public void Refresh()
		{
			if (Value / Total > 0.5)
			{
				IsLargeArc = true;
				IsRestArcLarge = false;
			}
			ArcSize = new Size(Radius, Radius);
			ArcStart = GetArcStartPoint();
			ArcEnd = GetArcEndPoint();
			if (InnerDiameter >= 2 * Radius)
			{
				InnerDiameter = Radius / 2;
			}
			Content = (Total == 0 ? 0 : Value / Total).ToString("P2");
		}
	}
}
