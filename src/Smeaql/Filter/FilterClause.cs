namespace Smeaql.Filter;

internal abstract class FilterClause : SqlClause
{
    public FilterFlag FilterFlag { get; protected init; } = FilterFlag.And;
}
