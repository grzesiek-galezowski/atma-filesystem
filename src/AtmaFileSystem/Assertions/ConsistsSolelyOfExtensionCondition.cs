using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class ConsistsSolelyOfExtensionCondition : IInitializerValueCondition
{
  public Exception RuleException(string? extensionString)
  {
    return new ArgumentException(
      $"Invalid extension {extensionString}. Expected extension: {Path.GetExtension(extensionString)}");
  }

  public bool FailsFor(string? extensionString)
  {
    return Path.GetExtension(extensionString) != extensionString;
  }
}