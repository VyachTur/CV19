using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModels
{
	internal class CountriesStatisticViewModel : ViewModel
	{
		private readonly MainWindowViewModel _mainViewModel;

		public CountriesStatisticViewModel(MainWindowViewModel viewModel)
		{
			_mainViewModel = viewModel;
		}
	}
}
