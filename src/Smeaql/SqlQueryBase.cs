using Smeaql.Filter;
using Smeaql.From;

namespace Smeaql;

public abstract class SqlQueryBase<T>
    where T : SqlQueryBase<T>
{
    private readonly List<SqlClause> _clauses = [];
    internal IReadOnlyList<SqlClause> Clauses => _clauses;

    internal void AddClause(SqlClause clause) => _clauses.Add(clause);

    internal void AddClauses(IEnumerable<SqlClause> clauses) => _clauses.AddRange(clauses);

    internal void AddOrReplaceClause<TClause>(TClause clause)
        where TClause : SqlClause
    {
        _clauses.RemoveAll(x => x is TClause);
        _clauses.Add(clause);
    }

    public T From(string table)
    {
        AddOrReplaceClause<FromClause>(new FromTableClause(table));
        return This();
    }

    internal bool HasClause<TClause>()
        where TClause : SqlClause
    {
        foreach (var clause in _clauses)
            if (clause is TClause)
                return true;

        return false;
    }

    internal abstract T This();

    public T WhereColumns(string leftColumn, string rightColumn)
    {
        _clauses.Add(new WhereColumnsClause(leftColumn, rightColumn, FilterFlag.And));
        return This();
    }
}
