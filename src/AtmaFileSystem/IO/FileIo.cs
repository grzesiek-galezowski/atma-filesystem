using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using AtmaFileSystem.InternalInterfaces;
using Core.Maybe;

namespace AtmaFileSystem.IO;

public static class FileIo
{
  public static StreamReader OpenText(this IFilePath path)
  {
    return File.OpenText(path.ToString());
  }

  public static StreamWriter CreateText(this IFilePath path)
  {
    return File.CreateText(path.ToString());
  }

  public static StreamWriter AppendText(this IFilePath path)
  {
    return File.AppendText(path.ToString());
  }

  public static FileStream Create(this IFilePath path)
  {
    return File.Create(path.ToString());
  }

  public static FileStream Create(this IFilePath path, int bufferSize)
  {
    return File.Create(path.ToString(), bufferSize);
  }

  public static FileStream Create(this IFilePath path, int bufferSize, FileOptions options)
  {
    return File.Create(path.ToString(), bufferSize, options);
  }

  public static void Delete(this IFilePath path)
  {
    File.Delete(path.ToString());
  }

  public static bool Exists(this IFilePath path)
  {
    return File.Exists(path.ToString());
  }

  public static FileStream Open(this IFilePath path, FileMode mode)
  {
    return File.Open(path.ToString(), mode);
  }

  public static FileStream Open(this IFilePath path, FileMode mode, FileAccess access)
  {
    return File.Open(path.ToString(), mode, access);
  }

  public static FileStream Open(this IFilePath path, FileMode mode, FileAccess access, FileShare share)
  {
    return File.Open(path.ToString(), mode, access, share);
  }

  public static void SetCreationTime(this IFilePath path, DateTime creationTime)
  {
    File.SetCreationTime(path.ToString(), creationTime);
  }

  public static void SetCreationTimeUtc(this IFilePath path, DateTime creationTimeUtc)
  {
    File.SetCreationTimeUtc(path.ToString(), creationTimeUtc);
  }

  public static DateTime GetCreationTime(this IFilePath path)
  {
    return File.GetCreationTime(path.ToString());
  }

  public static DateTime GetCreationTimeUtc(this IFilePath path)
  {
    return File.GetCreationTimeUtc(path.ToString());
  }

  public static void SetLastAccessTime(this IFilePath path, DateTime lastAccessTime)
  {
    File.SetLastAccessTime(path.ToString(), lastAccessTime);
  }

  public static void SetLastAccessTimeUtc(this IFilePath path, DateTime lastAccessTime)
  {
    File.SetLastAccessTimeUtc(path.ToString(), lastAccessTime);
  }

  public static DateTime GetLastAccessTime(this IFilePath path)
  {
    return File.GetLastAccessTime(path.ToString());
  }

  public static DateTime GetLastAccessTimeUtc(this IFilePath path)
  {
    return File.GetLastAccessTimeUtc(path.ToString());
  }

  public static void SetLastWriteTime(this IFilePath path, DateTime lastWriteTime)
  {
    File.SetLastWriteTime(path.ToString(), lastWriteTime);
  }

  public static void SetLastWriteTimeUtc(this IFilePath path, DateTime lastWriteTime)
  {
    File.SetLastWriteTimeUtc(path.ToString(), lastWriteTime);
  }

  public static DateTime GetLastWriteTime(this IFilePath path)
  {
    return File.GetLastWriteTime(path.ToString());
  }

  public static DateTime GetLastWriteTimeUtc(this IFilePath path)
  {
    return File.GetLastWriteTimeUtc(path.ToString());
  }

  public static FileAttributes GetAttributes(this IFilePath path)
  {
    return File.GetAttributes(path.ToString());
  }

  public static void SetAttributes(this IFilePath path, FileAttributes fileAttributes)
  {
    File.SetAttributes(path.ToString(), fileAttributes);
  }

  public static FileStream OpenRead(this IFilePath path)
  {
    return File.OpenRead(path.ToString());
  }

  public static FileStream OpenWrite(this IFilePath path)
  {
    return File.OpenWrite(path.ToString());
  }

