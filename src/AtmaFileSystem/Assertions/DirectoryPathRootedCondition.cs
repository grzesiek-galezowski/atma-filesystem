using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class DirectoryPathRootedCondition : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentException(ExceptionMessages.PathFragment(path, "is not a valid directory path"));
  }

  public bool FailsFor(string? path)
  {
    var failed = false;
    try
    {
      Path.IsPathRooted(path);
    }
    catch (ArgumentException e)
    {
      failed = true;
    }

    return failed;
  }
}