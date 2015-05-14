using System;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class FileNameSpecification
  {
    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => FileName.Value(null));
    }

    [Fact]
    public void ShouldThrowExceptionWhenCreatedWithStringContainingMoreThanJustAFileName()
    {
      Assert.Throws<ArgumentException>(() => FileName.Value(@"c:\lolek\lolki2.txt"));
    }

    [Fact]
    public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(FileName.Value("lolki2.txt"));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      XAssert.IsValue<FileName>();
    }

    [Fact]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
    {
      //GIVEN
      var initialValue = Any.String();
      var path = new FileName(initialValue);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(initialValue, convertedToString);
    }

    [Fact]
    public void ShouldAllowGettingExtensionWhenItExists()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();
      var extensionString = "." + Any.String();
      var fileNameWithExtensionString = fileNameWithoutExtensionString + extensionString;

      var fileNameWithExtension = new FileName(fileNameWithExtensionString);

      //WHEN
      var maybeExtension = fileNameWithExtension.Extension();

      //THEN
      Assert.True(maybeExtension.Found);
      Assert.Equal(FileExtension.Value(extensionString), maybeExtension.Value());
    }

    [Fact]
    public void ShouldYieldNoExtensionWhenThePathHasNoExtension()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();

      var fileNameWithoutExtension = new FileName(fileNameWithoutExtensionString);

      //WHEN
      var maybeExtension = fileNameWithoutExtension.Extension();

      //THEN
      Assert.False(maybeExtension.Found);
      Assert.Throws<InvalidOperationException>(() => maybeExtension.Value());
    }

    [Fact]
    public void ShouldAllowAccessingFileNameWithoutExtension()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();
      var extensionString = "." + Any.String();
      var fileNameWithExtensionString = fileNameWithoutExtensionString + extensionString;

      var fileNameWithExtension = new FileName(fileNameWithExtensionString);

      //WHEN
      var fileNameWithoutExtension = fileNameWithExtension.WithoutExtension();

      //THEN
      Assert.Equal(new FileNameWithoutExtension(fileNameWithoutExtensionString), fileNameWithoutExtension);

    }
  }
}
