using System;
using AtmaFileSystem.Assertions;
using System.IO;
using AtmaFileSystem.InternalInterfaces;
using Functional.Maybe;
using Functional.Maybe.Just;

namespace AtmaFileSystem
{
  public sealed class FileName : 
    IEquatable<FileName>, 
    IEquatableAccordingToFileSystem<FileName>,
    IExtensionChangable<FileName>
  {
    private readonly string _path;

    internal FileName(string path)
    {
      _path = path;
    }

    public FileName(FileNameWithoutExtension nameWithoutExtension, FileExtension extension)
      : this(nameWithoutExtension.ToString() + extension.ToString())
    {
    }

    public bool ShallowEquals(FileName other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public bool Equals(FileName other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public static FileName Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "File name should not be empty");
      Asserts.ConsistsSolelyOfFileName(path);

      return new FileName(path);
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
      return Equals((FileName) obj);
    }

    public override int GetHashCode()
    {
      return _path.GetHashCode();
    }

    public static bool operator ==(FileName left, FileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(FileName left, FileName right)
    {
      return !Equals(left, right);
    }

    public Maybe<FileExtension> Extension()
    {
      var extension = Path.GetExtension(_path);
      return AsMaybe(extension);
    }

    private static Maybe<FileExtension> AsMaybe(string extension)
    {
      return extension == string.Empty ? Maybe<FileExtension>.Nothing : new FileExtension(extension).Just();
    }

    public FileNameWithoutExtension WithoutExtension()
    {
      return FileNameWithoutExtension.Value(Path.GetFileNameWithoutExtension(_path));
    }

    public bool Has(FileExtension extensionValue)
    {
      var extension = Extension();
      return extension.HasValue && extension.Value.Equals(extensionValue);
    }

    public FileName ChangeExtensionTo(FileExtension value)
    {
      return new FileName(Path.ChangeExtension(_path, value.ToString()));
    }
  }

  //TODO implement file system
}