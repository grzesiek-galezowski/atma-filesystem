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

  [Fact]
  public void ShouldGetOnlyTheLastExtension()
  {
    //GIVEN
    var fileName = FileName.Value("archive.tar.gz");

    //WHEN
    var extension = fileName.Extension();

    //THEN
    Assert.Equal(FileExtension.Value(".gz"), extension.Value());
  }

  [Theory]
  [InlineData("file.txt", "_suffix", "file_suffix.txt")]
  [InlineData("file", "_suffix", "file_suffix")]
  [InlineData("file.tar.gz", "_suffix", "file.tar_suffix.gz")]
  [InlineData("file.multiple.dots.txt", "_suffix", "file.multiple.dots_suffix.txt")]
  public void ShouldAppendBeforeExtension(string fileName, string suffix, string expected)
  {
    //GIVEN
    var name = FileName.Value(fileName);

    //WHEN
    var result = name.AppendBeforeExtension(suffix);

    //THEN
    result.Should().Be(FileName.Value(expected));
  }

  [Fact]
  public void ShouldAcceptEmptySuffixWhenAppendingBeforeExtension()
  {
    //GIVEN
    var name = FileName.Value("file.txt");

    //WHEN
    var result = name.AppendBeforeExtension(string.Empty);

    //THEN
    result.Should().Be(name);
  }

  [Theory]
  [InlineData(null)]
  public void ShouldThrowWhenAppendingNullSuffix(string suffix)
  {
    //GIVEN
    var name = FileName.Value("file.txt");

    //WHEN
    var action = () => name.AppendBeforeExtension(suffix);

    //THEN
    action.Should().Throw<ArgumentNullException>();
  }

  [Theory]
  [InlineData("file.txt", "prefix_", "prefix_file.txt")]
  [InlineData("file", "prefix_", "prefix_file")]
  [InlineData("file.tar.gz", "prefix_", "prefix_file.tar.gz")]
  [InlineData("multiple.dots.in.name.txt", "prefix_", "prefix_multiple.dots.in.name.txt")]
  public void ShouldPrependToFileName(string fileName, string prefix, string expected)
  {
    //GIVEN
    var name = FileName.Value(fileName);

    //WHEN
    var result = name.Prepend(prefix);

    //THEN
    result.Should().Be(FileName.Value(expected));
  }

  [Fact]
  public void ShouldAcceptEmptyPrefixWhenPrepending()
  {
    //GIVEN
    var name = FileName.Value("file.txt");

    //WHEN
    var result = name.Prepend(string.Empty);

    //THEN
    result.Should().Be(name);
  }

  [Theory]
  [InlineData(null)]
  public void ShouldThrowWhenPrependingNullPrefix(string prefix)
  {
    //GIVEN
    var name = FileName.Value("file.txt");

    //WHEN
    var action = () => name.Prepend(prefix);

    //THEN
    action.Should().Throw<ArgumentNullException>();
  }
}