using System.Text;
using Smeaql.Helpers;

namespace Smeaql.Order;

internal sealed class OrderColumnsClause : OrderClause
{
    private readonly IReadOnlyList<string> _columns;

    public OrderColumnsClause(IReadOnlyList<string> columns)
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
            stringBuilder.Append($"{_columns[i].Bracket()} {Direction.ToSql()}");

            if (i < _columns.Count - 1)
                stringBuilder.Append(',');
        }
    }
}
