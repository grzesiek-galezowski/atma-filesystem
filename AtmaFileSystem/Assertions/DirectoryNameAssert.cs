using System;
using Pri.LongPath;

namespace AtmaFileSystem
{
  public static class DirectoryNameAssert
  {
    public static void NotNull(string value)
    {
      if(value == null) throw new ArgumentException("directory name cannot be null");
    }

    public static void NotEmpty(string value)
    {
      if(value == string.Empty) throw new ArgumentException("directory name cannot be empty");
    }

    public static void Valid(string value)
    {
      var directoryName = new DirectoryInfo(value).Name;
      if (directoryName != value)
      {
        throw new ArgumentException("The value " + value + " does not constitute a valid directory name");
      }
    }
  }
}