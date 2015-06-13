using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;


namespace AtmaFileSystem
{
  public class PathWithFileName : IEquatable<PathWithFileName>
  {
    private readonly string _path;

    internal PathWithFileName(string path)
    {
      _path = path;
    }

    public PathWithFileName(DirectoryPath dirPath, FileName fileName)
    {
      _path = Path.Combine(dirPath.ToString(), fileName.ToString());
    }

    public static PathWithFileName Value(string path)
    {
      PathWithFileNameAssert.NotNull(path);
      PathWithFileNameAssert.Valid(path);

      return new PathWithFileName(path);
    }

    public override string ToString()
    {
      return _path;
    }

    public static PathWithFileName From(DirectoryPath dirPath, FileName fileName)
    {
      return new PathWithFileName(dirPath, fileName);
    }

    public DirectoryPath Directory()
    {
      return new DirectoryPath(Path.GetDirectoryName(_path));
    }

    public FileInfo Info()
    {
      return new FileInfo(_path);
    }

    public FileName FileName()
    {
      return new FileName(Path.GetFileName(_path));
    }

    public DirectoryPath Root()
    {
      return new DirectoryPath(Path.GetPathRoot(_path));
    }
    public bool Equals(PathWithFileName other)
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
      return Equals((PathWithFileName)obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(PathWithFileName left, PathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(PathWithFileName left, PathWithFileName right)
    {
      return !Equals(left, right);
    }
  }


  //TODO relative directory path, relative directory path with file name
}