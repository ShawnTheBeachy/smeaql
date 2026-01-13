namespace Smeaql.Where;

internal sealed class WhereColumnsClause : WhereClause
{
    private readonly string _leftColumn,
        _rightColumn;

    public WhereColumnsClause(string leftColumn, string rightColumn, WhereFlag whereFlag)
    {
        _leftColumn = leftColumn;
        _rightColumn = rightColumn;
        WhereFlag = whereFlag;
    }

    public override void Compile<TCompiler>(TCompiler compiler, CompiledSqlQuery compiledQuery) =>
        compiledQuery.Write($"{_leftColumn} = {_rightColumn}");
}
