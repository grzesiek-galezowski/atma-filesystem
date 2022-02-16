using System;
using System.IO;
using System.Runtime.CompilerServices;
using AtmaFileSystem;
using FluentAssertions;
using Core.Maybe;
using NSubstitute;
using NullableReferenceTypesExtensions;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Api;
using Xunit;
using static AtmaFileSystem.AtmaFileSystemPaths;
using static TddXt.AnyRoot.Root;
using AbsoluteDirectoryPath = AtmaFileSystem.AbsoluteDirectoryPath;
using DirectoryName = AtmaFileSystem.DirectoryName;
using FileName = AtmaFileSystem.FileName;
using RelativeDirectoryPath = AtmaFileSystem.RelativeDirectoryPath;
using RelativeFilePath = AtmaFileSystem.RelativeFilePath;

namespace AtmaFileSystemSpecification
{
  public class AbsoluteDirectoryPathSpecification
  {

    [Theory,
     InlineData(null, typeof (ArgumentNullException)),
     InlineData("", typeof (ArgumentException)),
     InlineData(@"\\\\\\\\\?|/\/|", typeof (ArgumentException)),
     InlineData("C:", typeof (ArgumentException)),
    ]
    public void ShouldThrowExceptionWhenCreatedWithInvalidValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AbsoluteDirectoryPath.Value(invalidInput));
    }

    [Theory, InlineData(@"c:\lolek\")]
    public void ShouldReturnNonNullPathToFileNameWhenCreatedWithWellFormedPathString(string path)
    {
      Assert.NotNull(AbsoluteDirectoryPath.Value(path));
    }

    [Fact]
    public void ShouldBehaveLikeValueObject()
    {
      ObjectsOfType<AbsoluteDirectoryPath>.ShouldHaveValueSemantics(
        new Func<AbsoluteDirectoryPath>[]
        {
          () => AbsoluteDirectoryPath.Value("C:\\")
        },
        new Func<AbsoluteDirectoryPath>[]
        {
          () => AbsoluteDirectoryPath.Value("C:\\a"),
          () => AbsoluteDirectoryPath.Value("C:/a")
        });
    }

    [Theory]
    [InlineData("C:\\", "C:\\")]
    [InlineData("C:\\lol\\", "C:\\lol\\")]
    [InlineData("C:\\lol", "C:\\lol")] //...hmmm
    [InlineData("C:/lol", "C:\\lol")]
    public void ShouldReturnTheStringItWasCreatedWithWhenConvertedToString(string input, string expected)
    {
      //GIVEN
      var path = AbsoluteDirectoryPath.Value(input);

      //WHEN
      var convertedToString = path.ToString();

      //THEN
      Assert.Equal(expected, convertedToString);
    }

    [Fact]
    public void ShouldAllowUsingTheAdditionOperatorToConcatenateFileName()
    {
      //GIVEN
      var path = Any.Instance<AbsoluteDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      AbsoluteFilePath absoluteFilePath = path + fileName;

      //WHEN
      var convertedToString = absoluteFilePath.ToString();

      //THEN
      Assert.Equal(Path.Join(path.ToString(), fileName.ToString()), convertedToString);
    }
    
    [Fact]
    public void ShouldAllowAddingConcatenatingFileName()
    {
      //GIVEN
      var path = Any.Instance<AbsoluteDirectoryPath>();
      var fileName = Any.String();
      AbsoluteFilePath absoluteFilePath = path.AddFileName(fileName);

      //WHEN
      var convertedToString = absoluteFilePath.ToString();

      //THEN
      Assert.Equal(Path.Join(path.ToString(), fileName), convertedToString);
    }

    [Fact]
    public void ShouldBeConvertibleToDirectoryInfo()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"C:\lolek\");

      //WHEN
      var directoryInfo = directoryPath.Info();

      //THEN
      Assert.Equal(directoryInfo.FullName, directoryPath.ToString());
    }

    [Fact]
    public void ShouldAllowGettingPathRoot()
    {
      //GIVEN
      var pathString = @"C:\lolek\";
      var dir = AbsoluteDirectoryPath.Value(pathString);

      //WHEN
      AbsoluteDirectoryPath root = dir.Root();

      //THEN
      Assert.Equal(AbsoluteDirectoryPath.Value(Path.GetPathRoot(pathString).OrThrow()), root);
    }

    [Theory,
     InlineData(@"C:\parent\child\", @"C:\parent"),
     InlineData(@"C:\parent\", @"C:\")]
    public void ShouldAllowGettingProperParentDirectoryWhenItExists(string input, string expected)
    {
      //GIVEN
      var dir = AbsoluteDirectoryPath.Value(input);

      //WHEN
      var parent = dir.ParentDirectory();

      //THEN
      Assert.True(parent.HasValue);
      Assert.Equal(AbsoluteDirectoryPath.Value(expected), parent.Value());
    }

    [Fact]
    public void ShouldAllowAccessingDirectoryOfThePathAtSpecifiedLevel()
    {
      //GIVEN
      var dirPath = AbsoluteDirectoryPath("C:\\");
      var dirName1 = Any.Instance<DirectoryName>();
      var dirName2 = Any.Instance<DirectoryName>();
      var dirName3 = Any.Instance<DirectoryName>();
      var dirName4 = Any.Instance<DirectoryName>();
      AbsoluteDirectoryPath absoluteFilePath = dirPath + dirName1 + dirName2 + dirName3 + dirName4;
      
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
    public void ShouldProduceParentWithoutValueThatThrowsOnAccessWhenThereIsNoParentInPath()
    {
      //GIVEN
      const string pathString = @"C:\";
      var dir = AbsoluteDirectoryPath.Value(pathString);

      //WHEN
      var parent = dir.ParentDirectory();

      //THEN
      Assert.False(parent.HasValue);
      Assert.Throws<InvalidOperationException>(() => parent.Value());
    }

    [Theory,
     InlineData(@"F:\Segment1\Segment2\", "Segment2"),
     InlineData(@"F:\Segment1\", "Segment1"),
     InlineData(@"F:\", @"F:\"),
    ]
    public void ShouldAllowGettingTheNameOfCurrentDirectory(string fullPath, string expectedDirectoryName)
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(fullPath);

      //WHEN
      DirectoryName dirName = directoryPath.DirectoryName();

      //THEN
      Assert.Equal(expectedDirectoryName, dirName.ToString());
    }

    [Fact]
    public void ShouldAllowAddingDirectoryNameUsingPlusOperator()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      AbsoluteDirectoryPath directoryPathWithAnotherDirectoryName = directoryPath + DirectoryName.Value("Subdir2");

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Subdir2", directoryPathWithAnotherDirectoryName.ToString());
    }

    [Fact]
    public void ShouldAllowAppendingDirectoryName()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      AbsoluteDirectoryPath directoryPathWithAnotherDirectoryName = 
        directoryPath.AddDirectoryName("Subdir2");

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Subdir2", directoryPathWithAnotherDirectoryName.ToString());
    }

    [Fact]
    public void ShouldAllowAddingDirectoryNameAndFileName()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var directoryName = DirectoryName.Value("Lolek");
      var directoryName2 = DirectoryName.Value("Lolek2");
      var fileName = FileName.Value("File.txt");
      AbsoluteFilePath absoluteFilePath = directoryPath + directoryName + directoryName2 + fileName;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek\Lolek2\File.txt", absoluteFilePath.ToString());
    }

    [Fact]
    public void ShouldAllowAddingRelativeDirectory()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var relativePath = RelativeDirectoryPath.Value(@"Lolek\Lolek2");
      AbsoluteDirectoryPath newDirectoryPath = directoryPath + relativePath;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Lolek\Lolek2", newDirectoryPath.ToString());
    }

    [Fact]
    public void ShouldAllowAddingRelativeDirectoryWithNavigationMarks()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var relativePath = RelativeDirectoryPath.Value(@"..\Lolek\Lolek2");
      AbsoluteDirectoryPath newDirectoryPath = directoryPath + relativePath;

      //THEN
      Assert.Equal(@"G:\Directory\Lolek\Lolek2", newDirectoryPath.ToString());
    }

    [Fact]
    public void ShouldAllowGettingPathEndingOnLastOccurrenceOfDirectoryName()
    {
      //GIVEN
      var pathString = @"C:\lolek1\lolek2\lolek2\lolek3\";
      var pathWithFilename = AbsoluteDirectoryPath(pathString);

      //WHEN
      var fragment = pathWithFilename.FragmentEndingOnLast(DirectoryName("lolek2"));

      //THEN
      fragment.Value().Should().Be(AbsoluteDirectoryPath(@"C:\lolek1\lolek2\lolek2"));
    }

    [Fact]
    public void ShouldThrowExceptionWhenGettingFragmentEndingOnNonExistentDirectoryName()
    {
      //GIVEN
      var pathString = @"C:\lolek1\lolek2\lolek3\";
      var pathWithFilename = AbsoluteDirectoryPath(pathString);

      //WHEN
      var fragment = pathWithFilename.FragmentEndingOnLast(DirectoryName("Trolololo"));

      //THEN
      fragment.Should().Be(Maybe<AbsoluteDirectoryPath>.Nothing);
    }

    [Fact]
    public void ShouldAllowAddingRelativePathWithFileName()
    {
      //GIVEN
      var directoryPath = AbsoluteDirectoryPath.Value(@"G:\Directory\Subdirectory");

      //WHEN
      var relativePathWithFileName = RelativeFilePath.Value(@"Subdirectory2\file.txt");
      AbsoluteFilePath absoluteFilePath = directoryPath + relativePathWithFileName;

      //THEN
      Assert.Equal(@"G:\Directory\Subdirectory\Subdirectory2\file.txt", absoluteFilePath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyDirectoryPath()
    {
      //GIVEN
      var dirPath = Any.Instance<AbsoluteDirectoryPath>();

      //WHEN
      AnyDirectoryPath anyDirectoryPath = dirPath.AsAnyDirectoryPath();

      //THEN
      Assert.Equal(dirPath.ToString(), anyDirectoryPath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var directorypath = Any.Instance<AbsoluteDirectoryPath>();

      //WHEN
      AnyPath anyPathWithFileName = directorypath.AsAnyPath();

      //THEN
      Assert.Equal(directorypath.ToString(), anyPathWithFileName.ToString());
    }

    [Fact]
    public void ShouldDetermineEqualityToAnotherInstanceUsingFileSystemComparisonRules()
    {
      //GIVEN
      var directoryPath1 = Any.Instance<AbsoluteDirectoryPath>();
      var directoryPath2 = Any.Instance<AbsoluteDirectoryPath>();
      var fileSystemComparisonRules = Substitute.For<FileSystemComparisonRules>();
      var comparisonResult = Any.Boolean();

      fileSystemComparisonRules
        .ArePathStringsEqual(directoryPath1.ToString(), directoryPath2.ToString())
        .Returns(comparisonResult);

      //WHEN
      var equality = directoryPath1.ShallowEquals(directoryPath2, fileSystemComparisonRules);

      //THEN
      Assert.Equal(comparisonResult, equality);
    }

    [Theory]
    [InlineData("C:\\d1\\d2", "C:/d1/d2")]
    public void ShouldBeEqualToAnotherPathWithDifferentSeparators(string p1, string p2)
    {
      AbsoluteDirectoryPath(p1).Equals(AbsoluteDirectoryPath(p2)).Should().BeTrue();
    }

    [Theory]
    [InlineData("C:\\d1\\d2", "C:\\", "d1\\d2")]
    [InlineData("C:\\", "C:\\", null)]
    [InlineData("C:\\", "D:\\", null)]
    public void ShouldAllowTrimmingStart(string p1, string p2, string expected)
    {
      Maybe<RelativeDirectoryPath> trimmedPath = AbsoluteDirectoryPath(p1)
        .TrimStart(AbsoluteDirectoryPath(p2));

      trimmedPath.Select(p =>p.ToString()).Should().Be(expected.ToMaybe());
    }
    
    [Theory]
    [InlineData("C:\\d1\\d2", "C:\\", true)]
    [InlineData("C:\\d1\\d2", "C:\\d1", true)]
    [InlineData("C:\\", "D:\\", false)]
    [InlineData("C:\\lolek", "C:\\lol", false)]
    public void ShouldCorrectlyRespondsWhetherPathStartsWithAnother(
        string p1, string p2, bool expected)
    {
        var result = AbsoluteDirectoryPath(p1)
            .StartsWith(AbsoluteDirectoryPath(p2));

        result.Should().Be(expected);
    }

    [Fact]
    public void ShouldBeAbleToCreateDirectoryPathForCurrentFile()
    {
      //GIVEN
      var thisDirPath = AbsoluteDirectoryPath.OfThisFile();
      
      //THEN
      thisDirPath.Should().Be(AbsoluteDirectoryPath(Path.GetDirectoryName(CurrentFilePath()).OrThrow()));
    }

    private static string CurrentFilePath([CallerFilePath] string path = "")
    {
      return path;
    }


  }
}



