﻿using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.ViewModels
{
    internal class StudentsManagementWindowViewModel : ViewModel
    {
        private string _title = "Управление студентами";

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
