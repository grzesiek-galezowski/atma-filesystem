using System;

namespace AtmaFileSystem
{
  public sealed class FileNameWithoutExtension : IEquatable<FileNameWithoutExtension>,
    IEquatableAccordingToFileSystem<FileNameWithoutExtension>
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
  }
}