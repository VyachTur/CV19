using CV19.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CV19.ViewModels
{
	class DirectoryViewModel : ViewModel
	{
		private readonly DirectoryInfo _dirInfo;

		public IEnumerable<DirectoryViewModel> SubDirectories => _dirInfo
										.EnumerateDirectories()
										.Select(dir_info => new DirectoryViewModel(dir_info.FullName));

		public IEnumerable<FileViewModel> Files => _dirInfo
										.EnumerateFiles()
										.Select(file_info => new FileViewModel(file_info.FullName));

		//public IEnumerable<object> DirectoryItems => SubDirectories.Cast<object>().Concat(Files);
		public IEnumerable<object> DirectoryItems => ((IEnumerable<object>)SubDirectories).Concat(Files);

		public string Name => _dirInfo.Name;

		public string Path => _dirInfo.FullName;

		public DateTime CreationTime => _dirInfo.CreationTime;


		public DirectoryViewModel(string path) => _dirInfo = new DirectoryInfo(path);
	}

	class FileViewModel : ViewModel
	{
		private readonly FileInfo _fInfo;

		public string Name => _fInfo.Name;

		public string Path => _fInfo.FullName;

		public DateTime CreationTime => _fInfo.CreationTime;


		public FileViewModel(string path) => _fInfo = new FileInfo(path);
	}
}
