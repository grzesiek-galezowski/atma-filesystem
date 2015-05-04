using System;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativeDirectoryPath : IEquatable<RelativeDirectoryPath>
  {
    private readonly string _relativePath;

    public RelativeDirectoryPath(DirectoryName dir, DirectoryName subdir)
      : this(Path.Combine(dir.ToString(), subdir.ToString()))
    {
      
    }

    private RelativeDirectoryPath(string relativePath)
    {
      _relativePath = relativePath;
    }

    public override string ToString()
    {
      return _relativePath;
    }

    public bool Equals(RelativeDirectoryPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_relativePath, other._relativePath);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((RelativeDirectoryPath)obj);
    }

    public override int GetHashCode()
    {
      return (_relativePath != null ? _relativePath.GetHashCode() : 0);
    }

    public static bool operator ==(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return !Equals(left, right);
    }
  }
}