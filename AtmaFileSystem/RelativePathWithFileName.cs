using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativePathWithFileName : IEquatable<RelativePathWithFileName>
  {

    private readonly string _path;

    public RelativePathWithFileName(RelativeDirectoryPath relativeDirectoryPath, FileName fileName)
    {
      _path = Path.Combine(relativeDirectoryPath.ToString(), fileName.ToString());
    }

    internal RelativePathWithFileName(string pathString)
    {
      _path = pathString;
    }

    public RelativePathWithFileName(DirectoryName directoryName, FileName fileName)
      : this(Path.Combine(directoryName.ToString(), fileName.ToString()))
    {
      
    }

    public RelativePathWithFileName(DirectoryName directoryName, RelativePathWithFileName pathWithFileName)
      : this(Path.Combine(directoryName.ToString(), pathWithFileName.ToString()))
    {

    }

    public RelativePathWithFileName(RelativeDirectoryPath relativeDirectoryPath, RelativePathWithFileName relativePathWithFileName)
      : this(Combine(relativeDirectoryPath, relativePathWithFileName))
    {
      
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }

    public static RelativePathWithFileName From(RelativeDirectoryPath dirPath, FileName fileName)
    {
      return new RelativePathWithFileName(dirPath, fileName);
    }

    public static RelativePathWithFileName From(DirectoryName dir, FileName fileName)
    {
      return new RelativePathWithFileName(dir, fileName);
    }

    public static RelativePathWithFileName From(DirectoryName dir, RelativePathWithFileName pathWithFileName)
    {
      return new RelativePathWithFileName(dir, pathWithFileName);
    }


    public RelativeDirectoryPath Directory()
    {
      return new RelativeDirectoryPath(Path.GetDirectoryName(_path));
    }

    public FileName FileName()
    {
      return new FileName(Path.GetFileName(_path));
    }

    public static RelativePathWithFileName Value(string path)
    {
      RelativePathWithFileNameAssertions.NotNull(path);
      RelativePathWithFileNameAssertions.NotEmpty(path);
      RelativePathWithFileNameAssertions.Valid(path);
      return new RelativePathWithFileName(path);
    }

    public FileInfo Info()
    {
      return new FileInfo(_path);
    }

    public AnyPathWithFileName AsAnyPathWithFileName()
    {
      return new AnyPathWithFileName(_path);
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    #region Generated members

    public override string ToString()
    {
      return _path;
    }

    public bool Equals(RelativePathWithFileName other)
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
      return Equals((RelativePathWithFileName)obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(RelativePathWithFileName left, RelativePathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(RelativePathWithFileName left, RelativePathWithFileName right)
    {
      return !Equals(left, right);
    }
    #endregion
  }
}