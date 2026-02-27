using System.Text;
using Smeaql.Helpers;

namespace Smeaql.Where;

internal sealed class WhereInSubQueryClause<TQuery> : WhereClause
    where TQuery : SqlQueryBase<TQuery>
{
    private readonly string _column;
    private readonly TQuery _subQuery;

    public WhereInSubQueryClause(TQuery subQuery, string column)
    {
        _subQuery = subQuery;
        _column = column;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append(_column.Bracket());
        stringBuilder.Append(" IN (");
        compiler.Compile(_subQuery, stringBuilder, parameterFactory);
        stringBuilder.Append(')');
    }
}
