using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereColumnsClauseTests
{
    [Test]
    public async Task CustomOperator_ShouldBeUsed_WhenProvided(CancellationToken cancellationToken)
    {
        // Act.
        var query = new SqlQuery("Books").WhereColumns("Title", ">", "Author");

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereColumnsClause>().Count()).IsEqualTo(1);
        await Assert
            .That(query.Clauses.OfType<WhereColumnsClause>().First().Operator)
            .IsEqualTo(">");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert.That(compiledQuery.Sql).IsEqualTo("SELECT * FROM Books WHERE Title > Author");
    }

    [Test]
    public async Task EqualsOperator_ShouldBeUsed_WhenOperatorNotSpecified(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").WhereColumns("Title", "Author");

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereColumnsClause>().Count()).IsEqualTo(1);
        await Assert
            .That(query.Clauses.OfType<WhereColumnsClause>().First().Operator)
            .IsEqualTo("=");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert.That(compiledQuery.Sql).IsEqualTo("SELECT * FROM Books WHERE Title = Author");
    }
}
