using System.Text;

namespace Smeaql.Where;

internal sealed class WhereInSubQueryClause : WhereClause
{
    private readonly string _column;
    private readonly string _table;
    private readonly IReadOnlyList<object?> _values;

    public WhereInSubQueryClause(string column, string table, params object?[] values)
    {
        _column = column;
        _table = table;
        _values = values;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) =>
        stringBuilder.Append(
            $"{_column} IN ({new SqlQuery(_table).Select(_column).WhereColumns()})"
        );
}