using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AtmaFileSystem.IO;

public static class DirectoryIo
{
    public static DirectoryInfo Create(this AbsoluteDirectoryPath path)
    {
        return Directory.CreateDirectory(path.ToString());
    }
    public static DirectoryInfo Create(this RelativeDirectoryPath path)
    {
        return Directory.CreateDirectory(path.ToString());
    }
    public static DirectoryInfo Create(this AnyDirectoryPath path)
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

    public static void SetAsCurrentDirectory(this AbsoluteDirectoryPath path)
    {
        Directory.SetCurrentDirectory(path.ToString());
    }
    public static void SetAsCurrentDirectory(this RelativeDirectoryPath path)
    {
        Directory.SetCurrentDirectory(path.ToString());
    }
    public static void SetAsCurrentDirectory(this AnyDirectoryPath path)
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
    
    public static ImmutableArray<AbsoluteFilePath> GetFiles(this AbsoluteDirectoryPath path)
    {
        return [
          ..Directory.GetFiles(path.ToString())
            .Select(AbsoluteFilePath.Value)
        ];
    }
    public static ImmutableArray<RelativeFilePath> GetFiles(this RelativeDirectoryPath path)
    {
        return [
          ..Directory.GetFiles(path.ToString())
            .Select(RelativeFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AnyFilePath> GetFiles(this AnyDirectoryPath path)
    {
        return [
          ..Directory.GetFiles(path.ToString())
            .Select(AnyFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AbsoluteFilePath> GetFiles(this AbsoluteDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern)
            .Select(AbsoluteFilePath.Value)
        ];
    }
    public static ImmutableArray<RelativeFilePath> GetFiles(this RelativeDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern)
            .Select(RelativeFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AnyFilePath> GetFiles(this AnyDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern)
            .Select(AnyFilePath.Value)
        ];
    }

    public static ImmutableArray<AbsoluteFilePath> GetFiles(
        this AbsoluteDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteFilePath.Value)
        ];
    }
    public static ImmutableArray<RelativeFilePath> GetFiles(
        this RelativeDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern, searchOption)
            .Select(RelativeFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AnyFilePath> GetFiles(
        this AnyDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern, searchOption)
            .Select(AnyFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AbsoluteFilePath> GetFiles(
        this AbsoluteDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteFilePath.Value)
        ];
    }
    public static ImmutableArray<RelativeFilePath> GetFiles(
        this RelativeDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern, enumerationOptions)
            .Select(RelativeFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AnyFilePath> GetFiles(
        this AnyDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetFiles(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyFilePath.Value)
        ];
    }
    
    public static ImmutableArray<AbsoluteDirectoryPath> GetDirectories(this AbsoluteDirectoryPath path)
    {
        return [
          ..Directory.GetDirectories(path.ToString())
            .Select(AbsoluteDirectoryPath.Value)
        ];
    }
    public static ImmutableArray<RelativeDirectoryPath> GetDirectories(this RelativeDirectoryPath path)
    {
        return [
          ..Directory.GetDirectories(path.ToString())
            .Select(RelativeDirectoryPath.Value)
        ];
    }
    
    public static ImmutableArray<AnyDirectoryPath> GetDirectories(this AnyDirectoryPath path)
    {
        return [
          ..Directory.GetDirectories(path.ToString())
            .Select(AnyDirectoryPath.Value)
        ];
    }
    
    public static ImmutableArray<AbsoluteDirectoryPath> GetDirectories(this AbsoluteDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern)
            .Select(AbsoluteDirectoryPath.Value)
        ];
    }
    public static ImmutableArray<RelativeDirectoryPath> GetDirectories(this RelativeDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern)
            .Select(RelativeDirectoryPath.Value)
        ];
    }
    
    public static ImmutableArray<AnyDirectoryPath> GetDirectories(this AnyDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern)
            .Select(AnyDirectoryPath.Value)
        ];
    }

    public static ImmutableArray<AbsoluteDirectoryPath> GetDirectories(
        this AbsoluteDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteDirectoryPath.Value)
        ];
    }
    public static ImmutableArray<RelativeDirectoryPath> GetDirectories(
        this RelativeDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern, searchOption)
            .Select(RelativeDirectoryPath.Value)
        ];
    }
    
    public static ImmutableArray<AnyDirectoryPath> GetDirectories(
        this AnyDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern, searchOption)
            .Select(AnyDirectoryPath.Value)
        ];
    }
    
    public static ImmutableArray<AbsoluteDirectoryPath> GetDirectories(
        this AbsoluteDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteDirectoryPath.Value)
        ];
    }
    public static ImmutableArray<RelativeDirectoryPath> GetDirectories(
        this RelativeDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern, enumerationOptions)
            .Select(RelativeDirectoryPath.Value)
        ];
    }
    
    public static ImmutableArray<AnyDirectoryPath> GetDirectories(
        this AnyDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetDirectories(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyDirectoryPath.Value)
        ];
    }

    /////////////////////////////
    
    //these should return AnyAbsolutePath array
    public static ImmutableArray<AnyPath> GetFileSystemEntries(this AbsoluteDirectoryPath path)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString())
            .Select(AnyPath.Value)
        ];
    }
    public static ImmutableArray<AnyPath> GetFileSystemEntries(this RelativeDirectoryPath path)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString())
            .Select(AnyPath.Value)
        ];
    }
    public static ImmutableArray<AnyPath> GetFileSystemEntries(this AnyDirectoryPath path)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString())
            .Select(AnyPath.Value)
        ];
    }

    public static ImmutableArray<AnyPath> GetFileSystemEntries(this AbsoluteDirectoryPath path, string searchPattern)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString(), searchPattern)
            .Select(AnyPath.Value)
        ];
    }

    public static ImmutableArray<AnyPath> GetFileSystemEntries(this RelativeDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, searchOption)
            .Select(AnyPath.Value)
        ];
    }
    public static ImmutableArray<AnyPath> GetFileSystemEntries(this AnyDirectoryPath path, string searchPattern, SearchOption searchOption)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, searchOption)
            .Select(AnyPath.Value)
        ];
    }

    public static ImmutableArray<AnyPath> GetFileSystemEntries(this AbsoluteDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyPath.Value)
        ];
    }
    public static ImmutableArray<AnyPath> GetFileSystemEntries(this RelativeDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyPath.Value)
        ];
    }
    public static ImmutableArray<AnyPath> GetFileSystemEntries(this AnyDirectoryPath path, string searchPattern, EnumerationOptions enumerationOptions)
    {
        return [
          ..Directory.GetFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyPath.Value)
        ];
    }

    ///////////////////////////////////////

    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(this AbsoluteDirectoryPath path)
    {
        return Directory.EnumerateDirectories(path.ToString())
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(this RelativeDirectoryPath path)
    {
        return Directory.EnumerateDirectories(path.ToString())
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(this AnyDirectoryPath path)
    {
        return Directory.EnumerateDirectories(path.ToString())
            .Select(AbsoluteDirectoryPath.Value);
    }

    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this AbsoluteDirectoryPath path,
        string searchPattern)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern)
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this RelativeDirectoryPath path,
        string searchPattern)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern)
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this AnyDirectoryPath path,
        string searchPattern)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern)
            .Select(AbsoluteDirectoryPath.Value);
    }

    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this AbsoluteDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this RelativeDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this AnyDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteDirectoryPath.Value);
    }

    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this AbsoluteDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this RelativeDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteDirectoryPath.Value);
    }
    public static IEnumerable<AbsoluteDirectoryPath> EnumerateDirectories(
        this AnyDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateDirectories(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteDirectoryPath.Value);
    }

    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this AbsoluteDirectoryPath path)
    {
        return Directory.EnumerateFiles(path.ToString())
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this RelativeDirectoryPath path)
    {
        return Directory.EnumerateFiles(path.ToString())
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this AnyDirectoryPath path)
    {
        return Directory.EnumerateFiles(path.ToString())
            .Select(AbsoluteFilePath.Value);
    }

    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this AbsoluteDirectoryPath path, string searchPattern)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern)
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this RelativeDirectoryPath path, string searchPattern)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern)
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(this AnyDirectoryPath path, string searchPattern)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern)
            .Select(AbsoluteFilePath.Value);
    }

    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
        this AbsoluteDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
        this RelativeDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
        this AnyDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern, enumerationOptions)
            .Select(AbsoluteFilePath.Value);
    }

    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
        this AbsoluteDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
        this RelativeDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteFilePath.Value);
    }
    public static IEnumerable<AbsoluteFilePath> EnumerateFiles(
        this AnyDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateFiles(path.ToString(), searchPattern, searchOption)
            .Select(AbsoluteFilePath.Value);
    }

    //bug should return AbsolutePath instead of AnyPath
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(this AbsoluteDirectoryPath path)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString())
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(this RelativeDirectoryPath path)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString())
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(this AnyDirectoryPath path)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString())
            .Select(AnyPath.Value);
    }

    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this AbsoluteDirectoryPath path,
        string searchPattern)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern)
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this RelativeDirectoryPath path,
        string searchPattern)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern)
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this AnyDirectoryPath path,
        string searchPattern)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern)
            .Select(AnyPath.Value);
    }

    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this AbsoluteDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions) 
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this RelativeDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions) 
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this AnyDirectoryPath path,
        string searchPattern,
        EnumerationOptions enumerationOptions) 
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, enumerationOptions)
            .Select(AnyPath.Value);
    }

    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this AbsoluteDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, searchOption)
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this RelativeDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, searchOption)
            .Select(AnyPath.Value);
    }
    public static IEnumerable<AnyPath> EnumerateFileSystemEntries(
        this AnyDirectoryPath path,
        string searchPattern,
        SearchOption searchOption)
    {
        return Directory.EnumerateFileSystemEntries(path.ToString(), searchPattern, searchOption)
            .Select(AnyPath.Value);
    }
}

//bug create AnyAbsolutePath and AnyRelativePath
//bug AnyPath and other paths with partial information should have dynamic info like IsAbsolute