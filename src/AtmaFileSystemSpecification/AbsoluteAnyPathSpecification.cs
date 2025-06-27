using System.IO;
using Core.NullableReferenceTypesExtensions;
using static AtmaFileSystem.AtmaFileSystemPaths;
using AbsoluteDirectoryPath = AtmaFileSystem.AbsoluteDirectoryPath;

namespace AtmaFileSystemSpecification;

public class AbsoluteAnyPathSpecification
{
  [Fact]
  public void ShouldNotAllowToBeCreatedWithNullValue()
  {
    Assert.Throws<ArgumentNullException>(() => AbsoluteAnyPath(null!));
  }

  [Theory,
   InlineData(null, typeof(ArgumentNullException)),
   InlineData("", typeof(ArgumentException)),
   InlineData(@"\\\\\\\\\?|/\/|", typeof(ArgumentException)),
   InlineData("C:", typeof(ArgumentException)),
  ]
  public void ShouldThrowExceptionWhenCreatedWithInvalidValue(string? invalidInput, Type exceptionType)
  {
    Assert.Throws(exceptionType, () => AbsoluteAnyPath(invalidInput!));
  }

  [Fact]
  public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
  {
    Assert.NotNull(AbsoluteAnyPath(@"c:\\lolek\\lolki2.txt"));
  }

  [Fact]
  public void ShouldNormalizePathsWithNavigationCharacters()
  {
    var absolutePath = AbsoluteAnyPath(@"c:\\lolek\\..\\lolki2.txt");
    absolutePath.Should().Be(AbsoluteAnyPath(@"c:\\lolki2.txt"));
  }

