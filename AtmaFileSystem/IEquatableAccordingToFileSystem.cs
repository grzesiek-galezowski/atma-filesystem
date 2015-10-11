namespace AtmaFileSystem
{
  public interface IEquatableAccordingToFileSystem<T>
  {
    bool ShallowEquals(T other, FileSystemComparisonRules fileSystemComparisonRules);
  }
}