﻿using System.IO;
using System.Runtime.CompilerServices;
using Core.NullableReferenceTypesExtensions;
using static AtmaFileSystem.AtmaFileSystemPaths;
using AbsoluteDirectoryPath = AtmaFileSystem.AbsoluteDirectoryPath;
using AbsoluteFilePath = AtmaFileSystem.AbsoluteFilePath;
using RelativeFilePath = AtmaFileSystem.RelativeFilePath;

namespace AtmaFileSystemSpecification;

public class AbsoluteFilePathSpecification
{
  [Fact]
  public void ShouldNotAllowToBeCreatedWithNullValue()
  {
    Assert.Throws<ArgumentNullException>(() => AbsoluteFilePath(null!));
  }

  [Theory,
   InlineData(null, typeof(ArgumentNullException)),
   InlineData("", typeof(ArgumentException)),
   InlineData(@"\\\\\\\\\?|/\/|", typeof(ArgumentException)),
   InlineData("C:", typeof(ArgumentException)),
  ]
  public void ShouldThrowExceptionWhenCreatedWithInvalidValue(string? invalidInput, Type exceptionType)
  {
    Assert.Throws(exceptionType, () => AbsoluteFilePath(invalidInput!));
  }

  [Fact]
  public void ShouldReturnNonNullFileNameWhenCreatedWithWellFormedPathString()
  {
    Assert.NotNull(AbsoluteFilePath(@"c:\\lolek\\lolki2.txt"));
  }

  [Fact]
  public void ShouldNormalizePathsWithNavigationCharacters()
  {
    var absoluteFilePath = AbsoluteFilePath(@"c:\\lolek\\..\\lolki2.txt");
    absoluteFilePath.Should().Be(AbsoluteFilePath(@"c:\\lolki2.txt"));
  }

