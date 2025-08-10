using Core.Maybe;

namespace AtmaFileSystem.InternalInterfaces;

public interface IDirectoryPath
{
  public string ToString();
}

internal interface IDirectoryPath<T> : IDirectoryPath where T : IDirectoryPath<T>
{
  DirectoryName DirectoryName();
  Maybe<T> ParentDirectory();
  Maybe<T> FindCommonDirectoryPathWith(T path2);
  bool StartsWith(T path2);
  T AddDirectoryName(string dirName);
}