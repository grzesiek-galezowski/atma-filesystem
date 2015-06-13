using System;

namespace AtmaFileSystem
{
  //bug add static creation method
  public class AnyPathWithFileName
    : IEquatable<AnyPathWithFileName>
  {
    public bool Equals(AnyPathWithFileName other)
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
      return Equals((AnyPathWithFileName) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyPathWithFileName left, AnyPathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyPathWithFileName left, AnyPathWithFileName right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    internal AnyPathWithFileName(string path)
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