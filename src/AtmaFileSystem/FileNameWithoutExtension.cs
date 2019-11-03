using System;
using System.Collections.Generic;

namespace AtmaFileSystem
{
  public sealed class FileNameWithoutExtension : 
    IEquatable<FileNameWithoutExtension>,
    IEquatableAccordingToFileSystem<FileNameWithoutExtension>,
    IComparable<FileNameWithoutExtension>, IComparable
  {
    private readonly string _value;

    internal FileNameWithoutExtension(string value)
    {
      _value = value;
    }

    public bool Equals(FileNameWithoutExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_value, other._value);
    }


    public bool ShallowEquals(FileNameWithoutExtension other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public static FileNameWithoutExtension Value(string fileNameWithoutExtensionString)
    {
      return new FileNameWithoutExtension(fileNameWithoutExtensionString);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileNameWithoutExtension) obj);
    }

    public override int GetHashCode()
    {
      return _value.GetHashCode();
    }

    public static bool operator ==(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return !Equals(left, right);
    }

    public static FileName operator +(FileNameWithoutExtension nameWithoutExtension, FileExtension extension)
    {
      return new FileName(nameWithoutExtension, extension);
    }

    public override string ToString()
    {
      return _value;
    }

    public FileName AsFileName()
    {
      return new FileName(_value);
    }

    public int CompareTo(FileNameWithoutExtension other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;
      return string.Compare(_value, other._value, StringComparison.InvariantCulture);
    }

    public int CompareTo(object obj)
    {
      if (ReferenceEquals(null, obj)) return 1;
      if (ReferenceEquals(this, obj)) return 0;
      return obj is FileNameWithoutExtension other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(FileNameWithoutExtension)}");
    }

    public static bool operator <(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Comparer<FileNameWithoutExtension>.Default.Compare(left, right) < 0;
    }

    public static bool operator >(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Comparer<FileNameWithoutExtension>.Default.Compare(left, right) > 0;
    }

    public static bool operator <=(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Comparer<FileNameWithoutExtension>.Default.Compare(left, right) <= 0;
    }

    public static bool operator >=(FileNameWithoutExtension left, FileNameWithoutExtension right)
    {
      return Comparer<FileNameWithoutExtension>.Default.Compare(left, right) >= 0;
    }
  }
}