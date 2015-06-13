using System;
using Pri.LongPath;

namespace AtmaFileSystem.Assertions
{
  static internal class AnyDirectoryPathAssertions
  {
    public static void NotEmpty(string path)
    {
      if (path == String.Empty) throw new ArgumentException("relative path cannot be empty");
    }

    public static void NotNull(string path)
    {
      if (path == null) throw new ArgumentNullException("path cannot be null");
    }

    public static void AssertPathValid(string path) //TODO refactor all assertions
    {
      try
      {
        Path.IsPathRooted(path);
      }
      catch (ArgumentException e)
      {
        throw new ArgumentException("The value is invalid", "path", e);
      }
    }
  }
}