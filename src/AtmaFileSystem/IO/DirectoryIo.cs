using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using AtmaFileSystem.InternalInterfaces;

namespace AtmaFileSystem.IO;

public static class DirectoryIo
{
  public static DirectoryInfo Create(this IDirectoryPath path)
  {
    return Directory.CreateDirectory(path.ToString());
  }

  public static bool Exists(this IDirectoryPath path)
  {
    return Directory.Exists(path.ToString());
  }

  public static void SetCreationTime(this IDirectoryPath path, DateTime creationTime)
  {
    Directory.SetCreationTime(path.ToString(), creationTime);
  }

  public static void SetCreationTimeUtc(this IDirectoryPath path, DateTime creationTimeUtc)
  {
    Directory.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
  }

  public static DateTime GetCreationTime(this IDirectoryPath path)
  {
    return Directory.GetCreationTime(path.ToString());
  }

  public static DateTime GetCreationTimeUtc(this IDirectoryPath path)
  {
    return Directory.GetCreationTimeUtc(path.ToString());
  }

  public static void SetLastWriteTime(this IDirectoryPath path, DateTime lastWriteTime)
  {
    Directory.SetLastWriteTime(path.ToString(), lastWriteTime);
  }

  public static void SetLastWriteTimeUtc(this IDirectoryPath path, DateTime lastWriteTime)
  {
    Directory.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
  }

  public static DateTime GetLastWriteTime(this IDirectoryPath path)
  {
    return Directory.GetLastWriteTime(path.ToString());
  }

  public static DateTime GetLastWriteTimeUtc(this IDirectoryPath path)
  {
    return Directory.GetLastWriteTimeUtc(path.ToString());
  }

  public static void SetLastAccessTime(this IDirectoryPath path, DateTime lastAccessTime)
  {
    Directory.SetLastAccessTime(path.ToString(), lastAccessTime);
  }

  public static void SetLastAccessTimeUtc(this IDirectoryPath path, DateTime lastAccessTime)
  {
    Directory.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
  }

  public static DateTime GetLastAccessTime(this IDirectoryPath path)
  {
    return Directory.GetLastAccessTime(path.ToString());
  }

  public static DateTime GetLastAccessTimeUtc(this IDirectoryPath path)
  {
    return Directory.GetLastAccessTimeUtc(path.ToString());
  }

  public static AbsoluteDirectoryPath GetCurrentDirectory()
    => AtmaFileSystemPaths.AbsoluteDirectoryPath(Environment.CurrentDirectory);

  public static void SetAsCurrentDirectory(this IDirectoryPath path)
  {
    Directory.SetCurrentDirectory(path.ToString());
  }

  public static void Delete(this IDirectoryPath path) => Directory.Delete(path.ToString());

  public static void Delete(this IDirectoryPath path, bool recursive)
  {
    Directory.Delete(path.ToString(), recursive);
  }

  public static ImmutableArray<AnyFilePath> GetFiles(this IDirectoryPath path)
  {
    return
    [
      ..Directory.GetFiles(path.ToString())
        .Select(AnyFilePath.Value)
    ];
  }

  public static ImmutableArray<AnyFilePath> GetFiles(this IDirectoryPath path, string searchPattern)
  {
    return
    [
      ..Directory.GetFiles(path.ToString(), searchPattern)
        .Select(AnyFilePath.Value)
    ];
  }

  public static ImmutableArray<AnyFilePath> GetFiles(
    this IDirectoryPath path, string searchPattern, SearchOption searchOption)
  {
    return
    [
      ..Directory.GetFiles(path.ToString(), searchPattern, searchOption)
        .Select(AnyFilePath.Value)
    ];
  }

