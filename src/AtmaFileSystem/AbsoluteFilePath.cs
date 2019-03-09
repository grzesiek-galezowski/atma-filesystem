using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using AtmaFileSystem.Assertions;
using Functional.Maybe;

namespace AtmaFileSystem
{
  public sealed class AbsoluteFilePath : IEquatable<AbsoluteFilePath>, IEquatableAccordingToFileSystem<AbsoluteFilePath>
  {
    private readonly string _path;

    // ReSharper disable once MemberCanBePrivate.Global
    internal AbsoluteFilePath(string path)
    {
      _path = Path.GetFullPath(path);
    }

    public static AbsoluteFilePath From(AbsoluteDirectoryPath dirPath, FileName fileName)
    {
      return new AbsoluteFilePath(Combine(dirPath, fileName));
    }

    public static AbsoluteFilePath From(AbsoluteDirectoryPath dirPath, RelativeFilePath relativeFilePath)
    {
      return new AbsoluteFilePath(Combine(dirPath, relativeFilePath));
    }

    public bool Equals(AbsoluteFilePath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public bool ShallowEquals(AbsoluteFilePath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(this.ToString(), other.ToString());
    }

    private static string Combine(object part1, object part2)
    {
      return Path.Combine(part1.ToString(), part2.ToString());
    }


    public static AbsoluteFilePath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.Rooted(path, path + " is not an absolute path");
      Asserts.DoesNotContainInvalidChars(path);

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

    public override string ToString()
    {
      return _path;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AbsoluteFilePath) obj);
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

    public bool Has(FileExtension extensionValue)
    {
      return FileName().Has(extensionValue);
    }

    public AbsoluteFilePath ChangeExtensionTo(FileExtension value)
    {
      return new AbsoluteFilePath(Path.ChangeExtension(_path, value.ToString()));
    }

    public Maybe<AbsoluteDirectoryPath> FragmentEndingOnLast(DirectoryName directoryName)
    {
      return this.ParentDirectory().FragmentEndingOnLast(directoryName);
    }
  }
}

internal interface X
{
  void Lol();
}

public class Y : X
{
  public void Lol()
  {
    throw new NotImplementedException();
  }
}