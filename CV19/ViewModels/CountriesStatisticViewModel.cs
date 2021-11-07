using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
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
			set => Set(ref _selectedCountry, value);
		}

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

		}
	}
}
