using System.Text;
using Smeaql.From;
using Smeaql.Where;

namespace Smeaql.Join;

internal sealed class JoinClause : SqlClause
{
    private readonly FromClause _from;
    private readonly List<WhereClause> _wheres = [];

    public string Type { get; init; } = "INNER";

    public JoinClause(string table)
    {
        _from = new FromTableClause(table);
    }

    public override void Compile<TCompiler>(
        TCompiler compiler,
        StringBuilder stringBuilder,
        ParameterFactory parameterFactory
    )
    {
        stringBuilder.Append($"{Type} JOIN ");
        _from.Compile(compiler, stringBuilder, parameterFactory);

        if (_wheres.Count < 1)
            return;

        stringBuilder.Append(" ON ");

        for (var i = 0; i < _wheres.Count; i++)
        {
            _wheres[i].Compile(compiler, stringBuilder, parameterFactory);

            if (i < _wheres.Count - 1)
                stringBuilder.Append(" AND ");
        }
    }

    public void On(string column, object? value, string @operator = "=") =>
        _wheres.Add(new WhereValueClause(column, value) { Operator = @operator });

    public void OnColumns(string left, string right, string @operator = "=") =>
        _wheres.Add(new WhereColumnsClause(left, right) { Operator = @operator });
}
