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
  public class AnyPathWithFileNameSpecification
  {
    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
      InlineData(@"\\\\\\\\\?|/\/|", typeof(ArgumentException)),
    ]
    public void ShouldThrowExceptionWhenCreatedWithNullValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyPathWithFileName.Value(invalidInput));
    }

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<AnyPathWithFileName>();
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<AnyPathWithFileName>();

      //WHEN
      AnyPath anyPath = pathWithFileName.AsAnyPath();

      //THEN
      Assert.Equal(pathWithFileName.ToString(), anyPath.ToString());
    }

    [Theory,
     InlineData(@"Dir\Subdir\fileName.txt", ".txt", true),
     InlineData(@"Dir\Subdir\fileName.tx", ".txt", false),
     InlineData(@"Dir\Subdir\fileName", ".txt", false),
    ]
    public void ShouldBeAbleToRecognizeWhetherItHasCertainExtension(string path, string extension, bool expectedResult)
    {
      //GIVEN
      var anyPathWithFileName = AnyPathWithFileName.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = anyPathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }
  }
}
