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
  public class AnyDirectoryPathSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<AnyDirectoryPath>();
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var anyDirectoryPath = Any.Instance<AnyDirectoryPath>();

      //WHEN
      AnyPath anyPathWithFileName = anyDirectoryPath.AsAnyPath();

      //THEN
      Assert.Equal(anyDirectoryPath.ToString(), anyPathWithFileName.ToString());
    }

  }
}
