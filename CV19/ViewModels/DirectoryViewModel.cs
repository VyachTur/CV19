using System;
using System.Collections.Generic;
using System.IO;

namespace CV19.ViewModels
{
	class DirectoryViewModel
	{
		private readonly DirectoryInfo _dirInfo;

		public DirectoryViewModel(string path) => _dirInfo = new DirectoryInfo(path);
	}

	class FileViewModel
	{
		private readonly FileInfo _fInfo;

		public FileViewModel(string path) => _fInfo = new FileInfo(path);
	}
}
