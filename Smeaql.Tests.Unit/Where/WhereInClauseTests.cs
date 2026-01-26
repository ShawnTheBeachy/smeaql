using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereInClauseTests
{
    [Test]
    public async Task Parameters_ShouldBeCreated_WhenUsingWhereIn(
        CancellationToken cancellationToken
    )
    {
        // Act.
        const string authorA = "James Islington";
        const string authorB = "Jonathan Renshaw";
        var query = new SqlQuery("Books").WhereIn("Author", authorA, authorB);

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereInClause>().Count()).IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books WHERE Author IN (@p0,@p1)");
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(2);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(authorA);
        await Assert.That(compiledQuery.Parameters["p1"]).IsEqualTo(authorB);
    }

    [Test]
    public async Task Parameters_ShouldBeSetInParentQuery_WhenUsingWhereInSubQueryAndParametersAreAddedToTheSubQuery(CancellationToken cancellationToken)
    {
        //Act
        const string employeeId1 = "123";
        var query = new SqlQuery("Person")
            .Select("FirstName", "LastName")
            .WhereIn("LastName", new SqlQuery("Employee")
                .Where("EmployeeId", employeeId1)
                );

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereInSubQueryClause>().Count()).IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT FirstName,LastName FROM Person WHERE LastName IN (SELECT * FROM Employee WHERE EmployeeId = @p0)");
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(employeeId1);
    }
}
