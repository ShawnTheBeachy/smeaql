using System.Text;

namespace Smeaql.Where;

internal sealed class WhereInSubQueryClause : WhereClause
{
    private readonly string _column;
    private readonly SqlQuery _subQuery;
    
    public WhereInSubQueryClause(SqlQuery subQuery, string column)
    {
        _subQuery = subQuery;
        _column = column;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        var compiled = compiler.Compile(_subQuery, parameterFactory);
        stringBuilder.Append(
            $"{_column} IN ({compiled.Sql})");
    }
}
