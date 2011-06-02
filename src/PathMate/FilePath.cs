using System;
using System.IO;

namespace PathMate
{
	public class FilePath
	{
		public FilePath(string path)
		{
			Path = path;
		}

		public bool IsRelative
		{
			get { return IsRelativePath(this); }
		}

		public string Path { get; private set; }

		public FilePath RelativeTo(DirectoryPath directoryPath)
		{
			if (directoryPath == null) throw new ArgumentNullException("directoryPath");
			if (IsRelative == false)
				throw new InvalidOperationException("FilePath is not relative.");

			var newpath = System.IO.Path.Combine(directoryPath.Path, Path);
			return new FilePath(newpath);
		}

		public static bool IsRelativePath(string path)
		{
			return !System.IO.Path.IsPathRooted(path);
		}

		public static implicit operator string(FilePath path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return path.Path;
		}
	}
}