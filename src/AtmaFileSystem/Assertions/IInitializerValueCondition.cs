using System;

namespace AtmaFileSystem.Assertions;

public interface IInitializerValueCondition
{
  Exception RuleException(string path);
  bool FailsFor(string path);
}