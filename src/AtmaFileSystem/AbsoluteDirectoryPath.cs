using System;
using System.IO;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class AbsoluteDirectoryPath : IEquatable<AbsoluteDirectoryPath>,
    IEquatableAccordingToFileSystem<AbsoluteDirectoryPath>
  {
    private readonly DirectoryInfo _directoryInfo;
    private readonly string _path;

    internal AbsoluteDirectoryPath(string path)
    {
      _path = path;
      _directoryInfo = new DirectoryInfo(_path);
    }

    public bool Equals(AbsoluteDirectoryPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public bool ShallowEquals(AbsoluteDirectoryPath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
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
      Asserts.DoesNotContainInvalidChars(path);
      return new AbsoluteDirectoryPath(path);
    }

    public static AbsoluteDirectoryPath From(AbsoluteDirectoryPath path, DirectoryName directoryName)
    {
      return new AbsoluteDirectoryPath(Combine(path, directoryName));
    }

    public static AbsoluteDirectoryPath From(AbsoluteDirectoryPath path, RelativeDirectoryPath directoryName)
    {
      return new AbsoluteDirectoryPath(Combine(path, directoryName));
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
      return AbsoluteFilePath.From(path, fileName);
    }

    public static AbsoluteDirectoryPath operator +(AbsoluteDirectoryPath path, DirectoryName directoryName)
    {
      return From(path, directoryName);
    }

    public static AbsoluteDirectoryPath operator +(AbsoluteDirectoryPath path, RelativeDirectoryPath relativePath)
    {
      return From(path, relativePath);
    }

    public static AbsoluteFilePath operator +(AbsoluteDirectoryPath path, RelativeFilePath relativeFilePath)
    {
      return AbsoluteFilePath.From(path, relativeFilePath);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AbsoluteDirectoryPath) obj);
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

