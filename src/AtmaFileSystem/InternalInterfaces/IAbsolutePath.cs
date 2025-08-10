using Core.Maybe;

namespace AtmaFileSystem.InternalInterfaces;

internal interface IAbsolutePath
{
  AbsoluteDirectoryPath Root();
  Maybe<AbsoluteDirectoryPath> ParentDirectory(uint index);
}