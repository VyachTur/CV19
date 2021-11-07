﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models
{
	internal class CountryInfo : PlaceInfo
	{
		private Point? _location;

		public override Point Location 
		{ 
			get
			{
				if (_location != null)
					return (Point)_location;

				if (ProvinceCounts is null) return default;

				var average_x = ProvinceCounts.Average(p => p.Location.X);
				var average_y = ProvinceCounts.Average(p => p.Location.Y);

				return (Point)(_location = new Point(average_x, average_y));
			}

			set => _location = value;
		}

		public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
	}
}
