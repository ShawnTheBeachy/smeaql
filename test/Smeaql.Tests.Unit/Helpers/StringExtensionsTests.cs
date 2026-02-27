using Smeaql.Helpers;

namespace Smeaql.Tests.Unit.Helpers;

public sealed class StringExtensionsTests
{
    [Test]
    public async Task Sanitize_ShouldDoNothing_WhenValueIsEmpty()
    {
        // Arrange.
        const string value = "";

        // Act.
        var sanitized = new string(value.Sanitize());

        // Assert.
        await Assert.That(sanitized).IsEqualTo(value);
    }

    [Test]
    public async Task Sanitize_ShouldDoNothing_WhenValueIsEnclosedInDoubleQuotes()
    {
        // Arrange.
        const string value = "\"Value\"";

        // Act.
        var sanitized = new string(value.Sanitize());

        // Assert.
        await Assert.That(sanitized).IsEqualTo(value);
    }

    [Test]
    public async Task Sanitize_ShouldDoNothing_WhenValueIsEnclosedInSquareBrackets()
    {
        // Arrange.
        const string value = "[Value]";

        // Act.
        var sanitized = new string(value.Sanitize());

        // Assert.
        await Assert.That(sanitized).IsEqualTo(value);
    }

    [Test]
    public async Task Sanitize_ShouldEncloseValueInSquareBrackets_WhenItIsNotAlreadyEnclosed()
    {
        // Arrange.
        const string value = "My Value";

        // Act.
        var sanitized = new string(value.Sanitize());

        // Assert.
        await Assert.That(sanitized).IsEqualTo("[My Value]");
    }
}
