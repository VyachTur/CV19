using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Services;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV19.ViewModels
{
	internal class CountriesStatisticViewModel : ViewModel
	{
		#region Fields

		private DataService _dataService;

		private IEnumerable<CountryInfo> _countries;

		#endregion // Fields

		#region Properties

		private MainWindowViewModel MainVM { get; }

		public IEnumerable<CountryInfo> Countries
		{
			get => _countries;
			private set => Set(ref _countries, value);
		}

		#endregion // Properties

		#region Commands

		public ICommand RefreshDataCommand { get; }

		private void OnRefreshDataCommand(object p)
		{
			Countries = _dataService.GetData();
		}

		#endregion // Commands


		public CountriesStatisticViewModel(MainWindowViewModel viewModel)
		{
			MainVM = viewModel;

			_dataService = new DataService();

			RefreshDataCommand = new LambdaCommand(OnRefreshDataCommand);

		}
	}
}
