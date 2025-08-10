using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.Internals;
using Core.Maybe;

namespace AtmaFileSystem;

public sealed class RelativeAnyPath :
    IEquatable<RelativeAnyPath>,
    IEquatableAccordingToFileSystem<RelativeAnyPath>,
    IComparable<RelativeAnyPath>, IComparable
{
  private readonly string _path;

  internal RelativeAnyPath(string pathString)
  {
    _path = pathString.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
  }

  public Maybe<RelativeDirectoryPath> ParentDirectory()
  {
    var directoryName = Path.GetDirectoryName(_path);
    if (string.IsNullOrEmpty(directoryName))
    {
      return Maybe<RelativeDirectoryPath>.Nothing;
    }

    return RelativeDirectoryPath.Value(directoryName).Just();
  }

  public static RelativeAnyPath Value(string path)
  {
    Asserts.AssertAreMet(ConditionSets.GetRelativeAnyPathConditions(nameof(path)), path);
    return new RelativeAnyPath(path);
  }

  public AnyPath AsAnyPath()
  {
    return new AnyPath(_path);
  }

  public bool ShallowEquals(RelativeAnyPath other, FileSystemComparisonRules fileSystemComparisonRules)
  {
    return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
  }

  public override string ToString()
  {
    return _path;
  }

  public bool Equals(RelativeAnyPath? other)
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
    return Equals((RelativeAnyPath)obj);
  }

  public override int GetHashCode()
  {
    return _path.GetHashCode();
  }

  public static bool operator ==(RelativeAnyPath? left, RelativeAnyPath? right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(RelativeAnyPath? left, RelativeAnyPath? right)
  {
    return !Equals(left, right);
  }

  public int CompareTo(RelativeAnyPath? other)
  {
    if (ReferenceEquals(this, other)) return 0;
    if (ReferenceEquals(null, other)) return 1;
    return string.Compare(_path, other._path, StringComparison.InvariantCulture);
  }

  public int CompareTo(object? obj)
  {
    if (ReferenceEquals(null, obj)) return 1;
    if (ReferenceEquals(this, obj)) return 0;
    return obj is RelativeAnyPath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(RelativeAnyPath)}");
  }

  public static bool operator <(RelativeAnyPath left, RelativeAnyPath right)
  {
    return Comparer<RelativeAnyPath>.Default.Compare(left, right) < 0;
  }

  public static bool operator >(RelativeAnyPath left, RelativeAnyPath right)
  {
    return Comparer<RelativeAnyPath>.Default.Compare(left, right) > 0;
  }

  public static bool operator <=(RelativeAnyPath left, RelativeAnyPath right)
  {
    return Comparer<RelativeAnyPath>.Default.Compare(left, right) <= 0;
  }

  public static bool operator >=(RelativeAnyPath left, RelativeAnyPath right)
  {
    return Comparer<RelativeAnyPath>.Default.Compare(left, right) >= 0;
  }

  public Maybe<RelativeDirectoryPath> FindCommonRelativeDirectoryPathWith(RelativeAnyPath path2)
  {
    return from thisFileDir in ParentDirectory()
           from otherFileDir in path2.ParentDirectory()
           select thisFileDir.FindCommonDirectoryPathWith(otherFileDir);
  }

  public Maybe<RelativeAnyPath> TrimStart(RelativeDirectoryPath startPath)
  {
    return PathAlgorithms.TrimStart(_path, startPath.ToString())
        .Select(s => new RelativeAnyPath(s));
  }
}