using System.Collections.Generic;

namespace AtmaFileSystem.Assertions;

internal static class Asserts
{
  public static void AssertAreMet(IEnumerable<IInitializerValueCondition> conditions, string path)
  {
    foreach (var condition in conditions)
    {
      if (condition.FailsFor(path))
      {
        throw condition.RuleException(path);
      }
    }
  }
}