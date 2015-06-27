using System;
using AtmaFileSystem;
using TddEbook.TddToolkit;
using Xunit;
using Maybe = AtmaFileSystem.Maybe;

namespace AtmaFileSystemSpecification
{
  public class MaybeSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      XAssert.IsValue<AtmaFileSystem.Maybe<AbsoluteDirectoryPath>>();
    }

    [Fact]
    public void ShouldReturnResultOfNotNullExecutionWhenItHasValue()
    {
      //GIVEN
      var str = Any.String();
      var maybeWithValue = AtmaFileSystem.Maybe.Wrap(str);
      var additionalString = Any.String();

      //WHEN
      AtmaFileSystem.Maybe<string> additionResult = maybeWithValue.Transform(s => s + additionalString);

      //THEN
      Assert.True(additionResult.Found);
      Assert.Equal(str + additionalString, additionResult.Value());

    }

    [Fact]
    public void ShouldReturnNothingWhenTransformedWhenItHasNoValue()
    {
      //GIVEN
      var maybeWithValue = AtmaFileSystem.Maybe<string>.Not;
      var additionalString = Any.String();

      //WHEN
      AtmaFileSystem.Maybe<string> additionResult = maybeWithValue.Transform(s => s + additionalString);

      //THEN
      Assert.False(additionResult.Found);
      Assert.Throws<InvalidOperationException>(
        () => additionResult.Value());

    }
  }
}