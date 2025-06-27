using System;
using System.Collections.Generic;
using System.IO;
using AtmaFileSystem.Assertions;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using Core.Maybe;

namespace AtmaFileSystem;

//bug mixed/different path separators
public sealed class AnyFilePath
    : IEquatable<AnyFilePath>, 
        IEquatableAccordingToFileSystem<AnyFilePath>,
        IExtensionChangable<AnyFilePath>,
        IComparable<AnyFilePath>, 
        IComparable,
        IInternalFilePath<AnyFilePath>
{
    private readonly string _path;

    internal AnyFilePath(string path)
    {
        _path = path;
    }

    internal AnyFilePath(AnyDirectoryPath left, FileName right)
        : this(Path.Join(left.ToString(), right.ToString()))
    {
    }

    public AnyFilePath(AnyDirectoryPath left, RelativeFilePath right)
        : this(Path.Join(left.ToString(), right.ToString()))
    {
    }

    public bool Equals(AnyFilePath? other)
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
        return Equals((AnyFilePath) obj);
    }

    public override int GetHashCode()
    {
        return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyFilePath? left, AnyFilePath? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AnyFilePath? left, AnyFilePath? right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return _path;
    }

    public AnyPath AsAnyPath() => new(_path);

    public static AnyFilePath Value(string path)
    {
        Asserts.NotNull(path, nameof(path));
        Asserts.NotEmpty(path, ExceptionMessages.PathCannotBeAnEmptyString);
        Asserts.DirectoryPathValid(path);
        Asserts.DoesNotContainInvalidChars(path);

        return new AnyFilePath(PathAlgorithms.NormalizeSeparators(path));
    }

    public bool Has(FileExtension extensionValue)
    {
        return FileName().Has(extensionValue);
    }

    public FileName FileName()
    {
        return AtmaFileSystemPaths.FileName(Path.GetFileName(_path));
    }

    public Maybe<AnyDirectoryPath> ParentDirectory()
    {
        var directoryName = Path.GetDirectoryName(_path);
        if (!string.IsNullOrEmpty(directoryName))
        {
            return AnyDirectoryPath.Value(directoryName).Just();
        }
        else
        {
            return Maybe<AnyDirectoryPath>.Nothing;
        }
    }

    public FileInfo Info() => new(_path);

    public AnyFilePath ChangeExtensionTo(FileExtension value)
    {
        var pathWithNewExtension = Path.ChangeExtension(_path, value.ToString());
        return new AnyFilePath(pathWithNewExtension);
    }

    public bool ShallowEquals(AnyFilePath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
        return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public int CompareTo(AnyFilePath? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object? obj)
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

    public static AnyFilePath operator +(AnyFilePath path, FileExtension fileExtension)
    {
      return path.ParentDirectory().Select(x => x + (path.FileName() + fileExtension))
        .OrElse(new AnyFilePath((path.FileName() + fileExtension).ToString()));
    }

    public AnyFilePath AddExtension(string extensionString)
    {
      return this + FileExtension.Value(extensionString);
    }
}

/* TODO missing methods:
AnyPath:
  <> FileName()
  <> Parent()

*/