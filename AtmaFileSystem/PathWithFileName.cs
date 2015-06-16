using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;


namespace AtmaFileSystem
{
  public class PathWithFileName : IEquatable<PathWithFileName>
  {
    private readonly string _path;

    // ReSharper disable once MemberCanBePrivate.Global
    internal PathWithFileName(string path)
    {
      _path = path;
    }

    public PathWithFileName(DirectoryPath dirPath, FileName fileName)
      : this(Combine(dirPath, fileName))
    {

    }

    public PathWithFileName(DirectoryPath dirPath, RelativePathWithFileName relativePath)
      : this(Combine(dirPath, relativePath))
    {
      
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }


    public static PathWithFileName Value(string path)
    {
      PathWithFileNameAssert.NotNull(path);
      PathWithFileNameAssert.Valid(path);

      return new PathWithFileName(path);
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


    #endregion
  }


  //TODO relative directory path, relative directory path with file name
}