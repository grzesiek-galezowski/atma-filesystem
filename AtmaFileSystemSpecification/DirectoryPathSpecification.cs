﻿using System;
using System.IO;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class DirectoryPathSpecification
  {

    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => DirectoryPath.Value(null));
    }

    [Fact]
    public void ShouldReturnNonNullPathToFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(DirectoryPath.Value(@"c:\lolek\"));
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotWellFormedUri()
    {
      Assert.Throws<ArgumentException>(() => DirectoryPath.Value(@"C:\?||\|\\|\"));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      XAssert.IsValue<DirectoryPath>();
    }

    [Fact]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var initialValue = Path.Combine(@"C:\", Any.String());
      var path = DirectoryPath.Value(initialValue);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(initialValue, convertedToString);
    }

    [Fact]
    public void ShouldAllowUsingTheAdditionOperatorToConcatenateFileName()
    {
      //GIVEN
      var path = Any.Instance<DirectoryPath>();
      var fileName = Any.Instance<FileName>();
      PathWithFileName pathWithFileName = path + fileName;

      //WHEN
      var convertedToString = pathWithFileName.ToString();

      //THEN
      Assert.Equal(Path.Combine(path.ToString(), fileName.ToString()), convertedToString);
    }


    [Fact]
    public void ShouldBeConvertibleToDirectoryInfo()
    {
      //GIVEN
      var directoryPath = DirectoryPath.Value(@"C:\lolek\");

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
      var dir = DirectoryPath.Value(pathString);

      //WHEN
      DirectoryPath root = dir.Root();

      //THEN
      Assert.Equal(DirectoryPath.Value(Path.GetPathRoot(pathString)), root);
    }

    [Theory, 
      InlineData(@"C:\parent\child\", @"C:\parent"),
      InlineData(@"C:\parent\", @"C:\")]
    public void ShouldAllowGettingProperParentDirectoryWhenItExists(string input, string expected)
    {
      //GIVEN
      var dir = DirectoryPath.Value(input);

      //WHEN
      var parent = dir.Parent();

      //THEN
      Assert.True(parent.Found);
      Assert.Equal(DirectoryPath.Value(expected), parent.Value());
    }

    [Fact]
    public void ShouldProduceParentWithoutValueThatThrowsOnAccessWhenThereIsNoParentInPath()
    {
      //GIVEN
      const string pathString = @"C:\";
      var dir = DirectoryPath.Value(pathString);

      //WHEN
      var parent = dir.Parent();

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
      var directoryPath = DirectoryPath.Value(fullPath);

      //WHEN
      DirectoryName dirName = directoryPath.DirectoryName();
      
      //THEN
      Assert.Equal(expectedDirectoryName, dirName.ToString());
    }

    [Fact]
    public void ShouldAllowAddingDirectoryName()
    {
      //GIVEN
      var directoryPath = DirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      DirectoryPath directoryPathWithAnotherDirectoryName = directoryPath + DirectoryName.Value("Lolek");

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek", directoryPathWithAnotherDirectoryName.ToString());

    }

    [Fact]
    public void ShouldAllowAddingDirectoryNameAndFileName()
    {
      //GIVEN
      var directoryPath = DirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var directoryName = DirectoryName.Value("Lolek");
      var directoryName2 = DirectoryName.Value("Lolek2");
      var fileName = FileName.Value("File.txt");
      PathWithFileName pathWithFileName = directoryPath + directoryName + directoryName2 + fileName;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek\Lolek2\File.txt", pathWithFileName.ToString());
    }

    [Fact]
    public void ShouldAllowAddingRelativeDirectory()
    {
      //GIVEN
      var directoryPath = DirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var relativePath = RelativeDirectoryPath.Value(@"Lolek\Lolek2");
      DirectoryPath pathWithFileName = directoryPath + relativePath;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek\Lolek2", pathWithFileName.ToString());
    }


  }
}
