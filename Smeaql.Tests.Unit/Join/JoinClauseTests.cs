using Smeaql.Compilers;
using Smeaql.Join;

namespace Smeaql.Tests.Unit.Join;

public sealed class JoinClauseTests
{
    [Test]
    public async Task CustomOperator_ShouldBeUsed_WhenSpecifiedInInnerJoin(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").InnerJoin("Authors", "Authors.Id", ">", "Books.AuthorId");

        // Assert.
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books INNER JOIN Authors ON Authors.Id > Books.AuthorId");
    }

    [Test]
    public async Task CustomOperator_ShouldBeUsed_WhenSpecifiedInLeftJoin(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").LeftJoin("Authors", "Authors.Id", ">", "Books.AuthorId");

        // Assert.
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books LEFT JOIN Authors ON Authors.Id > Books.AuthorId");
    }

    [Test]
    public async Task CustomOperator_ShouldBeUsed_WhenSpecifiedInRightJoin(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").RightJoin("Authors", "Authors.Id", ">", "Books.AuthorId");

        // Assert.
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books RIGHT JOIN Authors ON Authors.Id > Books.AuthorId");
    }

    [Test]
    public async Task JoinType_ShouldBeInner_WhenInnerJoinIsUsed(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").InnerJoin("Authors", "Books.AuthorId", "Authors.Id");

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<JoinClause>().Count()).IsEqualTo(1);
        await Assert.That(query.Clauses.OfType<JoinClause>().First().Type).IsEqualTo("INNER");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books INNER JOIN Authors ON Books.AuthorId = Authors.Id");
    }

    [Test]
    public async Task JoinType_ShouldBeLeft_WhenLeftJoinIsUsed(CancellationToken cancellationToken)
    {
        // Act.
        var query = new SqlQuery("Books").LeftJoin("Authors", "Books.AuthorId", "Authors.Id");

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<JoinClause>().Count()).IsEqualTo(1);
        await Assert.That(query.Clauses.OfType<JoinClause>().First().Type).IsEqualTo("LEFT");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books LEFT JOIN Authors ON Books.AuthorId = Authors.Id");
    }

    [Test]
    public async Task JoinType_ShouldBeRight_WhenRightJoinIsUsed(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").RightJoin("Authors", "Books.AuthorId", "Authors.Id");

        // Assert.
        using var asserts = Assert.Multiple();
        await Assert.That(query.Clauses.OfType<JoinClause>().Count()).IsEqualTo(1);
        await Assert.That(query.Clauses.OfType<JoinClause>().First().Type).IsEqualTo("RIGHT");
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo("SELECT * FROM Books RIGHT JOIN Authors ON Books.AuthorId = Authors.Id");
    }

    [Test]
    public async Task MultipleConditions_ShouldBeAdded_WhenCallbackOverloadIsUsedWithMultipleConditions(
        CancellationToken cancellationToken
    )
    {
        // Act.
        var query = new SqlQuery("Books").LeftJoin(
            "Ratings",
            j => j.OnColumns("Ratings.BookId", "Books.Id").On("Ratings.Rating", ">", 5)
        );

        // Assert.
        var compiledQuery = new SqlServerCompiler().Compile(query);
        await Assert
            .That(compiledQuery.Sql)
            .IsEqualTo(
                "SELECT * FROM Books LEFT JOIN Ratings ON Ratings.BookId = Books.Id AND Ratings.Rating > @p0"
            );
    }
}
