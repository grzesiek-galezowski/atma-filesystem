using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativeDirectoryPath : IEquatable<RelativeDirectoryPath>
  {
    private readonly string _path;

    public static RelativeDirectoryPath Value(string relativePath)
    {
      Asserts.NotNull(relativePath, "path");
      Asserts.NotEmpty(relativePath, "relative path cannot be empty");
      Asserts.NotRooted(relativePath, "Expected relative path, but got " + relativePath);

      return new RelativeDirectoryPath(relativePath);
    }

    private RelativeDirectoryPath(RelativeDirectoryPath relativePath, DirectoryName dirName)
      : this(Combine(relativePath, dirName))
    {
      
    }

    private RelativeDirectoryPath(RelativeDirectoryPath relativeDirectoryPath, RelativeDirectoryPath relativeDirectoryPath2)
      : this(Combine(relativeDirectoryPath, relativeDirectoryPath2))
    {

    }

    internal RelativeDirectoryPath(string relativePath)
    {
      _path = relativePath;
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }

    public static RelativeDirectoryPath operator+(RelativeDirectoryPath path, DirectoryName dirName)
    {
      return new RelativeDirectoryPath(path, dirName);
    }

    public static RelativeFilePath operator +(RelativeDirectoryPath path, FileName dirName)
    {
      return new RelativeFilePath(path, dirName);
    }

    public static RelativeDirectoryPath operator +(RelativeDirectoryPath path, RelativeDirectoryPath path2)
    {
      return new RelativeDirectoryPath(path, path2);
    }

    public static RelativeFilePath operator +(RelativeDirectoryPath path, RelativeFilePath relativeFilePath)
    {
      return new RelativeFilePath(path, relativeFilePath);
    }


    public Maybe<RelativeDirectoryPath> ParentDirectory()
    {
      var directoryName = Path.GetDirectoryName(_path);
      if (directoryName == string.Empty)
      {
        return Maybe<RelativeDirectoryPath>.Not;
      }
      return Maybe.Wrap(Value(directoryName));
    }

    public DirectoryInfo Info()
    {
      return new DirectoryInfo(_path);
    }

    public AnyDirectoryPath AsAnyDirectoryPath()
    {
      return new AnyDirectoryPath(_path);
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

    public bool Equals(RelativeDirectoryPath other)
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
      return Equals((RelativeDirectoryPath)obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(RelativeDirectoryPath left, RelativeDirectoryPath right)
    {
      return !Equals(left, right);
    }

    #endregion

    public DirectoryName DirectoryName()
    {
      return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_path).Name);
    }
  }
}