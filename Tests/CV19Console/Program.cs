using System;
using System.Net.Http;
//using System.Net;

namespace CV19Console
{
	class Program
	{
		private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

		static void Main(string[] args)
		{
			//var items = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			//var last_item = items[^1];	// запись разрешенная в .Net Core
			//var prelast_item = items[^2];
			//Console.WriteLine($"{last_item} {prelast_item}");

			//WebClient web_client = new WebClient(); // устарел

			var client = new HttpClient();

			var response = client.GetAsync(data_url).Result;
			var csv_str = response.Content.ReadAsStringAsync().Result;

			Console.ReadKey();
		}
	}
}
