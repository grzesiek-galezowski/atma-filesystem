using System;

namespace AtmaFileSystem.Assertions;

public class NotNullCondition(string paramName) : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentNullException(paramName);
  }

  public bool FailsFor(string? path)
  {
    return path == null;
  }
}