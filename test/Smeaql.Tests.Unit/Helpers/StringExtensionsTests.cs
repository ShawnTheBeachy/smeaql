using Smeaql.Helpers;

namespace Smeaql.Tests.Unit.Helpers;

public sealed class StringExtensionsTests
{
    [Test]
    public async Task Bracket_ShouldDoNothing_WhenValueIsEmpty()
    {
        // Arrange.
        const string value = "";

        // Act.
        var bracketed = value.Bracket();

        // Assert.
        using var assert = Assert.Multiple();
        await Assert.That(bracketed).IsEqualTo(value);
        await Assert.That(bracketed).IsSameReferenceAs(value);
    }

    [Test]
    public async Task Bracket_ShouldDoNothing_WhenValueIsEnclosedInDoubleQuotes()
    {
        // Arrange.
        const string value = "\"Value\"";

        // Act.
        var bracketed = value.Bracket();

        // Assert.
        using var assert = Assert.Multiple();
        await Assert.That(bracketed).IsEqualTo(value);
        await Assert.That(bracketed).IsSameReferenceAs(value);
    }

    [Test]
    public async Task Bracket_ShouldDoNothing_WhenValueIsEnclosedInSquareBrackets()
    {
        // Arrange.
        const string value = "[Value]";

        // Act.
        var bracketed = value.Bracket();

        // Assert.
        using var assert = Assert.Multiple();
        await Assert.That(bracketed).IsEqualTo(value);
        await Assert.That(bracketed).IsSameReferenceAs(value);
    }

    [Test]
    public async Task Bracket_ShouldEncloseValueInSquareBrackets_WhenItIsNotAlreadyEnclosed()
    {
        // Arrange.
        const string value = "My Value";

        // Act.
        var bracketed = value.Bracket();

        // Assert.
        using var assert = Assert.Multiple();
        await Assert.That(bracketed).IsEqualTo("[My Value]");
        await Assert.That(bracketed).IsNotSameReferenceAs(value);
    }
}
