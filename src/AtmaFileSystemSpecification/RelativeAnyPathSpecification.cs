public class RelativeAnyPathSpecification
{
  [Fact]
  public void ShouldNotAllowToBeCreatedWithNullValue()
  {
    Assert.Throws<ArgumentNullException>(() => RelativeAnyPath.Value(null!));
  }

  [Fact]
  public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
  {
    Assert.NotNull(RelativeAnyPath.Value(@"lolek\\lolki2.txt"));
  }

  [Fact]
  public void ShouldThrowExceptionWhenTryingToCreateInstanceWithRootedPath()
  {
    Assert.Throws<ArgumentException>(() => RelativeAnyPath.Value(@"C:\Dir\Subdir"));
  }

  [Fact]
  public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithEmptyValue()
  {
    Assert.Throws<ArgumentException>(() => RelativeAnyPath.Value(string.Empty));
  }

  [Fact]
  public void ShouldBehaveLikeValue()
  {
    var anyString = Any.String();
    var anyOtherString = Any.OtherThan(anyString);
    ObjectsOfType<RelativeAnyPath>.ShouldHaveValueSemantics(
      [
        () => RelativeAnyPath.Value(anyString)
      ],
      [
        () => RelativeAnyPath.Value(anyOtherString)
      ]);
  }

  [Fact]
  public void ShouldReturnNothingWhenAskingForDirectoryOfThePathAndSuchDirectoryDoesNotExist()
  {
    //GIVEN
    RelativeAnyPath filePath = RelativeAnyPath.Value("file.txt");

    //WHEN
    var dirObtainedFromPath = filePath.ParentDirectory();

    //THEN
    Assert.False(dirObtainedFromPath.HasValue);
    Assert.Throws<InvalidOperationException>(() => dirObtainedFromPath.Value());
  }

  [Fact]
  public void ShouldBeConvertibleToAnyPath()
  {
    //GIVEN
    var pathWithFileName = Any.Instance<RelativeAnyPath>();

    //WHEN
    AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

    //THEN
    Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
  }

  [Fact]
  public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
  {
    //GIVEN
    var path1 = Any.Instance<RelativeAnyPath>();
    var path2 = Any.Instance<RelativeAnyPath>();
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
  public void ShouldAllowAccessingParentDirectoryOfThePathWhenSuchDirectoryExists()
  {
    //GIVEN
    var dirPath = Any.Instance<RelativeDirectoryPath>();
    var fileName = Any.Instance<FileName>();
    RelativeAnyPath filePath = dirPath.Add(fileName).AsRelativePath();

    //WHEN
    var dirObtainedFromPath = filePath.ParentDirectory();

    //THEN
    Assert.Equal(dirPath, dirObtainedFromPath.Value());
  }

  [Theory]
  [InlineData("lol\\lol.txt", "lol\\lol.txt", "lol")]
  [InlineData("lol\\lol2\\lol.txt", "lol\\lol3\\lol.txt", "lol")]
  [InlineData("lol\\lol2\\lol2\\lol.txt", "lol\\lol2\\lol3\\lol.txt", "lol\\lol2")]
  [InlineData("lol\\lol2\\lol.txt", "lol2\\lol2\\lol.txt", null)]
  public void ShouldAllowGettingCommonDirectoryPath(string left, string right, string? expected)
  {
    //GIVEN
    var path1 = RelativeAnyPath.Value(left);
    var path2 = RelativeAnyPath.Value(right);

    //WHEN
    var commonLeftRight = path1.FindCommonRelativeDirectoryPathWith(path2);
    var commonRightLeft = path2.FindCommonRelativeDirectoryPathWith(path1);

    //THEN
    Assert.Equal(commonRightLeft, commonLeftRight);
    Assert.Equal(expected, commonLeftRight.Select(v => v.ToString()).OrElseDefault());
  }

  [Theory]
  [InlineData("d1\\d2", "d1/d2")]
  public void ShouldBeEqualToSamePathWithDifferentSeparators(string left, string right)
  {
    RelativeAnyPath.Value(left).Equals(RelativeAnyPath.Value(right))
      .Should().BeTrue();
  }

  [Theory]
  [InlineData("d0\\d1\\d2", "d0\\", "d1\\d2")]
  [InlineData("d0\\", "d0\\", null)]
  [InlineData("d0\\", "f0", null)]
  public void ShouldAllowTrimmingStart(string p1, string p2, string? expected)
  {
    Maybe<RelativeAnyPath> trimmedPath = RelativeAnyPath.Value(p1)
      .TrimStart(RelativeDirectoryPath.Value(p2));

    trimmedPath.Select(p => p.ToString()).Should().Be(expected.ToMaybe());
  }
}