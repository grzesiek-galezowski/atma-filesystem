using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using AtmaFileSystem.Assertions;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using AtmaFileSystem.Lib;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;

namespace AtmaFileSystem;

public sealed class AbsoluteFilePath :
  IEquatable<AbsoluteFilePath>,
  IEquatableAccordingToFileSystem<AbsoluteFilePath>,
  IInternalFilePath<AbsoluteFilePath>,
  IAbsolutePath,
  IExtensionChangable<AbsoluteFilePath>,
  IComparable<AbsoluteFilePath>, IComparable
{
  private readonly string _path;

  // ReSharper disable once MemberCanBePrivate.Global
  internal AbsoluteFilePath(string path) => _path = Path.GetFullPath(path);

  public static AbsoluteFilePath From(AbsoluteDirectoryPath dirPath, FileName fileName)
  {
    return new AbsoluteFilePath(PathAlgorithms.Combine(dirPath, fileName));
  }

  public static AbsoluteFilePath From(AbsoluteDirectoryPath dirPath, RelativeFilePath relativeFilePath)
  {
    return new AbsoluteFilePath(PathAlgorithms.Combine(dirPath, relativeFilePath));
  }

  public bool Equals(AbsoluteFilePath? other)
  {
    if (ReferenceEquals(null, other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return string.Equals(_path, other._path, StringComparison.InvariantCulture);
  }

  public bool ShallowEquals(AbsoluteFilePath other, FileSystemComparisonRules fileSystemComparisonRules)
  {
    return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
  }

  public static AbsoluteFilePath Value(string path)
  {
    Asserts.NotNull(path, nameof(path));
    Asserts.FullyQualified(path);
    Asserts.DoesNotContainInvalidChars(path);

    return new AbsoluteFilePath(path);
  }

  public AbsoluteDirectoryPath ParentDirectory() => new(Path.GetDirectoryName(_path).OrThrow());

  public Maybe<AbsoluteDirectoryPath> ParentDirectory(uint index)
  {
    var parent = ParentDirectory().ToMaybe();
    index.Times(() => parent = parent.Select(p => p.ParentDirectory()));
    return parent;
  }

  public FileInfo Info() => new(_path);
  public FileName FileName() => new(Path.GetFileName(_path));
  public AbsoluteDirectoryPath Root() => new(Path.GetPathRoot(_path).OrThrow());
  public AnyFilePath AsAnyFilePath() => new(_path);
  public AnyPath AsAnyPath() => new(_path);
  public AbsoluteAnyPath AsAbsoluteAnyPath() => new(_path);

  public override string ToString() => _path;

  public override int GetHashCode() => _path.GetHashCode();

  public override bool Equals(object? obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != GetType()) return false;
    return Equals((AbsoluteFilePath)obj);
  }

  public static bool operator ==(AbsoluteFilePath? left, AbsoluteFilePath? right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(AbsoluteFilePath? left, AbsoluteFilePath? right)
  {
    return !Equals(left, right);
  }

  public bool Has(FileExtension extensionValue)
  {
    return FileName().Has(extensionValue);
  }

  public AbsoluteFilePath ChangeExtensionTo(FileExtension value)
  {
    var pathWithNewExtension = Path.ChangeExtension(_path, value.ToString());
    return new AbsoluteFilePath(pathWithNewExtension);
  }

  public Maybe<AbsoluteDirectoryPath> FragmentEndingOnLast(DirectoryName directoryName)
  {
    return ParentDirectory().FragmentEndingOnLast(directoryName);
  }

  public int CompareTo(AbsoluteFilePath? other)
  {
    if (ReferenceEquals(this, other)) return 0;
    if (ReferenceEquals(null, other)) return 1;
    return string.Compare(_path, other._path, StringComparison.InvariantCulture);
  }

  public int CompareTo(object? obj)
  {
    if (ReferenceEquals(null, obj)) return 1;
    if (ReferenceEquals(this, obj)) return 0;
    return obj is AbsoluteFilePath other
      ? CompareTo(other)
      : throw new ArgumentException($"Object must be of type {nameof(AbsoluteFilePath)}");
  }

  public static bool operator <(AbsoluteFilePath left, AbsoluteFilePath right)
  {
    return Comparer<AbsoluteFilePath>.Default.Compare(left, right) < 0;
  }

  public static bool operator >(AbsoluteFilePath left, AbsoluteFilePath right)
  {
    return Comparer<AbsoluteFilePath>.Default.Compare(left, right) > 0;
  }

  public static bool operator <=(AbsoluteFilePath left, AbsoluteFilePath right)
  {
    return Comparer<AbsoluteFilePath>.Default.Compare(left, right) <= 0;
  }

  public static bool operator >=(AbsoluteFilePath left, AbsoluteFilePath right)
  {
    return Comparer<AbsoluteFilePath>.Default.Compare(left, right) >= 0;
  }

  public Maybe<AbsoluteDirectoryPath> FindCommonDirectoryWith(AbsoluteFilePath path2)
  {
    return ParentDirectory().FindCommonDirectoryPathWith(path2.ParentDirectory());
  }

  public bool StartsWith(AbsoluteDirectoryPath currentPath)
  {
    return PathAlgorithms.StartsWith(this, currentPath);
  }

  public Maybe<RelativeFilePath> TrimStart(AbsoluteDirectoryPath startPath)
  {
    return PathAlgorithms.TrimStart(_path, startPath.ToString())
      .Select(s => new RelativeFilePath(s));
  }

  public static AbsoluteFilePath OfThisFile([CallerFilePath] string callerFilePath = "")
  {
    return Value(callerFilePath);
  }

  public static AbsoluteFilePath operator +(AbsoluteFilePath path, FileExtension fileExtension)
  {
    return path.ParentDirectory() + (path.FileName() + fileExtension);
  }

  public AbsoluteFilePath AddExtension(string extensionString)
  {
    return this + FileExtension.Value(extensionString);
  }

  public AbsoluteFilePath AppendToFileNameBeforeExtension(string suffix)
  {
    return ParentDirectory() + FileName().AppendBeforeExtension(suffix);
  }

  public AbsoluteFilePath ChangeFileNameTo(FileName fileName)
  {
    return ParentDirectory() + fileName;
  }
}