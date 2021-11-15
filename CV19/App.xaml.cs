using CV19.Services;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Windows;

namespace CV19
{
	public partial class App : Application
	{
		public static bool IsDesignMode { get; private set; } = true;

		protected override void OnStartup(StartupEventArgs e)
		{
			IsDesignMode = false;
			base.OnStartup(e);

			//var service_test = new DataService();
			//var countries = service_test.GetData().ToArray();
		}

		public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
			services.AddSingleton<DataService>();
			services.AddSingleton<CountriesStatisticViewModel>();
        }
	}
}
