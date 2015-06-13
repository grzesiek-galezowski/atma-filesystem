using System;
using System.CodeDom;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using AtmaFileSystem;
using AtmaFileSystem.Assertions;
using Pri.LongPath;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class RelativeDirectoryPathSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<RelativeDirectoryPath>();
    }

    [Fact]
    public void ShouldAllowAddingDirectoryNameToIt()
    {
      //GIVEN
      var relativeDir = RelativeDirectoryPath.Value(@"lolek\bolek");
      var dirName = DirectoryName.Value("zenek");

      //WHEN
      RelativeDirectoryPath mergedPath = relativeDir + dirName;

      //THEN
      Assert.Equal(@"lolek\bolek\zenek", mergedPath.ToString());
    }

    [Fact]
    public void ShouldAllowGettingPathWithoutLastDirectory()
    {
      //GIVEN
      var relativePath = RelativeDirectoryPath.Value(@"Directory\Subdirectory\Subsubdirectory");
      
      //WHEN
      AtmaFileSystem.Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.Parent();

      //THEN
      Assert.True(pathWithoutLastDir.Found);
      Assert.Equal(RelativeDirectoryPath.Value(@"Directory\Subdirectory"), pathWithoutLastDir.Value());

    }

    [Fact]
    public void ShouldReturnNothingWhenGettingPathWithoutLastDirectoryButCurrentDirectoryIsTheOnlyLeft()
    {
      //GIVEN
      var relativePath = RelativeDirectoryPath.Value(@"Directory");

      //WHEN
      AtmaFileSystem.Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.Parent();

      //THEN
      Assert.False(pathWithoutLastDir.Found);
      Assert.Throws<InvalidOperationException>(() => pathWithoutLastDir.Value());
    }

    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
      InlineData(@"C:\", typeof(ArgumentException))]
    public void ShouldNotAllowCreatingInvalidInstance(string input, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => RelativeDirectoryPath.Value(input));

    }

    [Fact]
    public void ShouldBeConvertibleToRelativePathWithFileNameWhenFileNameIsAddedToIt()
    {
      //GIVEN
      var relativePath = RelativeDirectoryPath.Value(@"Dir\subdir");
      var fileName = FileName.Value("file.txt");

      //WHEN
      RelativePathWithFileName pathWithFileName = relativePath.With(fileName);

      //THEN
      Assert.Equal(@"Dir\subdir\file.txt", pathWithFileName.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToDirectoryInfo()
    {
      //GIVEN
      var path = RelativeDirectoryPath.Value(@"Dir\Subdir");

      //WHEN
      var directoryInfo = path.Info();

      //THEN
      Assert.Equal(directoryInfo.FullName, FullNameFrom(path));
    }

    [Fact]
    public void ShouldBeConvertibleToAnyDirectoryPath()
    {
      //GIVEN
      var dirPath = Any.Instance<RelativeDirectoryPath>();

      //WHEN
      AnyDirectoryPath anyDirectoryPath = dirPath.AsAnyDirectoryPath();

      //THEN
      Assert.Equal(dirPath.ToString(), anyDirectoryPath.ToString());
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var directorypath = Any.Instance<RelativeDirectoryPath>();

      //WHEN
      AnyPath anyPathWithFileName = directorypath.AsAnyPath();

      //THEN
      Assert.Equal(directorypath.ToString(), anyPathWithFileName.ToString());
    }

    private static string FullNameFrom(RelativeDirectoryPath path)
    {
      return Path.Combine(new DirectoryInfo(".").FullName, path.ToString());
    }


  }

  //todo cut out first directory from relative directory path = relative directory path
  //todo create relative path with file name
  //bug go back to using System.IO.DirectoryInfo? Or make two methods?
  //TODO make classes: AnyDirectoryPath, AnyPathWithFileName, AnyPath
}
