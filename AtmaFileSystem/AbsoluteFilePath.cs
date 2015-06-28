using System;
using AtmaFileSystem.Assertions;
using Pri.LongPath;


namespace AtmaFileSystem
{
  public class AbsoluteFilePath : IEquatable<AbsoluteFilePath>
  {
    private readonly string _path;

    // ReSharper disable once MemberCanBePrivate.Global
    internal AbsoluteFilePath(string path)
    {
      _path = path;
    }

    internal AbsoluteFilePath(AbsoluteDirectoryPath dirPath, FileName fileName)
      : this(Combine(dirPath, fileName))
    {

    }

    internal AbsoluteFilePath(AbsoluteDirectoryPath dirPath, RelativeFilePath relativeFilePath)
      : this(Combine(dirPath, relativeFilePath))
    {
      
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }


    public static AbsoluteFilePath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.Rooted(path, path + " is not an absolute path");

      return new AbsoluteFilePath(path);
    }

    public AbsoluteDirectoryPath ParentDirectory()
    {
      return new AbsoluteDirectoryPath(Path.GetDirectoryName(_path));
    }

    public FileInfo Info()
    {
      return new FileInfo(_path);
    }

    public FileName FileName()
    {
      return new FileName(Path.GetFileName(_path));
    }

    public AbsoluteDirectoryPath Root()
    {
      return new AbsoluteDirectoryPath(Path.GetPathRoot(_path));
    }

    public AnyFilePath AsAnyFilePath()
    {
      return new AnyFilePath(_path);
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

    public bool Equals(AbsoluteFilePath other)
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
      return Equals((AbsoluteFilePath)obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(AbsoluteFilePath left, AbsoluteFilePath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AbsoluteFilePath left, AbsoluteFilePath right)
    {
      return !Equals(left, right);
    }


    #endregion

    public bool Has(FileExtension extensionValue)
    {
      return FileName().Has(extensionValue);
    }

    public AbsoluteFilePath ChangeExtensionTo(FileExtension value)
    {
      return new AbsoluteFilePath(Path.ChangeExtension(_path, value.ToString()));
    }
  }
}