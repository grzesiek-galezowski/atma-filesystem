using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class DoesNotContainInvalidCharsCondition : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentException(
      $"Path {path} contains invalid char {path[path.IndexOfAny(Path.GetInvalidPathChars())]} at index {path.IndexOfAny(Path.GetInvalidPathChars())}");
  }

  public bool FailsFor(string path)
  {
    return path.IndexOfAny(Path.GetInvalidPathChars()) != -1;
  }
}