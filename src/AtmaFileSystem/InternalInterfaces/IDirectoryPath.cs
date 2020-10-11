using Functional.Maybe;

namespace AtmaFileSystem.InternalInterfaces
{
  internal interface IDirectoryPath<T> where T : IDirectoryPath<T>
  {
    DirectoryName DirectoryName();
    Maybe<T> ParentDirectory();
    Maybe<T> FindCommonDirectoryPathWith(T path2);
    bool StartsWith(T path2);
  }
}