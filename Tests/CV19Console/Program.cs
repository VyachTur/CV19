using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
//using System.Net;

namespace CV19Console
{
	class Program
	{
		private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

		private const string pattern = @",(?=\S)";

		private static async Task<Stream> GetDataStream()
		{
			var client = new HttpClient();
			var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
			return await response.Content.ReadAsStreamAsync();
		}


		private static IEnumerable<string> GetDataLines()
		{
			using var data_stream = GetDataStream().Result;
			using var data_reader = new StreamReader(data_stream);

			while(!data_reader.EndOfStream)
			{
				var line = data_reader.ReadLine();
				if (string.IsNullOrWhiteSpace(line)) continue;
				yield return line;
			}
		}

		//private IEnumerable<string[]> GetStrStr()
		//{
		//	const string pattern = @",\S";

		//	//var strstr = GetDataLines()
		//	//				.Select
		//	//return Regex.Split(GetDataLines(), pattern);
		//}

		//private static DateTime[] GetDates() => GetDataLines()
		//									.First()
		//									.Split(",")
		//									.Skip(4)
		//									.Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
		//									.ToArray();

		private static String[] ParseStringCSV(string input, string pattern) => Regex.Split(input, pattern);

		private static DateTime[] GetDates() => ParseStringCSV(GetDataLines().First().ToString(), pattern)
											.Skip(4)
											.Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
											.ToArray();


		private static IEnumerable<(string country, string province, int[] counts)> GetData()
		{
			var lines = GetDataLines()
						.Skip(1);

			foreach (var line in lines)
			{
				var row = ParseStringCSV(line, pattern);

				var province_name = row[0].Trim();
				var country_name = row[1].Trim(' ', '"');
				var counts_many = row.Skip(4).Select(int.Parse).ToArray();

				yield return (country_name, province_name, counts_many);
			}
		}



		//private static IEnumerable<(string country, string province, int[] counts)> GetData()
		//{
		//	var lines = GetDataLines()
		//				.Skip(1)
		//				.Select(line => line.Split(","));

		//	foreach (var row in lines)
		//	{
		//		var province_name = row[0].Trim();
		//		var country_name = row[1].Trim(' ', '"');
		//		var counts_many = row.Skip(4).Select(int.Parse).ToArray();


		//		//var counts_many = row.Skip(4).Where(s => int.TryParse(s, out int result)).Select(int.Parse).ToArray();

		//		yield return (country_name, province_name, counts_many);
		//	}
		//}


		static void Main(string[] args)
		{
			//var items = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

			//var last_item = items[^1];	// запись разрешенная в .Net Core
			//var prelast_item = items[^2];
			//Console.WriteLine($"{last_item} {prelast_item}");

			//WebClient web_client = new WebClient(); // устарел

			//var client = new HttpClient();

			//var response = client.GetAsync(data_url).Result;
			//var csv_str = response.Content.ReadAsStringAsync().Result;


			//foreach (var data_line in GetDataLines())
			//{
			//	Console.WriteLine(data_line);
			//}


			//DateTime[] dates = GetDates();

			//Console.WriteLine(string.Join("\r\n", dates));


			var russia_data = GetData()
								.First(v => v.country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

			Console.WriteLine(string.Join("\r\n", GetDates().Zip(russia_data.counts, (date, count) => $"{date:dd.MM.yyyy} - {count}")));
			//Console.WriteLine($"{russia_data.province} | {russia_data.country} | {string.Join(' ', russia_data.counts)}"); // мой вариант

		}
	}
}
