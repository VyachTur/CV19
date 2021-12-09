using CV19.Infrastructure.Commands;
using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class StudentsManagementWindowViewModel : ViewModel
    {
        private readonly StudentsManager _studentsManager;
        private readonly IUserDialogService _userDialog;


        #region Title
        private string _title = "Управление студентами";

            public string Title
            {
                get => _title;
                set => Set(ref _title, value);
            }
        #endregion // Title

        #region SelectedGroup
        private Group _selectedGroup;

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }
        #endregion // SelectedGroup

        #region SelectedStudent
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set => Set(ref _selectedStudent, value);
        }
        #endregion // SelectedStudent


        #region Commands

        #region EditStudentCommand

        private ICommand _editStudentCommand;

        /// <summary>
        /// Команда редактирования студента
        /// </summary>
        public ICommand EditStudentCommand => _editStudentCommand ??= new LambdaCommand(OnEditStudentCommandExecuted, CanEditStudentCommandExecute);

        private bool CanEditStudentCommandExecute(object p) => p is Student;

        private void OnEditStudentCommandExecuted(object p)
        {
            if (_userDialog.Edit(p))
            {
                _studentsManager.Update((Student)p);

                _userDialog.ShowInformation("Студент отредактирован", "Менеджер студентов");
            }
            else
            {
                _userDialog.ShowWarning("Отказ от редактирования", "Менеджер студентов");
            }
        }

        #endregion // EditStudentCommand


        #region CreateStudentCommand

        private ICommand _createStudentCommand;

        /// <summary>
        /// Создание нового студента
        /// </summary>
        public ICommand CreateStudentCommand => _createStudentCommand ??= new LambdaCommand(OnCreateStudentCommandExecuted, CanCreateStudentCommandExecute);

        private bool CanCreateStudentCommandExecute(object p) => p is Group;

        private void OnCreateStudentCommandExecuted(object p)
        {
            var group = (Group)p;

            var student = new Student();

            if (_userDialog.Edit(student) | _studentsManager.Create(student, group.Name))
            {
                OnPropertyChanged(nameof(Students));
                return;
            }

            if (_userDialog.Confirm("Не удалось создать студента. Повторить?", "Менеджер студентов"))
                OnCreateStudentCommandExecuted(p);
        }

        #endregion


        #endregion // Commands

        public IEnumerable<Student> Students => _studentsManager.Students;

        public IEnumerable<Group> Groups => _studentsManager.Groups;

        public StudentsManagementWindowViewModel(StudentsManager studentsManager, IUserDialogService userDialog) 
        { 
            _studentsManager = studentsManager;
            _userDialog = userDialog;
        }
    }
}
