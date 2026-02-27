namespace Smeaql.Where;

internal abstract class WhereClause : SqlClause
{
    public string Operator { get; init; } = "=";
    public ConditionType ConditionType { get; init; } = ConditionType.And;
}
