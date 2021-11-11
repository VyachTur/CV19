using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CV19.ViewModels
{
	internal class CountriesStatisticViewModel : ViewModel
	{
		#region Fields

		private DataService _dataService;

		private IEnumerable<CountryInfo> _countries;

		private CountryInfo _selectedCountry;

		#endregion // Fields

		#region Properties

		private MainWindowViewModel MainVM { get; }

		public IEnumerable<CountryInfo> Countries
		{
			get => _countries;
			private set => Set(ref _countries, value);
		}

		public CountryInfo SelectedCountry
		{
			get => _selectedCountry;
			set
			{
				Set(ref _selectedCountry, value);

				var plotCV19 = new PlotModel { Title = "Статистика Covid-19", Subtitle = "Данные" };

				var points_series = new LineSeries();

				foreach (var point in SelectedCountry.Counts.ToArray())
				{
					var x = point.Date;
					var y = point.Count;

					points_series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(x), y));
				}

				plotCV19.Series.Add(points_series);

				PlotModelCV19 = plotCV19;
			}
		}


		#region PlotModel CV19

		private PlotModel _plotModelCV19;

		public PlotModel PlotModelCV19
		{
			get => _plotModelCV19;
			set => Set(ref _plotModelCV19, value);
		}

		#endregion // PlotModel CV19


		#endregion // Properties

		#region Commands

		public ICommand RefreshDataCommand { get; }

		private void OnRefreshDataCommand(object p)
		{
			Countries = _dataService.GetData();
		}

		#endregion // Commands


		/// <summary>
		/// Отладочный конструктор для дизайнера xaml
		/// </summary>
		public CountriesStatisticViewModel() : this(null) 
		{
			if (!App.IsDesignMode)
				throw new InvalidOperationException("Конструктор без параметров предназначен для отладочного режима");

			_countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
			{
				Name = $"Country {i}",
				ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
				{
					Name = $"Province {i}",
					Location = new Point(i,j),
					Counts = Enumerable.Range(1,10).Select(k => new ConfirmedCount
					{
						Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
						Count = k
					}).ToArray()
				}).ToArray()
			}).ToArray();
		}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="viewModel"></param>
		public CountriesStatisticViewModel(MainWindowViewModel viewModel)
		{
			MainVM = viewModel;

			_dataService = new DataService();

			RefreshDataCommand = new LambdaCommand(OnRefreshDataCommand);



			#region Создание графика статистики Ковид


			////#region Создание графика синусоиды (наполнение данными)

			////var plotM = new PlotModel { Title = "Синусоида", Subtitle = "Проба OxyPlot" };

			////var point_series = new LineSeries();

			//////var data_points = new List<DataPoint>((int)(360 / 0.1));
			////for (var x = 0d; x < 360; x += 0.1)
			////{
			////	const double to_rad = Math.PI / 180;
			////	var y = Math.Sin(x * to_rad);

			////	point_series.Points.Add(new DataPoint(x, y));
			////}

			////plotM.Series.Add(point_series);

			////TestPlotModel = plotM;

			////#endregion // Создание графика синусоиды (наполнение данными)



			#endregion // Создание графика статистики Ковид

		}
	}
}
