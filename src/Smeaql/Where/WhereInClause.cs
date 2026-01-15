using System.Text;

namespace Smeaql.Where;

internal sealed class WhereInClause : WhereClause
{
    private readonly string _column;
    private readonly IReadOnlyList<object?> _values;

    public WhereInClause(string column, params object?[] values)
    {
        _column = column;
        _values = values;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) =>
        stringBuilder.Append(
            $"{_column} IN ({string.Join(',', parameterFactory.CreateParameters(_values))})"
        );
}
