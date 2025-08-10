using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class NotFullyQualifiedCondition : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentException(ExceptionMessages.PathFragment(path, "is fully qualified") + ExceptionMessages.RootedPathsAreIllegalPleasePassARelativePath);
  }

  public bool FailsFor(string path)
  {
    return Path.IsPathFullyQualified(path);
  }
}