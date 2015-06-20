using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativeDirectoryPath : IEquatable<RelativeDirectoryPath>
  {
    private readonly string _relativePath;

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
      _relativePath = relativePath;
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }

    public static RelativeDirectoryPath operator+(RelativeDirectoryPath path, DirectoryName dirName)
    {
      return new RelativeDirectoryPath(path, dirName);
    }

    public static RelativePathWithFileName operator +(RelativeDirectoryPath path, FileName dirName)
    {
      return new RelativePathWithFileName(path, dirName);
    }

    public static RelativeDirectoryPath operator +(RelativeDirectoryPath path, RelativeDirectoryPath path2)
    {
      return new RelativeDirectoryPath(path, path2);
    }

    public static RelativePathWithFileName operator +(RelativeDirectoryPath path, RelativePathWithFileName relativePathWithFileName)
    {
      return new RelativePathWithFileName(path, relativePathWithFileName);
    }


    public Maybe<RelativeDirectoryPath> Parent()
    {
      var directoryName = Path.GetDirectoryName(_relativePath);
      if (directoryName == string.Empty)
      {
        return Maybe<RelativeDirectoryPath>.Not;
      }
      return Maybe.Wrap(Value(directoryName));
    }

    public DirectoryInfo Info()
    {
      return new DirectoryInfo(_relativePath);
    }

    public AnyDirectoryPath AsAnyDirectoryPath()
    {
      return new AnyDirectoryPath(_relativePath);
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_relativePath);
    }


    #region Generated members

    public override string ToString()
    {
      return _relativePath; 
    }

    public bool Equals(RelativeDirectoryPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_relativePath, other._relativePath);
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
      return (_relativePath != null ? _relativePath.GetHashCode() : 0);
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
      return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_relativePath).Name);
    }
  }
}