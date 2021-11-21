using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using OxyPlot;
using OxyPlot.Series;
using CV19.ViewModels.Base;
using System.Collections.ObjectModel;
using CV19.Models.Decanat;
using System.Linq;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Markup;

namespace CV19.ViewModels
{
	[MarkupExtensionReturnType(typeof(MainWindowViewModel))]
	internal class MainWindowViewModel : ViewModel
	{
		#region Fields and Properties

		public CountriesStatisticViewModel CountriesStatisticVM { get; }



		#region DependencyProperty FuelControl

        private double _fuelControl;

        public double FuelControl
        {
            get => _fuelControl;
            set => Set(ref _fuelControl, value);
        }


        #endregion


        #region Students, CompositeCollection, FileSystem

        public ObservableCollection<Group> Groups { get; }  // Группы студентов

			public object[] CompositeCollection { get; }    // Массив разнородных типов

			public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("c:\\");

		#endregion


		#region SlectedDirectory - выбранная директория

		private DirectoryViewModel _selectedDirectory;

		public DirectoryViewModel SelectedDirectory
		{
			get { return _selectedDirectory; }
			set { Set(ref _selectedDirectory, value); }
		}


		#endregion


		#region SelectedGroupStudents - добавление фильтра по таблице со студентами

		private string _studentFilterText;

		public string StudentFilterText
		{
			get => _studentFilterText;
			set
			{
				if (!Set(ref _studentFilterText, value)) return;
				_selectedGroupStudents.View.Refresh();
			}
				
		}

		//
		private readonly CollectionViewSource _selectedGroupStudents = new CollectionViewSource();

		public ICollectionView SelectedGroupStudents => _selectedGroupStudents?.View;
		//

		private void OnStudentFiltered(object sender, FilterEventArgs e)
		{
			if (!(e.Item is Student student))
			{
				e.Accepted = false;
				return;
			}

			var filter_text = _studentFilterText;
			if (string.IsNullOrWhiteSpace(filter_text)) return;

			if (student.Name is null || student.Surname is null || student.Patronimic is null)
			{
				e.Accepted = false;
				return;
			}

			if (student.Name.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
			if (student.Surname.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;
			if (student.Patronimic.Contains(filter_text, StringComparison.OrdinalIgnoreCase)) return;

			e.Accepted = false;
		}

		#endregion // SelectedGroupStudents - добавление фильтра по таблице со студентами


		#region Тестирование большого списка визуальных элементов (расход памяти, виртуализованная панель)
		public IEnumerable<Student> LstTestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 : 100_000)
								.Select(i => new Student
								{
									Name = $"Имя {i}",
									Surname = $"Фамилия {i}"
								});
		#endregion

		#region SelectedGroup для работы привязки Групп и Студентов
		private Group _selectedGroup;

		public Group SelectedGroup
		{
			get => _selectedGroup;
			set
			{
				if (!Set(ref _selectedGroup, value)) return;

				_selectedGroupStudents.Source = value?.Students;
				OnPropertyChanged(nameof(SelectedGroupStudents));
			}
		}
		#endregion

		#region SelectedCompositeValue, object - выбранный непонятный элемент	
		private object _selectedCompositeValue;

		public object SelectedCompositeValue
		{
			get => _selectedCompositeValue;
			set => Set(ref _selectedCompositeValue, value);
		}
		#endregion

		#region Изменение вкладок по нажатию "вперед" и "назад"

		private int _selectedTabIndex;

		public int SelectedTabIndex
		{
			get => _selectedTabIndex;
			set => Set(ref _selectedTabIndex, value);
		}

        #endregion // Изменение вкладок по нажатию "вперед" и "назад"

        #region TestPlotModel SIN

        private PlotModel _testPlotModel;

		public PlotModel TestPlotModel
		{
			get => _testPlotModel;
			set => Set(ref _testPlotModel, value);
		}

		#endregion // TestPlotModel SIN

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

		#endregion // Fields and Properties


		#region Создание комманд (тело команд)

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


		#region Создание и Удаление групп со студентами (бизнес-логика)

		#region Создание
		public ICommand CreateGroupCommand { get; }

		private bool CanCreateGroupCommandExecute(object p) => true;

		private void OnCreateGroupCommandExecute(object p)
		{
			var new_group = new Group
			{
				Name = $"Группа {Groups.Count + 1}",
				Students = new ObservableCollection<Student>()
			};

			Groups.Add(new_group);
		}
		#endregion // Создание

		#region Удаление
		public ICommand DeleteGroupCommand { get; }

		private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

		private void OnDeleteGroupCommandExecute(object p)
		{
			if (!(p is Group group)) return;
			int group_index = Groups.IndexOf(group);
			Groups.Remove(group);

			// Индекс выбранной группы
			if (Groups.Count > 0)
			{
				SelectedGroup = group_index < Groups.Count ? Groups[group_index] : SelectedGroup = Groups[group_index - 1];
			}
		}
		#endregion // Удаление

		#endregion // Создание и Удаление групп со студентами (две команды)

		#endregion // Commands


		/// <summary>
		/// Конструктор класса
		/// </summary>
		public MainWindowViewModel(CountriesStatisticViewModel Statistic)
		{
			CountriesStatisticVM = Statistic;
			Statistic.MainVM = this;

			//CountriesStatisticVM = new CountriesStatisticViewModel(this);





			#region ИНИЦИАЛИЗАЦИЯ КОММАНД!

			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);  // закрытие приложения
			ChangeTabIndexCommand = new LambdaCommand(OnChangeTabIndexCommandExecute, CanChangeTabIndexCommandExecuted);        // изменение индекса вкладки
			CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecute, CanCreateGroupCommandExecute);                  // создание группы со студентами
			DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecute, CanDeleteGroupCommandExecute);                  // удаление группы со студентами

            #endregion // ИНИЦИАЛИЗАЦИЯ КОММАНД!


            #region Создание графика синусоиды (наполнение данными)

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

			#endregion // Создание графика синусоиды (наполнение данными)

			#region Создание групп студентов (стили)

			var student_index = 1;

			var students = Enumerable.Range(1, 10).Select(i => new Student
			{
				Name = $"Name {student_index}",
				Surname = $"Surname {student_index}",
				Patronimic = $"Patronimic {student_index++}",
				Birthday = DateTime.Now,
				Rating = 0
			});

			var groups = Enumerable.Range(1, 20).Select(i => new Group
			{
				Name = $"Группа {i}",
				Students = new ObservableCollection<Student>(students)
			});

			Groups = new ObservableCollection<Group>(groups);

			SelectedGroup = Groups[0];	// выбор при загрузке первой группы в коллекции

			#endregion

			#region Коллекция разнородных данных
			var list_objects = new List<object>();

			list_objects.Add("Hello World!");
			list_objects.Add(42);
			list_objects.Add(Groups[2]);
			list_objects.Add(Groups[2].Students[0]);

			CompositeCollection = list_objects.ToArray();
			#endregion

			#region FilterStudents

				_selectedGroupStudents.Filter += OnStudentFiltered;

				//_selectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
				//_selectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));

			#endregion

		}
		
	}
}
