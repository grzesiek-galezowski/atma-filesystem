using System;
using System.IO;
using AtmaFileSystem;
using FluentAssertions;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Api;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification;

public class FileExtensionSpecification
{
  [Fact]
  public void ShouldBehaveLikeValue()
  {
    var anyString = "." + Any.AlphaString();
    var anyOtherString = "." + Any.AlphaString();
    ObjectsOfType<FileExtension>.ShouldHaveValueSemantics(
      [
        () => FileExtension.Value(anyString)
      ],
      [
        () => FileExtension.Value(anyOtherString)
      ]);
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
   InlineData("zip", typeof(ArgumentException)),
   InlineData("..zip", typeof(ArgumentException)),
   InlineData(".tar.gz", typeof(ArgumentException)),
   InlineData("", typeof(ArgumentException)),
   InlineData(null, typeof(ArgumentNullException)),
  ]
  public void ShouldThrowExceptionWhenCreatedUsingCreationMethodWithInvalidInput(
    string extensionString, Type exceptionType)
  {
    Assert.Throws(exceptionType, () => FileExtension.Value(extensionString));
  }

  [Theory, 
   InlineData(".zip")
  ]
  public void ShouldNotThrowExceptionWhenCreatedUsingCreationMethodWithValidInput(string extensionString)
  {
    var extension = FileExtension.Value(extensionString);

    Assert.Equal(extensionString, extension.ToString());
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
    var equality = path1.ShallowEquals(path2, fileSystemComparisonRules);

    //THEN
    Assert.Equal(comparisonResult, equality);
  }

  //TODO add method called ExtractFrom() or Of() that extracts extension from string

}