  public static string ReadAllText(this IFilePath path)
  {
    return File.ReadAllText(path.ToString());
  }

  public static string ReadAllText(this IFilePath path, Encoding encoding)
  {
    return File.ReadAllText(path.ToString(), encoding);
  }

  public static void WriteAllText(this IFilePath path, string contents)
  {
    File.WriteAllText(path.ToString(), contents);
  }

  public static void WriteAllText(this IFilePath path, string contents, Encoding encoding)
  {
    File.WriteAllText(path.ToString(), contents, encoding);
  }

  public static byte[] ReadAllBytes(this IFilePath path)
  {
    return File.ReadAllBytes(path.ToString());
  }

  public static void WriteAllBytes(this IFilePath path, byte[] bytes)
  {
    File.WriteAllBytes(path.ToString(), bytes);
  }

  public static string[] ReadAllLines(this IFilePath path)
  {
    return File.ReadAllLines(path.ToString());
  }

  public static string[] ReadAllLines(this IFilePath path, Encoding encoding)
  {
    return File.ReadAllLines(path.ToString(), encoding);
  }

  public static IEnumerable<string> ReadLines(this IFilePath path)
  {
    return File.ReadLines(path.ToString());
  }

  public static IEnumerable<string> ReadLines(this IFilePath path, Encoding encoding)
  {
    return File.ReadLines(path.ToString(), encoding);
  }

  public static void WriteAllLines(this IFilePath path, string[] contents)
  {
    File.WriteAllLines(path.ToString(), contents);
  }

  public static void WriteAllLines(this IFilePath path, IEnumerable<string> contents)
  {
    File.WriteAllLines(path.ToString(), contents);
  }

  public static void WriteAllLines(this IFilePath path, string[] contents, Encoding encoding)
  {
    File.WriteAllLines(path.ToString(), contents, encoding);
  }

  public static void WriteAllLines(this IFilePath path, IEnumerable<string> contents, Encoding encoding)
  {
    File.WriteAllLines(path.ToString(), contents, encoding);
  }

  public static void AppendAllText(this IFilePath path, string contents)
  {
    File.AppendAllText(path.ToString(), contents);
  }

  public static void AppendAllText(this IFilePath path, string contents, Encoding encoding)
  {
    File.AppendAllText(path.ToString(), contents, encoding);
  }

  public static void AppendAllLines(this IFilePath path, IEnumerable<string> contents)
  {
    File.AppendAllLines(path.ToString(), contents);
  }

  public static void AppendAllLines(this IFilePath path, IEnumerable<string> contents, Encoding encoding)
  {
    File.AppendAllLines(path.ToString(), contents, encoding);
  }

  public static Task<string> ReadAllTextAsync(
    this IFilePath path,
    CancellationToken cancellationToken = default)
  {
    return File.ReadAllTextAsync(path.ToString(), cancellationToken);
  }

  public static Task<string> ReadAllTextAsync(
    this IFilePath path, Encoding encoding,
    CancellationToken cancellationToken = default)
  {
    return File.ReadAllTextAsync(path.ToString(), encoding, cancellationToken);
  }

  public static Task WriteAllTextAsync(
    this IFilePath path, string contents,
    CancellationToken cancellationToken = default)
  {
    return File.WriteAllTextAsync(path.ToString(), contents, cancellationToken);
  }

  public static Task WriteAllTextAsync(
    this IFilePath path, string contents, Encoding encoding,
    CancellationToken cancellationToken = default)
  {
    return File.WriteAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
  }

  public static Task<byte[]> ReadAllBytesAsync(
    this IFilePath path,
    CancellationToken cancellationToken = default)
  {
    return File.ReadAllBytesAsync(path.ToString(), cancellationToken);
  }

  public static Task WriteAllBytesAsync(
    this IFilePath path, byte[] bytes,
    CancellationToken cancellationToken = default)
  {
    return File.WriteAllBytesAsync(path.ToString(), bytes, cancellationToken);
  }

