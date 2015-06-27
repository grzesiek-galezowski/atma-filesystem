using AtmaFileSystem;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class Examples
  {
    [Fact]
    public void Example1_AssemblingAndDisassembling()
    {
      ////////////////////////
      // Disassembling:
      ////////////////////////
      
      AbsoluteFilePath fullPath = AbsoluteFilePath.Value(@"C:\Program Files\Lolokimono\Config.txt");
      AbsoluteDirectoryPath directoryPath = fullPath.Directory();
      FileName fileName = fullPath.FileName();
      FileNameWithoutExtension fileNameWithoutExtension = fileName.WithoutExtension();
      Maybe<FileExtension> extension = fileName.Extension();
      AbsoluteDirectoryPath rootFromFullPath = fullPath.Root();
      AbsoluteDirectoryPath rootFromDirectoryPath = directoryPath.Root();


      ////////////////////////
      // Assembling:
      ////////////////////////

      
      AbsoluteFilePath fullPathAssembled = directoryPath + (fileNameWithoutExtension + extension.Value());

      DirectoryName dirName = DirectoryName.Value("Subdirectory");
      AbsoluteFilePath fileMovedToSubdirectory = directoryPath + dirName + fileName;


      // TODO
      // - add WithoutRoot() to PathWithFileName method that returns relative directory
      // - add WithoutRoot() to PathToDirectory method that returns relative directory

    }

    [Fact]
    public void Example1_ParentDirectories()
    {
      AbsoluteFilePath fullPath = AbsoluteFilePath.Value(@"C:\Program Files\Lolokimono\Config.txt");
      AbsoluteDirectoryPath directoryPath = fullPath.Directory();

      Maybe<AbsoluteDirectoryPath> parent = directoryPath.Parent();

      if (parent.Found)
      {
        Maybe<AbsoluteDirectoryPath> parentParent = parent.Value().Parent();
        if (parentParent.Found)
        {
          Maybe<AbsoluteDirectoryPath> parentParentParent = parentParent.Value().Parent();
        }
      }

    }

  }
}
