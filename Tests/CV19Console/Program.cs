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
		private static bool _threadUpdate = true;

		static void Main(string[] args)
		{
			//Thread.CurrentThread.Name = "Main thread";

			//var clock_thread = new Thread(ThreadMethod);
			//clock_thread.Name = "Other thread";
			//clock_thread.IsBackground = true;
			//clock_thread.Priority = ThreadPriority.AboveNormal;

			//clock_thread.Start(42);

			//var count = 5;
			//var msg = "Hello World!";
			//var timeout = 150;

			//new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();


			//         ThreadCheck();

			//for (int i = 0; i < 5; i++)
			//         {
			//	Thread.Sleep(100);
			//             Console.WriteLine(i);
			//         }


			//var values = new List<int>();
			//var threads = new Thread[10];

			//var lock_object = new object();

			//for (int i = 0; i < threads.Length; i++)
   //         {
			//	threads[i] = new Thread(() =>
			//	{
			//		for (int j = 0; j < 10; j++)
			//			lock(lock_object)
			//				values.Add(Thread.CurrentThread.ManagedThreadId);
			//	});
   //         }

   //         foreach (var thread in threads)
   //         {
			//	thread.Start();
   //         }


			//Console.ReadLine();

   //         Console.WriteLine(String.Join(",", values));



			ManualResetEvent manualResetEvent = new ManualResetEvent(false);	// объект блокировки
			AutoResetEvent autoResetEvent = new AutoResetEvent(false);

			EventWaitHandle threadGuidance = manualResetEvent;

			var testThreads = new Thread[10];
			for (int i = 0; i < testThreads.Length; i++)
            {
				var local_i = i;
				testThreads[i] = new Thread(() =>
				{
					Console.WriteLine("Поток id: {0} - стартовал", Thread.CurrentThread.ManagedThreadId);

					threadGuidance.WaitOne();	// объект блокировки потока

                    Console.WriteLine($"Value: {local_i}");
                    Console.WriteLine("Поток id: {0} - завершился", Thread.CurrentThread.ManagedThreadId);
				});
				testThreads[i].Start();
            }

            Console.WriteLine("Главный поток готов к запуску потоков!");
			Console.ReadLine();

			threadGuidance.Set();	// установка блокировки
			threadGuidance.Reset();	// сброс блокировки


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

			while(_threadUpdate)
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
