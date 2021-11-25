using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
//using System.Net;

namespace CV19Console
{
	class Program
	{

		static void Main(string[] args)
		{
			Thread.CurrentThread.Name = "Main thread";

			var thread = new Thread(ThreadMethod);
			thread.Name = "Other thread";
			thread.IsBackground = true;
			thread.Priority = ThreadPriority.AboveNormal;

			thread.Start(42);

			var count = 5;
			var msg = "Hello World!";
			var timeout = 150;

			new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();


            ThreadCheck();

			for (int i = 0; i < 5; i++)
            {
				Thread.Sleep(100);
                Console.WriteLine(i);
            }

			Console.ReadLine();
		}

		private static void PrintMethod(string message, int count, int timeout)
        {
			for (int i = 0; i < count; i++)
            {
				Console.WriteLine(message);
				Thread.Sleep(timeout);
            }
        }

		private static void ThreadMethod(object parameter)
        {
			var value = (int)parameter;
            Console.WriteLine(value);

			ThreadCheck();

			while(true)
            {
				Thread.Sleep(100);
				Console.Title = DateTime.Now.ToString();
            }
        }

		private static void ThreadCheck()
        {


			var thread = Thread.CurrentThread;
            Console.WriteLine("{0}:{1}", thread.ManagedThreadId, thread.Name);
        }
	}
}
