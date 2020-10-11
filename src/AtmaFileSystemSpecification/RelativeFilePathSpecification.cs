using System;
using AtmaFileSystem;
using NSubstitute;
using System.IO;
using FluentAssertions;
using Functional.Maybe;
using TddXt.AnyRoot;
using TddXt.XFluentAssertRoot;
using Xunit;
using static TddXt.AnyRoot.Root;

namespace AtmaFileSystemSpecification
{
  public class RelativeFilePathSpecification
  {
    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => RelativeFilePath.Value(null));
    }

    [Fact]
    public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
    {
      Assert.NotNull(RelativeFilePath.Value(@"lolek\\lolki2.txt"));
    }

    [Fact]
    public void ShouldThrowExceptionWhenTryingToCreateInstanceWithRootedPath()
    {
      Assert.Throws<ArgumentException>(() => RelativeFilePath.Value(@"C:\Dir\Subdir"));
    }

    [Fact]
    public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithEmptyValue()
    {
      Assert.Throws<ArgumentException>(() => RelativeFilePath.Value(string.Empty));
    }

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(RelativeFilePath).Should().HaveValueSemantics();
    }

    [Fact]
    public void ShouldAllowAccessingDirectoryOfThePathWhenSuchDirectoryExists()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      RelativeFilePath filePath = dirPath + fileName;

      //WHEN
      var dirObtainedFromPath = filePath.ParentDirectory();


      //THEN
      Assert.Equal(dirPath, dirObtainedFromPath.Value);
    }

    [Fact]
    public void ShouldReturnNothingWhenAskingForDirectoryOfThePathAndSuchDirectoryDoesNotExist()
    {
      //GIVEN
      RelativeFilePath filePath = RelativeFilePath.Value("file.txt");

      //WHEN
      var dirObtainedFromPath = filePath.ParentDirectory();

      //THEN
      Assert.False(dirObtainedFromPath.HasValue);
      Assert.Throws<InvalidOperationException>(() => dirObtainedFromPath.Value);
    }

    [Fact]
    public void ShouldAllowAccessingFileNameOfThePath()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      RelativeFilePath filePath =dirPath + fileName;

      //WHEN
      FileName fileNameObtainedFromPath = filePath.FileName();

      //THEN
      Assert.Equal(fileName, fileNameObtainedFromPath);
    }

    [Fact]
    public void ShouldBeConvertibleToFileInfo()
    {
      //GIVEN
      var pathWithFilename = RelativeFilePath.Value(@"lolek\lol.txt");

      //WHEN
      var fileInfo = pathWithFilename.Info();

      //THEN
      Assert.Equal(fileInfo.FullName, Path.Combine(new DirectoryInfo(".").FullName, pathWithFilename.ToString()));
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPathWithFileName()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativeFilePath>();

      //WHEN
      AnyFilePath anyFilePath = pathWithFileName.AsAnyFilePath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyFilePath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<RelativeFilePath>();

      //WHEN
      AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
    }

    [Theory,
     InlineData(@"Dir\Subdir\fileName.txt", ".txt", true),
     InlineData(@"Dir\Subdir\fileName.tx", ".txt", false),
     InlineData(@"Dir\Subdir\fileName", ".txt", false),
    ]
    public void ShouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
    {
      //GIVEN
      var pathWithFileName = RelativeFilePath.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = pathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

    [Fact]
    public void ShouldAllowChangingExtension()
    {
      //GIVEN
      var filePath = RelativeFilePath.Value(@"Dir\subdir\file.txt");

      //WHEN
      RelativeFilePath pathWithNewExtension = filePath.ChangeExtensionTo(FileExtension.Value(".doc"));

      //THEN
      Assert.Equal(@"Dir\subdir\file.doc", pathWithNewExtension.ToString());

    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var path1 = Any.Instance<RelativeFilePath>();
      var path2 = Any.Instance<RelativeFilePath>();
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

    [Theory]
    [InlineData("lol\\lol.txt", "lol\\lol.txt", "lol")]
    [InlineData("lol\\lol2\\lol.txt", "lol\\lol3\\lol.txt", "lol")]
    [InlineData("lol\\lol2\\lol2\\lol.txt", "lol\\lol2\\lol3\\lol.txt", "lol\\lol2")]
    [InlineData("lol\\lol2\\lol.txt", "lol2\\lol2\\lol.txt", null)]
    public void ShouldAllowGettingCommonDirectoryPath(string left, string right, string expected)
    {
      //GIVEN
      var path1 = RelativeFilePath.Value(left);
      var path2 = RelativeFilePath.Value(right);

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
      RelativeFilePath.Value(left).Equals(RelativeFilePath.Value(right))
        .Should().BeTrue();
    }

    [Theory]
    [InlineData("d0\\d1\\d2", "d0\\", "d1\\d2")]
    [InlineData("d0\\", "d0\\", null)]
    [InlineData("d0\\", "f0", null)]
    public void ShouldAllowTrimmingStart(string p1, string p2, string expected)
    {
      Maybe<RelativeDirectoryPath> trimmedPath = RelativeFilePath.Value(p1)
        .TrimStart(RelativeDirectoryPath.Value(p2));

      trimmedPath.Select(p =>p.ToString()).Should().Be(expected.ToMaybe());
    }

    [Theory]
    [InlineData("d1\\d2", "d1", true)]
    [InlineData("d1\\d2", "d1\\d2", false)]
    [InlineData("d1\\", "d2\\", false)]
    [InlineData("lolek", "lol", false)]
    public void ShouldCorrectlyRespondsWhetherPathStartsWithAnother(
        string p1, string p2, bool expected)
    {
        var result = RelativeFilePath.Value(p1)
            .StartsWith(RelativeDirectoryPath.Value(p2));

        result.Should().Be(expected);
    }

  }

}
