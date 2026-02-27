using Smeaql.Compilers;
using Smeaql.From;

namespace Smeaql.Tests.Unit;

public sealed class SqlQueryBaseTests
{
    [Test]
    public async Task When_ShouldExecuteAction_WhenConditionIsTrue()
    {
        // Arrange.
        var query = new TestQuery();

        // Act.
        query = query.When(true, q => q.From("Table"));

        // Assert.
        using var assert = Assert.Multiple();
        await Assert.That(query.Clauses.Count).IsEqualTo(1);
        await Assert.That(query.Clauses[0]).IsTypeOf<FromTableClause>();
        var (sql, _) = new SqlServerCompiler().Compile(query);
        await Assert.That(sql).IsEqualTo("SELECT * FROM Table");
    }

    [Test]
    public async Task When_ShouldNotExecuteAction_WhenConditionIsFalse()
    {
        // Arrange.
        var query = new TestQuery();

        // Act.
        query = query.When(false, q => q.From("Table"));

        // Assert.
        using var assert = Assert.Multiple();
        await Assert.That(query.Clauses).IsEmpty();
        var (sql, _) = new SqlServerCompiler().Compile(query);
        await Assert.That(sql).IsEqualTo("SELECT * FROM ");
    }

    private sealed class TestQuery : SqlQueryBase<TestQuery>
    {
        internal override TestQuery This() => this;
    }
}
