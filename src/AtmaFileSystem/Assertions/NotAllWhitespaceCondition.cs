using System;
using System.Linq;

namespace AtmaFileSystem.Assertions;

public class NotAllWhitespaceCondition : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentException(ExceptionMessages.ValueFragment(path, "consists only of whitespace"));
  }

  public bool FailsFor(string path)
  {
    return path != string.Empty && path.All(char.IsWhiteSpace);
  }
}