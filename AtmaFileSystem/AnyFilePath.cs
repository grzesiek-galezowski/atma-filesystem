using System;
using System.IO;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class AnyFilePath
    : IEquatable<AnyFilePath>
  {
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
      Asserts.DoesNotConsistSolelyOfFileName(path, "Expected path not consisting solely of file name, but got " + path);

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

    public AnyDirectoryPath Directory()
    {
      return AnyDirectoryPath.Value(Path.GetDirectoryName(_path));
    }

    public FileInfo Info()
    {
      return new FileInfo(_path);
    }
  }

/* TODO missing methods:
AnyDirectoryPath:
  Info()

AnyPath:
  <> FileName()
  <> Parent()
  Info -> FileSystemInfo

RelativeDirectoryPath:
  Info()
*/
}