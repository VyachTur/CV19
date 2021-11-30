using System;
using System.Net;
using System.Net.Sockets;

namespace CV19.Web
{
    public class WebServer
    {
        public event EventHandler<RequestReceiverEventArgs> RequestReceived;

        //private TcpListener _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));

        private HttpListener _listener;
        private readonly int _port;
        private bool _enabled;
        private readonly object _syncRoot = new object();

        public int Port => _port;

        public bool Enabled
        {
            get => _enabled;
            set { if (value) Start(); else Stop(); }
        }


        public WebServer(int port) => _port = port;


        public void Start()
        {
            if (_enabled) return;   // первая проверка не запущен ли сервер (чтобы отсечь лишнюю дорогую операцию lock)

            lock (_syncRoot)
            {
                if (_enabled) return;   // вторая проверка, для одновременных обращений из разных потоков (когда первый поток поменяет значение _enabled
                                        // внутри критической секции, следующий за ним поток уже не будет создавать HttpListener

                _listener = new HttpListener();
                _listener.Prefixes.Add($"http://*:{Port}/");    // прописать в консоле под админом: netsh http add urlacl url=http://*:8080/ user=tampl
                _listener.Prefixes.Add($"http://+:{Port}/");
                _enabled = true;
                ListenAsync();
            }
        }

        public void Stop()
        {
            if (!_enabled) return;

            lock (_syncRoot)
            {
                if (!_enabled) return;

                _listener = null!;
                _enabled = false;
            }

        }

        private async void ListenAsync()
        {
            var listener = _listener;

            listener.Start();

            while (_enabled)
            {
                var context = await listener.GetContextAsync().ConfigureAwait(false);
                ProcessRequest(context);

            }

            listener.Stop();
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            RequestReceived?.Invoke(this, new RequestReceiverEventArgs(context));
        }

    }

    public class RequestReceiverEventArgs : EventArgs
    {
        public HttpListenerContext Context { get; }

        public RequestReceiverEventArgs(HttpListenerContext context)
        {
            Context = context;
        }
    }
}