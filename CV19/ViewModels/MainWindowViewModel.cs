using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using OxyPlot;
using OxyPlot.Series;
using CV19.Models;
using CV19.ViewModels.Base;


namespace CV19.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{

		#region Изменение вкладок по нажатию "вперед" и "назад"

		private int _selectedTabIndex;

		public int SelectedTabIndex
		{
			get => _selectedTabIndex;
			set => Set(ref _selectedTabIndex, value);
		}

		#endregion // Изменение вкладок по нажатию "вперед" и "назад"


		#region TestPlotModel : IEnumerable<DataPoint> - Тестовый набор данных для визуализации графиков

		//private IEnumerable<DataPoint> _testDataPoints;

		//public IEnumerable<DataPoint> TestDataPoints 
		//{ 
		//	get => _testDataPoints; 
		//	set => Set(ref _testDataPoints, value); 
		//}

		// рабочий вариант поля
		private PlotModel _testPlotModel;

		/// <summary>
		/// Рабочий вариант свойства
		/// </summary>
		public PlotModel TestPlotModel
		{
			get => _testPlotModel;
			set => Set(ref _testPlotModel, value);
		}

		#endregion // TestPlotModel

		#region Title

		private string _title = "Анализ статистики CV19";

		/// <summary>
		/// Заголовок окна
		/// </summary>
		public string Title
		{
			get => _title;

			//set
			//{
			//	if (Equals(_title, value)) return;
			//	_title = value;
			//	OnPropertyChanged();
			//}
			set => Set(ref _title, value);
		}

		#endregion // Title

		#region Status

		private string _status = "Готов!";

		/// <summary>
		/// Статус программы
		/// </summary>
		public string Status
		{
			get => _status;
			set => Set(ref _status, value);
		}

		#endregion // Status

		#region Commands

		#region CloseApplicationCommand

		public ICommand CloseApplicationCommand { get; }

		private void OnCloseApplicationCommandExecuted(object p)
		{
			Application.Current.Shutdown();
		}

		private bool CanCloseApplicationCommandExecute(object p) => true;

		#endregion // CloseApplicationCommand


		#region ArrowLeftRightCommand
		public ICommand ChangeTabIndexCommand { get; }

		private void OnChangeTabIndexCommandExecute(object p)
		{
			if (p is null) return;
			SelectedTabIndex += Convert.ToInt32(p);
		}

		private bool CanChangeTabIndexCommandExecuted(object p) => _selectedTabIndex >= 0;
		#endregion

		#endregion // Commands

		public MainWindowViewModel()
		{
			#region Commands init

			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

			ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecute, CanChangeTabIndexCommandExecuted);

			#endregion // Commands init


			var plotM = new PlotModel { Title = "Синусоида", Subtitle = "Проба OxyPlot" };

			var point_series = new LineSeries();

			//var data_points = new List<DataPoint>((int)(360 / 0.1));
			for (var x = 0d; x < 360; x += 0.1)
			{
				const double to_rad = Math.PI / 180;
				var y = Math.Sin(x * to_rad);

				point_series.Points.Add(new DataPoint(x, y));
			}

			plotM.Series.Add(point_series);

			TestPlotModel = plotM;
		}


	}
}