  public static ImmutableArray<AnyFilePath> GetFiles(
    this IDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
  {
    return
    [
      ..Directory.GetFiles(path.ToString(), searchPattern, enumerationOptions)
        .Select(AnyFilePath.Value)
    ];
  }

  public static ImmutableArray<AnyDirectoryPath> GetDirectories(this IDirectoryPath path)
  {
    return
    [
      ..Directory.GetDirectories(path.ToString())
        .Select(AnyDirectoryPath.Value)
    ];
  }

  public static ImmutableArray<AnyDirectoryPath> GetDirectories(this IDirectoryPath path, string searchPattern)
  {
    return
    [
      ..Directory.GetDirectories(path.ToString(), searchPattern)
        .Select(AnyDirectoryPath.Value)
    ];
  }

  public static ImmutableArray<AnyDirectoryPath> GetDirectories(
    this IDirectoryPath path, string searchPattern, SearchOption searchOption)
  {
    return
    [
      ..Directory.GetDirectories(path.ToString(), searchPattern, searchOption)
        .Select(AnyDirectoryPath.Value)
    ];
  }

  public static ImmutableArray<AnyDirectoryPath> GetDirectories(
    this IDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
  {
    return
    [
      ..Directory.GetDirectories(path.ToString(), searchPattern, enumerationOptions)
        .Select(AnyDirectoryPath.Value)
    ];
  }

  /////////////////////////////
  public static ImmutableArray<AbsoluteAnyPath> GetFileSystemEntries(this IDirectoryPath path)
  {
    return
    [
      ..Directory.GetFileSystemEntries(path.ToString())
        .Select(AbsoluteAnyPath.Value)
    ];
  }

  public static ImmutableArray<AbsoluteAnyPath> GetFileSystemEntries(
    this IDirectoryPath path, string searchPattern, SearchOption searchOption)
  {
    return
    [
      ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, searchOption)
        .Select(AbsoluteAnyPath.Value)
    ];
  }

  public static ImmutableArray<AbsoluteAnyPath> GetFileSystemEntries(
    this IDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
  {
    return
    [
      ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
        .Select(AbsoluteAnyPath.Value)
    ];
  }

  ///////////////////////////////////////

  public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(this IDirectoryPath path)
  {
    return Directory.EnumerateDirectories(path.ToString())
      .Select(AbsoluteDirectoryPath.Value);
  }

  public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
    this IDirectoryPath path,
    string searchPattern)
  {
    return Directory.EnumerateDirectories(path.ToString(), searchPattern)
      .Select(AbsoluteDirectoryPath.Value);
  }

  public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
    this IDirectoryPath path,
    string searchPattern,
    EnumerationOptions enumerationOptions)
  {
    return Directory.EnumerateDirectories(path.ToString(), searchPattern, enumerationOptions)
      .Select(AbsoluteDirectoryPath.Value);
  }

  public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
    this IDirectoryPath path,
    string searchPattern,
    SearchOption searchOption)
  {
    return Directory.EnumerateDirectories(path.ToString(), searchPattern, searchOption)
      .Select(AbsoluteDirectoryPath.Value);
  }

  public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this IDirectoryPath path)
  {
    return Directory.EnumerateFiles(path.ToString())
      .Select(AbsoluteFilePath.Value);
  }

  public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this IDirectoryPath path, string searchPattern)
  {
    return Directory.EnumerateFiles(path.ToString(), searchPattern)
      .Select(AbsoluteFilePath.Value);
  }

  public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
    this IDirectoryPath path,
    string searchPattern,
    EnumerationOptions enumerationOptions)
  {
    return Directory.EnumerateFiles(path.ToString(), searchPattern, enumerationOptions)
      .Select(AbsoluteFilePath.Value);
  }

  public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
    this IDirectoryPath path,
    string searchPattern,
    SearchOption searchOption)
  {
    return Directory.EnumerateFiles(path.ToString(), searchPattern, searchOption)
      .Select(AbsoluteFilePath.Value);
  }

  //bug should return AbsoluteAnyPath instead of AnyPath
  public static IEnumerable<AnyPath> EnumerateFileSystemEntries(this IDirectoryPath path)
  {
    return Directory.EnumerateFileSystemEntries(path.ToString())
      .Select(AnyPath.Value);
  }

  public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
    this IDirectoryPath path,
    string searchPattern)
  {
    return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern)
      .Select(AnyPath.Value);
  }

  public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
    this IDirectoryPath path,
    string searchPattern,
    EnumerationOptions enumerationOptions)
  {
    return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
      .Select(AnyPath.Value);
  }

  public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
    this IDirectoryPath path,
    string searchPattern,
    SearchOption searchOption)
  {
    return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, searchOption)
      .Select(AnyPath.Value);
  }

  public static void Move(this IDirectoryPath source, IDirectoryPath dest)
  {
    Directory.Move(source.ToString(), dest.ToString());
  }

  // SetAccessControl/GetAccessControl methods
  public static void SetAccessControl(
    this AbsoluteDirectoryPath path, DirectorySecurity directorySecurity)
  {
    path.Info().SetAccessControl(directorySecurity);
  }

  public static void SetAccessControl(
    this RelativeDirectoryPath path, DirectorySecurity directorySecurity)
  {
    path.Info().Value().SetAccessControl(directorySecurity);
  }

  public static void SetAccessControl(
    this AnyDirectoryPath path, DirectorySecurity directorySecurity)
  {
    path.Info().Value().SetAccessControl(directorySecurity);
  }

  public static DirectorySecurity GetAccessControl(this AbsoluteDirectoryPath path)
  {
    return path.Info().GetAccessControl();
  }

  public static DirectorySecurity GetAccessControl(this RelativeDirectoryPath path)
  {
    return path.Info().Value().GetAccessControl();
  }

  public static DirectorySecurity GetAccessControl(this AnyDirectoryPath path)
  {
    return path.Info().Value().GetAccessControl();
  }
}

//bug create AnyAbsolutePath and AnyRelativePath
//bug AnyPath and other paths with partial information should have dynamic info like IsAbsolute