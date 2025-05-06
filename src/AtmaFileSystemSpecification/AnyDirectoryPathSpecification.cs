using System;
using AtmaFileSystem;
using NSubstitute;
using System.IO;
using FluentAssertions;
using Core.Maybe;
using TddXt.AnyRoot;
using TddXt.XFluentAssert.Api;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification;

public class AnyDirectoryPathSpecification
{
  [Theory,
   InlineData(null, typeof(ArgumentNullException)),
   InlineData(" ", typeof(ArgumentException)),
   InlineData(@"\\\\\\\\\?|/\/|", typeof(ArgumentException)),
  ]
  public void ShouldThrowExceptionWhenCreatedWithNullValue(string? invalidInput, Type exceptionType)
  {
    Assert.Throws(exceptionType, () => AnyDirectoryPath.Value(invalidInput!));
  }

  [Fact]
  public void ShouldReturnNonNullValueWhenValidPathIsPassed()
  {
    //GIVEN
    const string relativePath = @"Dir\Subdir";
    const string absolutePath = @"C:\Dir\Subdir";
    const string emptyPath = @"C:\Dir\Subdir";
    var path = AnyDirectoryPath.Value(relativePath);
    var path2 = AnyDirectoryPath.Value(absolutePath);
    var path3 = AnyDirectoryPath.Value(emptyPath);

    //THEN
    Assert.Equal(relativePath, path.ToString());
    Assert.Equal(absolutePath, path2.ToString());
    Assert.Equal(emptyPath, path3.ToString());
  }

  [Fact]
  public void ShouldBehaveLikeValue()
  {
    ObjectsOfType<AnyDirectoryPath>.ShouldHaveValueSemantics(
      [
        () => AnyDirectoryPath.Value("C:\\")
      ],
      [
        () => AnyDirectoryPath.Value("c:\\"), 
        () => AnyDirectoryPath.Value("D:\\")
      ]);
  }

  [Fact]
  public void ShouldBeConvertibleToAnyPath()
  {
    //GIVEN
    var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();

    //WHEN
    AnyPath anyPath = anyDirectoryPath.AsAnyPath();

    //THEN
    Assert.Equal(anyDirectoryPath.ToString(), anyPath.ToString());
  }

  [Fact]
  public void ShouldAllowConvertingToAnyPathWithFileNameByAddingFileName()
  {
    //GIVEN
    var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();
    var fileName = Any.Instance<FileName>();

    //WHEN
    AnyFilePath anyFilePath = anyDirectoryPath + fileName;

    //THEN
    Assert.Equal(
      Path.Join(anyDirectoryPath.ToString(), fileName.ToString()), anyFilePath.ToString());
  }

  [Fact]
  public void ShouldAllowAddingDirectoryName()
  {
    //GIVEN
    var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();
    var directoryName = Any.Instance<DirectoryName>();

    //WHEN
    AnyDirectoryPath directoryPath
      = anyDirectoryPath + directoryName;

    //THEN
    Assert.Equal(
      Path.Join(anyDirectoryPath.ToString(), directoryName.ToString()), directoryPath.ToString());
  }

  [Fact]
  public void ShouldAllowAddingRelativeDirectoryPath()
  {
    //GIVEN
    var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();
    var directoryName = Any.Instance<RelativeDirectoryPath>();

    //WHEN
    AnyDirectoryPath directoryPath
      = anyDirectoryPath + directoryName;

    //THEN
    Assert.Equal(
      Path.Join(anyDirectoryPath.ToString(), directoryName.ToString()), directoryPath.ToString());
  }

  [Fact]
  public void ShouldAllowAddingRelativePathWithFileName()
  {
    //GIVEN
    var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();
    var pathWithFileName = Any.Instance<RelativeFilePath>();

    //WHEN
    AnyFilePath anyFilePath
      = anyDirectoryPath + pathWithFileName;

    //THEN
    Assert.Equal(
      Path.Join(anyDirectoryPath.ToString(), pathWithFileName.ToString()), anyFilePath.ToString());
  }

  [Theory,
   InlineData(@"Segment1\Segment2\", "Segment2"),
   InlineData(@"Segment1\", "Segment1"),
   InlineData(@"C:\Segment1\", "Segment1"),
   InlineData("", ""),
  ]
  public void ShouldAllowGettingTheNameOfCurrentDirectory(string fullPath, string expectedDirectoryName)
  {
    //GIVEN
    var directoryPath = AnyDirectoryPath.Value(fullPath);

    //WHEN
    DirectoryName dirName = directoryPath.DirectoryName();

    //THEN
    Assert.Equal(expectedDirectoryName, dirName.ToString());
  }

  [Theory,
   InlineData(@"C:\parent\child\", @"C:\parent"),
   InlineData(@"C:\parent\", @"C:\")]
  public void ShouldAllowGettingProperParentDirectoryWhenItExists(string input, string expected)
  {
    //GIVEN
    var dir = AnyDirectoryPath.Value(input);

    //WHEN
    var parent = dir.ParentDirectory();

    //THEN
    Assert.True(parent.HasValue);
    parent.Value().Should().Be(AnyDirectoryPath.Value(expected));
  }

  [Theory]
  [InlineData(@"C:\")]
  [InlineData(@"")]
  public void ShouldProduceParentWithoutValueThatThrowsOnAccessWhenThereIsNoParentInPath(string pathString)
  {
    //GIVEN
    var dir = AnyDirectoryPath.Value(pathString);

    //WHEN
    var parent = dir.ParentDirectory();

    //THEN
    Assert.False(parent.HasValue);
    Assert.Throws<InvalidOperationException>(() => parent.Value());
  }

  [Fact]
  public void ShouldBeConvertibleToDirectoryInfoWhenPathExists()
  {
    //GIVEN
    var path = AnyDirectoryPath.Value(@"Dir\Subdir");

    //WHEN
    var directoryInfo = path.Info();

    //THEN
    directoryInfo.Value().FullName.Should().Be(FullNameFrom(path));
  }
    
  [Fact]
  public void ShouldBeConvertibleToNothingOfDirectoryInfoWhenPathIsEmpty()
  {
    //GIVEN
    var path = AnyDirectoryPath.Value("");

    //WHEN
    var directoryInfo = path.Info();

    //THEN
    directoryInfo.Should().Be(Maybe<DirectoryInfo>.Nothing);
  }

  [Fact]
  public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
  {
    //GIVEN
    var path1 = Any.Instance<AnyDirectoryPath>();
    var path2 = Any.Instance<AnyDirectoryPath>();
    var fileSystemComparisonRules = Substitute.For<FileSystemComparisonRules>();
    var comparisonResult = Any.Boolean();

    fileSystemComparisonRules
      .ArePathStringsEqual(path1.ToString(), path2.ToString())
      .Returns(comparisonResult);


    //WHEN
    var equality = path1.ShallowEquals(path2, fileSystemComparisonRules);

    //THEN
    equality.Should().Be(comparisonResult);
  }


  private static string FullNameFrom(AnyDirectoryPath path)
  {
    return Path.Join(new DirectoryInfo(".").FullName, path.ToString());
  }


}