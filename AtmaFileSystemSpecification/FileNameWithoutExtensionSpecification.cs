using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;

namespace AtmaFileSystemSpecification
{
  public class FileNameWithoutExtensionSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<FileNameWithoutExtension>();
    }

    [Fact]
    public void ShouldAllowAccessingTheNameAsString()
    {
      //GIVEN
      var fileName = Any.String();
      var fileNameObject = FileNameWithoutExtension.Value(fileName);

      //WHEN
      var nameObtainedFromConversion = fileNameObject.ToString();

      //THEN
      Assert.Equal(fileName, nameObtainedFromConversion);
    }

    [Fact]
    public void ShouldConvertIntoFileNameWhenExtensionIsAdded()
    {
      //GIVEN
      var fileNameWithoutExtensionString = Any.String();
      var extensionString = "." + Any.String();
      var fileNameWithoutExtension = FileNameWithoutExtension.Value(fileNameWithoutExtensionString);
      var extension = FileExtension.Value(extensionString);

      //WHEN
      FileName nameObtainedFromConversion = fileNameWithoutExtension.With(extension);

      //THEN
      Assert.Equal(fileNameWithoutExtensionString + extensionString, nameObtainedFromConversion.ToString());
    }

  }
}
