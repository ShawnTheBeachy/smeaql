using System.Text;

namespace Smeaql.Where;

internal sealed class WhereNotExistsClause<TQuery> : WhereClause
    where TQuery : SqlQueryBase<TQuery>
{
    private readonly TQuery _subQuery;

    public WhereNotExistsClause(TQuery subQuery)
    {
        _subQuery = subQuery;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append("NOT EXISTS (");
        compiler.Compile(_subQuery, stringBuilder, parameterFactory);
        stringBuilder.Append(')');
    }
}
