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
      Assert.Throws<InvalidOperationException>(() => FileName.Value(@"c:\lolek\lolki2.txt"));
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
      var path = FileName.Value(initialValue);

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

      var fileNameWithExtension = FileName.Value(fileNameWithExtensionString);

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

      var fileNameWithoutExtension = FileName.Value(fileNameWithoutExtensionString);

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

      var fileNameWithExtension = FileName.Value(fileNameWithExtensionString);

      //WHEN
      var fileNameWithoutExtension = fileNameWithExtension.WithoutExtension();

      //THEN
      Assert.Equal(FileNameWithoutExtension.Value(fileNameWithoutExtensionString), fileNameWithoutExtension);

    }

    [Fact]
    public void ShouldAllowChangingExtension()
    {
      //GIVEN
      var fileName = FileName.Value(@"file.txt");

      //WHEN
      FileName nameWithNewExtension = fileName.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"file.doc", nameWithNewExtension.ToString());

    }

  }
}
