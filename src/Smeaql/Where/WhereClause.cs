namespace Smeaql.Where;

internal abstract class WhereClause : SqlClause
{
    public WhereFlag WhereFlag { get; protected init; } = WhereFlag.And;
}
