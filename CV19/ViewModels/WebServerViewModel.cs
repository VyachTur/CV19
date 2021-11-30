using CV19.Infrastructure.Commands;
using CV19.Services.Interfaces;
using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV19.ViewModels
{
    internal class WebServerViewModel : ViewModel
    {
        #region Fields and Properties

        private readonly IWebServerService _server;

        #region Enabled

        public bool Enabled
        {
            get => _server.Enabled;
            set
            {
                _server.Enabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #endregion //  Fields and Properties


        #region Commands


        #region StartCommand
        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object p) => !Enabled;

        private void OnStartCommandExecuted(object p)
        {
            _server?.Start();
            OnPropertyChanged(nameof(Enabled));
        }
        #endregion


        #region StopCommand
        private ICommand _stopCommand;

        public ICommand StopCommand => _stopCommand ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object p) => Enabled;

        private void OnStopCommandExecuted(object p)
        {
            _server!.Stop();
            OnPropertyChanged();
        }
        #endregion

        #endregion // Commands



        public WebServerViewModel(IWebServerService Server)
        {
            _server = Server;
        }

    }
}
