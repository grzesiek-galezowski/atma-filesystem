using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class DirectoryPath : IEquatable<DirectoryPath>
  {
    private readonly string _path;
    private readonly DirectoryInfo _directoryInfo;

    internal DirectoryPath(string path)
    {
      _path = path;
      _directoryInfo = new DirectoryInfo(_path);
    }

    public DirectoryPath(DirectoryPath path, DirectoryName directoryName)
      : this(Path.Combine(path.ToString(), directoryName.ToString()))
    {

    }

    public DirectoryPath(DirectoryPath path, RelativeDirectoryPath directoryName)
      : this(Path.Combine(path.ToString(), directoryName.ToString()))
    {
      
    }

    public static DirectoryPath Value(string path)
    {
      DirectoryPathAssert.NotNull(path);
      DirectoryPathAssert.Valid(path);

      return new DirectoryPath(path);
    }

    public static DirectoryPath From(DirectoryPath path, DirectoryName directoryName)
    {
      return new DirectoryPath(path, directoryName);
    }


    public override string ToString()
    {
      return _path;
    }

    public DirectoryInfo Info()
    {
      return new DirectoryInfo(_path);
    }

    public Maybe<DirectoryPath> Parent()
    {
      var directoryName = _directoryInfo.Parent;
      return AsMaybe(directoryName);
    }

    private static Maybe<DirectoryPath> AsMaybe(DirectoryInfo directoryName)
    {
      return directoryName != null ? Maybe.Wrap(new DirectoryPath(directoryName.FullName)) : null;
    }

    public DirectoryPath Root()
    {
      return new DirectoryPath(Path.GetPathRoot(_path));
    }

    public static PathWithFileName operator +(DirectoryPath path, FileName fileName)
    {
      return PathWithFileName.From(path, fileName);
    }

    public static DirectoryPath operator +(DirectoryPath path, DirectoryName directoryName)
    {
      return From(path, directoryName);
    }

    public static DirectoryPath operator +(DirectoryPath path, RelativeDirectoryPath relativePath)
    {
      return new DirectoryPath(path, relativePath);
    }


    public bool Equals(DirectoryPath other)
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
      return Equals((DirectoryPath)obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(DirectoryPath left, DirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(DirectoryPath left, DirectoryPath right)
    {
      return !Equals(left, right);
    }

    public DirectoryName DirectoryName()
    {
      return new DirectoryName(_directoryInfo.Name);
    }
  }
}
