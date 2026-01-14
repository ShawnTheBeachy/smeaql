using System.Text;

namespace Smeaql.Where;

internal sealed class WhereValueClause : WhereClause
{
    private readonly string _column;
    private readonly object? _value;

    public WhereValueClause(string column, object? value)
    {
        _column = column;
        _value = value;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) => stringBuilder.Append($"{_column} {Operator} {parameterFactory.CreateParameter(_value)}");
}
