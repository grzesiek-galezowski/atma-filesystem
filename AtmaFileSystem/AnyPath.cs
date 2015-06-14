using System;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class AnyPath
    : IEquatable<AnyPath>
  {
    public bool Equals(AnyPath other)
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
      return Equals((AnyPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyPath left, AnyPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyPath left, AnyPath right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    internal AnyPath(string path)
    {
      _path = path;
    }

    public override string ToString()
    {
      return _path;
    }

    public static AnyPath Value(string path)
    {
      AnyDirectoryPathAndPathWithFileNameAssertions.NotNull(path);
      AnyDirectoryPathAndPathWithFileNameAssertions.NotEmpty(path);
      AnyDirectoryPathAndPathWithFileNameAssertions.AssertPathValid(path);

      return new AnyPath(path);
    }
  }
}