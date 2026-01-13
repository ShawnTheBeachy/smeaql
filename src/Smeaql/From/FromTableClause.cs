using System.Text;

namespace Smeaql.From;

internal sealed class FromTableClause : FromClause
{
    private readonly string _table;

    public FromTableClause(string table)
    {
        _table = table;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) => stringBuilder.Append(_table);
}
