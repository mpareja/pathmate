using System;

namespace PathMate
{
	public class FilePath : Path
	{
		public FilePath(string path) : base(path) { }

		public FilePath FileName
		{
			get { return System.IO.Path.GetFileName(this); }
		}

		public FilePath FileNameWithoutExtension
		{
			get { return System.IO.Path.GetFileNameWithoutExtension(this); }
		}

		public virtual FilePath RelativeTo(DirectoryPath directoryPath)
		{
			if (directoryPath == null) throw new ArgumentNullException("directoryPath");
			if (IsRelative == false)
				throw new InvalidOperationException("FilePath is not relative.");

			var newpath = System.IO.Path.Combine(directoryPath.ToString(), _path);
			return new FilePath(newpath);
		}

		public virtual FilePath RelativeToWorkingDir()
		{
			return RelativeTo(Environment.CurrentDirectory);
		}

		#region Equality
		public bool Equals(FilePath other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._path, _path);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (FilePath)) return false;
			return Equals((FilePath) obj);
		}

		public override int GetHashCode()
		{
			return (_path != null ? _path.GetHashCode() : 0);
		}

		public static bool operator ==(FilePath left, FilePath right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(FilePath left, FilePath right)
		{
			return !Equals(left, right);
		}
		#endregion

		public static implicit operator FilePath(string path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return new FilePath(path);
		}

		public static implicit operator string(FilePath path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return path._path;
		}

		public static FilePath operator +(DirectoryPath left, FilePath right)
		{
			return right.RelativeTo(left);
		}
	}
}