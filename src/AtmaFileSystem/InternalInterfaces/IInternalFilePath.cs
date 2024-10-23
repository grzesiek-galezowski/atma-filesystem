using System.IO;

namespace AtmaFileSystem.InternalInterfaces;

internal interface IInternalFilePath<out TPath> : IFilePath
{
    FileInfo Info();
    FileName FileName();
    bool Has(FileExtension extensionValue);
    TPath ChangeExtensionTo(FileExtension value);
}