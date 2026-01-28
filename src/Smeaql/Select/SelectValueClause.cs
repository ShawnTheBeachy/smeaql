using System.Text;

namespace Smeaql.Select;

internal sealed class SelectValueClause : SelectClause
{
    private readonly object? _value;

    public SelectValueClause(object? value)
    {
        _value = value;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) => stringBuilder.Append(parameterFactory.CreateParameter(_value));
}
