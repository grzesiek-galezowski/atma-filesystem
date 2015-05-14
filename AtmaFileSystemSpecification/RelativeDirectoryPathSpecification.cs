using System;
using AtmaFileSystem;
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
      var relativeDir = new RelativeDirectoryPath(@"lolek\bolek");
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
      var relativePath = new RelativeDirectoryPath(@"Directory\Subdirectory\Subsubdirectory");
      
      //WHEN
      AtmaFileSystem.Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.Parent();

      //THEN
      Assert.True(pathWithoutLastDir.Found);
      Assert.Equal(new RelativeDirectoryPath(@"Directory\Subdirectory"), pathWithoutLastDir.Value());

    }

    [Fact]
    public void ShouldReturnNothingWhenGettingPathWithoutLastDirectoryButCurrentDirectoryIsTheOnlyLeft()
    {
      //GIVEN
      var relativePath = new RelativeDirectoryPath(@"Directory");

      //WHEN
      AtmaFileSystem.Maybe<RelativeDirectoryPath> pathWithoutLastDir = relativePath.Parent();

      //THEN
      Assert.False(pathWithoutLastDir.Found);
      Assert.Throws<InvalidOperationException>(() => pathWithoutLastDir.Value());
    }

  }

  //todo cut out first directory from relative directory path = relative directory path
  //todo create relative path with file name
}
