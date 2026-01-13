using Smeaql.From;
using Smeaql.Select;
using Smeaql.Where;

namespace Smeaql.Compilers;

public abstract class SqlCompiler<T>
    where T : SqlCompiler<T>
{
    public CompiledSqlQuery Compile<TQuery>(SqlQueryBase<TQuery> query)
        where TQuery : SqlQueryBase<TQuery>
    {
        var compiledQuery = new CompiledSqlQuery();
        CompileSelect(query, compiledQuery);
        CompileFrom(query, compiledQuery);
        CompileWheres(query, compiledQuery);
        return compiledQuery;
    }

    private void CompileFrom<TQuery>(SqlQueryBase<TQuery> query, CompiledSqlQuery compiledQuery)
        where TQuery : SqlQueryBase<TQuery>
    {
        compiledQuery.Write(" FROM ");

        foreach (var clause in query.Clauses.OfType<FromClause>())
            clause.Compile(This(), compiledQuery);
    }

    private void CompileSelect<TQuery>(SqlQueryBase<TQuery> query, CompiledSqlQuery compiledQuery)
        where TQuery : SqlQueryBase<TQuery>
    {
        compiledQuery.Write("SELECT ");

        foreach (var clause in query.Clauses.OfType<SelectClause>())
            clause.Compile(This(), compiledQuery);
    }

    private void CompileWheres<TQuery>(SqlQueryBase<TQuery> query, CompiledSqlQuery compiledQuery)
        where TQuery : SqlQueryBase<TQuery>
    {
        if (!query.HasClause<WhereClause>())
            return;

        compiledQuery.Write(" WHERE ");
        var firstClause = true;

        foreach (var clause in query.Clauses.OfType<WhereClause>())
        {
            if (!firstClause)
                compiledQuery.Write($" {clause.WhereFlag.ToSql()} ");

            clause.Compile(This(), compiledQuery);
            firstClause = false;
        }
    }

    internal abstract T This();
}
