using System.Text;
using Smeaql.Compilers;

namespace Smeaql.Where;

internal sealed class WhereNotExistsClause : WhereClause
{
    private readonly string? _column;
    private readonly SqlQuery? _subQuery;
    private readonly string? _table;
    private readonly object? _value;
    

    public WhereNotExistsClause(SqlQuery? subQuery, string? table, string? column, object? value)
    {
        _column = column;
        _subQuery = subQuery;
        _table = table;
        _value = value;
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        if(_subQuery is not null)
        {
            var compiled = new SqlServerCompiler().Compile(_subQuery);
            parameterFactory.Parameters = compiled.Parameters.ToDictionary();
            stringBuilder.Append(
                $"NOT EXISTS ({compiled.Sql})");
        }
        else
        {
            if (_column is null || _table is null || _value is null)
                return;
            var query = new SqlQuery(_table).SelectOne().Where(_column, _value);
            var compiled = new SqlServerCompiler().Compile(query);
            parameterFactory.Parameters = compiled.Parameters.ToDictionary();
            stringBuilder.Append(
                $"NOT EXISTS ({compiled.Sql})");
        }
        
    }
        
}