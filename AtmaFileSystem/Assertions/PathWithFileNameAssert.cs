using System;
using Pri.LongPath;

namespace AtmaFileSystem.Assertions
{
  static internal class PathWithFileNameAssert
  {
    internal static void Valid(string path)
    {
      if (!Path.IsPathRooted(path))
      {
        throw new ArgumentException(path);
      }
    }

    internal static void NotNull(string path)
    {
      if (null == path)
      {
        throw new ArgumentNullException(path);
      }
    }
  }
}