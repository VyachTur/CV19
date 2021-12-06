using Microsoft.Extensions.DependencyInjection;

namespace CV19.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowVM => App.Host.Services.GetService<MainWindowViewModel>();

        public StudentsManagementWindowViewModel StudentsManagement => App.Host.Services.GetService<StudentsManagementWindowViewModel>();
    }
}
