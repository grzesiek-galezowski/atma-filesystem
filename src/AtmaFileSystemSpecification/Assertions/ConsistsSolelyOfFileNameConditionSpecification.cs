using AtmaFileSystem.Assertions;

namespace AtmaFileSystemSpecification.Assertions;

public class ConsistsSolelyOfFileNameConditionSpecification
{
  [Theory]
  [InlineData("file.txt", false)] // Valid file name
  [InlineData("folder\\file.txt", true)] // Path with folder
  [InlineData("folder/file.txt", true)] // Path with folder (Unix style)
  [InlineData("", false)] // Empty string
  [InlineData(null, false)] // Null value
  [InlineData("   ", false)] // Whitespace only
  [InlineData("file.name.with.many.dots.txt", false)] // File name with multiple dots
  [InlineData("file", false)] // File name without extension
  [InlineData("C:\\folder\\file.txt", true)] // Absolute path (Windows style)
  [InlineData("/folder/file.txt", true)] // Absolute path (Unix style)
  public void ShouldFailForIncorrectInput(string path, bool expected)
  {
    // Arrange
    var condition = new ConsistsSolelyOfFileNameCondition();

    // Act
    var result = condition.FailsFor(path);

    // Assert
    Assert.Equal(expected, result);
  }

  [Theory]
  [InlineData("file.txt", "Expected file name, but got file.txt")]
  [InlineData("folder\\file.txt", "Expected file name, but got folder\\file.txt")]
  [InlineData("", "Expected file name, but got ")] //bug rethink
  [InlineData(null, "Expected file name, but got ")] //bug rethink
  public void ShouldReturnExpectedExceptionMessage(string path, string expectedMessage)
  {
    // Arrange
    var condition = new ConsistsSolelyOfFileNameCondition();

    // Act
    var exception = condition.RuleException(path);

    // Assert
    Assert.NotNull(exception);
    Assert.IsType<ArgumentException>(exception);
    Assert.Equal(expectedMessage, exception.Message);
  }
}