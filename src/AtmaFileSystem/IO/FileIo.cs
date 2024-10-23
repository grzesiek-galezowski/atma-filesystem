using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtmaFileSystem.InternalInterfaces;

namespace AtmaFileSystem.IO;

public static class FileIo
{
    public static StreamReader OpenText(this AbsoluteFilePath path)
    {
        return File.OpenText(path.ToString());
    }
    public static StreamReader OpenText(this RelativeFilePath path)
    {
        return File.OpenText(path.ToString());
    }
    public static StreamReader OpenText(this AnyFilePath path)
    {
        return File.OpenText(path.ToString());
    }

    public static StreamWriter CreateText(this AbsoluteFilePath path)
    {
        return File.CreateText(path.ToString());
    }
    public static StreamWriter CreateText(this RelativeFilePath path)
    {
        return File.CreateText(path.ToString());
    }
    public static StreamWriter CreateText(this AnyFilePath path)
    {
        return File.CreateText(path.ToString());
    }

    public static StreamWriter AppendText(this AbsoluteFilePath path)
    {
        return File.AppendText(path.ToString());
    }
    public static StreamWriter AppendText(this RelativeFilePath path)
    {
        return File.AppendText(path.ToString());
    }
    public static StreamWriter AppendText(this AnyFilePath path)
    {
        return File.AppendText(path.ToString());
    }

    public static FileStream Create(this AbsoluteFilePath path)
    {
        return File.Create(path.ToString());
    }
    public static FileStream Create(this RelativeFilePath path)
    {
        return File.Create(path.ToString());
    }
    public static FileStream Create(this AnyFilePath path)
    {
        return File.Create(path.ToString());
    }

    public static FileStream Create(this AbsoluteFilePath path, int bufferSize)
    {
        return File.Create(path.ToString(), bufferSize);
    }
    public static FileStream Create(this RelativeFilePath path, int bufferSize)
    {
        return File.Create(path.ToString(), bufferSize);
    }
    public static FileStream Create(this AnyFilePath path, int bufferSize)
    {
        return File.Create(path.ToString(), bufferSize);
    }

    public static FileStream Create(this AbsoluteFilePath path, int bufferSize, FileOptions options)
    {
        return File.Create(path.ToString(), bufferSize, options);
    }
    public static FileStream Create(this RelativeFilePath path, int bufferSize, FileOptions options)
    {
        return File.Create(path.ToString(), bufferSize, options);
    }
    public static FileStream Create(this AnyFilePath path, int bufferSize, FileOptions options)
    {
        return File.Create(path.ToString(), bufferSize, options);
    }

    public static void Delete(this AbsoluteFilePath path)
    {
        File.Delete(path.ToString());
    }
    public static void Delete(this RelativeFilePath path)
    {
        File.Delete(path.ToString());
    }
    public static void Delete(this AnyFilePath path)
    {
        File.Delete(path.ToString());
    }

    public static bool Exists(this AbsoluteFilePath path)
    {
        return File.Exists(path.ToString());
    }
    public static bool Exists(this RelativeFilePath path)
    {
        return File.Exists(path.ToString());
    }
    public static bool Exists(this AnyFilePath path)
    {
        return File.Exists(path.ToString());
    }

    public static FileStream Open(this AbsoluteFilePath path, FileMode mode)
    {
        return File.Open(path.ToString(), mode);
    }
    public static FileStream Open(this RelativeFilePath path, FileMode mode)
    {
        return File.Open(path.ToString(), mode);
    }
    public static FileStream Open(this AnyFilePath path, FileMode mode)
    {
        return File.Open(path.ToString(), mode);
    }

    public static FileStream Open(this AbsoluteFilePath path, FileMode mode, FileAccess access)
    {
        return File.Open(path.ToString(), mode, access);
    }
    public static FileStream Open(this RelativeFilePath path, FileMode mode, FileAccess access)
    {
        return File.Open(path.ToString(), mode, access);
    }
    public static FileStream Open(this AnyFilePath path, FileMode mode, FileAccess access)
    {
        return File.Open(path.ToString(), mode, access);
    }

