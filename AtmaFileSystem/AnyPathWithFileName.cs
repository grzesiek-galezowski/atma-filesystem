using System;
using System.IO;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class AnyPathWithFileName
    : IEquatable<AnyPathWithFileName>
  {
    public bool Equals(AnyPathWithFileName other)
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
      return Equals((AnyPathWithFileName) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyPathWithFileName left, AnyPathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyPathWithFileName left, AnyPathWithFileName right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    internal AnyPathWithFileName(string path)
    {
      _path = path;
    }

    internal AnyPathWithFileName(AnyDirectoryPath left, FileName right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {
      
    }

    public AnyPathWithFileName(AnyDirectoryPath left, RelativePathWithFileName right)
    : this(Path.Combine(left.ToString(), right.ToString()))
    {
      
    }

    public override string ToString()
    {
      return _path;
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    public static AnyPathWithFileName Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "Path cannot be empty");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");
      Asserts.DoesNotConsistSolelyOfFileName(path, "Expected path not consisting solely of file name, but got " + path);

      return new AnyPathWithFileName(path);
    }

    public bool Has(FileExtension extensionValue)
    {
      return FileName().Has(extensionValue);
    }

    public FileName FileName()
    {
      return AtmaFileSystem.FileName.Value(Path.GetFileName(_path));
    }

    public AnyDirectoryPath Directory()
    {
      return AnyDirectoryPath.Value(Path.GetDirectoryName(_path));
    }
  }

/* TODO missing methods:
AnyPathWithFileName:
  Directory()
  Info()

AnyDirectoryPath:
  Parent()
  DirectoryName()
  Info()

AnyPath:
  <> FileName()
  <> Parent()
  Info -> FileSystemInfo

RelativeDirectoryPath:
  DirectoryName()
  Info()
*/
}