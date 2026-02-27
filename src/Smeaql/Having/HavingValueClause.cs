using System.Text;
using Smeaql.Helpers;

namespace Smeaql.Having;

internal sealed class HavingValueClause : HavingClause
{
    private readonly string _column;
    private readonly object? _value;

    public HavingValueClause(string column, object? value)
    {
        _column = column;
        _value = value;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append(_column.Bracket());
        stringBuilder.Append(' ');
        stringBuilder.Append(Operator);
        stringBuilder.Append(' ');
        stringBuilder.Append(parameterFactory.CreateParameter(_value));
    }
}
