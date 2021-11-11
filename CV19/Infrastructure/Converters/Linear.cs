using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace CV19.Infrastructure.Converters
{
	[ValueConversion(typeof(double), typeof(double))]
	/// <summary>
	/// Реализация линейного преобразования f(x) = k*x + b
	/// </summary>
	internal class Linear : Converter
	{
		//[ConstructorArgument("K")]
		public double K { get; set; } = 1;

		//[ConstructorArgument("B")]
		public double B { get; set; }


        public Linear() { }

        public Linear(double k) => K = k;

        public Linear(double k, double b) : this(k) => B = b;


        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) return null;

			var x = System.Convert.ToDouble(value, culture);
			return K * x + B;
		}

		public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is null) return null;

			var y = System.Convert.ToDouble(value, culture);

			return (y - B) / K;
		}
	}
}
