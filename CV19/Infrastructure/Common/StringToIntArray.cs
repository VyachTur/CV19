using System;
using System.Collections.Generic;
using System.Windows.Markup;

namespace CV19.Infrastructure.Common
{
	internal class StringToIntArray
	{
		[ConstructorArgument("Str")]
		public string Str { get; set; }

		public StringToIntArray() { }

		public StringToIntArray(string str) => Str = str;
	}
}
