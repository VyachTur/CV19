using System;
using System.Globalization;
using System.Linq;

namespace CV19.Infrastructure.Converters
{
	internal class ToListConverter : MultiConverter
	{
		public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => values.ToList();
	}
}