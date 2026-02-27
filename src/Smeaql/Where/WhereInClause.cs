using System.Text;
using Smeaql.Helpers;

namespace Smeaql.Where;

internal sealed class WhereInClause : WhereClause
{
    private readonly string _column;
    private readonly IReadOnlyList<object?> _values;

    public WhereInClause(string column, IReadOnlyList<object?> values)
    {
        _column = column;
        _values = values;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append(_column.Bracket());
        stringBuilder.Append(" IN (");

        for (var i = 0; i < _values.Count; i++)
        {
            if (i > 0)
                stringBuilder.Append(',');

            stringBuilder.Append(parameterFactory.CreateParameter(_values[i]));
        }

        stringBuilder.Append(')');
    }
}
