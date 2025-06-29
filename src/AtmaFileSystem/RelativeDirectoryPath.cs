using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using Core.Maybe;

namespace AtmaFileSystem;

public sealed class RelativeDirectoryPath :
  IEquatable<RelativeDirectoryPath>,
  IEquatableAccordingToFileSystem<RelativeDirectoryPath>,
  IDirectoryPath<RelativeDirectoryPath>,
  IComparable<RelativeDirectoryPath>, IComparable
{
  private readonly string _path;

  private RelativeDirectoryPath(RelativeDirectoryPath relativePath, DirectoryName dirName)
    : this(PathAlgorithms.Combine(relativePath, dirName))
  {
  }

  private RelativeDirectoryPath(
    RelativeDirectoryPath relativeDirectoryPath,
    RelativeDirectoryPath relativeDirectoryPath2)
    : this(PathAlgorithms.Combine(relativeDirectoryPath, relativeDirectoryPath2))
  {
  }

  internal RelativeDirectoryPath(string relativePath)
  {
    _path = relativePath;
  }

  public static RelativeDirectoryPath Value(string path)
  {
    Asserts.NotNull(path, nameof(path));
    Asserts.NotAllWhitespace(path, "relative path cannot consist of whitespaces");
    Asserts.NotFullyQualified(path);
    Asserts.DoesNotContainInvalidChars(path);

    return new RelativeDirectoryPath(PathAlgorithms.NormalizeSeparators(path));
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

  public RelativeFilePath Add(FileName fileName) => this + fileName;
  public RelativeDirectoryPath Add(DirectoryName directoryName) => this + directoryName;
  public RelativeDirectoryPath Add(RelativeDirectoryPath relativePath) => this + relativePath;
  public RelativeFilePath Add(RelativeFilePath relativeFilePath) => this + relativeFilePath;


  public Maybe<RelativeDirectoryPath> ParentDirectory()
  {
    if (_path == string.Empty)
    {
      return Maybe<RelativeDirectoryPath>.Nothing;
    }

    var directoryName = Path.GetDirectoryName(_path);
    if (string.IsNullOrEmpty(directoryName))
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

  public AnyDirectoryPath AsAnyDirectoryPath() => new(_path);
  public RelativeAnyPath AsRelativePath() => new(_path);
  public AnyPath AsAnyPath() => new(_path);

  public DirectoryName DirectoryName()
  {
    if (_path == string.Empty)
    {
      return AtmaFileSystem.DirectoryName.Value(string.Empty);
    }

    return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_path).Name);
  }

  public bool ShallowEquals(RelativeDirectoryPath other, FileSystemComparisonRules fileSystemComparisonRules)
  {
    return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
  }

  public override string ToString()
  {
    return _path;
  }

  public bool Equals(RelativeDirectoryPath? other)
  {
    if (ReferenceEquals(null, other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return string.Equals(_path, other._path, StringComparison.InvariantCulture);
  }

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((RelativeDirectoryPath)obj);
  }

  public override int GetHashCode()
  {
    return _path.GetHashCode();
  }

  public static bool operator ==(RelativeDirectoryPath? left, RelativeDirectoryPath? right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(RelativeDirectoryPath? left, RelativeDirectoryPath? right)
  {
    return !Equals(left, right);
  }

  public int CompareTo(RelativeDirectoryPath? other)
  {
    if (ReferenceEquals(this, other)) return 0;
    if (ReferenceEquals(null, other)) return 1;
    return string.Compare(_path, other._path, StringComparison.InvariantCulture);
  }

  public int CompareTo(object? obj)
  {
    if (ReferenceEquals(null, obj)) return 1;
    if (ReferenceEquals(this, obj)) return 0;
    return obj is RelativeDirectoryPath other
      ? CompareTo(other)
      : throw new ArgumentException($"Object must be of type {nameof(RelativeDirectoryPath)}");
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
    return PathAlgorithms.FindCommonDirectoryPathWith(
      this, path2, path => (RelativeDirectoryPath)path, path => path);
  }

  public bool StartsWith(RelativeDirectoryPath subPath)
  {
    return PathAlgorithms.StartsWith(this, subPath, path => path);
  }

  public Maybe<RelativeDirectoryPath> TrimStart(RelativeDirectoryPath startPath)
  {
    return PathAlgorithms.TrimStart(_path, startPath._path)
      .Select(s => new RelativeDirectoryPath(s));
  }

  public RelativeDirectoryPath AddDirectoryName(string directoryName)
  {
    return this + AtmaFileSystemPaths.DirectoryName(directoryName);
  }

  public RelativeFilePath AddFileName(string fileName)
  {
    return this + AtmaFileSystemPaths.FileName(fileName);
  }

}