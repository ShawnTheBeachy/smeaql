using System.Text;

namespace Smeaql.Where;

internal sealed class WhereExistsClause<TQuery> : WhereClause
    where TQuery : SqlQueryBase<TQuery>
{
    private readonly TQuery _subQuery;
    public ExistsFlag ExistsFlag { get; init; } = ExistsFlag.Exists;

    public WhereExistsClause(TQuery subQuery)
    {
        _subQuery = subQuery;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append(ExistsFlag.ToSql());
        stringBuilder.Append('(');
        compiler.Compile(_subQuery, stringBuilder, parameterFactory);
        stringBuilder.Append(')');
    }
}
