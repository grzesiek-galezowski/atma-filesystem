using System;

namespace AtmaFileSystem
{
  public class AnyPath //bug behave like value
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
  }
}