  [Fact]
  public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotWellFormedUri()
  {
    Assert.Throws<ArgumentException>(() => AbsoluteAnyPath(@"C:\?||\|\\|\"));
  }

  [Fact]
  public void ShouldBehaveLikeValueObject()
  {
    ObjectsOfType<AbsoluteAnyPath>.ShouldHaveValueSemantics(
      [
        () => AbsoluteAnyPath("C:\\1"),
      ],
      [
        () => AbsoluteAnyPath("C:\\2"),
        () => AbsoluteAnyPath("c:\\1")
      ]);
  }

  [Fact]
  public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
  {
    //GIVEN
    var initialValue = @"C:\Dir\Subdir\file.csproj";
    var path = AbsoluteAnyPath(initialValue);

    //WHEN
    var convertedToString = path.ToString();

    //THEN
    Assert.Equal(initialValue, convertedToString);
  }

  [Fact]
  public void ShouldAllowAccessingDirectoryOfThePath()
  {
    //GIVEN
    var dirPath = Any.Instance<AbsoluteDirectoryPath>();
    var fileName = Any.Instance<FileName>();
    var absoluteFilePath = AbsoluteAnyPath(dirPath.Add(fileName).ToString());

    //WHEN
    var dirObtainedFromPath = absoluteFilePath.ParentDirectory();

    //THEN
    Assert.Equal(dirPath, dirObtainedFromPath.Value());
  }

  [Fact]
  public void ShouldAllowAccessingDirectoryOfThePathAtSpecifiedLevel()
  {
    //GIVEN
    var dirPath = AbsoluteDirectoryPath("C:\\");
    var dirName1 = Any.Instance<DirectoryName>();
    var dirName2 = Any.Instance<DirectoryName>();
    var dirName3 = Any.Instance<DirectoryName>();
    var fileName = Any.Instance<FileName>();
    var absoluteFilePath = dirPath.Add(dirName1).Add(dirName2).Add(dirName3).Add(fileName).AsAbsoluteAnyPath();

    //WHEN
    var dirIndex0 = absoluteFilePath.ParentDirectory(0);
    var dirIndex1 = absoluteFilePath.ParentDirectory(1);
    var dirIndex2 = absoluteFilePath.ParentDirectory(2);
    var dirIndex3 = absoluteFilePath.ParentDirectory(3);
    var dirIndex4 = absoluteFilePath.ParentDirectory(4);

    //THEN
    dirIndex0.Value().Should().Be(dirPath + dirName1 + dirName2 + dirName3);
    dirIndex1.Value().Should().Be(dirPath + dirName1 + dirName2);
    dirIndex2.Value().Should().Be(dirPath + dirName1);
    dirIndex3.Value().Should().Be(dirPath);
    dirIndex4.Should().Be(Maybe<AbsoluteDirectoryPath>.Nothing);
  }

  [Fact]
  public void ShouldAllowGettingPathRoot()
  {
    //GIVEN
    var pathString = @"C:\lolek\lol.txt";
    var pathWithFilename = AbsoluteAnyPath(pathString);

    //WHEN
    var root = pathWithFilename.Root();

    //THEN
    Assert.Equal(AbsoluteDirectoryPath.Value(Path.GetPathRoot(pathString).OrThrow()), root);
  }

  [Fact]
  public void ShouldAllowGettingPathEndingOnLastOccurrenceOfDirectoryName()
  {
    //GIVEN
    var pathString = @"C:\lolek1\lolek2\lolek3\lolek3\lol.txt";
    var pathWithFilename = AbsoluteAnyPath(pathString);

    //WHEN
    var fragment = pathWithFilename.FragmentEndingOnLast(DirectoryName("lolek3"));

    //THEN
    fragment.Value().Should().Be(AbsoluteDirectoryPath(@"C:\lolek1\lolek2\lolek3\lolek3"));
  }

  [Fact]
  public void ShouldThrowExceptionWhenGettingFragmentEndingOnNonExistentDirectoryName()
  {
    //GIVEN
    var pathString = @"C:\lolek1\lolek2\lolek3\lol.txt";
    var pathWithFilename = AbsoluteAnyPath(pathString);

    //WHEN
    var fragment = pathWithFilename.FragmentEndingOnLast(DirectoryName("lol.txt"));

    //THEN
    fragment.Should().Be(Maybe<AbsoluteDirectoryPath>.Nothing);
  }

  [Fact]
  public void ShouldBeConvertibleToAnyPath()
  {
    //GIVEN
    var pathWithFileName = Any.Instance<AbsoluteAnyPath>();

    //WHEN
    AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

    //THEN
    Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
  }

  [Fact]
  public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
  {
    //GIVEN
    var path1 = Any.Instance<AbsoluteAnyPath>();
    var path2 = Any.Instance<AbsoluteAnyPath>();
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
  [InlineData("C:\\lol.txt", "C:\\lol.txt", "C:\\")]
  [InlineData("C:\\lol\\lol.txt", "C:\\lol\\lol.txt", "C:\\lol")]
  [InlineData("C:\\LOL\\lol.txt", "C:\\lol\\lol.txt", "C:\\")]
  [InlineData("C:\\lol\\lol2\\lol.txt", "C:\\lol\\lol3\\lol.txt", "C:\\lol")]
  [InlineData("C:\\lol\\lol2\\lol.txt", "D:\\lol\\lol3\\lol.txt", null)]
  public void ShouldAllowGettingCommonDirectoryPath(string left, string right, string? expected)
  {
    //GIVEN
    var path1 = AbsoluteAnyPath(left);
    var path2 = AbsoluteAnyPath(right);

    //WHEN
    var commonLeftRight = path1.FindCommonDirectoryWith(path2);
    var commonRightLeft = path2.FindCommonDirectoryWith(path1);

    //THEN
    Assert.Equal(commonRightLeft, commonLeftRight);
    Assert.Equal(expected, commonLeftRight.Select(v => v.ToString()).OrElseDefault());
  }

  [Theory]
  [InlineData("C:\\d1\\d2", "C:\\", "d1\\d2", true)]
  [InlineData("C:\\a", "C:\\a", null, false)]
  [InlineData("C:\\a", "C:\\b", null, false)]
  public void ShouldAllowTrimmingStart(string p1, string p2, string? expectedValue, bool hasValue)
  {
    Maybe<RelativeAnyPath> trimmedPath = AbsoluteAnyPath(p1)
      .TrimStart(AbsoluteDirectoryPath(p2));

    trimmedPath.HasValue.Should().Be(hasValue);
    trimmedPath.OrElseDefault()?.ToString().Should().Be(expectedValue);
  }
}