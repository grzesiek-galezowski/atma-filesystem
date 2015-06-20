using System;
using System.Runtime.CompilerServices;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class AnyDirectoryPath : IEquatable<AnyDirectoryPath>
  {
    private AnyDirectoryPath(AnyDirectoryPath left, DirectoryName right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {

    }

    private AnyDirectoryPath(AnyDirectoryPath left, RelativeDirectoryPath right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {

    }

    internal AnyDirectoryPath(string path)
    {
      _path = path;
    }

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

    public static AnyPathWithFileName operator+(AnyDirectoryPath left, FileName right)
    {
      return new AnyPathWithFileName(left, right);
    }

    public static AnyDirectoryPath operator+(AnyDirectoryPath left, DirectoryName right)
    {
      return new AnyDirectoryPath(left, right);
    }

    public static AnyDirectoryPath operator +(AnyDirectoryPath left, RelativeDirectoryPath right)
    {
      return new AnyDirectoryPath(left, right);
    }

    public static AnyPathWithFileName operator +(AnyDirectoryPath left, RelativePathWithFileName right)
    {
      return new AnyPathWithFileName(left, right);
    }


    private readonly string _path;


    public override string ToString()
    {
      return _path;
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    public static AnyDirectoryPath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "Path cannot be empty");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");

      return new AnyDirectoryPath(path);
    }

    public DirectoryName DirectoryName()
    {
      return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_path).Name);
    }

    public Maybe<AnyDirectoryPath> Parent()
    {
      var directoryName = new DirectoryInfo(_path).Parent;
      return AsMaybe(directoryName);
    }

    private static Maybe<AnyDirectoryPath> AsMaybe(DirectoryInfo directoryName)
    {
      return directoryName != null ? Maybe.Wrap(Value(directoryName.FullName)) : null;
    }

  }
}