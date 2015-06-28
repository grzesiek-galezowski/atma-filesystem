using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class AbsoluteDirectoryPath : IEquatable<AbsoluteDirectoryPath>
  {
    private readonly string _path;
    private readonly DirectoryInfo _directoryInfo;

    internal AbsoluteDirectoryPath(string path)
    {
      _path = path;
      _directoryInfo = new DirectoryInfo(_path);
    }

    public AbsoluteDirectoryPath(AbsoluteDirectoryPath path, DirectoryName directoryName)
      : this(Combine(path, directoryName))
    {

    }

    public AbsoluteDirectoryPath(AbsoluteDirectoryPath path, RelativeDirectoryPath directoryName)
      : this(Combine(path, directoryName))
    {
      
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }


    public static AbsoluteDirectoryPath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "Path cannot be empty");
      Asserts.Rooted(path, "Expected absolute path, but got " + path);

      return new AbsoluteDirectoryPath(path);
    }

    public override string ToString()
    {
      return _path;
    }

    public DirectoryInfo Info()
    {
      return _directoryInfo;
    }

    public Maybe<AbsoluteDirectoryPath> ParentDirectory()
    {
      var directoryName = _directoryInfo.Parent;
      return AsMaybe(directoryName);
    }

    private static Maybe<AbsoluteDirectoryPath> AsMaybe(DirectoryInfo directoryName)
    {
      return directoryName != null ? Maybe.Wrap(Value(directoryName.FullName)) : null;
    }

    public AbsoluteDirectoryPath Root()
    {
      return new AbsoluteDirectoryPath(Path.GetPathRoot(_path));
    }

    public static AbsoluteFilePath operator +(AbsoluteDirectoryPath path, FileName fileName)
    {
      return new AbsoluteFilePath(path, fileName);
    }

    public static AbsoluteDirectoryPath operator +(AbsoluteDirectoryPath path, DirectoryName directoryName)
    {
      return new AbsoluteDirectoryPath(path, directoryName);
    }

    public static AbsoluteDirectoryPath operator +(AbsoluteDirectoryPath path, RelativeDirectoryPath relativePath)
    {
      return new AbsoluteDirectoryPath(path, relativePath);
    }

    public static AbsoluteFilePath operator +(AbsoluteDirectoryPath path, RelativeFilePath relativeFilePath)
    {
      return new AbsoluteFilePath(path, relativeFilePath);
    }


    public bool Equals(AbsoluteDirectoryPath other)
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
      return Equals((AbsoluteDirectoryPath)obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(AbsoluteDirectoryPath left, AbsoluteDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AbsoluteDirectoryPath left, AbsoluteDirectoryPath right)
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

  }
}