using System.Text;

namespace Smeaql.Where;

internal sealed class WhereValueClause : WhereClause
{
    private readonly string _column;
    private readonly string _operator;
    private readonly object? _value;

    public WhereValueClause(string column, object? value, string @operator)
    {
        _column = column;
        _value = value;
        _operator = @operator;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) => stringBuilder.Append($"{_column} {_operator} {parameterFactory.CreateParameter(_value)}");
}
