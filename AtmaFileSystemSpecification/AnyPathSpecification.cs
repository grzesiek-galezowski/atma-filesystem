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

    [Theory,
  InlineData(null, typeof(ArgumentNullException)),
  InlineData("", typeof(ArgumentException)),
  InlineData(@"\\\\\\\\\?|/\/|", typeof(InvalidOperationException)),
]
    public void ShouldThrowExceptionWhenCreatedWithInvalidValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyPath.Value(invalidInput));
    }

  }
}
