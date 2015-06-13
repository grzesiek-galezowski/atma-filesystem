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
  public class AnyPathSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<AnyPath>();
    }

  }
}
