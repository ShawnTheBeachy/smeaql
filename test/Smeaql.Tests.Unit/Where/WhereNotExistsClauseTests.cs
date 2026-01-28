using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereNotExistsClauseTests
{
    [Test]
    public async Task Parameter_ShouldBeAdded_WhenUsingWhereNotExistsClause(
        CancellationToken cancellationToken
    )
    {
        //Act.
        const string customerId = "1234";
        var query = new SqlQuery("Customers")
            .Select("FirstName", "LastName")
            .WhereNotExists("Customers", "CustomerId", customerId);
        // Assert.
        using var asserts = Assert.Multiple();
        await Assert
            .That(query.Clauses.OfType<WhereNotExistsClause<SqlSelectQuery>>().Count())
            .IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo(
                "SELECT FirstName,LastName FROM Customers WHERE NOT EXISTS (SELECT @p0 FROM Customers WHERE CustomerId = @p1)"
            );
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(2);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters["p1"]).IsEqualTo(customerId);
    }

    [Test]
    public async Task SubQueryComparingTwoColumns_ShouldBeCreated_WhenUsingWhereNotExistsClause(
        CancellationToken cancellationToken
    )
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
            .That(query.Clauses.OfType<WhereNotExistsClause<SqlSelectQuery>>().Count())
            .IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo(
                "SELECT FirstName,LastName FROM Customers WHERE NOT EXISTS (SELECT @p0 FROM InactiveCustomers WHERE CustomerId = InactiveCustomerId)"
            );
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(1);
    }
}
