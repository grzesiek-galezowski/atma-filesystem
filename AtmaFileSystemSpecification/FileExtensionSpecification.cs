using System;
using System.IO;
using AtmaFileSystem;
using NSubstitute;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class FileExtensionSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<FileExtension>();
    }

    [Fact]
    public void ShouldAllowAccessingItsContentAsString()
    {
      //GIVEN
      var extensionString = ".zip";
      var extension = FileExtension.Value(extensionString);

      //WHEN
      var obtainedExtensionString = extension.ToString();

      //THEN
      Assert.Equal(extensionString, obtainedExtensionString);
    }

    [Theory,
    InlineData("zip", typeof(InvalidOperationException)),
    InlineData("..zip", typeof(InvalidOperationException)),
    InlineData(".tar.gz", typeof(InvalidOperationException)),
    InlineData("", typeof(ArgumentException)),
    InlineData(null, typeof(ArgumentNullException)),
    ]
    public void ShouldThrowExceptionWhenCreatedUsingCreationMethodWithInvalidInput(
      string extensionString, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => FileExtension.Value(extensionString));
    }

    [Theory,
    InlineData(".zip"),
    ]
    public void ShouldNotThrowExceptionWhenCreatedUsingCreationMethodWithValidInput(string extensionString)
    {
      var extension = FileExtension.Value(extensionString);

      XAssert.Equal(extensionString, extension.ToString());
    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<FileExtension>();
      var path2 = Any.Instance<FileExtension>();
      var fileSystemComparisonRules = Substitute.For<FileSystemComparisonRules>();
      var comparisonResult = Any.Boolean();

      fileSystemComparisonRules
        .ArePathStringsEqual(path1.ToString(), path2.ToString())
        .Returns(comparisonResult);


      //WHEN
      var equality = path1.Equals(path2, fileSystemComparisonRules);

      //THEN
      XAssert.Equal(comparisonResult, equality);
    }

    //TODO add method called ExtractFrom() or Of() that extracts extension from string

  }
}
