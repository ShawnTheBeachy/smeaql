using Smeaql.Group;
using Smeaql.Helpers;
using Smeaql.Limit;
using Smeaql.Order;
using Smeaql.Select;

namespace Smeaql;

public sealed class SqlSelectQuery : SqlQueryBase<SqlSelectQuery>
{
    internal SqlSelectQuery(IEnumerable<SqlClause> clauses)
    {
        Clauses.AddRange(clauses);
    }

    public SqlSelectQuery GroupBy(params string[] columns)
    {
        Clauses.Add(new GroupColumnsClause(columns));
        return This();
    }

    public SqlSelectQuery OrderByAsc(params string[] columns)
    {
        Clauses.Add(new OrderColumnsClause(columns) { Direction = OrderDirection.Asc });
        return This();
    }

    public SqlSelectQuery OrderByDesc(params string[] columns)
    {
        Clauses.Add(new OrderColumnsClause(columns) { Direction = OrderDirection.Desc });
        return This();
    }

    public SqlSelectQuery Page(int page, int size)
    {
        page = page < 1 ? 1 : page;
        Clauses.Replace(new LimitClause(size, (page - 1) * size));
        return This();
    }

    public SqlSelectQuery Select(params string[] columns)
    {
        Clauses.Add(new SelectColumnsClause(columns));
        return this;
    }

    internal override SqlSelectQuery This() => this;
}
