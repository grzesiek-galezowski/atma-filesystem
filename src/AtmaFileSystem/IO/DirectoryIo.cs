﻿using System;
using System.IO;

namespace AtmaFileSystem.IO
{
  public static class DirectoryIo
  {
    public static DirectoryInfo CreateDirectory(this AbsoluteDirectoryPath path)
    {
      return Directory.CreateDirectory(path.ToString());
    }
    public static DirectoryInfo CreateDirectory(this RelativeDirectoryPath path)
    {
      return Directory.CreateDirectory(path.ToString());
    }
    public static DirectoryInfo CreateDirectory(this AnyDirectoryPath path)
    {
      return Directory.CreateDirectory(path.ToString());
    }

    public static bool Exists(this AbsoluteDirectoryPath path)
    {
      return Directory.Exists(path.ToString());
    }
    public static bool Exists(this RelativeDirectoryPath path)
    {
      return Directory.Exists(path.ToString());
    }
    public static bool Exists(this AnyDirectoryPath path)
    {
      return Directory.Exists(path.ToString());
    }

    public static void SetCreationTime(this AbsoluteDirectoryPath path, DateTime creationTime)
    {
      Directory.SetCreationTime(path.ToString(), creationTime);
    }
    public static void SetCreationTime(this RelativeDirectoryPath path, DateTime creationTime)
    {
      Directory.SetCreationTime(path.ToString(), creationTime);
    }
    public static void SetCreationTime(this AnyDirectoryPath path, DateTime creationTime)
    {
      Directory.SetCreationTime(path.ToString(), creationTime);
    }

    public static void SetCreationTimeUtc(this AbsoluteDirectoryPath path, DateTime creationTimeUtc)
    {
      Directory.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
    }
    public static void SetCreationTimeUtc(this RelativeDirectoryPath path, DateTime creationTimeUtc)
    {
      Directory.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
    }
    public static void SetCreationTimeUtc(this AnyDirectoryPath path, DateTime creationTimeUtc)
    {
      Directory.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
    }

    public static DateTime GetCreationTime(this AbsoluteDirectoryPath path)
    {
      return Directory.GetCreationTime(path.ToString());
    }
    public static DateTime GetCreationTime(this RelativeDirectoryPath path)
    {
      return Directory.GetCreationTime(path.ToString());
    }
    public static DateTime GetCreationTime(this AnyDirectoryPath path)
    {
      return Directory.GetCreationTime(path.ToString());
    }

    public static DateTime GetCreationTimeUtc(this AbsoluteDirectoryPath path)
    {
      return Directory.GetCreationTimeUtc(path.ToString());
    }
    public static DateTime GetCreationTimeUtc(this RelativeDirectoryPath path)
    {
      return Directory.GetCreationTimeUtc(path.ToString());
    }
    public static DateTime GetCreationTimeUtc(this AnyDirectoryPath path)
    {
      return Directory.GetCreationTimeUtc(path.ToString());
    }

    public static void SetLastWriteTime(this AbsoluteDirectoryPath path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTime(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTime(this RelativeDirectoryPath path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTime(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTime(this AnyDirectoryPath path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTime(path.ToString(), lastWriteTime);
    }

    public static void SetLastWriteTimeUtc(this AbsoluteDirectoryPath path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTimeUtc(this RelativeDirectoryPath path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTimeUtc(this AnyDirectoryPath path, DateTime lastWriteTime)
    {
      Directory.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
    }

    public static DateTime GetLastWriteTime(this AbsoluteDirectoryPath path)
    {
      return Directory.GetLastWriteTime(path.ToString());
    }
    public static DateTime GetLastWriteTime(this RelativeDirectoryPath path)
    {
      return Directory.GetLastWriteTime(path.ToString());
    }
    public static DateTime GetLastWriteTime(this AnyDirectoryPath path)
    {
      return Directory.GetLastWriteTime(path.ToString());
    }
    
    public static DateTime GetLastWriteTimeUtc(this AbsoluteDirectoryPath path)
    {
      return Directory.GetLastWriteTimeUtc(path.ToString());
    }
    public static DateTime GetLastWriteTimeUtc(this RelativeDirectoryPath path)
    {
      return Directory.GetLastWriteTimeUtc(path.ToString());
    }
    public static DateTime GetLastWriteTimeUtc(this AnyDirectoryPath path)
    {
      return Directory.GetLastWriteTimeUtc(path.ToString());
    }

    public static void SetLastAccessTime(this AbsoluteDirectoryPath path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTime(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTime(this RelativeDirectoryPath path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTime(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTime(this AnyDirectoryPath path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTime(path.ToString(), lastAccessTime);
    }
    
    public static void SetLastAccessTimeUtc(this AbsoluteDirectoryPath path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTimeUtc(this RelativeDirectoryPath path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTimeUtc(this AnyDirectoryPath path, DateTime lastAccessTime)
    {
      Directory.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
    }

    public static DateTime GetLastAccessTime(this AbsoluteDirectoryPath path)
    {
      return Directory.GetLastAccessTime(path.ToString());
    }
    public static DateTime GetLastAccessTime(this RelativeDirectoryPath path)
    {
      return Directory.GetLastAccessTime(path.ToString());
    }
    public static DateTime GetLastAccessTime(this AnyDirectoryPath path)
    {
      return Directory.GetLastAccessTime(path.ToString());
    }

    public static DateTime GetLastAccessTimeUtc(this AbsoluteDirectoryPath path)
    {
      return Directory.GetLastAccessTimeUtc(path.ToString());
    }
    public static DateTime GetLastAccessTimeUtc(this RelativeDirectoryPath path)
    {
      return Directory.GetLastAccessTimeUtc(path.ToString());
    }
    public static DateTime GetLastAccessTimeUtc(this AnyDirectoryPath path)
    {
      return Directory.GetLastAccessTimeUtc(path.ToString());
    }

    public static AbsoluteDirectoryPath GetCurrentDirectory() 
      => AtmaFileSystemPaths.AbsoluteDirectoryPath(Environment.CurrentDirectory);

    public static void SetCurrentDirectory(this AbsoluteDirectoryPath path)
    {
      Directory.SetCurrentDirectory(path.ToString());
    }
    public static void SetCurrentDirectory(this RelativeDirectoryPath path)
    {
      Directory.SetCurrentDirectory(path.ToString());
    }
    public static void SetCurrentDirectory(this AnyDirectoryPath path)
    {
      Directory.SetCurrentDirectory(path.ToString());
    }

    public static void Delete(this AbsoluteDirectoryPath path) => Directory.Delete(path.ToString());

    public static void Delete(this RelativeDirectoryPath path) => Directory.Delete(path.ToString());

    public static void Delete(this AnyDirectoryPath path) => Directory.Delete(path.ToString());

    public static void Delete(this AbsoluteDirectoryPath path, bool recursive)
    {
      Directory.Delete(path.ToString(), recursive);
    }
    public static void Delete(this RelativeDirectoryPath path, bool recursive)
    {
      Directory.Delete(path.ToString(), recursive);
    }
    public static void Delete(this AnyDirectoryPath path, bool recursive)
    {
      Directory.Delete(path.ToString(), recursive);
    }
  }
}