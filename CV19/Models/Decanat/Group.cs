﻿using CV19.Models.Interfaces;
using System.Collections.Generic;

namespace CV19.Models.Decanat
{
	internal class Group : IEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public IList<Student> Students { get; set; } = new List<Student>();

		public string Description { get; set; }
	}
}
