using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtmaFileSystem;
using AtmaFileSystem.Assertions;
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



    
    [Theory,
      InlineData(null),
      InlineData(""),
      InlineData(@"C:\a")]
    public void ShouldNotLetCreateInvalidInstance(string input)
    {
      Assert.Throws<ArgumentException>(() => DirectoryName.Value(input));
    }


  }
}
