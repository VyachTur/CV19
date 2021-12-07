﻿using CV19.Models.Decanat;
using CV19.Services.Students;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModels
{
    internal class StudentsManagementWindowViewModel : ViewModel
    {
        private readonly StudentsManager _studentsManager;


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

        public IEnumerable<Student> Students => _studentsManager.Students;

        public IEnumerable<Group> Groups => _studentsManager.Groups;

        public StudentsManagementWindowViewModel(StudentsManager studentsManager) { _studentsManager = studentsManager; }
    }
}
