using System;
using System.Globalization;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
	internal class CompositeConverter : Converter
	{
		public IValueConverter First { get; set; }

		public IValueConverter Second { get; set; }

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result1 = First?.Convert(value, targetType, parameter, culture) ?? value;
			var result2 = First?.Convert(result1, targetType, parameter, culture) ?? result1;

			return result2;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var result2 = Second?.ConvertBack(value, targetType, parameter, culture) ?? value;
			var result1 = First?.ConvertBack(result2, targetType, parameter, culture) ?? result2;

			return result1;
		}
	}
}
