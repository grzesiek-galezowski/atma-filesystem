using System.IO;

namespace AtmaFileSystem.InternalInterfaces;

internal interface IFilePath<out TPath>
{
    FileInfo Info();
    FileName FileName();
    bool Has(FileExtension extensionValue);
    TPath ChangeExtensionTo(FileExtension value);
}