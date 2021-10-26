using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{
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
		#endregion

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

		#endregion

		#region Commands

		#region CloseApplicationCommand
		public ICommand CloseApplicationCommand { get; }

		private void OnCloseApplicationCommandExecuted(object p)
		{
			Application.Current.Shutdown();
		}

		private bool CanCloseApplicationCommandExecute(object p) => true;
		#endregion // CloseApplicationCommand


		#endregion // Commands

		public MainWindowViewModel()
		{
			CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
		}
	}
}
