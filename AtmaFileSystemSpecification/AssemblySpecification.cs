using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class AssemblySpecification
  {
    [Fact]
    public void ShouldDOWHAT()
    {
      XAssert.SingleConstructor(typeof(RelativeDirectoryPath).Assembly);
    }
  }
}
