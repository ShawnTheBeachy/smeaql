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

    public T InnerJoin(string table, string left, string right) =>
        InnerJoin(table, left, "=", right);

    public T InnerJoin(string table, string left, string @operator, string right)
    {
        var joinClause = new JoinClause(table) { Type = "INNER" };
        joinClause.OnColumns(left, right, @operator);
        Clauses.Add(joinClause);
        return This();
    }

    public T InnerJoin(string table, Action<JoinConditionBuilder> join)
    {
        var joinClause = new JoinClause(table) { Type = "INNER" };
        join(new JoinConditionBuilder(joinClause));
        Clauses.Add(joinClause);
        return This();
    }

    public T LeftJoin(string table, string left, string right) => LeftJoin(table, left, "=", right);

    public T LeftJoin(string table, string left, string @operator, string right)
    {
        var joinClause = new JoinClause(table) { Type = "LEFT" };
        joinClause.OnColumns(left, right, @operator);
        Clauses.Add(joinClause);
        return This();
    }

    public T LeftJoin(string table, Action<JoinConditionBuilder> join)
    {
        var joinClause = new JoinClause(table) { Type = "LEFT" };
        join(new JoinConditionBuilder(joinClause));
        Clauses.Add(joinClause);
        return This();
    }

    public T OrWhere(string column, object? value) => OrWhere(column, "=", value);

    public T OrWhere(string column, string @operator, object? value) =>
        WherePrivate(column, @operator, value, WhereFlag.Or);

    public T OrWhereColumns(string leftColumn, string rightColumn) =>
        OrWhereColumns(leftColumn, "=", rightColumn);

    public T OrWhereColumns(string leftColumn, string @operator, string rightColumn) =>
        WhereColumnsPrivate(leftColumn, @operator, rightColumn, WhereFlag.Or);

    public T OrWhereFalse(string column) => WhereFalsePrivate(column, WhereFlag.Or);

    public T OrWhereIn(string column, params object?[] values) =>
        WhereInPrivate(column, values, WhereFlag.Or);

    public T OrWhereTrue(string column) => WhereTruePrivate(column, WhereFlag.Or);

    public T RightJoin(string table, string left, string right) =>
        RightJoin(table, left, "=", right);

    public T RightJoin(string table, string left, string @operator, string right)
    {
        var joinClause = new JoinClause(table) { Type = "RIGHT" };
        joinClause.OnColumns(left, right, @operator);
        Clauses.Add(joinClause);
        return This();
    }

    public T RightJoin(string table, Action<JoinConditionBuilder> join)
    {
        var joinClause = new JoinClause(table) { Type = "RIGHT" };
        join(new JoinConditionBuilder(joinClause));
        Clauses.Add(joinClause);
        return This();
    }

    internal abstract T This();

    public T Where(string column, object? value) => Where(column, "=", value);

    public T Where(string column, string @operator, object? value) =>
        WherePrivate(column, @operator, value, WhereFlag.And);

    public T WhereColumns(string leftColumn, string rightColumn) =>
        WhereColumns(leftColumn, "=", rightColumn);

    public T WhereColumns(string leftColumn, string @operator, string rightColumn) =>
        WhereColumnsPrivate(leftColumn, @operator, rightColumn, WhereFlag.And);

    private T WhereColumnsPrivate(
        string leftColumn,
        string @operator,
        string rightColumn,
        WhereFlag whereFlag
    )
    {
        Clauses.Add(
            new WhereColumnsClause(leftColumn, rightColumn)
            {
                Operator = @operator,
                WhereFlag = whereFlag,
            }
        );
        return This();
    }

    public T WhereFalse(string column) => WhereFalsePrivate(column, WhereFlag.And);

    private T WhereFalsePrivate(string column, WhereFlag whereFlag)
    {
        Clauses.Add(new WhereValueClause(column, 0) { WhereFlag = whereFlag });
        return This();
    }
    
    public T WhereIn(string column, SqlQuery subQuery)
    {
        Clauses.Add(new WhereInSubQueryClause(subQuery, column));
        return This();
    }

    public T WhereIn(string column, params object?[] values) =>
        WhereInPrivate(column, values, WhereFlag.And);

    private T WhereInPrivate(string column, IReadOnlyList<object?> values, WhereFlag whereFlag)
    {
        Clauses.Add(new WhereInClause(column, values) { WhereFlag = whereFlag });
        return This();
    }

    private T WherePrivate(string column, string @operator, object? value, WhereFlag whereFlag)
    {
        Clauses.Add(
            new WhereValueClause(column, value) { Operator = @operator, WhereFlag = whereFlag }
        );
        return This();
    }

    public T WhereTrue(string column) => WhereTruePrivate(column, WhereFlag.And);

    private T WhereTruePrivate(string column, WhereFlag whereFlag)
    {
        Clauses.Add(new WhereValueClause(column, 1) { WhereFlag = whereFlag });
        return This();
    }
}
