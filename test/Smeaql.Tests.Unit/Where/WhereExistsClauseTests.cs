using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereExistsClauseTests
{
    [Test]
    public async Task Condition_ShouldBeExists_WhenExistsFlagIsUsed()
    {
        // Arrange.
        var query = new SqlQuery().Select().From("Fruits").WhereExists("Favorites", "Id", 123);

        // Act.
        var (sql, parameters) = new SqlServerCompiler().Compile(query);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(sql)
            .IsEqualTo(
                "SELECT * FROM Fruits WHERE EXISTS(SELECT @p0 FROM Favorites WHERE Id = @p1)"
            );
        await Assert.That(parameters.Count).IsEqualTo(2);
        await Assert.That(parameters["p0"]).IsEqualTo(1);
        await Assert.That(parameters["p1"]).IsEqualTo(123);
    }

    [Test]
    public async Task Condition_ShouldBeNotExists_WhenNotExistsFlagIsUsed()
    {
        // Arrange.
        var query = new SqlQuery().Select().From("Fruits").WhereNotExists("Favorites", "Id", 123);

        // Act.
        var (sql, parameters) = new SqlServerCompiler().Compile(query);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(sql)
            .IsEqualTo(
                "SELECT * FROM Fruits WHERE NOT EXISTS(SELECT @p0 FROM Favorites WHERE Id = @p1)"
            );
        await Assert.That(parameters.Count).IsEqualTo(2);
        await Assert.That(parameters["p0"]).IsEqualTo(1);
        await Assert.That(parameters["p1"]).IsEqualTo(123);
    }

    [Test]
    public async Task SubQueryComparingTwoColumns_ShouldBeCreated_WhenUsingWhereNotExistsClause()
    {
        var query = new SqlQuery("Customers")
            .Select("FirstName", "LastName")
            .WhereNotExists(
                new SqlQuery("InactiveCustomers")
                    .SelectValue(1)
                    .WhereColumns("CustomerId", "InactiveCustomerId")
            );
        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(query.Clauses.OfType<WhereExistsClause<SqlSelectQuery>>().Count())
            .IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo(
                "SELECT FirstName,LastName FROM Customers WHERE NOT EXISTS(SELECT @p0 FROM InactiveCustomers WHERE CustomerId = InactiveCustomerId)"
            );
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(1);
    }
}
