using System;

namespace AtmaFileSystem
{
  //bug add public factory method
  public class AnyDirectoryPath : IEquatable<AnyDirectoryPath>
  {
    public bool Equals(AnyDirectoryPath other)
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
      return Equals((AnyDirectoryPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    internal AnyDirectoryPath(string path)
    {
      _path = path;
    }

    public override string ToString()
    {
      return _path;
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }
  }
}