using System;

namespace AtmaFileSystem;

public interface FileSystemComparisonRules
{
  bool ArePathStringsEqual(string pathString1, string pathString2);
}

public class UnixTypicalFileSystem : FileSystemComparisonRules
{
  public bool ArePathStringsEqual(string pathString1, string pathString2)
  {
    return string.Equals(pathString1, pathString2, StringComparison.Ordinal);
  }
}

public class WindowsTypicalFileSystem : FileSystemComparisonRules
{
  public bool ArePathStringsEqual(string pathString1, string pathString2)
  {
    return string.Equals(pathString1, pathString2, StringComparison.OrdinalIgnoreCase);
  }
}