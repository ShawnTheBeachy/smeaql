using Smeaql.Select;

namespace Smeaql;

public class SqlQuery : SqlQueryBase<SqlQuery>
{
    public SqlQuery() { }

    public SqlQuery(string table)
    {
        From(table);
    }

    public SqlSelectQuery Select(params string[] columns)
    {
        var query = new SqlSelectQuery(Clauses);
        return query.Select(columns);
    }

    public SqlSelectQuery SelectValue(object? value)
    {
        var query = new SqlSelectQuery(Clauses);
        query.Clauses.Add(new SelectValueClause(value));
        return query;
    }

    internal override SqlQuery This() => this;
}
