using System.IO;

namespace PathMate
{
	public abstract class Path
	{
		protected readonly string _path;

		public Path(string path)
		{
			_path = path;
		}

		public bool IsRelative
		{
			get { return IsRelativePath(_path); }
		}

		public FileInfo FileInfo 
		{
			get { return new FileInfo(_path); }
		}

		public override string ToString()
		{
			return _path;
		}

		public static bool IsRelativePath(string path)
		{
			return !System.IO.Path.IsPathRooted(path);
		}
	}
}