  [Fact]
  public void ShouldThrowArgumentExceptionWhenTryingToCreateInstanceWithNotWellFormedUri()
  {
    Assert.Throws<ArgumentException>(() => AbsoluteFilePath.Value(@"C:\?||\|\\|\"));
  }

  [Fact]
  public void ShouldBehaveLikeValueObject()
  {
    ObjectsOfType<AbsoluteFilePath>.ShouldHaveValueSemantics(
      [
        () => AbsoluteFilePath.Value("C:\\1"),
      ],
      [
        () => AbsoluteFilePath.Value("C:\\2"), 
        () => AbsoluteFilePath.Value("c:\\1")
      ]);
  }

  [Fact]
  public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString()
  {
    //GIVEN
    var initialValue = @"C:\Dir\Subdir\file.csproj";
    var path = AbsoluteFilePath(initialValue);

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
    AbsoluteFilePath absoluteFilePath = dirPath + fileName;
      
    //WHEN
    var dirObtainedFromPath = absoluteFilePath.ParentDirectory();

    //THEN
    Assert.Equal(dirPath, dirObtainedFromPath);
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
    AbsoluteFilePath absoluteFilePath = dirPath + dirName1 + dirName2 + dirName3 + fileName;
      
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
  public void ShouldAllowAccessingFileNameOfThePath()
  {
    //GIVEN
    var dirPath = Any.Instance<AbsoluteDirectoryPath>();
    var fileName = Any.Instance<FileName>();
    AbsoluteFilePath absoluteFilePath = dirPath + fileName;

    //WHEN
    var fileNameObtainedFromPath = absoluteFilePath.FileName();

    //THEN
    Assert.Equal(fileName, fileNameObtainedFromPath);
  }

  [Fact]
  public void ShouldBeConvertibleToFileInfo()
  {
    //GIVEN
    var pathWithFilename = AbsoluteFilePath(@"C:\lolek\lol.txt");

    //WHEN
    var fileInfo = pathWithFilename.Info();

    //THEN
    Assert.Equal(fileInfo.FullName, pathWithFilename.ToString());
  }

  [Fact]
  public void ShouldAllowGettingPathRoot()
  {
    //GIVEN
    var pathString = @"C:\lolek\lol.txt";
    var pathWithFilename = AbsoluteFilePath(pathString);

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
    var pathWithFilename = AbsoluteFilePath(pathString);

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
    var pathWithFilename = AbsoluteFilePath(pathString);

    //WHEN
    var fragment = pathWithFilename.FragmentEndingOnLast(DirectoryName("lol.txt"));

    //THEN
    fragment.Should().Be(Maybe<AbsoluteDirectoryPath>.Nothing);
  }

  [Fact]
  public void ShouldBeConvertibleToAnyPathWithFileName()
  {
    //GIVEN
    var pathWithFileName = Any.Instance<AbsoluteFilePath>();

    //WHEN
    AnyFilePath anyFilePath = pathWithFileName.AsAnyFilePath();

    //THEN
    Assert.Equal(pathWithFileName.ToString(), anyFilePath.ToString());
  }

  [Fact]
  public void ShouldBeConvertibleToAnyPath()
  {
    //GIVEN
    var pathWithFileName = Any.Instance<AbsoluteFilePath>();

    //WHEN
    AnyPath anyPathWithFileName = pathWithFileName.AsAnyPath();

    //THEN
    Assert.Equal(pathWithFileName.ToString(), anyPathWithFileName.ToString());
  }


  [Theory,
   InlineData(@"C:\Dir\Subdir\fileName.txt", ".txt", true),
   InlineData(@"C:\Dir\Subdir\fileName.tx", ".txt", false),
   InlineData(@"C:\Dir\Subdir\fileName", ".txt", false),
  ]
  public void ShouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
  {
    //GIVEN
    var pathWithFileName = AbsoluteFilePath(path);
    var extensionValue = FileExtension(extension);

    //WHEN
    var hasExtension = pathWithFileName.Has(extensionValue);

    //THEN
    Assert.Equal(expectedResult, hasExtension);
  }

  [Fact]
  public void ShouldAllowChangingExtension()
  {
    //GIVEN
    var filePath = AbsoluteFilePath(@"C:\Dir\subdir\file.txt");

    //WHEN
    AbsoluteFilePath pathWithNewExtension = filePath.ChangeExtensionTo(FileExtension(".doc"));

    //THEN
    Assert.Equal(@"C:\Dir\subdir\file.doc", pathWithNewExtension.ToString());

  }

  [Fact]
  public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
  {
    //GIVEN
    var path1 = Any.Instance<AbsoluteFilePath>();
    var path2 = Any.Instance<AbsoluteFilePath>();
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
    var path1 = AbsoluteFilePath(left);
    var path2 = AbsoluteFilePath(right);

    //WHEN
    var commonLeftRight = path1.FindCommonDirectoryWith(path2);
    var commonRightLeft = path2.FindCommonDirectoryWith(path1);

    //THEN
    Assert.Equal(commonRightLeft, commonLeftRight);
    Assert.Equal(expected, commonLeftRight.Select(v => v.ToString()).OrElseDefault());
  }

  [Theory]
  [InlineData("C:\\d1\\d2", "C:\\", "d1\\d2")]
  [InlineData("C:\\a", "C:\\a", null)]
  [InlineData("C:\\a", "C:\\b", null)]
  public void ShouldAllowTrimmingStart(string p1, string p2, string? expected)
  {
    Maybe<RelativeFilePath> trimmedPath = AbsoluteFilePath(p1)
      .TrimStart(AbsoluteDirectoryPath(p2));

    trimmedPath.Select(p =>p.ToString()).Should().Be(expected.ToMaybe());
  }

  [Theory]
  [InlineData("C:\\d1", "C:\\", true)]
  [InlineData("C:\\d1\\d2", "C:\\d1", true)]
  [InlineData("C:\\d1\\d2", "C:\\d1\\d2", false)]
  [InlineData("C:\\", "D:\\d2", false)]
  [InlineData("C:\\lolek", "C:\\lol", false)]
  public void ShouldCorrectlyRespondsWhetherPathStartsWithAnother(
    string p1, string p2, bool expected)
  {
    var result = AbsoluteFilePath.Value(p1)
      .StartsWith(AbsoluteDirectoryPath.Value(p2));

    result.Should().Be(expected);
  }

  [Fact]
  public void ShouldBeAbleToCreatePathForCurrentFile()
  {
    //GIVEN
    var thisFilePath = AbsoluteFilePath.OfThisFile();
      
    //THEN
    thisFilePath.Should().Be(AbsoluteFilePath.Value(CurrentFilePath()));
  }

  [Fact]
  public void ShouldAllowAddingExtensionsEvenIfExtensionAlreadyExists()
  {
    //GIVEN
    var path = AbsoluteFilePath.Value("C:\\archive.tar");

    //WHEN
    var newPath = path + FileExtension(".gz");

    //THEN
    Assert.Equal(AbsoluteFilePath.Value("C:\\archive.tar.gz"), newPath);
  }

  [Fact]
  public void ShouldAllowAddingExtensionStringEvenIfExtensionAlreadyExists()
  {
    //GIVEN
    var path = AbsoluteFilePath("C:\\archive.tar");

    //WHEN
    var newFileName = path.AddExtension(".gz");

    //THEN
    Assert.Equal(AbsoluteFilePath.Value("C:\\archive.tar.gz"), newFileName);
  }

  [Theory]
  [InlineData("C:\\Dir\\file.txt", "_test", "C:\\Dir\\file_test.txt")]
  [InlineData("C:\\Dir\\file", "_test", "C:\\Dir\\file_test")]
  [InlineData("C:\\file.txt.tar", "_test", "C:\\file.txt_test.tar")]
  public void ShouldAppendToFileNameBeforeExtension(string initial, string suffix, string expected)
  {
    //GIVEN
    var path = AbsoluteFilePath.Value(initial);

    //WHEN
    var result = path.AppendToFileNameBeforeExtension(suffix);

    //THEN
    result.Should().Be(AbsoluteFilePath.Value(expected));
  }

  [Theory]
  [InlineData("C:\\Dir\\oldname.txt", "newname.txt", "C:\\Dir\\newname.txt")]
  [InlineData("C:\\oldname.txt", "newname.doc", "C:\\newname.doc")]
  [InlineData("C:\\Dir\\Sub\\oldname", "newname", "C:\\Dir\\Sub\\newname")]
  public void ShouldChangeFileName(string initial, string newName, string expected)
  {
    //GIVEN
    var path = AbsoluteFilePath.Value(initial);
    var fileName = FileName(newName);

    //WHEN
    var result = path.ChangeFileNameTo(fileName);

    //THEN
    result.Should().Be(AbsoluteFilePath.Value(expected));
  }

  private static string CurrentFilePath([CallerFilePath] string path = "")
  {
    return path;
  }

}