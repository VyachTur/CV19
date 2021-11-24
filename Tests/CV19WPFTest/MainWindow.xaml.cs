using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CV19WPFTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			new Thread(() =>
            {
				var value = LongProcess(DateTime.Now);
                if (txtProc.Dispatcher.CheckAccess())
                {
					txtProc.Text = value;
                }
				else
                {
                    txtProc.Dispatcher.Invoke(() => txtProc.Text = value);              // синхронный вызов
                    txtProc.Dispatcher.BeginInvoke(new Action(() => txtProc.Text = value)); // асинхронный вызов
                }
                
            }).Start();

			MessageBox.Show("Сообщение!");
		}

		private static string LongProcess(DateTime time)
        {
			Thread.Sleep(5000);

			return $"Result: {time}";
        }
    }
}
