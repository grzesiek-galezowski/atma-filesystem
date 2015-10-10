using System;
using Pri.LongPath;

namespace AtmaFileSystem.Assertions
{
  static class Asserts
  {
    public static void Rooted(string path, string message)
    {
      if (!Path.IsPathRooted(path))
      {
        throw new InvalidOperationException(message);
      }
    }

    public static void NotRooted(string path, string message)
    {
      if (Path.IsPathRooted(path))
      {
        throw new InvalidOperationException(message);
      }
    }

    public static void DoesNotConsistSolelyOfFileName(string path, string message)
    {
      if (Path.GetFileName(path) == path)
      {
        throw new InvalidOperationException(message);
      }
    }

    public static void NotEmpty(string path, string message)
    {
      if (path == String.Empty)
      {
        throw new ArgumentException(message, "path");
      }
    }

    public static void NotNull(string path, string paramName)
    {
      if (path == null)
      {
        throw new ArgumentNullException(paramName);
      }
    }

    public static void ValidDirectoryName(string value, string message)
    {
      var directoryName = new DirectoryInfo(value).Name;
      if (directoryName != value)
      {
        throw new ArgumentException(message);
      }
    }

    public static void ConsistsSolelyOfExtension(string extensionString, string message)
    {
      if (Path.GetExtension(extensionString) != extensionString)
      {
        throw new InvalidOperationException(message);
      }
    }

    public static void ConsistsSolelyOfFileName(string path)
    {
      if (Path.GetFileName(path) != path)
      {
        throw new InvalidOperationException("Expected file name, but got " + path);
      }
    }

    public static void DirectoryPathValid(string path, string message)
    {
      try
      {
        Path.IsPathRooted(path);
      }
      catch (ArgumentException e)
      {
        throw new InvalidOperationException(message, e);
      }
    }
  }
}