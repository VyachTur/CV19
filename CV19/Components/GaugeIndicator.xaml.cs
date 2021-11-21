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

namespace CV19.Components
{
    /// <summary>
    /// Interaction logic for GaugeIndicator.xaml
    /// </summary>
    public partial class GaugeIndicator : UserControl
    {

        #region DependencyProperty

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(GaugeIndicator),
                new PropertyMetadata(
                    default(double),
                    OnValuePropertyChanged,
                    OnCoerceValue),
                OnValidateValue);

        private static bool OnValidateValue(object value)
        {
            return true;
        }

        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            double value = (double)baseValue;

            //return Math.Max(0, Math.Min(100, value));

            if (value > 100) return 100.0;
            else if(value < 0) return 0.0;
            return value;
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        #endregion // DependencyProperty

        public GaugeIndicator()
        {
            InitializeComponent();
        }


    }
}
