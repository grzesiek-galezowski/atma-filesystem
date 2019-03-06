using System;
using AtmaFileSystem.Assertions;
using System.IO;

namespace AtmaFileSystem
{
  public class AnyPath
    : IEquatable<AnyPath>, IEquatableAccordingToFileSystem<AnyPath>
  {
    private readonly string _path;

    internal AnyPath(string path)
    {
      _path = path;
    }

    public bool Equals(AnyPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public bool ShallowEquals(AnyPath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AnyPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyPath left, AnyPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyPath left, AnyPath right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      return _path;
    }

    public static AnyPath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "Path cannot be empty");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");
      Asserts.DoesNotContainInvalidChars(path);

      return new AnyPath(path);
    }

    public Maybe<AnyDirectoryPath> ParentDirectory()
    {
      var directoryName = Path.GetDirectoryName(_path);
      if (directoryName == string.Empty)
      {
        return Maybe<AnyDirectoryPath>.Not;
      }
      return Maybe.Wrap(AnyDirectoryPath.Value(directoryName));
    }
  }
}