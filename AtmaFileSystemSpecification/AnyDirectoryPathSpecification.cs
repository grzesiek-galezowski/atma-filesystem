﻿using System;
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
      AnyPathWithFileName anyPathWithFileName
        = anyDirectoryPath + fileName;

      //THEN
      Assert.Equal(
        Path.Combine(anyDirectoryPath.ToString(), fileName.ToString()), anyPathWithFileName.ToString());
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

    public void ShouldAllowAddingRelativePathWithFileName()
    {
      //GIVEN
      var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();
      var pathWithFileName = Any.Instance<RelativePathWithFileName>();

      //WHEN
      AnyPathWithFileName anyPathWithFileName
        = anyDirectoryPath + pathWithFileName;

      //THEN
      Assert.Equal(
        Path.Combine(anyDirectoryPath.ToString(), pathWithFileName.ToString()), anyPathWithFileName.ToString());
    }



  }
}
