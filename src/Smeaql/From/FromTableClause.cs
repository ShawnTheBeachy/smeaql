namespace Smeaql.From;

internal sealed class FromTableClause : FromClause
{
    private readonly string _table;

    public FromTableClause(string table)
    {
        _table = table;
    }

    public override void Compile<TCompiler>(TCompiler compiler, CompiledSqlQuery compiledQuery) =>
        compiledQuery.Write(_table);
}
