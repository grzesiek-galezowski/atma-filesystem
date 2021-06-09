﻿using System;
using AtmaFileSystem;
using FluentAssertions;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Api;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification
{
  public class DirectoryNameSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      var anyString = Any.String();
      var anyOtherString = Any.OtherThan(anyString);
      ObjectsOfType<DirectoryName>.ShouldHaveValueSemantics(
        new Func<DirectoryName>[]
        {
          () => DirectoryName.Value(anyString) 
        },
        new Func<DirectoryName>[]
        {
          () => DirectoryName.Value(anyOtherString) 
        });
    }


    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData(" ", typeof(ArgumentException)),
      InlineData(@"C:\a", typeof(ArgumentException))]
    public void ShouldNotLetCreateInvalidInstance(string input, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => DirectoryName.Value(input));
    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<DirectoryName>();
      var path2 = Any.Instance<DirectoryName>();
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
