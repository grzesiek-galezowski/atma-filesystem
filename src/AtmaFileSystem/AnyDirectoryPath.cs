using System;
using System.Collections.Generic;
using System.IO;
using AtmaFileSystem.Assertions;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem
{
  //bug mixed/different path separators
  public sealed class AnyDirectoryPath : 
    IEquatable<AnyDirectoryPath>, 
    IEquatableAccordingToFileSystem<AnyDirectoryPath>,
    IComparable<AnyDirectoryPath>, IComparable
  {
    private readonly string _path;

    private AnyDirectoryPath(AnyDirectoryPath left, DirectoryName right)
      : this(Path.Join(left.ToString(), right.ToString()))
    {
    }

    private AnyDirectoryPath(AnyDirectoryPath left, RelativeDirectoryPath right)
      : this(Path.Join(left.ToString(), right.ToString()))
    {
    }

    internal AnyDirectoryPath(string path)
    {
      _path = path;
    }

    public bool Equals(AnyDirectoryPath? other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path, StringComparison.InvariantCulture);
    }

    public bool ShallowEquals(AnyDirectoryPath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AnyDirectoryPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyDirectoryPath? left, AnyDirectoryPath? right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyDirectoryPath? left, AnyDirectoryPath? right)
    {
      return !Equals(left, right);
    }

    public static AnyFilePath operator +(AnyDirectoryPath left, FileName right)
    {
      return new AnyFilePath(left, right);
    }

    public static AnyDirectoryPath operator +(AnyDirectoryPath left, DirectoryName right)
    {
      return new AnyDirectoryPath(left, right);
    }

    public static AnyDirectoryPath operator +(AnyDirectoryPath left, RelativeDirectoryPath right)
    {
      return new AnyDirectoryPath(left, right);
    }

    public static AnyFilePath operator +(AnyDirectoryPath left, RelativeFilePath right)
    {
      return new AnyFilePath(left, right);
    }

    public override string ToString()
    {
      return _path;
    }

    public AnyPath AsAnyPath() => new(_path);

    public static AnyDirectoryPath Value(string path)
    {
      Asserts.NotNull(path, nameof(path));
      Asserts.NotWhitespace(path, "Path cannot be whitespace");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");
      Asserts.DoesNotContainInvalidChars(path);

      return new AnyDirectoryPath(path);
    }

    public DirectoryName DirectoryName()
    {
      if (_path == string.Empty)
      {
        return AtmaFileSystem.DirectoryName.Value(string.Empty);
      }
      return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_path).Name);
    }

    public Maybe<AnyDirectoryPath> ParentDirectory()
    {
      if (_path == string.Empty)
      {
        return Maybe<AnyDirectoryPath>.Nothing;
      }
      var directoryName = new DirectoryInfo(_path).Parent;
      return AsMaybe(directoryName);
    }

    private static Maybe<AnyDirectoryPath> AsMaybe(DirectoryInfo? directoryName)
    {
      return directoryName != null ? Value(directoryName.FullName).Just() : Maybe<AnyDirectoryPath>.Nothing;
    }

    public Maybe<DirectoryInfo> Info()
    {
      if (_path == string.Empty)
      {
        return Maybe<DirectoryInfo>.Nothing;
      }
      return new DirectoryInfo(_path).Just();
    }

    public int CompareTo(AnyDirectoryPath? other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object? obj)
    {
      if (ReferenceEquals(null, obj)) return 1;
      if (ReferenceEquals(this, obj)) return 0;
      return obj is AnyDirectoryPath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(AnyDirectoryPath)}");
    }

    public static bool operator <(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return Comparer<AnyDirectoryPath>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return Comparer<AnyDirectoryPath>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return Comparer<AnyDirectoryPath>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return Comparer<AnyDirectoryPath>.Default.Compare(left, right) >= 0;
    }
  }
}