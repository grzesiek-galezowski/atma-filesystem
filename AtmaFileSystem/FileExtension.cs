using System;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class FileExtension
    : IEquatable<FileExtension>
  {
    private readonly string _extension;

    public FileExtension(string extension)
    {
      this._extension = extension;
    }

    public override string ToString()
    {
      return _extension;
    }

    public static FileExtension Value(string extensionString)
    {
      if (extensionString == null)
      {
        throw new ArgumentException("Tried to create an extension with null value");
      }
      if (extensionString == string.Empty)
      {
        throw new ArgumentException("Tried to create an extension with empty value");
      }
      if (Path.GetExtension(extensionString) != extensionString)
      {
        throw new ArgumentException("Invalid extensionString " + extensionString ?? "null");
      }
      else
      {
        return new FileExtension(extensionString);
      }
    }

    public bool Equals(FileExtension other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_extension, other._extension);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FileExtension)obj);
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
