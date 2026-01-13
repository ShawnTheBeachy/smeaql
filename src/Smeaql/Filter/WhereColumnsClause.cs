namespace Smeaql.Filter;

internal sealed class WhereColumnsClause : FilterClause
{
    private readonly string _leftColumn,
        _rightColumn;

    public WhereColumnsClause(string leftColumn, string rightColumn, FilterFlag filterFlag)
    {
        _leftColumn = leftColumn;
        _rightColumn = rightColumn;
        FilterFlag = filterFlag;
    }

    public override void Compile<TCompiler>(TCompiler compiler, CompiledSqlQuery compiledQuery) =>
        compiledQuery.Write($"{_leftColumn} = {_rightColumn}");
}
