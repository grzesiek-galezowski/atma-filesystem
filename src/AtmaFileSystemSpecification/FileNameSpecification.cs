using System;
using AtmaFileSystem;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Api;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification;

public class FileNameSpecification
{
  [Fact]
  public void ShouldNotAllowToBeCreatedWithNullValue()
  {
    Assert.Throws<ArgumentNullException>(() => FileName.Value(null!));
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
  public void ShouldBehaveLikeValue()
  {
    var anyString = Any.String();
    var anyOtherString = Any.OtherThan(anyString);
    ObjectsOfType<FileName>.ShouldHaveValueSemantics(
      [
        () => FileName.Value(anyString)
      ],
      [
        () => FileName.Value(anyOtherString)
      ]);
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
    Assert.True(maybeExtension.HasValue);
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
    Assert.False(maybeExtension.HasValue);
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

  [Fact]
  public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
  {
    //GIVEN
    var path1 = Any.Instance<FileName>();
    var path2 = Any.Instance<FileName>();
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

  [Fact]
  public void ShouldAllowAddingExtensionsEvenIfExtensionAlreadyExists()
  {
    //GIVEN
    var fileName = FileName.Value("archive.tar");

    //WHEN
    var newFileName = fileName + FileExtension.Value(".gz");

    //THEN
    Assert.Equal(FileName.Value("archive.tar.gz"), newFileName);
  }

  [Fact]
  public void ShouldAllowAddingExtensionStringEvenIfExtensionAlreadyExists()
  {
    //GIVEN
    var fileName = FileName.Value("archive.tar");

    //WHEN
    var newFileName = fileName.AddExtension(".gz");

    //THEN
    Assert.Equal(FileName.Value("archive.tar.gz"), newFileName);
  }
}