﻿using System;

namespace AtmaFileSystem
{
  public class DirectoryName : IEquatable<DirectoryName>
  {
    private readonly string _directoryName;

    internal DirectoryName(string directoryName)
    {
      _directoryName = directoryName;
    }

    public override string ToString()
    {
      return _directoryName;
    }

    public static DirectoryName Value(string value) //bug validation
    {
      DirectoryNameAssert.NotNull(value);
      DirectoryNameAssert.NotEmpty(value);
      DirectoryNameAssert.Valid(value);
      return new DirectoryName(value);
    }


    public static RelativeDirectoryPath operator+(DirectoryName dir, DirectoryName subdir)
    {
     return new RelativeDirectoryPath(dir, subdir); 
    }

    public static RelativeDirectoryPath operator +(DirectoryName dir, RelativeDirectoryPath subdirs)
    {
      return new RelativeDirectoryPath(dir, subdirs);
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
  }

  public static class DirectoryNameAssert
  {
    public static void NotNull(string value)
    {
      //bug implement
    }

    public static void NotEmpty(string value)
    {
      //bug implement
    }

    public static void Valid(string value)
    {
      //bug implement
    }
  }
}