  public static Task<string[]> ReadAllLinesAsync(
    this IFilePath path,
    CancellationToken cancellationToken = default)
  {
    return File.ReadAllLinesAsync(path.ToString(), cancellationToken);
  }

  public static Task<string[]> ReadAllLinesAsync(
    this IFilePath path, Encoding encoding,
    CancellationToken cancellationToken = default)
  {
    return File.ReadAllLinesAsync(path.ToString(), encoding, cancellationToken);
  }

  public static Task WriteAllLinesAsync(
    this IFilePath path, IEnumerable<string> contents,
    CancellationToken cancellationToken = default)
  {
    return File.WriteAllLinesAsync(path.ToString(), contents, cancellationToken);
  }

  public static Task WriteAllLinesAsync(
    this IFilePath path, IEnumerable<string> contents, Encoding encoding,
    CancellationToken cancellationToken = default)
  {
    return File.WriteAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
  }

  public static Task AppendAllTextAsync(
    this IFilePath path, string contents,
    CancellationToken cancellationToken = default)
  {
    return File.AppendAllTextAsync(path.ToString(), contents, cancellationToken);
  }

  public static Task AppendAllTextAsync(
    this IFilePath path, string contents, Encoding encoding,
    CancellationToken cancellationToken = default)
  {
    return File.AppendAllTextAsync(path.ToString(), contents, encoding, cancellationToken);
  }

  public static Task AppendAllLinesAsync(
    this IFilePath path, IEnumerable<string> contents,
    CancellationToken cancellationToken = default)
  {
    return File.AppendAllLinesAsync(path.ToString(), contents, cancellationToken);
  }

  public static Task AppendAllLinesAsync(
    this IFilePath path, IEnumerable<string> contents, Encoding encoding,
    CancellationToken cancellationToken = default)
  {
    return File.AppendAllLinesAsync(path.ToString(), contents, encoding, cancellationToken);
  }

  public static void Copy(this IFilePath source, IFilePath destination, bool overwrite = false)
  {
    File.Copy(source.ToString(), destination.ToString(), overwrite);
  }

  public static void Move(this IFilePath source, IFilePath dest, bool overwrite = false)
  {
    File.Move(source.ToString(), dest.ToString(), overwrite);
  }

  public static void Replace(
    this IFilePath source, IFilePath destination, Maybe<AnyFilePath> destinationBackup = default,
    bool ignoreMetadataErrors = true)
  {
    File.Replace(source.ToString(), destination.ToString(), destinationBackup.Select(p => p.ToString()).OrElseDefault(), ignoreMetadataErrors);
  }

  public static void Encrypt(this IFilePath path)
  {
    File.Encrypt(path.ToString());
  }

  public static void Decrypt(this IFilePath path)
  {
    File.Decrypt(path.ToString());
  }

  public static void SetAccessControl(
    this AbsoluteFilePath path, FileSecurity fileSecurity)
  {
    path.Info().SetAccessControl(fileSecurity);
  }

  public static void SetAccessControl(
    this RelativeFilePath path, FileSecurity fileSecurity)
  {
    path.Info().SetAccessControl(fileSecurity);
  }

  public static void SetAccessControl(this AnyFilePath path, FileSecurity fileSecurity)
  {
    path.Info().SetAccessControl(fileSecurity);
  }

  public static FileSecurity GetAccessControl(this AbsoluteFilePath path)
  {
    return path.Info().GetAccessControl();
  }

  public static FileSecurity GetAccessControl(this RelativeFilePath path)
  {
    return path.Info().GetAccessControl();
  }

  public static FileSecurity GetAccessControl(this AnyFilePath path)
  {
    return path.Info().GetAccessControl();
  }

#if NET8_0_OR_GREATER
  // OpenHandle (.NET 8+)
  public static SafeFileHandle OpenHandle(
    this IFilePath path, FileMode mode = FileMode.Open, FileAccess access = FileAccess.Read,
    FileShare share = FileShare.Read, FileOptions options = FileOptions.None)
  {
    return File.OpenHandle(path.ToString(), mode, access, share, options);
  }
#endif
}