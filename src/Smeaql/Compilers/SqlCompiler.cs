using System.ComponentModel;
using System.Text;
using Smeaql.From;
using Smeaql.Group;
using Smeaql.Having;
using Smeaql.Join;
using Smeaql.Limit;
using Smeaql.Order;
using Smeaql.Select;
using Smeaql.Where;

namespace Smeaql.Compilers;

[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class SqlCompiler<T>
    where T : SqlCompiler<T>
{
    public (string Sql, IReadOnlyDictionary<string, object?> Parameters) Compile<TQuery>(
        SqlQueryBase<TQuery> query
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        var stringBuilder = ObjectPools.StringBuilders.Get();
        var parameterFactory = new ParameterFactory();

        try
        {
            Compile(query, stringBuilder, parameterFactory);
            return (stringBuilder.ToString(), parameterFactory.Parameters.AsReadOnly());
        }
        finally
        {
            ObjectPools.StringBuilders.Return(stringBuilder);
        }
    }

    internal void Compile<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        CompileSelects(query, stringBuilder, parameterFactory);
        CompileFrom(query, stringBuilder, parameterFactory);
        CompileJoins(query, stringBuilder, parameterFactory);
        CompileWheres(query, stringBuilder, parameterFactory);
        CompileGroups(query, stringBuilder, parameterFactory);
        CompileHavings(query, stringBuilder, parameterFactory);
        CompileOrders(query, stringBuilder, parameterFactory);
        CompileLimit(query, stringBuilder, parameterFactory);
        // CompileUnions
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

    private void CompileGroups<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        var firstClause = true;

        foreach (var clause in query.Clauses.OfType<GroupClause>())
        {
            if (!firstClause)
                stringBuilder.Append(',');
            else
                stringBuilder.Append(" GROUP BY ");

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }
    }

    private void CompileHavings<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        var firstClause = true;

        foreach (var clause in query.Clauses.OfType<HavingClause>())
        {
            if (!firstClause)
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(clause.ConditionType.ToSql());
                stringBuilder.Append(' ');
            }
            else
                stringBuilder.Append(" HAVING ");

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }
    }

    private void CompileJoins<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        foreach (var clause in query.Clauses.OfType<JoinClause>())
        {
            stringBuilder.Append(' ');
            clause.Compile(This(), stringBuilder, parameterFactory);
        }
    }

    private void CompileLimit<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        foreach (var clause in query.Clauses.OfType<LimitClause>())
        {
            stringBuilder.Append(' ');
            clause.Compile(This(), stringBuilder, parameterFactory);
            break;
        }
    }

    private void CompileOrders<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        var firstClause = true;

        foreach (var clause in query.Clauses.OfType<OrderClause>())
        {
            if (!firstClause)
                stringBuilder.Append(',');
            else
                stringBuilder.Append(" ORDER BY ");

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }
    }

    private void CompileSelects<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        try
        {
            stringBuilder.Append("SELECT ");
        }
        catch (Exception ex)
        {
            _ = ex;
        }
        var firstClause = true;

        foreach (var clause in query.Clauses.OfType<SelectClause>())
        {
            if (!firstClause)
                stringBuilder.Append(',');

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }

        if (firstClause)
            stringBuilder.Append('*');
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
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(clause.ConditionType.ToSql());
                stringBuilder.Append(' ');
            }
            else
                stringBuilder.Append(" WHERE ");

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }
    }

    internal abstract T This();
}
