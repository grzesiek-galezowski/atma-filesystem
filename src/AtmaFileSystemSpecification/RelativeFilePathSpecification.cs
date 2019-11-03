using System;
using AtmaFileSystem;
using NSubstitute;
using System.IO;
using FluentAssertions;
using TddXt.AnyRoot;
using TddXt.XFluentAssert.Root;
using TddXt.XFluentAssertRoot;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification
{
  public class RelativeFilePathSpecification
  {
    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => RelativeFilePath.Value(null));
    }

    [Fact]
    public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(RelativeFilePath.Value(@"lolek\\lolki2.txt"));
    }

    [Fact]
    public void ShouldThrowExceptionWhenTryingToCreateInstanceWithRootedPath()
    {
      Assert.Throws<ArgumentException>(() => RelativeFilePath.Value(@"C:\Dir\Subdir"));
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithEmptyValue()
    {
      Assert.Throws<ArgumentException>(() => RelativeFilePath.Value(string.Empty));
    }

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(RelativeFilePath).Should().HaveValueSemantics();
    }

    [Fact]
    public void ShouldAllowAccessingDirectoryOfThePathWhenSuchDirectoryExists()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      RelativeFilePath filePath = dirPath + fileName;

      //WHEN
      var dirObtainedFromPath = filePath.ParentDirectory();


      //THEN
      Assert.Equal(dirPath, dirObtainedFromPath.Value);
    }

    [Fact]
    public void ShouldReturnNothingWhenAskingForDirectoryOfThePathAndSuchDirectoryDoesNotExist()
    {
      //GIVEN
      RelativeFilePath filePath = RelativeFilePath.Value("file.txt");

      //WHEN
      var dirObtainedFromPath = filePath.ParentDirectory();

      //THEN
      Assert.False(dirObtainedFromPath.HasValue);
      Assert.Throws<InvalidOperationException>(() => dirObtainedFromPath.Value);
    }

    [Fact]
    public void ShouldAllowAccessingFileNameOfThePath()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      RelativeFilePath filePath =dirPath + fileName;

      //WHEN
      FileName fileNameObtainedFromPath = filePath.FileName();

      //THEN
      Assert.Equal(fileName, fileNameObtainedFromPath);
    }

    [Fact]
    public void ShouldBeConvertibleToFileInfo()
    {
      //GIVEN
      var pathWithFilename = RelativeFilePath.Value(@"lolek\lol.txt");

      //WHEN
      var fileInfo = pathWithFilename.Info();

      //THEN
      Assert.Equal(fileInfo.FullName, Path.Combine(new DirectoryInfo(".").FullName, pathWithFilename.ToString()));
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPathWithFileName()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativeFilePath>();

      //WHEN
      AnyFilePath anyFilePath = pathWithFileName.AsAnyFilePath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyFilePath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativeFilePath>();

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
      var pathWithFileName = RelativeFilePath.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = pathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

    [Fact]
    public void ShouldAllowChangingExtension()
    {
      //GIVEN
      var filePath = RelativeFilePath.Value(@"Dir\subdir\file.txt");

      //WHEN
      RelativeFilePath pathWithNewExtension = filePath.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"Dir\subdir\file.doc", pathWithNewExtension.ToString());

    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<RelativeFilePath>();
      var path2 = Any.Instance<RelativeFilePath>();
      var fileSystemComparisonRules = Substitute.For<FileSystemComparisonRules>();
      var comparisonResult = Any.Boolean();

      fileSystemComparisonRules
        .ArePathStringsEqual(path1.ToString(), path2.ToString())
        .Returns(comparisonResult);

      //WHEN
      var equality = path1.ShallowEquals(path2, fileSystemComparisonRules);

      //THEN
      Assert.Equal(comparisonResult, equality);
    }

  }

}
