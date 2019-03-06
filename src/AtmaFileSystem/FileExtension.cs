using System;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class FileExtension
    : IEquatable<FileExtension>, IEquatableAccordingToFileSystem<FileExtension>
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
      return string.Equals(_extension, other._extension);
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
  }
}