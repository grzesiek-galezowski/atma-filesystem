using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem
{
  public sealed class AnyPath
    : IEquatable<AnyPath>, 
      IEquatableAccordingToFileSystem<AnyPath>,
      IComparable<AnyPath>, IComparable
  {
    private readonly string _path;

    internal AnyPath(string path)
    {
      _path = path;
    }

    public bool Equals(AnyPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public bool ShallowEquals(AnyPath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AnyPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyPath left, AnyPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyPath left, AnyPath right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      return _path;
    }

    public static AnyPath Value(string path)
    {
      Asserts.NotNull(path, nameof(path));
      Asserts.NotWhitespace(path, "Path cannot be whitespace");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");
      Asserts.DoesNotContainInvalidChars(path);

      return new AnyPath(PathAlgorithms.NormalizeSeparators(path));
    }

    public Maybe<AnyDirectoryPath> ParentDirectory()
    {
      if (_path == string.Empty)
      {
        return Maybe<AnyDirectoryPath>.Nothing;
      }
      var directoryName = Path.GetDirectoryName(_path);
      if (directoryName == string.Empty)
      {
        return Maybe<AnyDirectoryPath>.Nothing;
      }
      return AnyDirectoryPath.Value(directoryName).Just();
    }

    public int CompareTo(AnyPath other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object obj)
    {
      if (ReferenceEquals(null, obj)) return 1;
      if (ReferenceEquals(this, obj)) return 0;
      return obj is AnyPath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(AnyPath)}");
    }

    public static bool operator <(AnyPath left, AnyPath right)
    {
      return Comparer<AnyPath>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(AnyPath left, AnyPath right)
    {
      return Comparer<AnyPath>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(AnyPath left, AnyPath right)
    {
      return Comparer<AnyPath>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(AnyPath left, AnyPath right)
    {
      return Comparer<AnyPath>.Default.Compare(left, right) >= 0;
    }
  }
}