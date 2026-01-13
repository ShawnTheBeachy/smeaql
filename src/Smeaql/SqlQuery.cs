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

    internal override SqlQuery This() => this;
}
