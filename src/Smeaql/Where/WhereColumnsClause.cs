using System.Text;

namespace Smeaql.Where;

internal sealed class WhereColumnsClause : WhereClause
{
    private readonly string _leftColumn,
        _rightColumn;

    public WhereColumnsClause(string leftColumn, string rightColumn)
    {
        _leftColumn = leftColumn;
        _rightColumn = rightColumn;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    ) => stringBuilder.Append($"{_leftColumn} {Operator} {_rightColumn}");
}
