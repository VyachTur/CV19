﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
	internal class Ratio : Converter
	{
		[ConstructorArgument("K")]
		public double K { get; set; } = 1;

		public Ratio() { }

		public Ratio(double k) => K = k;

		public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) return 0;

			var x = System.Convert.ToDouble(value, culture);

			return x * K;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) return 0;
			if (value is string str && string.IsNullOrEmpty(str))
			{
				return 0;
			}

			var x = System.Convert.ToDouble(value, culture);

			return x / K;
		}
	}
}
