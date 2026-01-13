using System.Text;

namespace Smeaql.Select;

internal sealed class SelectColumnsClause : SelectClause
{
    private readonly IReadOnlyList<string> _columns;

    public SelectColumnsClause(IReadOnlyList<string> columns)
    {
        _columns = columns;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        for (var i = 0; i < _columns.Count; i++)
        {
            stringBuilder.Append(_columns[i]);

            if (i < _columns.Count - 1)
                stringBuilder.Append(',');
        }
    }
}
