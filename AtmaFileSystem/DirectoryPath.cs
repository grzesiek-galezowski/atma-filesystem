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
      : this(Combine(path, directoryName))
    {

    }

    public DirectoryPath(DirectoryPath path, RelativeDirectoryPath directoryName)
      : this(Combine(path, directoryName))
    {
      
    }

    public DirectoryPath(DirectoryName directoryName1, DirectoryName directoryName2)
      : this(Combine(directoryName1, directoryName2))
    {
      
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }


    public static DirectoryPath Value(string path)
    {
      DirectoryPathAssert.NotNull(path);
      DirectoryPathAssert.Valid(path);

      return new DirectoryPath(path);
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
      return new DirectoryPath(path, directoryName);
    }

    public static DirectoryPath operator +(DirectoryPath path, RelativeDirectoryPath relativePath)
    {
      return new DirectoryPath(path, relativePath);
    }

    public static PathWithFileName operator +(DirectoryPath path, RelativePathWithFileName relativePath)
    {
      return new PathWithFileName(path, relativePath);
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

    public AnyDirectoryPath AsAnyDirectoryPath()
    {
      return new AnyDirectoryPath(_path);
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    public static DirectoryPath From(DirectoryName directoryName1, DirectoryName directoryName2)
    {
      return new DirectoryPath(directoryName1, directoryName2);
    }
  }
}
