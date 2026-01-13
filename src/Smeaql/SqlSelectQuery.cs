using Smeaql.Select;

namespace Smeaql;

public sealed class SqlSelectQuery : SqlQueryBase<SqlSelectQuery>
{
    internal SqlSelectQuery(IEnumerable<SqlClause> clauses)
    {
        AddClauses(clauses);
    }

    public SqlSelectQuery Select(params string[] columns)
    {
        AddClause(new SelectColumnsClause(columns));
        return this;
    }

    internal override SqlSelectQuery This() => this;

    public override string ToString()
    {
        /*var builder = new StringBuilder();
        builder.Append("SELECT ");

        foreach (var clause in Clauses)
            if (clause is SelectClause)
                clause.Write(builder);

        if (!string.IsNullOrWhiteSpace(_table))
            builder.Append($" FROM {_table}");

        var startedWhere = false;

        foreach (var clause in Clauses)
            if (clause is WhereClause whereClause)
            {
                if (!startedWhere)
                    builder.Append(" WHERE ");
                else
                    builder.Append($" {whereClause.WhereType.ToSql()} ");

                startedWhere = true;
                clause.Write(builder);
            }

        return builder.ToString();*/
        return "";
    }
}
