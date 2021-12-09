using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CV19.Views.Windows
{
    /// <summary>
    /// Interaction logic for StudentsEditorWindow.xaml
    /// </summary>
    public partial class StudentEditorWindow
    {
        #region DependencyProperties

        #region FirstName
        public static DependencyProperty FirstNameProperty = DependencyProperty.Register(
            nameof(FirstName),
            typeof(string),
            typeof(StudentEditorWindow),
            new PropertyMetadata(null));

        [Description("Имя")]
        public string FirstName { get => (string)GetValue(FirstNameProperty); set => SetValue(FirstNameProperty, value); }
        #endregion // FirstName

        #region LastName
        public static DependencyProperty LastNameProperty = DependencyProperty.Register(
            nameof(LastName),
            typeof(string),
            typeof(StudentEditorWindow),
            new PropertyMetadata(null));

        [Description("Фамилия")]
        public string LastName { get => (string)GetValue(LastNameProperty); set => SetValue(LastNameProperty, value); }
        #endregion // LastName

        #region Patronymic
        public static DependencyProperty PatronymicProperty = DependencyProperty.Register(
            nameof(Patronymic),
            typeof(string),
            typeof(StudentEditorWindow),
            new PropertyMetadata(null));

        [Description("Отчество")]
        public string Patronymic { get => (string)GetValue(PatronymicProperty); set => SetValue(PatronymicProperty, value); }
        #endregion // Patronymic

        #region Rating
        public static DependencyProperty RatingProperty = DependencyProperty.Register(
            nameof(Rating),
            typeof(double),
            typeof(StudentEditorWindow),
            new PropertyMetadata(default(double)));

        [Description("Оценка")]
        public double Rating { get => (double)GetValue(RatingProperty); set => SetValue(RatingProperty, value); }
        #endregion // Rating

        #region Birthday
        public static DependencyProperty BirthdayProperty = DependencyProperty.Register(
            nameof(Birthday),
            typeof(DateTime),
            typeof(StudentEditorWindow),
            new PropertyMetadata(default(DateTime)));

        [Description("Дата рождения")]
        public DateTime Birthday { get => (DateTime)GetValue(BirthdayProperty); set => SetValue(BirthdayProperty, value); }
        #endregion // Rating

        #endregion


        public StudentEditorWindow() => InitializeComponent();
    }
}
