using CV19.Models.Decanat;
using CV19.Services.Interfaces;
using CV19.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Services
{
    internal class WindowsUserDialogService : IUserDialogService
    {
        public bool Confirm(string Message, string Caption, bool Exclamation = false) =>
            MessageBox.Show(
                Message,
                Caption,
                MessageBoxButton.YesNo,
                Exclamation ? MessageBoxImage.Exclamation : MessageBoxImage.Question)
            == MessageBoxResult.Yes;

        public bool Edit(object item) 
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            switch (item)
            {
                case Student student:
                    return EditStudent(student);

                default:
                    throw new NotSupportedException($"Редактирование объекта типа {item.GetType().Name} не поддерживается");
            }
        }

        public void ShowError(string Message, string Caption) => MessageBox.Show(Message, Caption, MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowInformation(string Information, string Caption) => MessageBox.Show(Information, Caption, MessageBoxButton.OK, MessageBoxImage.Information);

        public void ShowWarning(string Message, string Caption) => MessageBox.Show(Message, Caption, MessageBoxButton.OK, MessageBoxImage.Warning);


        private static bool EditStudent(Student student)
        {
            var dlg = new StudentEditorWindow
            {
                FirstName = student.Name,
                LastName = student.Surname,
                Patronymic = student.Patronymic,
                Birthday = student.Birthday,
                Rating = student.Rating
            };

            if (dlg.ShowDialog() != true) return false;

            student.Name = dlg.FirstName;
            student.Surname = dlg.LastName;
            student.Patronymic = dlg.Patronymic;
            student.Birthday = dlg.Birthday;
            student.Rating = dlg.Rating;

            return true;
        }
    }
}
