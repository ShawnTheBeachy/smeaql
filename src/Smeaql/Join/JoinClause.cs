using System.Text;

namespace Smeaql.Join;

internal sealed class JoinClause : SqlClause
{
    private readonly string _table;
    private readonly string? _type;

    public JoinClause(string table, string? type = null)
    {
        _table = table;
        _type = type ?? "INNER";
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        if (!string.IsNullOrWhiteSpace(_type))
            stringBuilder.Append($" {_type} {_table}");
    }
}
