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
  public class AnyFilePathSpecification
  {
    [Theory,
      InlineData(null, typeof(ArgumentNullException)),
      InlineData("", typeof(ArgumentException)),
      InlineData(@"\\\\\\\\\?|/\/|", typeof(InvalidOperationException)),
      InlineData(@"file.txt", typeof(InvalidOperationException))
    ]
    public void ShouldThrowExceptionWhenCreatedWithNullValue(string invalidInput, Type exceptionType)
    {
      Assert.Throws(exceptionType, () => AnyFilePath.Value(invalidInput));
    }

    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<AnyFilePath>();
    }

    [Fact]
    public void ShouldBeConvertibleToAnyPath()
    {
      //GIVEN
      var pathWithFileName = Any.Instance<AnyFilePath>();

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
      var anyPathWithFileName = AnyFilePath.Value(path);
      var extensionValue = FileExtension.Value(extension);

      //WHEN
      var hasExtension = anyPathWithFileName.Has(extensionValue);

      //THEN
      Assert.Equal(expectedResult, hasExtension);
    }

    [Fact]
    public void ShouldAllowAccessingFileName()
    {
      //GIVEN
      var path = AnyFilePath.Value(@"Dir\Subdir\fileName.txt");

      //WHEN
      var fileName = path.FileName();

      //THEN
      Assert.Equal(FileName.Value("fileName.txt"), fileName);

    }

    [Fact]
    public void ShouldAllowAccessingDirectoryOfThePath()
    {
      //GIVEN
      var dirPath = Any.Instance<AnyDirectoryPath>();
      var fileName = Any.Instance<FileName>();
      AnyFilePath filePath = dirPath + fileName;

      //WHEN
      var dirObtainedFromPath = filePath.Directory();

      //THEN
      Assert.Equal(dirPath, dirObtainedFromPath);
    }

    [Fact]
    public void ShouldBeConvertibleToFileInfo()
    {
      //GIVEN
      var pathWithFilename = AnyFilePath.Value(@"C:\Directory\file.txt");

      //WHEN
      var fileInfo = pathWithFilename.Info();

      //THEN
      Assert.Equal(fileInfo.FullName, pathWithFilename.ToString());
    }


  }
}
