using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating the range to the entity's current move target.
/// </summary>
public sealed class CheckMoveTargetRange : ContextualScorerBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Operator Type", "What type of comparison operator is desired")]
    public RangeOperator operatorType = RangeOperator.LessThanOrEquals;

    [ApexSerialization, FriendlyName("Range", "What range to use in the comparison operator")]
    public float range = 2f;

    public override float Score(ContextBase context)
    {
        var entity = context.Entity;

        var moveTarget = entity.CurrentMoveTarget;
        if (!moveTarget.HasValue) {
            return 0f;
        }

        var distance = (entity.Position - moveTarget.Value).sqrMagnitude;
        if (ApexUtils.IsOperatorTrue(operatorType, distance, range * range)) {
            return score;
        }

        return 0f;
    }
}
