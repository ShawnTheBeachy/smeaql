using Smeaql.Filter;
using Smeaql.From;
using Smeaql.Select;

namespace Smeaql.Compilers;

public abstract class SqlCompiler<T>
    where T : SqlCompiler<T>
{
    public CompiledSqlQuery Compile<TQuery>(SqlQueryBase<TQuery> query)
        where TQuery : SqlQueryBase<TQuery>
    {
        var compiledQuery = new CompiledSqlQuery();
        compiledQuery.Write("SELECT ");

        foreach (var selectClause in query.Clauses.OfType<SelectClause>())
            CompileClause(selectClause);

        compiledQuery.Write(' ');

        foreach (var fromClause in query.Clauses.OfType<FromClause>())
            CompileClause(fromClause);

        if (!query.HasClause<FilterClause>())
            return compiledQuery;

        compiledQuery.Write(" WHERE ");
        var firstFilter = true;

        foreach (var filterClause in query.Clauses.OfType<FilterClause>())
        {
            if (!firstFilter)
                compiledQuery.Write($" {filterClause.FilterFlag.ToSql()} ");

            CompileClause(filterClause);
            firstFilter = false;
        }

        return compiledQuery;

        void CompileClause(SqlClause clause) => clause.Compile(This(), compiledQuery);
    }

    internal abstract T This();
}
