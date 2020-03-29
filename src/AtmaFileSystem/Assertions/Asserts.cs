using System;
using System.IO;
using System.Linq;

namespace AtmaFileSystem.Assertions
{
  internal static class Asserts
  {
    public static void Rooted(string path, string message)
    {
      if (!Path.IsPathRooted(path))
      {
        throw new ArgumentException(message);
      }
    }

    public static void NotRooted(string path, string message)
    {
      if (Path.IsPathRooted(path))
      {
        throw new ArgumentException(message);
      }
    }

    public static void DoesNotConsistSolelyOfFileName(string path, string message)
    {
      if (Path.GetFileName(path) == path)
      {
        throw new ArgumentException(message);
      }
    }

    public static void NotEmpty(string path, string message)
    {
      if (path == string.Empty)
      {
        throw new ArgumentException(message, nameof(path));
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
      if (value != string.Empty)
      {
        var directoryName = new DirectoryInfo(value).Name;
        if (directoryName != value)
        {
          throw new ArgumentException(message);
        }
      }
    }

    public static void ConsistsSolelyOfExtension(string extensionString, string message)
    {
      if (Path.GetExtension(extensionString) != extensionString)
      {
        throw new ArgumentException(message);
      }
    }

    public static void ConsistsSolelyOfFileName(string path)
    {
      if (Path.GetFileName(path) != path)
      {
        throw new ArgumentException("Expected file name, but got " + path);
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
        throw new ArgumentException(message, e);
      }
    }

    public static void DoesNotContainInvalidChars(string path)
    {
      var invalidCharIndex = path.IndexOfAny(Path.GetInvalidPathChars().ToArray());
      if (invalidCharIndex != -1)
      {
        throw new ArgumentException(
          $"Path {path} contains invalid char {path[invalidCharIndex]} at index {invalidCharIndex}");
      }
    }

    // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
    public static void NotWhitespace(string path, string message)
    {
      if (path != string.Empty && path.All(char.IsWhiteSpace))
      {
        throw new ArgumentException(message);
      }
    }
  }
}