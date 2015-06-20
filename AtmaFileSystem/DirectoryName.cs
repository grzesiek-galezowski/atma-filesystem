using System;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class DirectoryName : IEquatable<DirectoryName>
  {
    private readonly string _directoryName;

    internal DirectoryName(string directoryName)
    {
      _directoryName = directoryName;
    }

    public static DirectoryName Value(string directoryName)
    {
      Asserts.NotNull(directoryName, "directoryName");
      Asserts.NotEmpty(directoryName, "directory name cannot be empty");
      Asserts.ValidDirectoryName(directoryName, "The value " + directoryName + " does not constitute a valid directory name");
      return new DirectoryName(directoryName);
    }

    //operators cannot return relative or non relative because dir name can be root as well



    #region Generated members
    public override string ToString()
    {
      return _directoryName;
    }

    public bool Equals(DirectoryName other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_directoryName, other._directoryName);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DirectoryName)obj);
    }

    public override int GetHashCode()
    {
      return (_directoryName != null ? _directoryName.GetHashCode() : 0);
    }

    public static bool operator ==(DirectoryName left, DirectoryName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(DirectoryName left, DirectoryName right)
    {
      return !Equals(left, right);
    }
    #endregion
  }
}