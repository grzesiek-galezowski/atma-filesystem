using System;
using AtmaFileSystem;
using FluentAssertions;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.XFluentAssert.Root;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification
{
  public class AnyPathSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(AnyPath).Should().HaveValueSemantics();
    }

    [Theory,
     InlineData(null, typeof (ArgumentNullException)),
     InlineData("", typeof (ArgumentException)),
     InlineData(@"\\\\\\\\\?|/\/|", typeof (ArgumentException)),
    ]
    public void ShouldThrowExceptionWhenCreatedWithInvalidValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyPath.Value(invalidInput));
    }

    [Fact]
    public void ShouldAllowGettingParentDirectoryPath()
    {
      //GIVEN
      var anyPath = AnyPath.Value(@"Directory\Subdirectory\Subsubdirectory");

      //WHEN
      Maybe<AnyDirectoryPath> parentDirectory = anyPath.ParentDirectory();

      //THEN
      Assert.True(parentDirectory.Found);
      Assert.Equal(AnyDirectoryPath.Value(@"Directory\Subdirectory"), parentDirectory.Value());

    }

    [Fact]
    public void ShouldReturnNothingWhenGettingPathWithoutLastDirectoryButCurrentDirectoryIsTheOnlyLeft()
    {
      //GIVEN
      var anyPath = AnyPath.Value(@"Directory");

      //WHEN
      AtmaFileSystem.Maybe<AnyDirectoryPath> parentDirectoryPath = anyPath.ParentDirectory();

      //THEN
      Assert.False(parentDirectoryPath.Found);
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
  }
}
