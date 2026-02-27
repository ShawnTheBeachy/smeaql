using Smeaql.Compilers;
using Smeaql.Order;

namespace Smeaql.Tests.Unit.Order;

public sealed class OrderColumnsClauseTests
{
    [Test]
    public async Task OrderBy_ShouldBeAppended_WhenPresent(CancellationToken cancellationToken)
    {
        // Arrange.
        var query = new SqlQuery("Books")
            .Select("*")
            .OrderByAsc("Title", "Author")
            .OrderByDesc("Rating");

        // Act.
        var (sql, _) = new SqlServerCompiler().Compile(query);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<OrderColumnsClause>().Count()).IsEqualTo(2);
        await Assert
            .That(sql)
            .IsEqualTo("SELECT * FROM [Books] ORDER BY [Title] ASC,[Author] ASC,[Rating] DESC");
    }
}
