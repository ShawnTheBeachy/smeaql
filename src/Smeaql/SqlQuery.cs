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

    public SqlQuery SelectOne()
    {
        Clauses.Add(new SelectOneClause());
        return This();
    }

    internal override SqlQuery This() => this;
}
