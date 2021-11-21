using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CV19.ViewModels
{
	internal class CountriesStatisticViewModel : ViewModel
	{
		#region Fields

		private IDataService _dataService;

		private IEnumerable<CountryInfo> _countries;

		private CountryInfo _selectedCountry;

		#endregion // Fields

		#region Properties

		public MainWindowViewModel MainVM { get; internal set; }

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

				MakePlotStatCV19();
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
		//public CountriesStatisticViewModel() : this(null) 
		//{
		//	if (!App.IsDesignMode)
		//		throw new InvalidOperationException("Конструктор без параметров предназначен для отладочного режима");

		//	_countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
		//	{
		//		Name = $"Country {i}",
		//		ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
		//		{
		//			Name = $"Province {i}",
		//			Location = new Point(i,j),
		//			Counts = Enumerable.Range(1,10).Select(k => new ConfirmedCount
		//			{
		//				Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
		//				Count = k
		//			}).ToArray()
		//		}).ToArray()
		//	}).ToArray();
		//}

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="viewModel"></param>
		public CountriesStatisticViewModel(IDataService dataService)
		{
			_dataService = dataService;

			RefreshDataCommand = new LambdaCommand(OnRefreshDataCommand);
		}


		#region Создание графика статистики Ковид

		private void MakePlotStatCV19()
        {
			if (SelectedCountry != null)
            {
				var plotCV19 = new PlotModel 
								{ 
									Title = "Статистика Covid-19", 
									TitleFontSize = 22,
									Subtitle = SelectedCountry.Name,
									SubtitleFontSize = 18,
									SubtitleFontWeight = OxyPlot.FontWeights.Bold,
									PlotType = PlotType.XY
								};

				var x_axis = new DateTimeAxis
				{
					Position = AxisPosition.Bottom,
					Title = "Дата",
					AxisTitleDistance = 6,
					TitleFontWeight = OxyPlot.FontWeights.Bold,
					StringFormat = "dd.MM.yyyy",
					MajorGridlineStyle = LineStyle.Solid,
					MinorGridlineStyle = LineStyle.Dot
				};

				plotCV19.Axes.Add(x_axis);

				var y_axis = new LinearAxis
				{
					Position = AxisPosition.Left,
					Title = "Количество заболевших",
					AxisTitleDistance = 6,
					TitleFontWeight = OxyPlot.FontWeights.Bold,
					MajorGridlineStyle = LineStyle.Solid,
					MinorGridlineStyle = LineStyle.Dot
				};

				plotCV19.Axes.Add(y_axis);

				var points_series = new LineSeries
				{
					Color = OxyColor.FromRgb(255, 0, 0),
					StrokeThickness = 2
				};

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

		#endregion // Создание графика статистики Ковид

	}
}
