using System;
using AtmaFileSystem;
using FluentAssertions;
using TddXt.AnyRoot.Strings;
using TddXt.XFluentAssert.Root;
using Xunit;
using static TddXt.AnyRoot.Root;
using Maybe = AtmaFileSystem.Maybe;

namespace AtmaFileSystemSpecification
{
  public class MaybeSpecification
  {
    [Fact]
    public void ShouldBehaveLikeValue()
    {
      typeof(AtmaFileSystem.Maybe<AbsoluteDirectoryPath>).Should().HaveValueSemantics();
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