using System.IO;

namespace AtmaFileSystem.InternalInterfaces
{
  internal interface IFilePath
  {
    FileInfo Info();
    FileName FileName();
    bool Has(FileExtension extensionValue);
    //bug AbsoluteFilePath ChangeExtensionTo(FileExtension value);
    //bug Maybe<AbsoluteDirectoryPath> FragmentEndingOnLast(DirectoryName directoryName);
  }
}