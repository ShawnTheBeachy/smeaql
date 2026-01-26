using Smeaql.From;
using Smeaql.Helpers;
using Smeaql.Join;
using Smeaql.Where;

namespace Smeaql;

public abstract class SqlQueryBase<T>
    where T : SqlQueryBase<T>
{
    internal List<SqlClause> Clauses { get; } = [];

    public T From(string table)
    {
        Clauses.Replace(new FromTableClause(table));
        return This();
    }

    public T LeftJoin(string table, string left, string right, string @operator = "=")
    {
        var joinClause = new JoinClause(table) { Type = "LEFT" };
        joinClause.OnColumns(left, right, @operator);
        return This();
    }

    public T LeftJoin(string table, Action<JoinConditionBuilder> join)
    {
        var joinClause = new JoinClause(table) { Type = "LEFT" };
        join(new JoinConditionBuilder(joinClause));
        return This();
    }

    public T RightJoin(string table, string left, string right, string @operator = "=")
    {
        var joinClause = new JoinClause(table) { Type = "RIGHT" };
        joinClause.OnColumns(left, right, @operator);
        return This();
    }

    public T RightJoin(string table, Action<JoinConditionBuilder> join)
    {
        var joinClause = new JoinClause(table) { Type = "RIGHT" };
        join(new JoinConditionBuilder(joinClause));
        return This();
    }

    internal abstract T This();

    public T Where(string column, object? value)
    {
        Clauses.Add(new WhereValueClause(column, value) { WhereFlag = WhereFlag.And });
        return This();
    }

    public T Where(string column, string @operator, object? value)
    {
        Clauses.Add(
            new WhereValueClause(column, value) { Operator = @operator, WhereFlag = WhereFlag.And }
        );
        return This();
    }

    public T WhereColumns(string leftColumn, string rightColumn)
    {
        Clauses.Add(new WhereColumnsClause(leftColumn, rightColumn) { WhereFlag = WhereFlag.And });
        return This();
    }

    public T WhereColumns(string leftColumn, string @operator, string rightColumn)
    {
        Clauses.Add(
            new WhereColumnsClause(leftColumn, rightColumn)
            {
                Operator = @operator,
                WhereFlag = WhereFlag.And,
            }
        );
        return This();
    }

    public T WhereIn(string column, params object?[] values)
    {
        Clauses.Add(new WhereInClause(column, values));
        return This();
    }
    
    public T WhereIn(string column, SqlQuery subQuery)
    {
        Clauses.Add(new WhereInSubQueryClause(subQuery, column));
        return This();
    }

    public T WhereTrue(string column)
    {
        Clauses.Add(new WhereValueClause(column, 1));
        return This();
    }
}
