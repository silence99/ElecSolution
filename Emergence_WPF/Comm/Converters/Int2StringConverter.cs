using System;
using System.Globalization;
using System.Windows.Data;

namespace Emergence_WPF.Comm.Converters
{
	public class Int2StringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (int)value;
			return val == 0 ? "否" : "是";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = value.ToString();
			return val == "是" ? 1 : 0;
		}
	}
}
