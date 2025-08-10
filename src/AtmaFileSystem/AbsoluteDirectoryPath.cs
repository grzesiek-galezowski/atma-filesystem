using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using AtmaFileSystem.Assertions;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using AtmaFileSystem.Lib;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;

namespace AtmaFileSystem;

public sealed class AbsoluteDirectoryPath :
    IEquatable<AbsoluteDirectoryPath>,
    IEquatableAccordingToFileSystem<AbsoluteDirectoryPath>,
    IAbsolutePath,
    IDirectoryPath<AbsoluteDirectoryPath>,
    IComparable<AbsoluteDirectoryPath>, IComparable
{
  private readonly DirectoryInfo _directoryInfo;
  private readonly string _path;

  internal AbsoluteDirectoryPath(string path)
  {
    _path = Path.GetFullPath(path);
    _directoryInfo = new DirectoryInfo(_path);
  }

  public bool Equals(AbsoluteDirectoryPath? other)
  {
    if (ReferenceEquals(null, other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return string.Equals(_path, other._path, StringComparison.InvariantCulture);
  }

  public bool ShallowEquals(AbsoluteDirectoryPath other, FileSystemComparisonRules fileSystemComparisonRules)
  {
    return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
  }

  public static AbsoluteDirectoryPath Value(string path)
  {
    Asserts.AssertAreMet(ConditionSets.GetAbsoluteDirectoryPathConditions(nameof(path)), path);

    return new AbsoluteDirectoryPath(path);
  }

  public static AbsoluteDirectoryPath From(AbsoluteDirectoryPath path, DirectoryName directoryName)
      => new(PathAlgorithms.Combine(path, directoryName));

  public static AbsoluteDirectoryPath From(AbsoluteDirectoryPath path, RelativeDirectoryPath directoryName)
      => new(PathAlgorithms.Combine(path, directoryName));

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

  public Maybe<AbsoluteDirectoryPath> ParentDirectory(uint index)
  {
    var parent = ParentDirectory();
    index.Times(() => parent = parent.Select(p => p.ParentDirectory()));
    return parent;
  }

  private static Maybe<AbsoluteDirectoryPath> AsMaybe(FileSystemInfo? directoryName)
  {
    return directoryName != null ? Value(directoryName.FullName).Just() : Maybe<AbsoluteDirectoryPath>.Nothing;
  }

  public AbsoluteDirectoryPath Root() => new(Path.GetPathRoot(_path).OrThrow());

  public static AbsoluteFilePath operator +(AbsoluteDirectoryPath path, FileName fileName) =>
    AbsoluteFilePath.From(path, fileName);
  public static AbsoluteDirectoryPath operator +(AbsoluteDirectoryPath path, DirectoryName directoryName) =>
    From(path, directoryName);
  public static AbsoluteDirectoryPath operator +(AbsoluteDirectoryPath path, RelativeDirectoryPath relativePath) =>
    From(path, relativePath);
  public static AbsoluteFilePath operator +(AbsoluteDirectoryPath path, RelativeFilePath relativeFilePath) =>
    AbsoluteFilePath.From(path, relativeFilePath);

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((AbsoluteDirectoryPath)obj);
  }

  public override int GetHashCode()
  {
    return _path.GetHashCode();
  }

  public static bool operator ==(AbsoluteDirectoryPath? left, AbsoluteDirectoryPath? right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(AbsoluteDirectoryPath? left, AbsoluteDirectoryPath? right)
  {
    return !Equals(left, right);
  }

  public DirectoryName DirectoryName() => new(_directoryInfo.Name);
  public AnyDirectoryPath AsAnyDirectoryPath() => new(_path);
  public AnyPath AsAnyPath() => new(_path);
  public AbsoluteAnyPath AsAbsoluteAnyPath() => new(_path);

  public Maybe<AbsoluteDirectoryPath> FragmentEndingOnLast(DirectoryName directoryName) //bug move to path algorithms?
  {
    var result = this.ToMaybe();
    while (result.HasValue && !result.Value().DirectoryName().Equals(directoryName))
    {
      result = result.Value().ParentDirectory();
    }

    if (result.HasValue)
    {
      return result;
    }
    else
    {
      return Maybe<AbsoluteDirectoryPath>.Nothing;
    }
  }

  public int CompareTo(AbsoluteDirectoryPath? other)
  {
    if (ReferenceEquals(this, other)) return 0;
    if (ReferenceEquals(null, other)) return 1;
    return string.Compare(_path, other._path, StringComparison.InvariantCulture);
  }

  public int CompareTo(object? obj)
  {
    if (ReferenceEquals(null, obj)) return 1;
    if (ReferenceEquals(this, obj)) return 0;
    return obj is AbsoluteDirectoryPath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(AbsoluteDirectoryPath)}");
  }

  public static bool operator <(AbsoluteDirectoryPath left, AbsoluteDirectoryPath right)
  {
    return Comparer<AbsoluteDirectoryPath>.Default.Compare(left, right) < 0;
  }

  public static bool operator >(AbsoluteDirectoryPath left, AbsoluteDirectoryPath right)
  {
    return Comparer<AbsoluteDirectoryPath>.Default.Compare(left, right) > 0;
  }

  public static bool operator <=(AbsoluteDirectoryPath left, AbsoluteDirectoryPath right)
  {
    return Comparer<AbsoluteDirectoryPath>.Default.Compare(left, right) <= 0;
  }

  public static bool operator >=(AbsoluteDirectoryPath left, AbsoluteDirectoryPath right)
  {
    return Comparer<AbsoluteDirectoryPath>.Default.Compare(left, right) >= 0;
  }

  public Maybe<AbsoluteDirectoryPath> FindCommonDirectoryPathWith(AbsoluteDirectoryPath path2)
  {
    return PathAlgorithms.FindCommonDirectoryPathWith(
        this, path2, path => (AbsoluteDirectoryPath)path, path => path);
  }

  public Maybe<RelativeDirectoryPath> TrimStart(AbsoluteDirectoryPath startPath)
  {
    return PathAlgorithms.TrimStart(ToString(), startPath.ToString())
        .Select(s => new RelativeDirectoryPath(s));
  }

  public bool StartsWith(AbsoluteDirectoryPath subPath)
  {
    return PathAlgorithms.StartsWith(this, subPath, path => path);
  }

  public static AbsoluteDirectoryPath OfThisFile([CallerFilePath] string callerFilePath = "")
  {
    return AbsoluteFilePath.Value(callerFilePath).ParentDirectory();
  }

  public AbsoluteDirectoryPath AddDirectoryName(string fileName)
  {
    return this + AtmaFileSystemPaths.DirectoryName(fileName);
  }

  public AbsoluteFilePath AddFileName(string fileName)
  {
    return this + AtmaFileSystemPaths.FileName(fileName);
  }

  public AbsoluteFilePath Add(FileName fileName) => this + fileName;
  public AbsoluteDirectoryPath Add(DirectoryName directoryName) => this + directoryName;
  public AbsoluteDirectoryPath Add(RelativeDirectoryPath relativePath) => this + relativePath;
  public AbsoluteFilePath Add(RelativeFilePath relativeFilePath) => this + relativeFilePath;

  public static AbsoluteDirectoryPath OfCurrentWorkingDirectory()
  {
    return new AbsoluteDirectoryPath(Directory.GetCurrentDirectory());
  }

  public static AbsoluteDirectoryPath OfExecutingAssembly()
  {
    //bug refactor
    return new AbsoluteDirectoryPath(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location).OrThrow());
  }

  public static AbsoluteDirectoryPath OfTemp()
  {
    return Value(Path.GetTempPath());
  }
}