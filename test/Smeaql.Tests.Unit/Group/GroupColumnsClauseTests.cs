using Smeaql.Compilers;

namespace Smeaql.Tests.Unit.Group;

public sealed class GroupColumnsClauseTests
{
    [Test]
    public async Task GroupBy_ShouldBeAddedToQuery_WhenGroupByIsUsed()
    {
        // Act.
        var query = new SqlQuery("Books").Select("*").GroupBy("Title", "Author").GroupBy("Rating");

        // Assert.
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM [Books] GROUP BY [Title],[Author],[Rating]");
    }
}
