using System;
using System.IO;

namespace AtmaFileSystem.IO;

public class TempDirectory
{
  public static AbsoluteDirectoryPath CreateTempSubdirectory(string? prefix = null)
  {
    return AbsoluteDirectoryPath.Value(Directory.CreateTempSubdirectory(prefix).FullName);
  }
  
  public static AbsoluteFilePath CreateTempFileName()
  {
    return AbsoluteFilePath.Value(Path.GetTempFileName());
  }

  public static AbsoluteDirectoryPath GetTempPath()
  {
    return AbsoluteDirectoryPath.OfTemp();
  }
 
}