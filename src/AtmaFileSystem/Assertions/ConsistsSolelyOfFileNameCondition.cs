using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class ConsistsSolelyOfFileNameCondition : IInitializerValueCondition
{
  public Exception RuleException(string path)
  {
    return new ArgumentException("Expected file name, but got " + path);
  }

  public bool FailsFor(string path)
  {
    return Path.GetFileName(path) != path;
  }
}