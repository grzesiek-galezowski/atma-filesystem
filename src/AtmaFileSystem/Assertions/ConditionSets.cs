using System.Collections.Generic;
using System.IO;

namespace AtmaFileSystem.Assertions;

public static class ConditionSets
{
  private static readonly FullyQualifiedCondition FullyQualifiedCondition = new();
  private static readonly NotFullyQualifiedCondition NotFullyQualifiedCondition = new();
  private static readonly NotEmptyCondition PathNotEmptyCondition = new(ExceptionMessages.PathCannotBeAnEmptyString);
  private static readonly ValidDirectoryNameCondition ValidDirectoryNameCondition = new();
  private static readonly ConsistsSolelyOfExtensionCondition ConsistsSolelyOfExtensionCondition = new();
  private static readonly ConsistsSolelyOfFileNameCondition ConsistsSolelyOfFileNameCondition = new();
  private static readonly DirectoryPathValidCondition DirectoryPathValidCondition = new();
  private static readonly DoesNotContainInvalidCharsCondition DoesNotContainInvalidCharsCondition = new();
  private static readonly NotAllWhitespaceCondition NotAllWhitespaceCondition = new();
  
  public static IEnumerable<IInitializerValueCondition> GetAbsoluteAnyPathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return FullyQualifiedCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetAbsoluteDirectoryPathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return PathNotEmptyCondition; //bug not empty or not whitespace??
    yield return FullyQualifiedCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetAbsoluteFilePathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return PathNotEmptyCondition; //bug not empty or not whitespace??
    yield return FullyQualifiedCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetAnyDirectoryPathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return NotAllWhitespaceCondition;
    yield return DirectoryPathValidCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetAnyPathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return NotAllWhitespaceCondition;
    yield return DirectoryPathValidCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetDirectoryNameConditions(string directoryNameArgName)
  {
    yield return new NotNullCondition(directoryNameArgName);
    yield return NotAllWhitespaceCondition;
    yield return ValidDirectoryNameCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetFileNameConditions(string fileNameArgName)
  {
    yield return new NotNullCondition(fileNameArgName);
    yield return PathNotEmptyCondition;
    yield return ConsistsSolelyOfFileNameCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetRelativeAnyPathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return PathNotEmptyCondition;
    yield return NotFullyQualifiedCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetRelativeDirectoryPathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return NotAllWhitespaceCondition;
    yield return NotFullyQualifiedCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetRelativeFilePathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return PathNotEmptyCondition;
    yield return NotFullyQualifiedCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetAnyFilePathConditions(string pathName)
  {
    yield return new NotNullCondition(pathName);
    yield return PathNotEmptyCondition;
    yield return DirectoryPathValidCondition;
    yield return DoesNotContainInvalidCharsCondition;
  }

  public static IEnumerable<IInitializerValueCondition> GetFileExtensionConditions(string extensionStringName)
  {
    yield return new NotNullCondition(extensionStringName);
    yield return PathNotEmptyCondition;
    yield return ConsistsSolelyOfExtensionCondition;
  }
}