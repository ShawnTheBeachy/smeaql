using Smeaql.Compilers;
using Smeaql.Having;

namespace Smeaql.Tests.Unit.Having;

public sealed class HavingValueClauseTests
{
    [Test]
    public async Task Having_ShouldBeAppendedToSql_WhenPresent(CancellationToken cancellationToken)
    {
        // Arrange.
        var query = new SqlQuery("Books").Select("*").GroupBy("Author").Having("[Rating]", ">", 1);

        // Act.
        var (sql, parameters) = new SqlServerCompiler().Compile(query);

        // Assert.
        using var asserts = Assert.Multiple();
        var havingClauses = query.Clauses.OfType<HavingValueClause>().ToArray();
        await Assert.That(havingClauses.Length).IsEqualTo(1);
        await Assert.That(parameters["p0"]).IsEqualTo(1);
        await Assert
            .That(sql)
            .IsEqualTo("SELECT * FROM [Books] GROUP BY [Author] HAVING [Rating] > @p0");
    }
}
