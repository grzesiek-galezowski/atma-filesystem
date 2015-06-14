using System;
using AtmaFileSystem;
using Pri.LongPath;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class RelativePathWithFileNameSpecification
  {
    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => RelativePathWithFileName.Value(null));
    }

    [Fact]
    public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(RelativePathWithFileName.Value(@"lolek\\lolki2.txt"));
    }

    [Fact]
    public void ShouldThrowExceptionWhenTryingToCreateInstanceWithRootedPath()
    {
      Assert.Throws<InvalidOperationException>(() => RelativePathWithFileName.Value(@"C:\Dir\Subdir"));
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithEmptyValue()
    {
      Assert.Throws<ArgumentException>(() => RelativePathWithFileName.Value(string.Empty));
    }

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<RelativePathWithFileName>();
    }

    [Fact]
    public void ShouldAllowAccessingDirectoryOfThePath()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      var pathWithFileName = RelativePathWithFileName.From(dirPath, fileName);

      //WHEN
      RelativeDirectoryPath dirObtainedFromPath = pathWithFileName.Directory();

      //THEN
      Assert.Equal(dirPath, dirObtainedFromPath);
    }

    [Fact]
    public void ShouldAllowAccessingFileNameOfThePath()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      var pathWithFileName = RelativePathWithFileName.From(dirPath, fileName);

      //WHEN
      FileName fileNameObtainedFromPath = pathWithFileName.FileName();

      //THEN
      Assert.Equal(fileName, fileNameObtainedFromPath);
    }

    [Fact]
    public void ShouldBeConvertibleToFileInfo()
    {
      //GIVEN
      var pathWithFilename = RelativePathWithFileName.Value(@"lolek\lol.txt");

      //WHEN
      var fileInfo = pathWithFilename.Info();

      //THEN
      Assert.Equal(fileInfo.FullName, Path.Combine(new DirectoryInfo(".").FullName, pathWithFilename.ToString()));
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPathWithFileName()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativePathWithFileName>();

      //WHEN
      AnyPathWithFileName anyPathWithFileName = pathWithFileName.AsAnyPathWithFileName();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativePathWithFileName>();

      //WHEN
      AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
    }

    [Fact] //bug rename
    public void ShouldFormRelativePathWithFileNameWhenFileNameIsAddedToIt()
    {
      //GIVEN
      var directoryName = DirectoryName.Value("Dir1");
      var fileName = FileName.Value("file.txt");

      //WHEN
      RelativePathWithFileName relativePath = RelativePathWithFileName.From(directoryName, fileName);

      //THEN
      Assert.Equal(relativePath.ToString(), @"Dir1\file.txt");
    }

    [Fact] //bug rename
    public void ShouldFormRelativePathWithFileNameWhenRelativePathWithFileNameIsAddedToIt()
    {
      //GIVEN
      var directoryName = DirectoryName.Value("Dir1");
      var relativePathWithFileName = RelativePathWithFileName.Value(@"Subdir\file.txt");

      //WHEN
      RelativePathWithFileName relativePath = RelativePathWithFileName.From(directoryName, relativePathWithFileName);

      //THEN
      Assert.Equal(relativePath.ToString(), @"Dir1\Subdir\file.txt");
    }

    //bug add additional check to From() methods

  }
}
