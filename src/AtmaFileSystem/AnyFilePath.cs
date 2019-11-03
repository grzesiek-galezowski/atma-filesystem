using System;
using System.Collections.Generic;
using System.IO;
using AtmaFileSystem.Assertions;
using AtmaFileSystem.InternalInterfaces;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem
{
  public sealed class AnyFilePath
    : IEquatable<AnyFilePath>, 
      IEquatableAccordingToFileSystem<AnyFilePath>,
      IExtensionChangable<AnyFilePath>,
      IComparable<AnyFilePath>, IComparable
  {
    private readonly string _path;

    internal AnyFilePath(string path)
    {
      _path = path;
    }

    internal AnyFilePath(AnyDirectoryPath left, FileName right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {
    }

    public AnyFilePath(AnyDirectoryPath left, RelativeFilePath right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {
    }

    public bool Equals(AnyFilePath other)
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
      return Equals((AnyFilePath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyFilePath left, AnyFilePath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyFilePath left, AnyFilePath right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      return _path;
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    public static AnyFilePath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "Path cannot be empty");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");
      Asserts.DoesNotContainInvalidChars(path);

      return new AnyFilePath(path);
    }

    public bool Has(FileExtension extensionValue)
    {
      return FileName().Has(extensionValue);
    }

    public FileName FileName()
    {
      return AtmaFileSystem.FileName.Value(Path.GetFileName(_path));
    }

    public Maybe<AnyDirectoryPath> ParentDirectory() //bug allow file names only, but put Maybe<T> here!!
    {
      var directoryName = Path.GetDirectoryName(_path);
      if (directoryName != string.Empty)
      {
        return AnyDirectoryPath.Value(directoryName).Just();
      }
      else
      {
        return Maybe<AnyDirectoryPath>.Nothing;
      }
    }

    public FileInfo Info()
    {
      return new FileInfo(_path);
    }

    public AnyFilePath ChangeExtensionTo(FileExtension value)
    {
      return new AnyFilePath(Path.ChangeExtension(_path, value.ToString()));
    }

    public bool ShallowEquals(AnyFilePath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public int CompareTo(AnyFilePath other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object obj)
    {
      if (ReferenceEquals(null, obj)) return 1;
      if (ReferenceEquals(this, obj)) return 0;
      return obj is AnyFilePath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(AnyFilePath)}");
    }

    public static bool operator <(AnyFilePath left, AnyFilePath right)
    {
      return Comparer<AnyFilePath>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(AnyFilePath left, AnyFilePath right)
    {
      return Comparer<AnyFilePath>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(AnyFilePath left, AnyFilePath right)
    {
      return Comparer<AnyFilePath>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(AnyFilePath left, AnyFilePath right)
    {
      return Comparer<AnyFilePath>.Default.Compare(left, right) >= 0;
    }
  }

/* TODO missing methods:
AnyPath:
  <> FileName()
  <> Parent()

*/
}