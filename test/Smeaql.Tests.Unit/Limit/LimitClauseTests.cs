using Smeaql.Compilers;

namespace Smeaql.Tests.Unit.Limit;

public sealed class LimitClauseTests
{
    [Test]
    public async Task LimitOffset_ShouldBeUsed_WhenCompilingWithNonSqlServer()
    {
        // Act.
        var query = new SqlQuery("Books").Select("*").Page(2, 5);

        // Assert.
        var compiledQuery = new SqliteCompiler().Compile(query);
        using var asserts = Assert.Multiple();
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM [Books] LIMIT @p0 OFFSET @p1");
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(5);
        await Assert.That(compiledQuery.Parameters["p1"]).IsEqualTo(5);
    }

    [Test]
    public async Task OffsetFetchNext_ShouldBeUsed_WhenCompilingWithSqlServer()
    {
        // Act.
        var query = new SqlQuery("Books").Select("*").Page(2, 5);

        // Assert.
        var compiledQuery = new SqlServerCompiler().Compile(query);
        using var asserts = Assert.Multiple();
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM [Books] OFFSET @p0 ROWS FETCH NEXT @p1 ROWS ONLY");
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(5);
        await Assert.That(compiledQuery.Parameters["p1"]).IsEqualTo(5);
    }
}
