using Smeaql.Where;

namespace Smeaql.Having;

internal abstract class HavingClause : SqlClause
{
    public string Operator { get; init; } = "=";
    public ConditionType ConditionType { get; init; } = ConditionType.And;
}
