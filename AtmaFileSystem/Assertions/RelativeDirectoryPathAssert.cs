using System;
using Pri.LongPath;

namespace AtmaFileSystem.Assertions
{
  static internal class RelativeDirectoryPathAssert
  {
    public static void Valid(string relativePath)
    {
      if (Path.IsPathRooted(relativePath)) throw new ArgumentException("Expected relative path, but got " + relativePath);
    }

    public static void NotEmpty(string relativePath)
    {
      if (relativePath == string.Empty) throw new ArgumentException("relative path cannot be empty");
    }

    public static void NotNull(string relativePath)
    {
      if (relativePath == null) throw new ArgumentException("relative path cannot be null");
    }
  }
}