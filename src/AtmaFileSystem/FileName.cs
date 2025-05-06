using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using Core.Maybe;

namespace AtmaFileSystem;

public sealed class FileName : 
    IEquatable<FileName>, 
    IEquatableAccordingToFileSystem<FileName>,
    IExtensionChangable<FileName>,
    IComparable<FileName>, IComparable
{
    private readonly string _path;

    internal FileName(string path)
    {
        _path = path;
    }

    public FileName(FileNameWithoutExtension nameWithoutExtension, FileExtension extension)
        : this(nameWithoutExtension.ToString() + extension.ToString())
    {
    }

    public bool ShallowEquals(FileName other, FileSystemComparisonRules fileSystemComparisonRules)
    {
        return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public bool Equals(FileName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return string.Equals(_path, other._path);
    }

    public static FileName Value(string path)
    {
        Asserts.NotNull(path, nameof(path));
        Asserts.NotEmpty(path, "File name should not be empty");
        Asserts.ConsistsSolelyOfFileName(path);

        return new FileName(path);
    }

    public override string ToString()
    {
        return _path;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FileName) obj);
    }

    public override int GetHashCode()
    {
        return _path.GetHashCode();
    }

    public static bool operator ==(FileName? left, FileName? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(FileName? left, FileName? right)
    {
        return !Equals(left, right);
    }

    public Maybe<FileExtension> Extension()
    {
        var extension = Path.GetExtension(_path);
        return AsMaybe(extension);
    }

    private static Maybe<FileExtension> AsMaybe(string extension)
    {
        return extension == string.Empty ? Maybe<FileExtension>.Nothing : new FileExtension(extension).Just();
    }

    public static FileName Random() => Value(Path.GetRandomFileName());
    
    public FileNameWithoutExtension WithoutExtension()
    {
        return FileNameWithoutExtension.Value(Path.GetFileNameWithoutExtension(_path));
    }

    public bool Has(FileExtension extensionValue)
    {
        var extension = Extension();
        return extension.HasValue && extension.Value().Equals(extensionValue);
    }

    public FileName ChangeExtensionTo(FileExtension value)
    {
        return new FileName(Path.ChangeExtension(_path, value.ToString()));
    }

    public int CompareTo(FileName? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(_path, other._path, StringComparison.InvariantCulture);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;
        if (ReferenceEquals(this, obj)) return 0;
        return obj is FileName other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(FileName)}");
    }

    public static bool operator <(FileName left, FileName right)
    {
        return Comparer<FileName>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(FileName left, FileName right)
    {
        return Comparer<FileName>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(FileName left, FileName right)
    {
        return Comparer<FileName>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(FileName left, FileName right)
    {
        return Comparer<FileName>.Default.Compare(left, right) >= 0;
    }

    public static FileName operator +(FileName fileName, FileExtension fileExtension)
    {
      return new FileName(FileNameWithoutExtension.Value(fileName.ToString()), fileExtension);
    }

    public FileName AddExtension(string extensionString)
    {
      return this + FileExtension.Value(extensionString);
    }
}

//TODO implement file system