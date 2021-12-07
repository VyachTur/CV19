using CV19.Infrastructure.Commands.Base;
using CV19.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Infrastructure.Commands
{
    internal class ManageStudentsCommand : Command
    {
        private StudentsManagementWindow _window;

        public override bool CanExecute(object parameter) => _window is null;

        public override void Execute(object parameter)
        {
            var window = new StudentsManagementWindow
            {
                Owner = Application.Current.MainWindow
            };
            _window = window;
            window.Closed += OnWindow_Closed;

            window.ShowDialog();
        }

        private void OnWindow_Closed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= OnWindow_Closed;
            _window = null;
        }
    }
}
