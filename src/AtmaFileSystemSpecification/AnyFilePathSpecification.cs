using System;
using AtmaFileSystem;
using FluentAssertions;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.XFluentAssert.Root;
using TddXt.XFluentAssertRoot;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification
{
  public class AnyFilePathSpecification
  {
    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
      InlineData(@"\\\\\\\\\?|/\/|", typeof(ArgumentException)),
    ]
    public void ShouldThrowExceptionWhenCreatedWithNullValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyFilePath.Value(invalidInput));
    }

    [Fact]
    public void ShouldAllowToBeCreatedWithFileNameOnly()
    {
      //GIVEN
      var value = AnyFilePath.Value("file.txt");

      //THEN
      Assert.Equal("file.txt", value.ToString());

    }
    //bug parent directory returns maybe!

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(AnyFilePath).Should().HaveValueSemantics();
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<AnyFilePath>();

      //WHEN
      AnyPath anyPath = pathWithFileName.AsAnyPath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPath.ToString());
    }

    [Theory,
     InlineData(@"Dir\Subdir\fileName.txt", ".txt", true),
     InlineData(@"Dir\Subdir\fileName.tx", ".txt", false),
     InlineData(@"Dir\Subdir\fileName", ".txt", false),
    ]
    public void ShouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
    {
      //GIVEN
      var anyPathWithFileName = AnyFilePath.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = anyPathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

    [Fact]
    public void ShouldAllowAccessingFileName()
    {
      //GIVEN
      var path = AnyFilePath.Value(@"Dir\Subdir\fileName.txt");

      //WHEN
      var fileName = path.FileName();

      //THEN
      Assert.Equal(FileName.Value("fileName.txt"), fileName);

    }

    [Fact]
    public void ShouldAllowAccessingDirectoryOfThePathWhenSuchDirectoryExists()
    {
      //GIVEN
      var dirPath = Any.Instance<AnyDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      AnyFilePath filePath = dirPath + fileName;

      //WHEN
      var dirObtainedFromPath = filePath.ParentDirectory();

      //THEN
      Assert.Equal(dirPath, dirObtainedFromPath.Value);
    }

    [Fact]
    public void ShouldReturnNothingWhenAskingForDirectoryOfThePathAndSuchDirectoryDoesNotExist()
    {
      //GIVEN
      AnyFilePath filePath = AnyFilePath.Value("file.txt");

      //WHEN
      var dirObtainedFromPath = filePath.ParentDirectory();

      //THEN
      Assert.False(dirObtainedFromPath.HasValue);
      Assert.Throws<InvalidOperationException>(() => dirObtainedFromPath.Value);
    }

    [Fact]
    public void ShouldBeConvertibleToFileInfo()
    {
      //GIVEN
      var pathWithFilename = AnyFilePath.Value(@"C:\Directory\file.txt");

      //WHEN
      var fileInfo = pathWithFilename.Info();

      //THEN
      Assert.Equal(fileInfo.FullName, pathWithFilename.ToString());
    }

    [Fact]
    public void ShouldAllowChangingExtension()
    {
      //GIVEN
      var filePath = AnyFilePath.Value(@"C:\Dir\subdir\file.txt");

      //WHEN
      AnyFilePath pathWithNewExtension = filePath.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"C:\Dir\subdir\file.doc", pathWithNewExtension.ToString());

    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<AnyFilePath>();
      var path2 = Any.Instance<AnyFilePath>();
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
