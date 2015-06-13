using System;
using System.CodeDom;
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
    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
      InlineData(@"\\\\\\\\\?|/\/|", typeof(ArgumentException)),
      ]
    public void ShouldThrowExceptionWhenCreatedWithNullValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyDirectoryPath.Value(invalidInput));
    }

    [Fact]
    public void ShouldReturnNonNullValueWhenValidPathIsPassed()
    {
      //GIVEN
      const string relativePath = @"Dir\Subdir";
      const string absolutePath = @"C:\Dir\Subdir";
      var path = AnyDirectoryPath.Value(relativePath);
      var path2 = AnyDirectoryPath.Value(absolutePath);

      //THEN
      Assert.Equal(relativePath, path.ToString());
      Assert.Equal(absolutePath, path2.ToString());
    }

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
