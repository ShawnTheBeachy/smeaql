using System.Text;
using Smeaql.From;
using Smeaql.Select;
using Smeaql.Where;

namespace Smeaql.Compilers;

public abstract class SqlCompiler<T>
    where T : SqlCompiler<T>
{
    public (string Sql, IReadOnlyDictionary<string, object?> Parameters) Compile<TQuery>(
        SqlQueryBase<TQuery> query
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        var parameterFactory = new ParameterFactory();
        var stringBuilder = new StringBuilder();
        CompileSelect(query, stringBuilder, parameterFactory);
        CompileFrom(query, stringBuilder, parameterFactory);
        CompileWheres(query, stringBuilder, parameterFactory);
        return (stringBuilder.ToString(), parameterFactory.Parameters.AsReadOnly());
    }

    private void CompileFrom<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        stringBuilder.Append(" FROM ");

        foreach (var clause in query.Clauses.OfType<FromClause>())
            clause.Compile(This(), stringBuilder, parameterFactory);
    }

    private void CompileSelect<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        stringBuilder.Append("SELECT ");

        foreach (var clause in query.Clauses.OfType<SelectClause>())
            clause.Compile(This(), stringBuilder, parameterFactory);
    }

    private void CompileWheres<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        var firstClause = true;

        foreach (var clause in query.Clauses.OfType<WhereClause>())
        {
            if (!firstClause)
                stringBuilder.Append($" {clause.WhereFlag.ToSql()} ");
            else
                stringBuilder.Append(" WHERE ");

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }
    }

    internal abstract T This();
}
