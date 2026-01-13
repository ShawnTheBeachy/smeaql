namespace Smeaql.Where;

internal abstract class WhereClause : SqlClause
{
    public WhereFlag WhereFlag { get; init; } = WhereFlag.And;
}
