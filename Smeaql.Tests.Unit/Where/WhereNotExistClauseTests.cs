using Smeaql.Compilers;
using Smeaql.Where;

namespace Smeaql.Tests.Unit.Where;

public sealed class WhereNotExistClauseTests
{
    [Test]
    public async Task Parameter_ShouldBeSetInParentQuery_WhenUsingWhereNotExistsClause(CancellationToken cancellationToken)
    {
        //Act.
        const string customerId = "1234";
        var query = new SqlQuery("Customers").Select("FirstName", "LastName")
            .WhereNotExists(new SqlQuery("InactiveCustomers").Where("CustomerId", customerId));
        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereNotExistsClause>().Count()).IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT FirstName,LastName FROM Customers WHERE NOT EXISTS (SELECT * FROM InactiveCustomers WHERE CustomerId = @p0)");
        await Assert.That(compiledQuery.Parameters.Count).IsEqualTo(1);
        await Assert.That(compiledQuery.Parameters["p0"]).IsEqualTo(customerId);
    }

    [Test]
    public async Task SubQueryComparingTwoColumns_ShouldBeCreated_WhenUsingWhereNotExistsClause(CancellationToken cancellationToken)
    {
        var query = new SqlQuery("Customers")
            .Select("FirstName", "LastName")
            .WhereNotExists(new SqlQuery("InactiveCustomers")
                .WhereColumns("CustomerId", "InactiveCustomerId"));
        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<WhereNotExistsClause>().Count()).IsEqualTo(1);
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo( "SELECT FirstName,LastName FROM Customers WHERE NOT EXISTS (SELECT * FROM InactiveCustomers WHERE CustomerId = InactiveCustomerId)");
    }
}