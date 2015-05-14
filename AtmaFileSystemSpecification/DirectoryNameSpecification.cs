using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class DirectoryNameSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<DirectoryName>();
    }

    [Fact]
    public void ShouldFormRelativeDirectoryPathWhenAddedToAnotherDirectoryName()
    {
      //GIVEN
      var directoryName = DirectoryName.Value("Dir1");
      var subdirectoryName = DirectoryName.Value("Dir2");

      //WHEN
      RelativeDirectoryPath relativePath = directoryName + subdirectoryName;

      //THEN
      Assert.Equal(relativePath.ToString(), @"Dir1\Dir2");
    }

    [Fact]
    public void ShouldAllowAddingRelativeDirectoryName()
    {
      //GIVEN
      var directoryName = DirectoryName.Value("Dir1");
      var subdirectories = new RelativeDirectoryPath(@"Dir2\Dir3");

      //WHEN
      RelativeDirectoryPath relativePath = directoryName + subdirectories;

      //THEN
      Assert.Equal(relativePath.ToString(), @"Dir1\Dir2\Dir3");
    }



  }
}
