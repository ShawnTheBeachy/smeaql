using System.Text;

namespace Smeaql.Group;

internal sealed class GroupColumnsClause : GroupClause
{
    private readonly IReadOnlyList<string> _columns;

    public GroupColumnsClause(IReadOnlyList<string> columns)
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
