using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CV19.Infrastructure.Converters
{
    internal class ParametricMultiplyValueConverter : Freezable, IValueConverter
    {
        #region ValueProperty

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value),
                typeof(double),
                typeof(ParametricMultiplyValueConverter),
                new PropertyMetadata(1.0));

        [Description("Умножаемое значение")]
        public double Value { get => (double)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        #endregion
       

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToDouble(value, culture);

            return val * Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToDouble(value, culture);

            return val / Value;
        }

        protected override Freezable CreateInstanceCore()
        {
            return new ParametricMultiplyValueConverter { Value = Value };
        }
    }
}
