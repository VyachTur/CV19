using CV19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Services
{
	internal class DataService
	{
		private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

		private const string pattern = @",(?=\S)";

		private static async Task<Stream> GetDataStream()
		{
			var client = new HttpClient();
			var response = await client.GetAsync(
				_DataSourceAddress, 
				HttpCompletionOption.ResponseHeadersRead);
			return await response.Content.ReadAsStreamAsync();
		}


		private static IEnumerable<string> GetDataLines()
		{
			using var data_stream = GetDataStream().Result;
			using var data_reader = new StreamReader(data_stream);

			while (!data_reader.EndOfStream)
			{
				var line = data_reader.ReadLine();
				if (string.IsNullOrWhiteSpace(line)) continue;
				yield return line;
			}
		}

		private static String[] ParseStringCSV(string input, string pattern) => Regex.Split(input, pattern);

		private static DateTime[] GetDates() => ParseStringCSV(GetDataLines().First().ToString(), pattern)
											.Skip(4)
											.Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
											.ToArray();


		private static IEnumerable<(string province, string country, (double lat, double lon) place, int[] counts)> GetCountriesData()
		{
			var lines = GetDataLines()
						.Skip(1);

			foreach (var line in lines)
			{
				var row = ParseStringCSV(line, pattern);

				var province_name = row[0].Trim();
				var country_name = row[1].Trim(' ', '"');
				var latitude = double.Parse(row[2]);
				var longitude = double.Parse(row[3]);
				var other_values = row.Skip(4).Select(int.Parse).ToArray();

				yield return (province_name, country_name, (latitude, longitude), other_values);
			}
		}

		public IEnumerable<CountryInfo> GetData()
		{
			var dates = GetDates();

			var data = GetCountriesData().GroupBy(d => d.country);

			foreach (var country_info in data)
			{
				var country = new CountryInfo
				{
					Name = country_info.Key,
					ProvinceCounts = country_info.Select(c => new PlaceInfo
					{
						Name = c.province,
						Location = new Point(c.place.lat, c.place.lon),
						Counts = dates.Zip(c.counts, (date, count) => new ConfirmedCount { Date = date, Count = count })
					})
				};

				yield return country;
			}
		}
	}
}