    public static FileStream Open(this AbsoluteFilePath path, FileMode mode, FileAccess access, FileShare share)
    {
        return File.Open(path.ToString(), mode, access, share);
    }
    public static FileStream Open(this RelativeFilePath path, FileMode mode, FileAccess access, FileShare share)
    {
        return File.Open(path.ToString(), mode, access, share);
    }
    public static FileStream Open(this AnyFilePath path, FileMode mode, FileAccess access, FileShare share)
    {
        return File.Open(path.ToString(), mode, access, share);
    }

    public static void SetCreationTime(this AbsoluteFilePath path, DateTime creationTime)
    {
        File.SetCreationTime(path.ToString(), creationTime);
    }
    public static void SetCreationTime(this RelativeFilePath path, DateTime creationTime)
    {
        File.SetCreationTime(path.ToString(), creationTime);
    }
    public static void SetCreationTime(this AnyFilePath path, DateTime creationTime)
    {
        File.SetCreationTime(path.ToString(), creationTime);
    }
    
    public static void SetCreationTimeUtc(this AbsoluteFilePath path, DateTime creationTimeUtc)
    {
        File.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
    }
    public static void SetCreationTimeUtc(this RelativeFilePath path, DateTime creationTimeUtc)
    {
        File.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
    }
    public static void SetCreationTimeUtc(this AnyFilePath path, DateTime creationTimeUtc)
    {
        File.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
    }
    
    public static DateTime  GetCreationTime(this AbsoluteFilePath path)
    {
        return File.GetCreationTime(path.ToString());
    }
    public static DateTime GetCreationTime(this RelativeFilePath path)
    {
        return File.GetCreationTime(path.ToString());
    }
    public static DateTime GetCreationTime(this AnyFilePath path)
    {
        return File.GetCreationTime(path.ToString());
    }
    
    public static DateTime GetCreationTimeUtc(this AbsoluteFilePath path)
    {
        return File.GetCreationTimeUtc(path.ToString());
    }
    public static DateTime GetCreationTimeUtc(this RelativeFilePath path)
    {
        return File.GetCreationTimeUtc(path.ToString());
    }
    public static DateTime GetCreationTimeUtc(this AnyFilePath path)
    {
        return File.GetCreationTimeUtc(path.ToString());
    }
    
