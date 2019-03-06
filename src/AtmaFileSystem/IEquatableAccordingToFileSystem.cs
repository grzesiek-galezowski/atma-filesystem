namespace AtmaFileSystem
{
  public interface IEquatableAccordingToFileSystem<in T>
  {
    bool ShallowEquals(T other, FileSystemComparisonRules fileSystemComparisonRules);
  }
}