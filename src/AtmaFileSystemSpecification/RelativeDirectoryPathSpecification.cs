﻿using System;
using AtmaFileSystem;
using NSubstitute;
using System.IO;
using FluentAssertions;
using Functional.Maybe;
using TddXt.AnyRoot;
using TddXt.XFluentAssertRoot;
using Xunit;
using static TddXt.AnyRoot.Root;

//bug add convenient factory methods

namespace AtmaFileSystemSpecification
{
  public class RelativeDirectoryPathSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(RelativeDirectoryPath).Should().HaveValueSemantics();
    }

    [Theory]
    [InlineData(@"lolek\bolek", @"lolek\bolek\")]
    [InlineData(@"", @"")]
    public void ShouldAllowAddingDirectoryNameToIt(string path, string expectedPathPrefix)
    {
      //GIVEN
      var relativeDir = RelativeDirectoryPath.Value(path);
      var dirName = DirectoryName.Value("zenek");

      //WHEN
      RelativeDirectoryPath mergedPath = relativeDir + dirName;

      //THEN
      Assert.Equal(@$"{expectedPathPrefix}zenek", mergedPath.ToString());
    }

    [Theory]
    [InlineData(@"lolek\bolek", @"lolek\bolek\")]
    [InlineData(@"", @"")]
    public void ShouldAllowAddingFileNameToIt(string path, string expectedPathPrefix)
    {
      //GIVEN
      var relativeDir = RelativeDirectoryPath.Value(path);
      var fileName = FileName.Value("zenek.txt");

      //WHEN
      RelativeFilePath mergedFilePath = relativeDir + fileName;

      //THEN
      Assert.Equal(@$"{expectedPathPrefix}zenek.txt", mergedFilePath.ToString());
    }

    [Theory]
    [InlineData(@"Dir1\dir2", @"dir3\dir4", @"Dir1\dir2\dir3\dir4")]
    [InlineData("", "", "")]
    public void ShouldAllowAddingRelativeDirectoryPathToIt(string p1, string p2, string expected)
    {
      //GIVEN
      var relativeDir1 = RelativeDirectoryPath.Value(p1);
      var relativeDir2 = RelativeDirectoryPath.Value(p2);

      //WHEN
      RelativeDirectoryPath mergedPath = relativeDir1 + relativeDir2;

      //THEN
      Assert.Equal(expected, mergedPath.ToString());
    }

    [Theory]
    [InlineData(@"Dir1\dir2", @"Dir1\dir2\")]
    [InlineData("", "")]
    public void ShouldAllowAddingRelativePathWithFileNameToIt(string path, string expectedPathPrefix)
    {
      //GIVEN
      var relativeDir1 = RelativeDirectoryPath.Value(path);
      var relativePathWithFileName = RelativeFilePath.Value(@"dir3\dir4\file.txt");

      //WHEN
      RelativeFilePath mergedFilePath = relativeDir1 + relativePathWithFileName;

      //THEN
      Assert.Equal(@$"{expectedPathPrefix}dir3\dir4\file.txt", mergedFilePath.ToString());
    }

    [Fact]
    public void ShouldAllowGettingPathWithoutLastDirectory()
    {
      //GIVEN
      var relativePath = RelativeDirectoryPath.Value(@"Directory\Subdirectory\Subsubdirectory");
      
      //WHEN
      Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.ParentDirectory();

      //THEN
      Assert.True(pathWithoutLastDir.HasValue);
      Assert.Equal(RelativeDirectoryPath.Value(@"Directory\Subdirectory"), pathWithoutLastDir.Value);

    }

    [Fact]
    public void ShouldReturnNothingWhenGettingPathWithoutLastDirectoryButCurrentDirectoryIsTheOnlyLeft()
    {
      //GIVEN
      var relativePath = RelativeDirectoryPath.Value(@"Directory");

      //WHEN
      Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.ParentDirectory();

      //THEN
      Assert.False(pathWithoutLastDir.HasValue);
      Assert.Throws<InvalidOperationException>(() => pathWithoutLastDir.Value);
    }

    //bug disallow whitespace when creating
    [Fact]
    public void ShouldReturnNothingWhenGettingPathWithoutLastDirectoryFromEmptyPath()
    {
      //GIVEN
      var relativePath = RelativeDirectoryPath.Value(string.Empty);

      //WHEN
      Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.ParentDirectory();

      //THEN
      Assert.False(pathWithoutLastDir.HasValue);
      Assert.Throws<InvalidOperationException>(() => pathWithoutLastDir.Value);
    }

    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("  ", typeof(ArgumentException)),
      InlineData(@"C:\", typeof(ArgumentException))]
    public void ShouldNotAllowCreatingInvalidInstance(string input, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => RelativeDirectoryPath.Value(input));
    }

    [Fact]
    public void ShouldBeConvertibleToDirectoryInfoWhenNonEmpty()
    {
      //GIVEN
      var path = RelativeDirectoryPath.Value(@"Dir\Subdir");

      //WHEN
      var directoryInfo = path.Info();

      //THEN
      Assert.Equal(directoryInfo.Value.FullName, FullNameFrom(path));
    }

    [Fact]
    public void ShouldBeConvertToNothingOfDirectoryInfoWhenEmpty()
    {
      //GIVEN
      var path = RelativeDirectoryPath.Value(string.Empty);

      //WHEN
      var directoryInfo = path.Info();

      //THEN
      Assert.Equal(directoryInfo, Maybe<DirectoryInfo>.Nothing);
    }

    //bug check for Info() returning internally held object that it cannot be modified externally!!!!


    [Theory]
    [InlineData("trolololo")]
    [InlineData("")]
    public void ShouldBeConvertibleToAnyDirectoryPath(string path)
    {
      //GIVEN
      var dirPath = RelativeDirectoryPath.Value(path);

      //WHEN
      AnyDirectoryPath anyDirectoryPath = dirPath.AsAnyDirectoryPath();

      //THEN
      Assert.Equal(dirPath.ToString(), anyDirectoryPath.ToString());
    }

    [Theory]
    [InlineData("trolololo")]
    [InlineData("")]
    public void ShouldBeConvertibleToAnyPath(string path)
    {
      //GIVEN
      var directorypath = RelativeDirectoryPath.Value(path);

      //WHEN
      AnyPath anyPathWithFileName = directorypath.AsAnyPath();

      //THEN
      Assert.Equal(directorypath.ToString(), anyPathWithFileName.ToString());
    }

    [Theory,
      InlineData(@"Segment1\Segment2\", "Segment2"),
      InlineData(@"Segment1\", "Segment1"),
      InlineData(@"", ""),
      ]
    public void ShouldAllowGettingTheNameOfCurrentDirectory(string fullPath, string expectedDirectoryName)
    {
      //GIVEN
      var directoryPath = RelativeDirectoryPath.Value(fullPath);

      //WHEN
      DirectoryName dirName = directoryPath.DirectoryName();

      //THEN
      Assert.Equal(expectedDirectoryName, dirName.ToString());
    }

    private static string FullNameFrom(RelativeDirectoryPath path)
    {
      return Path.Combine(new DirectoryInfo(".").FullName, path.ToString());
    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<RelativeDirectoryPath>();
      var path2 = Any.Instance<RelativeDirectoryPath>();
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

  //todo cut out first directory from relative directory path = relative directory path
  //bug go back to using System.IO.DirectoryInfo? Or make two methods?
}
