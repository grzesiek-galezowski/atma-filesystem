using System;
using AtmaFileSystem;
using Pri.LongPath;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class AnyDirectoryPathSpecification
  {
    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
      InlineData(@"\\\\\\\\\?|/\/|", typeof(InvalidOperationException)),
    ]
    public void ShouldThrowExceptionWhenCreatedWithNullValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyDirectoryPath.Value(invalidInput));
    }

    [Fact]
    public void ShouldReturnNonNullValueWhenValidPathIsPassed()
    {
      //GIVEN
      const string relativePath = @"Dir\Subdir";
      const string absolutePath = @"C:\Dir\Subdir";
      var path = AnyDirectoryPath.Value(relativePath);
      var path2 = AnyDirectoryPath.Value(absolutePath);

      //THEN
      Assert.Equal(relativePath, path.ToString());
      Assert.Equal(absolutePath, path2.ToString());
    }

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<AnyDirectoryPath>();
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
      AnyFilePath anyFilePath
        = anyDirectoryPath + fileName;

      //THEN
      Assert.Equal(
        Path.Combine(anyDirectoryPath.ToString(), fileName.ToString()), anyFilePath.ToString());
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
        Path.Combine(anyDirectoryPath.ToString(), directoryName.ToString()), directoryPath.ToString());
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
        Path.Combine(anyDirectoryPath.ToString(), directoryName.ToString()), directoryPath.ToString());
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
        Path.Combine(anyDirectoryPath.ToString(), pathWithFileName.ToString()), anyFilePath.ToString());
    }

    [Theory,
      InlineData(@"Segment1\Segment2\", "Segment2"),
      InlineData(@"Segment1\", "Segment1"),
      InlineData(@"C:\Segment1\", "Segment1"),
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
      var parent = dir.Parent();

      //THEN
      Assert.True(parent.Found);
      Assert.Equal(AnyDirectoryPath.Value(expected), parent.Value());
    }

    [Fact]
    public void ShouldProduceParentWithoutValueThatThrowsOnAccessWhenThereIsNoParentInPath()
    {
      //GIVEN
      const string pathString = @"C:\";
      var dir = AnyDirectoryPath.Value(pathString);

      //WHEN
      var parent = dir.Parent();

      //THEN
      Assert.False(parent.Found);
      Assert.Throws<InvalidOperationException>(() => parent.Value());
    }

    [Fact]
    public void ShouldBeConvertibleToDirectoryInfo()
    {
      //GIVEN
      var path = AnyDirectoryPath.Value(@"Dir\Subdir");

      //WHEN
      var directoryInfo = path.Info();

      //THEN
      Assert.Equal(directoryInfo.FullName, FullNameFrom(path));
    }

    private static string FullNameFrom(AnyDirectoryPath path)
    {
      return Path.Combine(new DirectoryInfo(".").FullName, path.ToString());
    }


  }
}
