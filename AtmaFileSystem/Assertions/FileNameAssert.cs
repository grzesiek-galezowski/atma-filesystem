using System;
using Pri.LongPath;

namespace AtmaFileSystem.Assertions
{
  static internal class FileNameAssert
  {
    internal static void Valid(string path)
    {
      if (Path.GetFileName(path) != path)
      {
        throw new ArgumentException(path);
      }
    }

    internal static void NotEmpty(string path)
    {
      if (null == path)
      {
        throw new ArgumentNullException("path");
      }
    }
  }
}