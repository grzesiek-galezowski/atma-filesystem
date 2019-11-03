using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmaFileSystem;
using AtmaFileSystem.Assertions;
using FluentAssertions;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.XFluentAssert.Root;
using TddXt.XFluentAssertRoot;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification
{
  public class DirectoryNameSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(DirectoryName).Should().HaveValueSemantics();
    }

    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
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
