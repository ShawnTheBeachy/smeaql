using System.Text;

namespace Smeaql.Where;

internal sealed class WhereInSubQueryClause : WhereClause
{
    private readonly string _column;
    private readonly SqlSelectQuery _subQuery;
    private readonly IReadOnlyList<object?> _values;
    

    public WhereInSubQueryClause(SqlSelectQuery subQuery, string column, params object?[] values)
    {
        _subQuery = subQuery;
        _column = column;
        _values = values;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) =>
        stringBuilder.Append(
            $"({_subQuery.Where(_column, _values)})"
        );
}