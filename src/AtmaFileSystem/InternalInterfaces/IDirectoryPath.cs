using System.IO;
using Functional.Maybe;

namespace AtmaFileSystem.InternalInterfaces
{
  internal interface IDirectoryPath<T> where T : IDirectoryPath<T>
  {
    DirectoryName DirectoryName();
    Maybe<T> ParentDirectory();
  }
}