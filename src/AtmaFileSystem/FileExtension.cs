using System;
using System.Collections.Generic;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public sealed class FileExtension
    : IEquatable<FileExtension>, 
      IEquatableAccordingToFileSystem<FileExtension>,
      IComparable<FileExtension>, IComparable
  {
    private readonly string _extension;

    internal FileExtension(string extension)
    {
      this._extension = extension;
    }

    public bool ShallowEquals(FileExtension other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public bool Equals(FileExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_extension, other._extension, StringComparison.InvariantCulture);
    }

    public override string ToString()
    {
      return _extension;
    }

    public static FileExtension Value(string extensionString)
    {
      Asserts.NotNull(extensionString, "extensionString");
      Asserts.NotEmpty(extensionString, "Tried to create an extension with empty value");
      Asserts.ConsistsSolelyOfExtension(extensionString, "Invalid extension " + extensionString);

      return new FileExtension(extensionString);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileExtension) obj);
    }

    public override int GetHashCode()
    {
      return (_extension != null ? _extension.GetHashCode() : 0);
    }

    public static bool operator ==(FileExtension left, FileExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileExtension left, FileExtension right)
    {
      return !Equals(left, right);
    }

    public int CompareTo(FileExtension other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return string.Compare(_extension, other._extension, StringComparison.InvariantCulture);
    }

    public int CompareTo(object obj)
    {
      if (ReferenceEquals(null, obj)) return 1;
      if (ReferenceEquals(this, obj)) return 0;
      return obj is FileExtension other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(FileExtension)}");
    }

    public static bool operator <(FileExtension left, FileExtension right)
    {
      return Comparer<FileExtension>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(FileExtension left, FileExtension right)
    {
      return Comparer<FileExtension>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(FileExtension left, FileExtension right)
    {
      return Comparer<FileExtension>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(FileExtension left, FileExtension right)
    {
      return Comparer<FileExtension>.Default.Compare(left, right) >= 0;
    }
  }
}