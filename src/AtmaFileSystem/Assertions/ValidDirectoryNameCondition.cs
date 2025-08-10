using System;
using System.IO;

namespace AtmaFileSystem.Assertions;

public class ValidDirectoryNameCondition : IInitializerValueCondition
{
  public Exception RuleException(string value)
  {
    return new ArgumentException(
      ExceptionMessages.ValueFragment(value, "is not a valid directory name"));
  }

  public bool FailsFor(string value)
  {
    var failed = false;
    if (value != string.Empty)
    {
      var directoryName = new DirectoryInfo(value).Name;
      if (directoryName != value)
      {
        failed = true;
      }
    }

    return failed;
  }
}