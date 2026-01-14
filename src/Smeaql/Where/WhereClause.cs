namespace Smeaql.Where;

internal abstract class WhereClause : SqlClause
{
    public string Operator { get; init; } = "=";
    public WhereFlag WhereFlag { get; init; } = WhereFlag.And;
}
