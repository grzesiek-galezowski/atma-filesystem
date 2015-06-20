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
    public void ShouldThrowExceptionWhenTryingToCreateInstanceWithNoDirectory()
    {
      Assert.Throws<InvalidOperationException>(() => RelativePathWithFileName.Value(@"file.txt"));
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
      RelativePathWithFileName pathWithFileName = dirPath + fileName;

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
      RelativePathWithFileName pathWithFileName =dirPath + fileName;

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

    [Theory,
     InlineData(@"Dir\Subdir\fileName.txt", ".txt", true),
     InlineData(@"Dir\Subdir\fileName.tx", ".txt", false),
     InlineData(@"Dir\Subdir\fileName", ".txt", false),
    ]
    public void ShouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
    {
      //GIVEN
      var pathWithFileName = RelativePathWithFileName.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = pathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

  }
}
