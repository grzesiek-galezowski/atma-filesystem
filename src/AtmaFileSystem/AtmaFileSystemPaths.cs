namespace AtmaFileSystem;

public class AtmaFileSystemPaths
{
    public static AnyDirectoryPath AnyDirectoryPath(string value) =>
        AtmaFileSystem.AnyDirectoryPath.Value(value);
    public static AnyFilePath AnyFilePath(string value) =>
        AtmaFileSystem.AnyFilePath.Value(value);
    public static AnyPath AnyPath(string value) =>
        AtmaFileSystem.AnyPath.Value(value);
    public static AbsoluteDirectoryPath AbsoluteDirectoryPath(string value) =>
        AtmaFileSystem.AbsoluteDirectoryPath.Value(value);
    public static AbsoluteFilePath AbsoluteFilePath(string value) =>
        AtmaFileSystem.AbsoluteFilePath.Value(value);
    public static RelativeDirectoryPath RelativeDirectoryPath(string value) =>
        AtmaFileSystem.RelativeDirectoryPath.Value(value);
    public static RelativeFilePath RelativeFilePath(string value) =>
        AtmaFileSystem.RelativeFilePath.Value(value);
    public static DirectoryName DirectoryName(string value) =>
        AtmaFileSystem.DirectoryName.Value(value);
    public static FileName FileName(string value) =>
        AtmaFileSystem.FileName.Value(value);
    public static FileExtension FileExtension(string value) =>
        AtmaFileSystem.FileExtension.Value(value);
    public static FileNameWithoutExtension FileNameWithoutExtension(string value) =>
        AtmaFileSystem.FileNameWithoutExtension.Value(value);
}