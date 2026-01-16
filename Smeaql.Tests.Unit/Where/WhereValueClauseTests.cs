using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereValueClauseTests
{
    [Test]
    public async Task CustomOperator_ShouldBeUsed_WhenProvided(CancellationToken cancellationToken)
    {
        // Act.
        var query = new SqlQuery("Books").Where("Rating", ">", 3);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereValueClause>().Count()).IsEqualTo(1);
        await Assert.That(query.Clauses.OfType<WhereValueClause>().First().Operator).IsEqualTo(">");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert.That(compiledQuery.Sql).IsEqualTo("SELECT * FROM Books WHERE Rating > @p0");
    }

    [Test]
    public async Task EqualsOperator_ShouldBeUsed_WhenOperatorNotSpecified(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").Where("Rating", 5);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereValueClause>().Count()).IsEqualTo(1);
        await Assert.That(query.Clauses.OfType<WhereValueClause>().First().Operator).IsEqualTo("=");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert.That(compiledQuery.Sql).IsEqualTo("SELECT * FROM Books WHERE Rating = @p0");
    }

    [Test]
    public async Task Parameter_ShouldBeCreated_WhenUsingWhere(CancellationToken cancellationToken)
    {
        // Act.
        var query = new SqlQuery("Books").Where("Rating", 5);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereValueClause>().Count()).IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert.That(compiledQuery.Sql).IsEqualTo("SELECT * FROM Books WHERE Rating = @p0");
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters.First().Key).IsEqualTo("p0");
        await Assert.That(compiledQuery.Parameters.First().Value).IsEqualTo(5);
    }

    [Test]
    public async Task Parameter_ShouldBeEqualToOne_WhenUsingWhereTrue(CancellationToken cancellationToken)
    {
        // Act.
        var query = new SqlQuery("Books").WhereTrue("Rating");
        
        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereValueClause>().Count()).IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert.That(compiledQuery.Sql).IsEqualTo("SELECT * FROM Books WHERE Rating = @p0");
        await Assert.That(compiledQuery.Parameters.First().Value).IsEqualTo(1);
    }
}
