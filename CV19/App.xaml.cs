using CV19.Services;
using CV19.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CV19
{
	public partial class App : Application
	{
		public static bool IsDesignMode { get; private set; } = true;

		private static IHost _host;

		public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

		protected override async void OnStartup(StartupEventArgs e)
		{
			IsDesignMode = false;
			var host = Host;
			base.OnStartup(e);

			await host.StartAsync().ConfigureAwait(false);
			host.Dispose();
			_host = null;
		}

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
			services.AddSingleton<DataService>();
			services.AddSingleton<CountriesStatisticViewModel>();
        }

		public static string CurrentDirectory => IsDesignMode
			? Path.GetDirectoryName(GetSourceCodePath())
			: Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath] string Path = null) => Path;
    }
}
