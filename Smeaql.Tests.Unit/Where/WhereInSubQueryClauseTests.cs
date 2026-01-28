using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereInSubQueryClauseTests
{
    [Test]
    public async Task CorrectSql_ShouldBeGenerated_WhenCompilingWhereInSubQuery()
    {
        // Arrange.
        var query = new SqlQuery("Fruits").WhereIn(
            "Name",
            new SqlQuery("Favorites").Select("Name")
        );

        // Act.
        var (sql, parameters) = new SqlServerCompiler().Compile(query);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(sql)
            .IsEqualTo("SELECT * FROM Fruits WHERE Name IN (SELECT Name FROM Favorites)");
        await Assert.That(parameters).IsEmpty();
    }

    [Test]
    public async Task Parameters_ShouldBeAdded_WhenUsedInSubQuery(
        CancellationToken cancellationToken
    )
    {
        //Act
        const string employeeId = "123";
        var query = new SqlQuery("Person")
            .Select("FirstName", "LastName")
            .WhereIn(
                "LastName",
                new SqlQuery("Employee").Select("LastName").Where("EmployeeId", employeeId)
            );

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(query.Clauses.OfType<WhereInSubQueryClause<SqlSelectQuery>>().Count())
            .IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo(
                "SELECT FirstName,LastName FROM Person WHERE LastName IN (SELECT LastName FROM Employee WHERE EmployeeId = @p0)"
            );
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(employeeId);
    }
}
