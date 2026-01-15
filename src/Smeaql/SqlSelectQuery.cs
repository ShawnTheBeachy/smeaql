using Smeaql.Order;
using Smeaql.Select;

namespace Smeaql;

public sealed class SqlSelectQuery : SqlQueryBase<SqlSelectQuery>
{
    internal SqlSelectQuery(IEnumerable<SqlClause> clauses)
    {
        Clauses.AddRange(clauses);
    }

    public SqlSelectQuery OrderByAsc(params string[] columns)
    {
        Clauses.AddRange(new OrderColumnsClause(columns) { Direction = OrderDirection.Asc });
        return This();
    }

    public SqlSelectQuery OrderByDesc(params string[] columns)
    {
        Clauses.AddRange(new OrderColumnsClause(columns) { Direction = OrderDirection.Desc });
        return This();
    }

    public SqlSelectQuery Select(params string[] columns)
    {
        Clauses.AddRange(new SelectColumnsClause(columns));
        return this;
    }

    internal override SqlSelectQuery This() => this;
}
