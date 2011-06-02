using System;

namespace PathMate
{
	public class DirectoryPath
	{
		public readonly string Path;

		public DirectoryPath(string path)
		{
			if (path == null) throw new ArgumentNullException("path");
			Path = path;
		}

		#region Equality
		public bool Equals(DirectoryPath other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.Path, Path);
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
		#endregion

		public static implicit operator DirectoryPath(string path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return new DirectoryPath(path);
		}

		public static implicit operator string(DirectoryPath path)
		{
			if (path == null) throw new ArgumentNullException("path");
			return path.Path;
		}

		public override int GetHashCode()
		{
			return Path.GetHashCode();
		}
	}
}