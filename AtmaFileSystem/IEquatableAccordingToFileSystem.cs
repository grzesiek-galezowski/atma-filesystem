namespace AtmaFileSystem
{
  public interface IEquatableAccordingToFileSystem<T>
  {
    bool Equals(T other, FileSystemComparisonRules fileSystemComparisonRules);
  }
}