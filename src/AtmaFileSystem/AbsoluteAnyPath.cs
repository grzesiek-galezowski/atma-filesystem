using System;
using System.Collections.Generic;
using System.IO;
using AtmaFileSystem.Assertions;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using AtmaFileSystem.Lib;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;

namespace AtmaFileSystem;

public sealed class AbsoluteAnyPath :
  IEquatable<AbsoluteAnyPath>,
  IEquatableAccordingToFileSystem<AbsoluteAnyPath>,
  IAbsolutePath,
  IComparable<AbsoluteAnyPath>, IComparable
{
  private readonly string _path;

  // ReSharper disable once MemberCanBePrivate.Global
  internal AbsoluteAnyPath(string path) => _path = Path.GetFullPath(path);

  public bool Equals(AbsoluteAnyPath? other)
  {
    if (ReferenceEquals(null, other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return string.Equals(_path, other._path, StringComparison.InvariantCulture);
  }

  public bool ShallowEquals(AbsoluteAnyPath other, FileSystemComparisonRules fileSystemComparisonRules)
  {
    return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
  }

  public static AbsoluteAnyPath Value(string path)
  {
    Asserts.AssertAreMet(ConditionSets.GetAbsoluteAnyPathConditions(nameof(path)), path);

    return new AbsoluteAnyPath(path);
  }

  public Maybe<AbsoluteDirectoryPath> ParentDirectory()
  {
    return Directory.GetParent(_path).ToMaybe().Select(p => AbsoluteDirectoryPath.Value(p.FullName));
  }

  public Maybe<AbsoluteDirectoryPath> ParentDirectory(uint index)
  {
    var parent = ParentDirectory();
    index.Times(() => parent = parent.Select(p => p.ParentDirectory()));
    return parent;
  }

  public AbsoluteDirectoryPath Root() => new(Path.GetPathRoot(_path).OrThrow());

  public AnyPath AsAnyPath() => new(_path);

  public override string ToString() => _path;

  public override int GetHashCode() => _path.GetHashCode();

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((AbsoluteAnyPath)obj);
  }

  public static bool operator ==(AbsoluteAnyPath? left, AbsoluteAnyPath? right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(AbsoluteAnyPath? left, AbsoluteAnyPath? right)
  {
    return !Equals(left, right);
  }

  public Maybe<AbsoluteDirectoryPath> FragmentEndingOnLast(DirectoryName directoryName)
  {
    return ParentDirectory().Select(p => p.FragmentEndingOnLast(directoryName));
  }

  public int CompareTo(AbsoluteAnyPath? other)
  {
    if (ReferenceEquals(this, other)) return 0;
    if (ReferenceEquals(null, other)) return 1;
    return string.Compare(_path, other._path, StringComparison.InvariantCulture);
  }

  public int CompareTo(object? obj)
  {
    if (ReferenceEquals(null, obj)) return 1;
    if (ReferenceEquals(this, obj)) return 0;
    return obj is AbsoluteAnyPath other
      ? CompareTo(other)
      : throw new ArgumentException($"Object must be of type {nameof(AbsoluteAnyPath)}");
  }

  public static bool operator <(AbsoluteAnyPath left, AbsoluteAnyPath right)
  {
    return Comparer<AbsoluteAnyPath>.Default.Compare(left, right) < 0;
  }

  public static bool operator >(AbsoluteAnyPath left, AbsoluteAnyPath right)
  {
    return Comparer<AbsoluteAnyPath>.Default.Compare(left, right) > 0;
  }

  public static bool operator <=(AbsoluteAnyPath left, AbsoluteAnyPath right)
  {
    return Comparer<AbsoluteAnyPath>.Default.Compare(left, right) <= 0;
  }

  public static bool operator >=(AbsoluteAnyPath left, AbsoluteAnyPath right)
  {
    return Comparer<AbsoluteAnyPath>.Default.Compare(left, right) >= 0;
  }

  public Maybe<AbsoluteDirectoryPath> FindCommonDirectoryWith(AbsoluteAnyPath path2)
  {
    return from thisFileDir in ParentDirectory()
           from otherFileDir in path2.ParentDirectory()
           select thisFileDir.FindCommonDirectoryPathWith(otherFileDir);
  }

  public Maybe<RelativeAnyPath> TrimStart(AbsoluteDirectoryPath startPath)
  {
    return PathAlgorithms.TrimStart(_path, startPath.ToString())
      .Select(s => new RelativeAnyPath(s));
  }
}