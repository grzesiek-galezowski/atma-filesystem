using System;
using Pri.LongPath;

namespace AtmaFileSystem.Assertions
{
  static internal class RelativePathWithFileNameAssertions
  {
    public static void Valid(string path)
    {
      if (Path.IsPathRooted(path))
      {
        throw new InvalidOperationException("Rooted paths are illegal, please pass a relative path");
      }
    }

    public static void NotEmpty(string path)
    {
      if (path == String.Empty)
      {
        throw new ArgumentException("cannot be an empty string", "path");
      }
    }

    public static void NotNull(string path)
    {
      if (path == null)
      {
        throw new ArgumentNullException("path");
      }
    }
  }
}