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
        private bool _enabled;

        public bool Enabled
        {
            get => _enabled;
            set => Set(ref _enabled, value);
        }
        #endregion

        #endregion //  Fields and Properties


        #region Commands


        #region StartCommand
        private ICommand _startCommand;

        public ICommand StartCommand => _startCommand ??= new LambdaCommand(OnStartCommandExecuted, CanStartCommandExecute);

        private bool CanStartCommandExecute(object p) => !_enabled;

        private void OnStartCommandExecuted(object p)
        {
            Enabled = true;
        }
        #endregion


        #region StopCommand
        private ICommand _stopCommand;

        public ICommand StopCommand => _stopCommand ??= new LambdaCommand(OnStopCommandExecuted, CanStopCommandExecute);

        private bool CanStopCommandExecute(object p) => _enabled;

        private void OnStopCommandExecuted(object p)
        {
            Enabled = false;
        }
        #endregion

        #endregion // Commands



        public WebServerViewModel(IWebServerService Server)
        {
            _server = Server;
        }

    }
}
