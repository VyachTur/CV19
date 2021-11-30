using CV19.Web;
using System;
using System.IO;

namespace CV19Console
{
    internal static class WebServerTest
    {
        public static void Run()
        {
            var server = new WebServer(8080);
            server.RequestReceived += Server_RequestReceived;

            server.Start();

            Console.WriteLine("Сервер запущен!");
            Console.ReadLine();
        }

        private static void Server_RequestReceived(object sender, RequestReceiverEventArgs e)
        {
            var context = e.Context;

            Console.WriteLine("Connection {0}", context.Request.UserHostAddress);

            using var writer = new StreamWriter(context.Response.OutputStream);

            writer.WriteLine("Hello from Test Web Server!!!");
        }
    }
}
