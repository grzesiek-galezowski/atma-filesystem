using System;
using System.IO;
using AtmaFileSystem;
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
    InlineData("zip"),
    InlineData("..zip"),
    InlineData(".tar.gz"),
    InlineData(""),
    InlineData(null),
    ]
    public void ShouldThrowExceptionWhenCreatedUsingCreationMethodWithInvalidInput(string extensionString)
    {
      Assert.Throws<ArgumentException>(() => FileExtension.Value(extensionString));
    }

    [Theory,
    InlineData(".zip"),
    ]
    public void ShouldNotThrowExceptionWhenCreatedUsingCreationMethodWithValidInput(string extensionString)
    {
      var extension = FileExtension.Value(extensionString);

      XAssert.Equal(extensionString, extension.ToString());
    }

    //TODO add method called ExtractFrom() that extracts extension from string

  }
}
