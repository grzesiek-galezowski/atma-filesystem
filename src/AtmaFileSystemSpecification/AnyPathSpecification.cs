using System;
using AtmaFileSystem;
using FluentAssertions;
using Core.Maybe;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Api;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification;

public class AnyPathSpecification
{
  [Fact]
  public void ShouldBehaveLikeValue()
  {
    var pathString = Any.String();
    var otherPathString = Any.OtherThan(pathString);
    ObjectsOfType<AnyPath>.ShouldHaveValueSemantics(
      [
        () => AnyPath.Value(pathString)
      ],
      [
        () => AnyPath.Value(otherPathString)
      ]);
  }

  [Theory,
   InlineData(null, typeof (ArgumentNullException)),
   InlineData(" ", typeof (ArgumentException)),
   InlineData(@"\\\\\\\\\?|/\/|", typeof (ArgumentException)),
  ]
  public void ShouldThrowExceptionWhenCreatedWithInvalidValue(string? invalidInput, Type exceptionType)
  {
    Assert.Throws(exceptionType, () => AnyPath.Value(invalidInput!));
  }

  [Fact]
  public void ShouldAllowGettingParentDirectoryPath()
  {
    //GIVEN
    var anyPath = AnyPath.Value(@"Directory\Subdirectory\Subsubdirectory");

    //WHEN
    Maybe<AnyDirectoryPath> parentDirectory = anyPath.ParentDirectory();

    //THEN
    Assert.True(parentDirectory.HasValue);
    Assert.Equal(AnyDirectoryPath.Value(@"Directory\Subdirectory"), parentDirectory.Value());
  }

  [Fact]
  public void ShouldCreateNothingOfParentDirectoryPathWhenEmpty()
  {
    //GIVEN
    var anyPath = AnyPath.Value("");

    //WHEN
    Maybe<AnyDirectoryPath> parentDirectory = anyPath.ParentDirectory();

    //THEN
    Assert.Equal(Maybe<AnyDirectoryPath>.Nothing, parentDirectory);
  }


  [Fact]
  public void ShouldReturnNothingWhenGettingPathWithoutLastDirectoryButCurrentDirectoryIsTheOnlyLeft()
  {
    //GIVEN
    var anyPath = AnyPath.Value("Directory");

    //WHEN
    Maybe<AnyDirectoryPath> parentDirectoryPath = anyPath.ParentDirectory();

    //THEN
    Assert.False(parentDirectoryPath.HasValue);
    Assert.Throws<InvalidOperationException>(() => parentDirectoryPath.Value());
  }

  [Fact]
  public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
  {
    //GIVEN
    var path1 = Any.Instance<AnyPath>();
    var path2 = Any.Instance<AnyPath>();
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

  [Theory]
  [InlineData("d1\\d2", "d1/d2")]
  public void ShouldBeEqualToSamePathWithDifferentSeparators(string left, string right)
  {
    AnyPath.Value(left).Equals(AnyPath.Value(right)).Should().BeTrue();
  }
}