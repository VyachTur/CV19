using CV19.Services.Interfaces;
using CV19.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV19.Services
{
    internal class HttpListenerWebServer : IWebServerService
    {
        private WebServer _server = new(8080);

        public bool Enabled { get => _server.Enabled; set => _server.Enabled = value; }

        public void Start() => _server.Start();

        public void Stop() => _server.Stop();

        public HttpListenerWebServer()
        {
            _server.RequestReceived += OnRequestReceived;
        }

        private static void OnRequestReceived(object sender, RequestReceiverEventArgs e)
        {
            using StreamWriter writer = new(e.Context.Response.OutputStream);
            writer.WriteLine($"CV-19 Application ({DateTime.Now})");
        }
    }
}
