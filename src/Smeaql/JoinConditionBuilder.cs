using Smeaql.Join;

namespace Smeaql;

public sealed class JoinConditionBuilder
{
    private readonly JoinClause _join;

    internal JoinConditionBuilder(JoinClause join)
    {
        _join = join;
    }

    public JoinConditionBuilder On(string column, object? value, string @operator = "=")
    {
        _join.On(column, value, @operator);
        return this;
    }

    public JoinConditionBuilder OnColumns(string left, string right)
    {
        _join.OnColumns(left, right);
        return this;
    }
}