    public static void SetLastAccessTime(this AbsoluteFilePath path, DateTime lastAccessTime)
    {
        File.SetLastAccessTime(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTime(this RelativeFilePath path, DateTime lastAccessTime)
    {
        File.SetLastAccessTime(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTime(this AnyFilePath path, DateTime lastAccessTime)
    {
        File.SetLastAccessTime(path.ToString(), lastAccessTime);
    }

    public static void SetLastAccessTimeUtc(this AbsoluteFilePath path, DateTime lastAccessTime)
    {
        File.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTimeUtc(this RelativeFilePath path, DateTime lastAccessTime)
    {
        File.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
    }
    public static void SetLastAccessTimeUtc(this AnyFilePath path, DateTime lastAccessTime)
    {
        File.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
    }
    
    public static DateTime GetLastAccessTime(this AbsoluteFilePath path)
    {
        return File.GetLastAccessTime(path.ToString());
    }
    public static DateTime GetLastAccessTime(this RelativeFilePath path)
    {
        return File.GetLastAccessTime(path.ToString());
    }
    public static DateTime GetLastAccessTime(this AnyFilePath path)
    {
        return File.GetLastAccessTime(path.ToString());
    }

    public static DateTime GetLastAccessTimeUtc(this AbsoluteFilePath path)
    {
        return File.GetLastAccessTimeUtc(path.ToString());
    }
    public static DateTime GetLastAccessTimeUtc(this RelativeFilePath path)
    {
        return File.GetLastAccessTimeUtc(path.ToString());
    }
    public static DateTime GetLastAccessTimeUtc(this AnyFilePath path)
    {
        return File.GetLastAccessTimeUtc(path.ToString());
    }

    public static void SetLastWriteTime(this AbsoluteFilePath path, DateTime lastWriteTime)
    {
        File.SetLastWriteTime(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTime(this RelativeFilePath path, DateTime lastWriteTime)
    {
        File.SetLastWriteTime(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTime(this AnyFilePath path, DateTime lastWriteTime)
    {
        File.SetLastWriteTime(path.ToString(), lastWriteTime);
    }
    
    public static void SetLastWriteTimeUtc(this AbsoluteFilePath path, DateTime lastWriteTime)
    {
        File.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTimeUtc(this RelativeFilePath path, DateTime lastWriteTime)
    {
        File.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
    }
    public static void SetLastWriteTimeUtc(this AnyFilePath path, DateTime lastWriteTime)
    {
        File.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
    }
    
    public static DateTime GetLastWriteTime(this AbsoluteFilePath path)
    {
        return File.GetLastWriteTime(path.ToString());
    }
    public static DateTime GetLastWriteTime(this RelativeFilePath path)
    {
        return File.GetLastWriteTime(path.ToString());
    }
    public static DateTime GetLastWriteTime(this AnyFilePath path)
    {
        return File.GetLastWriteTime(path.ToString());
    }
    
    public static DateTime GetLastWriteTimeUtc(this AbsoluteFilePath path)
    {
        return File.GetLastWriteTimeUtc(path.ToString());
    }
    public static DateTime GetLastWriteTimeUtc(this RelativeFilePath path)
    {
        return File.GetLastWriteTimeUtc(path.ToString());
    }
    public static DateTime GetLastWriteTimeUtc(this AnyFilePath path)
    {
        return File.GetLastWriteTimeUtc(path.ToString());
    }
    
    public static FileAttributes GetAttributes(this AbsoluteFilePath path)
    {
        return File.GetAttributes(path.ToString());
    }
    public static FileAttributes GetAttributes(this RelativeFilePath path)
    {
        return File.GetAttributes(path.ToString());
    }
    public static FileAttributes GetAttributes(this AnyFilePath path)
    {
        return File.GetAttributes(path.ToString());
    }
    
    public static void SetAttributes(this AbsoluteFilePath path, FileAttributes fileAttributes)
    {
        File.SetAttributes(path.ToString(), fileAttributes);
    }
    public static void SetAttributes(this RelativeFilePath path, FileAttributes fileAttributes)
    {
        File.SetAttributes(path.ToString(), fileAttributes);
    }
    public static void SetAttributes(this AnyFilePath path, FileAttributes fileAttributes)
    {
        File.SetAttributes(path.ToString(), fileAttributes);
    }

    public static FileStream OpenRead(this AbsoluteFilePath path)
    {
        return File.OpenRead(path.ToString());
    }
    public static FileStream OpenRead(this RelativeFilePath path)
    {
        return File.OpenRead(path.ToString());
    }
    public static FileStream OpenRead(this AnyFilePath path)
    {
        return File.OpenRead(path.ToString());
    }

    public static FileStream OpenWrite(this AbsoluteFilePath path)
    {
        return File.OpenWrite(path.ToString());
    }
    public static FileStream OpenWrite(this RelativeFilePath path)
    {
        return File.OpenWrite(path.ToString());
    }
    public static FileStream OpenWrite(this AnyFilePath path)
    {
        return File.OpenWrite(path.ToString());
    }

    public static string ReadAllText(this AbsoluteFilePath path)
    {
        return File.ReadAllText(path.ToString());
    }
    public static string ReadAllText(this RelativeFilePath path)
    {
        return File.ReadAllText(path.ToString());
    }
    public static string ReadAllText(this AnyFilePath path)
    {
        return File.ReadAllText(path.ToString());
    }

    public static string ReadAllText(this AbsoluteFilePath path, Encoding encoding)
    {
        return File.ReadAllText(path.ToString(), encoding);
    }
    public static string ReadAllText(this RelativeFilePath path, Encoding encoding)
    {
        return File.ReadAllText(path.ToString(), encoding);
    }
    public static string ReadAllText(this AnyFilePath path, Encoding encoding)
    {
        return File.ReadAllText(path.ToString(), encoding);
    }

    public static void WriteAllText(this AbsoluteFilePath path, string contents)
    {
        File.WriteAllText(path.ToString(), contents);
    }
    public static void WriteAllText(this RelativeFilePath path, string contents)
    {
        File.WriteAllText(path.ToString(), contents);
    }
    public static void WriteAllText(this AnyFilePath path, string contents)
    {
        File.WriteAllText(path.ToString(), contents);
    }

    public static void WriteAllText(this AbsoluteFilePath path, string contents, Encoding encoding)
    {
        File.WriteAllText(path.ToString(), contents, encoding);
    }
    public static void WriteAllText(this RelativeFilePath path, string contents, Encoding encoding)
    {
        File.WriteAllText(path.ToString(), contents, encoding);
    }
    public static void WriteAllText(this AnyFilePath path, string contents, Encoding encoding)
    {
        File.WriteAllText(path.ToString(), contents, encoding);
    }

    public static byte[] ReadAllBytes(this AbsoluteFilePath path)
    {
        return File.ReadAllBytes(path.ToString());
    }
    public static byte[] ReadAllBytes(this RelativeFilePath path)
    {
        return File.ReadAllBytes(path.ToString());
    }
    public static byte[] ReadAllBytes(this AnyFilePath path)
    {
        return File.ReadAllBytes(path.ToString());
    }

    public static void WriteAllBytes(this AbsoluteFilePath path, byte[] bytes)
    {
        File.WriteAllBytes(path.ToString(), bytes);
    }
    public static void WriteAllBytes(this RelativeFilePath path, byte[] bytes)
    {
        File.WriteAllBytes(path.ToString(), bytes);
    }
    public static void WriteAllBytes(this AnyFilePath path, byte[] bytes)
    {
        File.WriteAllBytes(path.ToString(), bytes);
    }

    
    public static string[] ReadAllLines(this AbsoluteFilePath path)
    {
        return File.ReadAllLines(path.ToString());
    }
    public static string[] ReadAllLines(this RelativeFilePath path)
    {
        return File.ReadAllLines(path.ToString());
    }
    public static string[] ReadAllLines(this AnyFilePath path)
    {
        return File.ReadAllLines(path.ToString());
    }

    public static string[] ReadAllLines(this AbsoluteFilePath path, Encoding encoding)
    {
        return File.ReadAllLines(path.ToString(), encoding);
    }
    public static string[] ReadAllLines(this RelativeFilePath path, Encoding encoding)
    {
        return File.ReadAllLines(path.ToString(), encoding);
    }
    public static string[] ReadAllLines(this AnyFilePath path, Encoding encoding)
    {
        return File.ReadAllLines(path.ToString(), encoding);
    }
    
    public static IEnumerable<string> ReadLines(this AbsoluteFilePath path)
    {
        return File.ReadLines(path.ToString());
    }
    public static IEnumerable<string> ReadLines(this RelativeFilePath path)
    {
        return File.ReadLines(path.ToString());
    }
    public static IEnumerable<string> ReadLines(this AnyFilePath path)
    {
        return File.ReadLines(path.ToString());
    }
    
    public static IEnumerable<string> ReadLines(this AbsoluteFilePath path, Encoding encoding)
    {
        return File.ReadLines(path.ToString(), encoding);
    }
    public static IEnumerable<string> ReadLines(this RelativeFilePath path, Encoding encoding)
    {
        return File.ReadLines(path.ToString(), encoding);
    }
    public static IEnumerable<string> ReadLines(this AnyFilePath path, Encoding encoding)
    {
        return File.ReadLines(path.ToString(), encoding);
    }
    
    public static void WriteAllLines(this AbsoluteFilePath path, string[] contents)
    {
        File.WriteAllLines(path.ToString(), contents);
    }
    public static void WriteAllLines(this RelativeFilePath path, string[] contents)
    {
        File.WriteAllLines(path.ToString(), contents);
    }
    public static void WriteAllLines(this AnyFilePath path, string[] contents)
    {
        File.WriteAllLines(path.ToString(), contents);
    }

    public static void WriteAllLines(this AbsoluteFilePath path, IEnumerable<string> contents)
    {
        File.WriteAllLines(path.ToString(), contents);
    }
    public static void WriteAllLines(this RelativeFilePath path, IEnumerable<string> contents)
    {
        File.WriteAllLines(path.ToString(), contents);
    }
    public static void WriteAllLines(this AnyFilePath path, IEnumerable<string> contents)
    {
        File.WriteAllLines(path.ToString(), contents);
    }
    
    public static void WriteAllLines(this AbsoluteFilePath path, string[] contents, Encoding encoding)
    {
        File.WriteAllLines(path.ToString(), contents, encoding);
    }
    public static void WriteAllLines(this RelativeFilePath path, string[] contents, Encoding encoding)
    {
        File.WriteAllLines(path.ToString(), contents, encoding);
    }
    public static void WriteAllLines(this AnyFilePath path, string[] contents, Encoding encoding)
    {
        File.WriteAllLines(path.ToString(), contents, encoding);
    }
    
    public static void WriteAllLines(this AbsoluteFilePath path, IEnumerable<string> contents, Encoding encoding)
    {
        File.WriteAllLines(path.ToString(), contents, encoding);
    }
    public static void WriteAllLines(this RelativeFilePath path, IEnumerable<string> contents, Encoding encoding)
    {
        File.WriteAllLines(path.ToString(), contents, encoding);
    }
    public static void WriteAllLines(this AnyFilePath path, IEnumerable<string> contents, Encoding encoding)
    {
        File.WriteAllLines(path.ToString(), contents, encoding);
    }

    
    public static void AppendAllText(this AbsoluteFilePath path, string contents)
    {
        File.AppendAllText(path.ToString(), contents);
    }
    public static void AppendAllText(this RelativeFilePath path, string contents)
    {
        File.AppendAllText(path.ToString(), contents);
    }
    public static void AppendAllText(this AnyFilePath path, string contents)
    {
        File.AppendAllText(path.ToString(), contents);
    }
    
    public static void AppendAllText(this AbsoluteFilePath path, string contents, Encoding encoding)
    {
        File.AppendAllText(path.ToString(), contents, encoding);
    }
    public static void AppendAllText(this RelativeFilePath path, string contents, Encoding encoding)
    {
        File.AppendAllText(path.ToString(), contents, encoding);
    }
    public static void AppendAllText(this AnyFilePath path, string contents, Encoding encoding)
    {
        File.AppendAllText(path.ToString(), contents, encoding);
    }
    
    public static void AppendAllLines(this AbsoluteFilePath path, IEnumerable<string> contents)
    {
        File.AppendAllLines(path.ToString(), contents);
    }
    public static void AppendAllLines(this RelativeFilePath path, IEnumerable<string> contents)
    {
        File.AppendAllLines(path.ToString(), contents);
    }
    public static void AppendAllLines(this AnyFilePath path, IEnumerable<string> contents)
    {
        File.AppendAllLines(path.ToString(), contents);
    }

    public static void AppendAllLines(this AbsoluteFilePath path, IEnumerable<string> contents, Encoding encoding)
    {
        File.AppendAllLines(path.ToString(), contents, encoding);
    }
    public static void AppendAllLines(this RelativeFilePath path, IEnumerable<string> contents, Encoding encoding)
    {
        File.AppendAllLines(path.ToString(), contents, encoding);
    }
    public static void AppendAllLines(this AnyFilePath path, IEnumerable<string> contents, Encoding encoding)
    {
        File.AppendAllLines(path.ToString(), contents, encoding);
    }
    
    public static Task<string> ReadAllTextAsync(this AbsoluteFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(path.ToString(), cancellationToken);
    }
    public static Task<string> ReadAllTextAsync(this RelativeFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(path.ToString(), cancellationToken);
    }
    public static Task<string> ReadAllTextAsync(this AnyFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(path.ToString(), cancellationToken);
    }

    public static Task<string> ReadAllTextAsync(this AbsoluteFilePath path, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(path.ToString(), encoding, cancellationToken);
    }
    public static Task<string> ReadAllTextAsync(this RelativeFilePath path, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(path.ToString(), encoding, cancellationToken);
    }
    public static Task<string> ReadAllTextAsync(this AnyFilePath path, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllTextAsync(path.ToString(), encoding, cancellationToken);
    }
    
    public static Task WriteAllTextAsync(this AbsoluteFilePath path, string contents,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task WriteAllTextAsync(this RelativeFilePath path, string contents,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task WriteAllTextAsync(this AnyFilePath path, string contents,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path.ToString(), contents, cancellationToken);
    }
    
    public static Task WriteAllTextAsync(this AbsoluteFilePath path, string contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task WriteAllTextAsync(this RelativeFilePath path, string contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task WriteAllTextAsync(this AnyFilePath path, string contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    
    public static Task<byte[]> ReadAllBytesAsync(this AbsoluteFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllBytesAsync(path.ToString(), cancellationToken);
    }
    public static Task<byte[]> ReadAllBytesAsync(this RelativeFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllBytesAsync(path.ToString(), cancellationToken);
    }
    public static Task<byte[]> ReadAllBytesAsync(this AnyFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllBytesAsync(path.ToString(), cancellationToken);
    }
    
    public static Task WriteAllBytesAsync(this AbsoluteFilePath path, byte[] bytes,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllBytesAsync(path.ToString(), bytes, cancellationToken);
    }
    public static Task WriteAllBytesAsync(this RelativeFilePath path, byte[] bytes,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllBytesAsync(path.ToString(), bytes, cancellationToken);
    }
    public static Task WriteAllBytesAsync(this AnyFilePath path, byte[] bytes,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllBytesAsync(path.ToString(), bytes, cancellationToken);
    }
    
    public static Task<string[]> ReadAllLinesAsync(this AbsoluteFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllLinesAsync(path.ToString(), cancellationToken);
    }
    public static Task<string[]> ReadAllLinesAsync(this RelativeFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllLinesAsync(path.ToString(), cancellationToken);
    }
    public static Task<string[]> ReadAllLinesAsync(this AnyFilePath path,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllLinesAsync(path.ToString(), cancellationToken);
    }

    
    public static Task<string[]> ReadAllLinesAsync(this AbsoluteFilePath path, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllLinesAsync(path.ToString(), encoding, cancellationToken);
    }
    public static Task<string[]> ReadAllLinesAsync(this RelativeFilePath path, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllLinesAsync(path.ToString(), encoding, cancellationToken);
    }
    public static Task<string[]> ReadAllLinesAsync(this AnyFilePath path, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.ReadAllLinesAsync(path.ToString(), encoding, cancellationToken);
    }

    public static Task WriteAllLinesAsync(this AbsoluteFilePath path, IEnumerable<string> contents,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllLinesAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task WriteAllLinesAsync(this RelativeFilePath path, IEnumerable<string> contents,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllLinesAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task WriteAllLinesAsync(this AnyFilePath path, IEnumerable<string> contents,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllLinesAsync(path.ToString(), contents, cancellationToken);
    }

    
    public static Task WriteAllLinesAsync(this AbsoluteFilePath path, IEnumerable<string> contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task WriteAllLinesAsync(this RelativeFilePath path, IEnumerable<string> contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task WriteAllLinesAsync(this AnyFilePath path, IEnumerable<string> contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.WriteAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
    }

    public static Task AppendAllTextAsync(this AbsoluteFilePath path, string contents,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task AppendAllTextAsync(this RelativeFilePath path, string contents,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task AppendAllTextAsync(this AnyFilePath path, string contents,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(path.ToString(), contents, cancellationToken);
    }
    
    public static Task AppendAllTextAsync(this AbsoluteFilePath path, string contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task AppendAllTextAsync(this RelativeFilePath path, string contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task AppendAllTextAsync(this AnyFilePath path, string contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
    }

    public static Task AppendAllLinesAsync(this AbsoluteFilePath path, IEnumerable<string> contents,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllLinesAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task AppendAllLinesAsync(this RelativeFilePath path, IEnumerable<string> contents,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllLinesAsync(path.ToString(), contents, cancellationToken);
    }
    public static Task AppendAllLinesAsync(this AnyFilePath path, IEnumerable<string> contents,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllLinesAsync(path.ToString(), contents, cancellationToken);
    }

    
    public static Task AppendAllLinesAsync(this AbsoluteFilePath path, IEnumerable<string> contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task AppendAllLinesAsync(this RelativeFilePath path, IEnumerable<string> contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    public static Task AppendAllLinesAsync(this AnyFilePath path, IEnumerable<string> contents, Encoding encoding,
        CancellationToken cancellationToken = default)
    {
        return File.AppendAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
    }
    
    public static void Copy(this IFilePath source, IFilePath destination, bool overwrite = false)
    {
        File.Copy(source.ToString(), destination.ToString(), overwrite);
    }
}