﻿using System;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public class RelativePathWithFileName : IEquatable<RelativePathWithFileName>
  {
    public bool Equals(RelativePathWithFileName other)
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
      return Equals((RelativePathWithFileName) obj);
    }

    public override int GetHashCode()
    {
      return (_path != null ? _path.GetHashCode() : 0);
    }

    public static bool operator ==(RelativePathWithFileName left, RelativePathWithFileName right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(RelativePathWithFileName left, RelativePathWithFileName right)
    {
      return !Equals(left, right);
    }

    private readonly string _path;

    public RelativePathWithFileName(RelativeDirectoryPath relativeDirectoryPath, FileName fileName)
    {
      _path = Path.Combine(relativeDirectoryPath.ToString(), fileName.ToString());
    }

    internal RelativePathWithFileName(string pathString)
    {
      _path = pathString;
    }

    public override string ToString()
    {
      return _path;
    }

    public static RelativePathWithFileName From(RelativeDirectoryPath dirPath, FileName fileName)
    {
      return new RelativePathWithFileName(dirPath, fileName);
    }

    public RelativeDirectoryPath Directory()
    {
      return new RelativeDirectoryPath(Path.GetDirectoryName(_path));
    }

    public FileName FileName()
    {
      return new FileName(Path.GetFileName(_path));
    }

    public static RelativePathWithFileName Value(string path)
    {
      if (path == null)
      {
        throw new ArgumentNullException("path");
      }
      if (Path.IsPathRooted(path))
      {
        throw new InvalidOperationException("Rooted paths are illegal, please pass a relative path");
      }
      return new RelativePathWithFileName(path);
    }

    public FileInfo Info()
    {
      return new FileInfo(_path);
    }
  }
}