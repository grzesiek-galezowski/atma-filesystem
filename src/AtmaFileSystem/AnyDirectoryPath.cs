﻿using System;
using System.IO;
using AtmaFileSystem.Assertions;

namespace AtmaFileSystem
{
  public class AnyDirectoryPath : IEquatable<AnyDirectoryPath>, IEquatableAccordingToFileSystem<AnyDirectoryPath>
  {
    private readonly string _path;

    private AnyDirectoryPath(AnyDirectoryPath left, DirectoryName right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {
    }

    private AnyDirectoryPath(AnyDirectoryPath left, RelativeDirectoryPath right)
      : this(Path.Combine(left.ToString(), right.ToString()))
    {
    }

    internal AnyDirectoryPath(string path)
    {
      _path = path;
    }

    public bool Equals(AnyDirectoryPath other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(_path, other._path);
    }

    public bool ShallowEquals(AnyDirectoryPath other, FileSystemComparisonRules fileSystemComparisonRules)
    {
      return fileSystemComparisonRules.ArePathStringsEqual(ToString(), other.ToString());
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((AnyDirectoryPath) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(AnyDirectoryPath left, AnyDirectoryPath right)
    {
      return !Equals(left, right);
    }

    public static AnyFilePath operator +(AnyDirectoryPath left, FileName right)
    {
      return new AnyFilePath(left, right);
    }

    public static AnyDirectoryPath operator +(AnyDirectoryPath left, DirectoryName right)
    {
      return new AnyDirectoryPath(left, right);
    }

    public static AnyDirectoryPath operator +(AnyDirectoryPath left, RelativeDirectoryPath right)
    {
      return new AnyDirectoryPath(left, right);
    }

    public static AnyFilePath operator +(AnyDirectoryPath left, RelativeFilePath right)
    {
      return new AnyFilePath(left, right);
    }

    public override string ToString()
    {
      return _path;
    }

    public AnyPath AsAnyPath()
    {
      return new AnyPath(_path);
    }

    public static AnyDirectoryPath Value(string path)
    {
      Asserts.NotNull(path, "path");
      Asserts.NotEmpty(path, "Path cannot be empty");
      Asserts.DirectoryPathValid(path, "The path value " + path + " is invalid");
      Asserts.DoesNotContainInvalidChars(path);

      return new AnyDirectoryPath(path);
    }

    public DirectoryName DirectoryName()
    {
      return AtmaFileSystem.DirectoryName.Value(new DirectoryInfo(_path).Name);
    }

    public Maybe<AnyDirectoryPath> ParentDirectory()
    {
      var directoryName = new DirectoryInfo(_path).Parent;
      return AsMaybe(directoryName);
    }

    private static Maybe<AnyDirectoryPath> AsMaybe(DirectoryInfo directoryName)
    {
      return directoryName != null ? Maybe.Wrap(Value(directoryName.FullName)) : null;
    }

    public DirectoryInfo Info()
    {
      return new DirectoryInfo(_path);
    }
  }
}