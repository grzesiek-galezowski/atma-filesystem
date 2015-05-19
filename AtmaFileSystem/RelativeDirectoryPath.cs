using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativeDirectoryPath : IEquatable<RelativeDirectoryPath>
  {
    public static RelativeDirectoryPath Value(string relativePath)
    {
      RelativeDirectoryPathAssert.NotNull(relativePath);
      RelativeDirectoryPathAssert.NotEmpty(relativePath);
      RelativeDirectoryPathAssert.Valid(relativePath);

      return new RelativeDirectoryPath(relativePath);
    }

    private readonly string _relativePath;

    public RelativeDirectoryPath(DirectoryName dir, DirectoryName subdir)
      : this(Path.Combine(dir.ToString(), subdir.ToString()))
    {
      
    }

    public RelativeDirectoryPath(RelativeDirectoryPath relativePath, DirectoryName dirName)
      : this(Path.Combine(relativePath.ToString(), dirName.ToString()))
    {
      
    }

    public RelativeDirectoryPath(DirectoryName relativePath, RelativeDirectoryPath dirName)
      : this(Path.Combine(relativePath.ToString(), dirName.ToString()))
    {
      
    }

    internal RelativeDirectoryPath(string relativePath)
    {
      _relativePath = relativePath;
    }

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

    public static RelativeDirectoryPath operator +(RelativeDirectoryPath path, DirectoryName dirName)
    {
      return new RelativeDirectoryPath(path, dirName);
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
  }
}