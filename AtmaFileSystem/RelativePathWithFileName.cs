using System;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativePathWithFileName : IEquatable<RelativePathWithFileName>
  {
    public bool Equals(RelativePathWithFileName other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((RelativePathWithFileName) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(RelativePathWithFileName left, RelativePathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(RelativePathWithFileName left, RelativePathWithFileName right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    public RelativePathWithFileName(RelativeDirectoryPath relativeDirectoryPath, FileName fileName)
    {
      _path = Path.Combine(relativeDirectoryPath.ToString(), fileName.ToString());
    }

    public override string ToString()
    {
      return _path;
    }
  }
}