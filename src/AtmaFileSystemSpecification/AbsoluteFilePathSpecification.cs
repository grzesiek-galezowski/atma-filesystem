using System;
using System.IO;
using AtmaFileSystem;
using FluentAssertions;
using Functional.Maybe;
using NSubstitute;
using TddXt.AnyRoot;
using TddXt.XFluentAssertRoot;
using Xunit;
using static AtmaFileSystem.AtmaFileSystemPaths;
using static TddXt.AnyRoot.Root;
using AbsoluteDirectoryPath = AtmaFileSystem.AbsoluteDirectoryPath;
using AbsoluteFilePath = AtmaFileSystem.AbsoluteFilePath;

namespace AtmaFileSystemSpecification
{

  public class AbsoluteFilePathSpecification
  {

    [Fact]
    public void ShouldNotAllowToBeCreatedWithNullValue()
    {
      Assert.Throws<ArgumentNullException>(() => AbsoluteFilePath(null));
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
      typeof(AbsoluteFilePath).Should().HaveValueSemantics();
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
      Assert.Equal(AbsoluteDirectoryPath.Value(Path.GetPathRoot(pathString)), root);
    }

    [Fact]
    public void ShouldAllowGettingPathEndingOnLastOccurenceOfDirectoryName()
    {
      //GIVEN
      var pathString = @"C:\lolek1\lolek2\lolek3\lolek3\lol.txt";
      var pathWithFilename = AbsoluteFilePath(pathString);

      //WHEN
      var fragment = pathWithFilename.FragmentEndingOnLast(DirectoryName("lolek3"));

      //THEN
      fragment.Value.Should().Be(AbsoluteDirectoryPath(@"C:\lolek1\lolek2\lolek3\lolek3"));
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
      fragment.Should().Be(Functional.Maybe.Maybe<AbsoluteDirectoryPath>.Nothing);
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
    public void ShouldAllowGettingCommonDirectoryPath(string left, string right, string expected)
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

  }
}
