using System.Text;
using Smeaql.Compilers;

namespace Smeaql.Where;

internal sealed class WhereNotExistsClause : WhereClause
{
    private readonly SqlQuery _subQuery;
    

    public WhereNotExistsClause(SqlQuery subQuery)
    {
        _subQuery = subQuery;
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
            $"NOT EXISTS ({compiled.Sql})");
    }
        
}