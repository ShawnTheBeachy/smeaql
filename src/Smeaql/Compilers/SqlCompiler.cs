using System.Text;
using Microsoft.Extensions.ObjectPool;
using Smeaql.From;
using Smeaql.Join;
using Smeaql.Order;
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
        var stringBuilder = ObjectPools.StringBuilders.Get();

        try
        {
            CompileSelect(query, stringBuilder, parameterFactory);
            CompileFrom(query, stringBuilder, parameterFactory);
            CompileJoins(query, stringBuilder, parameterFactory);
            CompileWheres(query, stringBuilder, parameterFactory);
            CompileOrders(query, stringBuilder, parameterFactory);
            return (stringBuilder.ToString(), parameterFactory.Parameters.AsReadOnly());
        }
        finally
        {
            ObjectPools.StringBuilders.Return(stringBuilder);
        }

        /* Selects
           Froms
           Joins
           Wheres
           Groups
           Havings
           Orders
           Limits
           Unions
        */
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

    private void CompileJoins<TQuery>(
        SqlQueryBase<TQuery> query,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
        where TQuery : SqlQueryBase<TQuery>
    {
        foreach (var clause in query.Clauses.OfType<JoinClause>())
            clause.Compile(This(), stringBuilder, parameterFactory);
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

    private void CompileSelect<TQuery>(
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
                stringBuilder.Append($" {clause.WhereFlag.ToSql()} ");
            else
                stringBuilder.Append(" WHERE ");

            clause.Compile(This(), stringBuilder, parameterFactory);
            firstClause = false;
        }
    }

    internal abstract T This();
}
