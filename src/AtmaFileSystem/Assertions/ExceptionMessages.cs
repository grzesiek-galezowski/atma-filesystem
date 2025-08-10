namespace AtmaFileSystem.Assertions;

internal static class ExceptionMessages
{
  public static readonly string RootedPathsAreIllegalPleasePassARelativePath =
      "Rooted paths are illegal, please pass a relative path";

  public static readonly string ThePathCannotConsistSolelyOfFileName = "The path cannot consist solely of file name";
  public static readonly string PathCannotBeAnEmptyString = "cannot be an empty string";

  public static string PathFragment(string path, string constraintBreak)
  {
    return $"Path {path} {constraintBreak}. ";
  }

  public static string ValueFragment(string value, string constraintBreak)
  {
    return $"Value {value} {constraintBreak}. ";
  }
}