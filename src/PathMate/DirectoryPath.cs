using System;

namespace PathMate
{
	public class DirectoryPath : Path
	{
		public DirectoryPath(string path) : base(path) { }

		public virtual DirectoryPath RelativeTo(DirectoryPath directoryPath)
		{
			if (directoryPath == null) throw new ArgumentNullException("directoryPath");
			if (IsRelative == false)
				throw new InvalidOperationException("DirectoryPath is not relative.");

			var newpath = System.IO.Path.Combine(directoryPath.ToString(), _path);
			return new DirectoryPath(newpath);
		}

		public virtual DirectoryPath RelativeToWorkingDir()
		{
			return RelativeTo(Environment.CurrentDirectory);
		}

		public virtual DirectoryPath MakeAbsolute()
		{
			if (IsRelative == false)
				return this;
			return WorkingDir + this;
		}

		#region Equality
		public bool Equals(DirectoryPath other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other._path, _path);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (DirectoryPath)) return false;
			return Equals((DirectoryPath) obj);
		}

		public static bool operator ==(DirectoryPath left, DirectoryPath right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(DirectoryPath left, DirectoryPath right)
		{
			return !Equals(left, right);
		}

		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}
		#endregion

		public static implicit operator DirectoryPath(string path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return new DirectoryPath(path);
		}

		public static implicit operator string(DirectoryPath path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return path._path;
		}

		public static DirectoryPath operator +(DirectoryPath left, DirectoryPath right)
		{
			return right.RelativeTo(left);
		}
	}
}