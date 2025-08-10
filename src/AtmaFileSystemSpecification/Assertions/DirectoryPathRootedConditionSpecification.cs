using AtmaFileSystem.Assertions;
using System;
using Xunit;

namespace AtmaFileSystemSpecification.Assertions
{
  public class DirectoryPathRootedConditionSpecification
  {
    [Theory]
    [InlineData(null)] // Null path
    [InlineData("")] // Empty string
    [InlineData(" ")] // Whitespace only
    [InlineData("C:\\")] // Valid rooted path
    [InlineData("C:\\Valid\\Path")] // Valid directory path
    [InlineData("Invalid|Path")] // Path with invalid characters
    [InlineData("C:\\TrailingSlash\\")] // Path with trailing slash
    [InlineData("Relative\\Path")] // Relative path
    [InlineData("..\\Relative\\Path")] // Relative path with parent directory
    [InlineData("C:\\..\\..\\")] // Path with excessive parent directory traversal
    [InlineData("C:\\CON\\Path")] // Path with reserved device name
    public void ShouldReturnExpectedExceptionMessage(string path)
    {
      // Arrange
      var condition = new DirectoryPathRootedCondition();

      // Act
      var exception = condition.RuleException(path);

      // Assert
      Assert.IsType<ArgumentException>(exception);
      Assert.Equal($"Path {path} is not a valid directory path.", exception.Message);
    }

    [Theory]
    [InlineData(null, false)] // Null path
    [InlineData("", false)] // Empty string
    [InlineData(" ", false)] // Whitespace only
    [InlineData("C:\\", false)] // Valid rooted path
    [InlineData("C:\\Valid\\Path", false)] // Valid directory path
    [InlineData("C:\\TrailingSlash\\", false)] // Path with trailing slash
    [InlineData("Relative\\Path", false)] // Relative path
    [InlineData("..\\Relative\\Path", false)] // Relative path with parent directory
    [InlineData("C:\\..\\..\\", false)] // Path with excessive parent directory traversal
    public void ShouldFailForInvalidInput(string? path, bool expectedResult)
    {
      // Arrange
      var condition = new DirectoryPathRootedCondition();

      // Act
      var result = condition.FailsFor(path);

      // Assert
      Assert.Equal(expectedResult, result);
    }
  }
}