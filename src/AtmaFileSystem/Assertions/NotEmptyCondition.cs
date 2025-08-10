using System;

namespace AtmaFileSystem.Assertions;

public class NotEmptyCondition(string errorMessage) : IInitializerValueCondition
{
  public Exception RuleException(string value)
  {
    return new ArgumentException(ExceptionMessages.PathFragment(value, "is empty") + errorMessage, nameof(value));
  }

  public bool FailsFor(string path)
  {
    return path == string.Empty;
  }
}