using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class FullyQualifiedCondition : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentException("Expected an absolute path, but got " + path);
  }

  public bool FailsFor(string path)
  {
    return !Path.IsPathFullyQualified(path);
  }
}