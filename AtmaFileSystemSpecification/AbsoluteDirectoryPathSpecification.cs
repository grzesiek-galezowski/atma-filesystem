using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AtmaFileSystem;
using AtmaFileSystemSpecification;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class AbsoluteDirectoryPathSpecification
  {

    [Theory,
     InlineData(null, typeof (ArgumentNullException)),
     InlineData("", typeof (ArgumentException)),
     InlineData(@"\\\\\\\\\?|/\/|", typeof (ArgumentException)),
    ]
    public void ShouldThrowExceptionWhenCreatedWithNullValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AbsoluteDirectoryPath.Value(invalidInput));
    }

    [Fact]
    public void ShouldReturnNonNullPathToFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(AbsoluteDirectoryPath.Value(@"c:\lolek\"));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      XAssert.IsValue<AbsoluteDirectoryPath>();
    }

    [Fact]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var initialValue = Path.Combine(@"C:\", Any.String());
      var path = AbsoluteDirectoryPath.Value(initialValue);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(initialValue, convertedToString);
    }

    [Fact]
    public void ShouldAllowUsingTheAdditionOperatorToConcatenateFileName()
    {
      //GIVEN
      var path = Any.Instance<AbsoluteDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      AbsoluteFilePath absoluteFilePath = path + fileName;

      //WHEN
      var convertedToString = absoluteFilePath.ToString();

      //THEN
      Assert.Equal(Path.Combine(path.ToString(), fileName.ToString()), convertedToString);
    }


    [Fact]
    public void ShouldBeConvertibleToDirectoryInfo()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"C:\lolek\");

      //WHEN
      var directoryInfo = directoryPath.Info();

      //THEN
      Assert.Equal(directoryInfo.FullName, directoryPath.ToString());
    }

    [Fact]
    public void ShouldAllowGettingPathRoot()
    {
      //GIVEN
      var pathString = @"C:\lolek\";
      var dir = AbsoluteDirectoryPath.Value(pathString);

      //WHEN
      AbsoluteDirectoryPath root = dir.Root();

      //THEN
      Assert.Equal(AbsoluteDirectoryPath.Value(Path.GetPathRoot(pathString)), root);
    }

    [Theory,
     InlineData(@"C:\parent\child\", @"C:\parent"),
     InlineData(@"C:\parent\", @"C:\")]
    public void ShouldAllowGettingProperParentDirectoryWhenItExists(string input, string expected)
    {
      //GIVEN
      var dir = AbsoluteDirectoryPath.Value(input);

      //WHEN
      var parent = dir.ParentDirectory();

      //THEN
      Assert.True(parent.Found);
      Assert.Equal(AbsoluteDirectoryPath.Value(expected), parent.Value());
    }

    [Fact]
    public void ShouldProduceParentWithoutValueThatThrowsOnAccessWhenThereIsNoParentInPath()
    {
      //GIVEN
      const string pathString = @"C:\";
      var dir = AbsoluteDirectoryPath.Value(pathString);

      //WHEN
      var parent = dir.ParentDirectory();

      //THEN
      Assert.False(parent.Found);
      Assert.Throws<InvalidOperationException>(() => parent.Value());
    }

    [Theory,
     InlineData(@"F:\Segment1\Segment2\", "Segment2"),
     InlineData(@"F:\Segment1\", "Segment1"),
     InlineData(@"F:\", @"F:\"),
    ]
    public void ShouldAllowGettingTheNameOfCurrentDirectory(string fullPath, string expectedDirectoryName)
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(fullPath);

      //WHEN
      DirectoryName dirName = directoryPath.DirectoryName();

      //THEN
      Assert.Equal(expectedDirectoryName, dirName.ToString());
    }

    [Fact]
    public void ShouldAllowAddingDirectoryName()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      AbsoluteDirectoryPath directoryPathWithAnotherDirectoryName = directoryPath + DirectoryName.Value("Subdir2");

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Subdir2", directoryPathWithAnotherDirectoryName.ToString());

    }

    [Fact]
    public void ShouldAllowAddingDirectoryNameAndFileName()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var directoryName = DirectoryName.Value("Lolek");
      var directoryName2 = DirectoryName.Value("Lolek2");
      var fileName = FileName.Value("File.txt");
      AbsoluteFilePath absoluteFilePath = directoryPath + directoryName + directoryName2 + fileName;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek\Lolek2\File.txt", absoluteFilePath.ToString());
    }

    [Fact]
    public void ShouldAllowAddingRelativeDirectory()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var relativePath = RelativeDirectoryPath.Value(@"Lolek\Lolek2");
      AbsoluteDirectoryPath newDirectoryPath = directoryPath + relativePath;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek\Lolek2", newDirectoryPath.ToString());
    }

    [Fact]
    public void ShouldAllowAddingRelativePathWithFileName()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var relativePathWithFileName = RelativeFilePath.Value(@"Subdirectory2\file.txt");
      AbsoluteFilePath absoluteFilePath = directoryPath + relativePathWithFileName;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Subdirectory2\file.txt", absoluteFilePath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyDirectoryPath()
    {
      //GIVEN
      var dirPath = Any.Instance<AbsoluteDirectoryPath>();

      //WHEN
      AnyDirectoryPath anyDirectoryPath = dirPath.AsAnyDirectoryPath();

      //THEN
      Assert.Equal(dirPath.ToString(), anyDirectoryPath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var directorypath = Any.Instance<AbsoluteDirectoryPath>();

      //WHEN
      AnyPath anyPathWithFileName = directorypath.AsAnyPath();

      //THEN
      Assert.Equal(directorypath.ToString(), anyPathWithFileName.ToString());
    }

  }

}



