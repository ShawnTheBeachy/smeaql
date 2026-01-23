using System.Text;

namespace Smeaql.Select;

internal sealed class SelectOneClause : SelectClause
{
    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append(1);
    }
}