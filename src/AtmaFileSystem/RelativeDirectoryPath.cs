using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem
{
  public sealed class RelativeDirectoryPath : 
    IEquatable<RelativeDirectoryPath>,
    IEquatableAccordingToFileSystem<RelativeDirectoryPath>,
    IDirectoryPath<RelativeDirectoryPath>,
    IComparable<RelativeDirectoryPath>, IComparable
  {
    private readonly string _path;

    private RelativeDirectoryPath(RelativeDirectoryPath relativePath, DirectoryName dirName)
      : this(Combine(relativePath, dirName))
    {
    }

    private RelativeDirectoryPath(RelativeDirectoryPath relativeDirectoryPath,
      RelativeDirectoryPath relativeDirectoryPath2)
      : this(Combine(relativeDirectoryPath, relativeDirectoryPath2))
    {
    }

    internal RelativeDirectoryPath(string relativePath)
    {
      _path = relativePath;
    }

    public static RelativeDirectoryPath Value(string relativePath)
    {
      Asserts.NotNull(relativePath, "path");
      Asserts.NotWhitespace(relativePath, "relative path cannot consist of whitespaces");
      Asserts.NotRooted(relativePath, "Expected relative path, but got " + relativePath);
      Asserts.DoesNotContainInvalidChars(relativePath);

      return new RelativeDirectoryPath(relativePath);
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }

    public static RelativeDirectoryPath operator +(RelativeDirectoryPath path, DirectoryName dirName)
    {
      return new RelativeDirectoryPath(path, dirName);
    }

    public static RelativeFilePath operator +(RelativeDirectoryPath path, FileName dirName)
    {
      return RelativeFilePath.From(path, dirName);
    }

    public static RelativeDirectoryPath operator +(RelativeDirectoryPath path, RelativeDirectoryPath path2)
    {
      return new RelativeDirectoryPath(path, path2);
    }

    public static RelativeFilePath operator +(RelativeDirectoryPath path, RelativeFilePath relativeFilePath)
    {
      return RelativeFilePath.From(path, relativeFilePath);
    }


    public Maybe<RelativeDirectoryPath> ParentDirectory()
    {
      if (_path == string.Empty)
      {
        return Maybe<RelativeDirectoryPath>.Nothing;
      }
      var directoryName = Path.GetDirectoryName(_path);
      if (directoryName == string.Empty)
      {
        return Maybe<RelativeDirectoryPath>.Nothing;
      }
      return Value(directoryName).Just();
    }

    public Maybe<DirectoryInfo> Info()
    {
      if (_path == string.Empty)
      {
        return Maybe<DirectoryInfo>.Nothing;
      }
      return new DirectoryInfo(_path).Just();
    }

    public AnyDirectoryPath AsAnyDirectoryPath()
    {
      return new AnyDirectoryPath(_path);
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    public DirectoryName DirectoryName()
    {
      if (_path == string.Empty)
      {
        return AtmaFileSystem.DirectoryName.Value(string.Empty);
      }
      return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_path).Name);
    }

    #region Generated members

    public bool ShallowEquals(RelativeDirectoryPath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public override string ToString()
    {
      return _path;
    }

    public bool Equals(RelativeDirectoryPath other)
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
      return Equals((RelativeDirectoryPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return !Equals(left, right);
    }

    #endregion

    public int CompareTo(RelativeDirectoryPath other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object obj)
    {
      if (ReferenceEquals(null, obj)) return 1;
      if (ReferenceEquals(this, obj)) return 0;
      return obj is RelativeDirectoryPath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(RelativeDirectoryPath)}");
    }

    public static bool operator <(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Comparer<RelativeDirectoryPath>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Comparer<RelativeDirectoryPath>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Comparer<RelativeDirectoryPath>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Comparer<RelativeDirectoryPath>.Default.Compare(left, right) >= 0;
    }

    public Maybe<RelativeDirectoryPath> FindCommonDirectoryPathWith(RelativeDirectoryPath path2)
    {
      return DirectoryPathAlgorithms<RelativeDirectoryPath>.FindCommonDirectoryPathWith(
        this, path2, path => (RelativeDirectoryPath)path, path => path);
    }

    public bool StartsWith(RelativeDirectoryPath subPath)
    {
      return DirectoryPathAlgorithms<RelativeDirectoryPath>.StartsWith(this, subPath, path => path);
    }
  }
}