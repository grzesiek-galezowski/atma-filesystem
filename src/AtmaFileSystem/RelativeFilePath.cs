using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using AtmaFileSystem.Internals;
using Core.Maybe;

namespace AtmaFileSystem;

public sealed class RelativeFilePath : 
    IEquatable<RelativeFilePath>, 
    IEquatableAccordingToFileSystem<RelativeFilePath>, 
    IFilePath<RelativeFilePath>, 
    IExtensionChangable<RelativeFilePath>,
    IComparable<RelativeFilePath>, IComparable
{
    private readonly string _path;


    internal RelativeFilePath(string pathString)
    {
        _path = pathString.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
    }

    public static RelativeFilePath From(RelativeDirectoryPath relativeDirectoryPath, FileName fileName)
    {
        return new RelativeFilePath(Path.Join(relativeDirectoryPath.ToString(), fileName.ToString()));
    }

    public static RelativeFilePath From(RelativeDirectoryPath relativeDirectoryPath, RelativeFilePath relativeFilePath)
    {
        return new RelativeFilePath(PathAlgorithms.Combine(relativeDirectoryPath, relativeFilePath));
    }

    public Maybe<RelativeDirectoryPath> ParentDirectory()
    {
        var directoryName = Path.GetDirectoryName(_path);
        if (directoryName != string.Empty)
        {
            return new RelativeDirectoryPath(directoryName).Just();
        }
        else
        {
            return Maybe<RelativeDirectoryPath>.Nothing;
        }
    }

    public FileName FileName()
    {
        return new FileName(Path.GetFileName(_path));
    }

    public static RelativeFilePath Value(string path)
    {
        Asserts.NotNull(path, nameof(path));
        Asserts.NotEmpty(path, ExceptionMessages.PathCannotBeAnEmptyString);
        Asserts.NotFullyQualified(path, ExceptionMessages.RootedPathsAreIllegalPleasePassARelativePath);
        return new RelativeFilePath(path);
    }

    public FileInfo Info()
    {
        return new FileInfo(_path);
    }

    public AnyFilePath AsAnyFilePath()
    {
        return new AnyFilePath(_path);
    }

    public AnyPath AsAnyPath()
    {
        return new AnyPath(_path);
    }

    public bool Has(FileExtension extensionValue)
    {
        return FileName().Has(extensionValue);
    }

    public RelativeFilePath ChangeExtensionTo(FileExtension value)
    {
        return new RelativeFilePath(Path.ChangeExtension(_path, value.ToString()));
    }

    public bool ShallowEquals(RelativeFilePath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
        return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public override string ToString()
    {
        return _path;
    }

    public bool Equals(RelativeFilePath? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(_path, other._path, StringComparison.InvariantCulture);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RelativeFilePath) obj);
    }

    public override int GetHashCode()
    {
        return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(RelativeFilePath? left, RelativeFilePath? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(RelativeFilePath? left, RelativeFilePath? right)
    {
        return !Equals(left, right);
    }

    public int CompareTo(RelativeFilePath? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is RelativeFilePath other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(RelativeFilePath)}");
    }

    public static bool operator <(RelativeFilePath left, RelativeFilePath right)
    {
        return Comparer<RelativeFilePath>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(RelativeFilePath left, RelativeFilePath right)
    {
        return Comparer<RelativeFilePath>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(RelativeFilePath left, RelativeFilePath right)
    {
        return Comparer<RelativeFilePath>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(RelativeFilePath left, RelativeFilePath right)
    {
        return Comparer<RelativeFilePath>.Default.Compare(left, right) >= 0;
    }

    public Maybe<RelativeDirectoryPath> FindCommonRelativeDirectoryPathWith(RelativeFilePath path2)
    {
        return from thisFileDir in this.ParentDirectory()
            from otherFileDir in path2.ParentDirectory()
            select thisFileDir.FindCommonDirectoryPathWith(otherFileDir);
    }

    public bool StartsWith(RelativeDirectoryPath currentPathValue)
    {
        return PathAlgorithms.StartsWith(this, currentPathValue);
    }

    public Maybe<RelativeDirectoryPath> TrimStart(RelativeDirectoryPath startPath)
    {
        return PathAlgorithms.TrimStart(_path, startPath.ToString())
            .Select(s => new RelativeDirectoryPath(s));
    }
}