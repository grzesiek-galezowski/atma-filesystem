using AtmaFileSystem.Assertions;

namespace AtmaFileSystemSpecification.Assertions;

public class ConsistsSolelyOfExtensionConditionSpecification
{
  [Theory]
  [InlineData(".txt", false)] // Valid extension
  [InlineData(".jpg", false)] // Valid extension
  [InlineData("txt", true)] // Missing dot
  [InlineData("", false)] // Empty string
  [InlineData(null, false)] // Null value
  [InlineData(".TXT", false)] // Valid extension with uppercase
  [InlineData(".123", false)] // Numeric extension
  [InlineData(".ext.ext", true)] // Multiple dots
  [InlineData(".", true)] // Only dot
  [InlineData("..", true)] // Two dots
  public void ShouldFailForStringThatConsistsOfMoreThanAnExtension(string? extensionString, bool expectedResult)
  {
    // Arrange
    var condition = new ConsistsSolelyOfExtensionCondition();

    // Act
    var result = condition.FailsFor(extensionString);

    // Assert
    Assert.Equal(expectedResult, result);
  }

  [Theory]
  [InlineData("txt.txt", "Invalid extension txt.txt. Expected extension: .txt")]
  [InlineData("txt", "Invalid extension txt. Expected extension: ")]
  [InlineData("", "Invalid extension . Expected extension: ")]
  [InlineData(null, "Invalid extension . Expected extension: ")]
  [InlineData(".123", "Invalid extension .123. Expected extension: .123")]
  public void ShouldCreateRuleExceptionWithDescriptiveError(string? extensionString, string expectedMessage)
  {
    // Arrange
    var condition = new ConsistsSolelyOfExtensionCondition();

    // Act
    var exception = condition.RuleException(extensionString);

    // Assert
    Assert.Equal(expectedMessage, exception.Message);
  }
}