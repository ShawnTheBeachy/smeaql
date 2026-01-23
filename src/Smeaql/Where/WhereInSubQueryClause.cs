using System.Text;
using Smeaql.Compilers;

namespace Smeaql.Where;

internal sealed class WhereInSubQueryClause : WhereClause
{
    private readonly string _column;
    private readonly SqlSelectQuery _subQuery;
    

    public WhereInSubQueryClause(SqlSelectQuery subQuery, string column)
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
        var compiled = new SqlServerCompiler().Compile(_subQuery);
        parameterFactory.Parameters = compiled.Parameters.ToDictionary();
        stringBuilder.Append(
            $"{_column} IN ({compiled.Sql})");
    }
        
}