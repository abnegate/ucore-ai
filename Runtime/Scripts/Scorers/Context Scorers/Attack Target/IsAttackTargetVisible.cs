using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity's attack target is visible or not, within the given range.
/// </summary>
public sealed class IsAttackTargetVisible : ContextualScorerBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    [ApexSerialization, FriendlyName("Use Scan Range", "Set to true to use the scan range"), MemberDependency("useAttackRange", false)]
    public bool useScanRange = false;

    [ApexSerialization, FriendlyName("Use Attack Range", "Set to true to use the attack range"), MemberDependency("useScanRange", false)]
    public bool useAttackRange = false;

    [ApexSerialization, FriendlyName("Custom Range", "Input a custom range here (if not using scan or attack range"), MemberDependency("useScanRange", false), MemberDependency("useAttackRange", false)]
    public float customRange = 10f;

    public override float Score(ContextBase context)
    {
        var entity = context.Entity;

        var attackTarget = entity.CurrentAttackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var range = customRange;
        if (useScanRange) {
            range = entity.EntityScanRange;
        } else if (useAttackRange) {
            range = entity.AttackRange;
        }

        var visibility = ApexUtils.IsVisible(
            entity.Position,
            entity.CurrentAttackTarget.Position,
            range,
            entity.CurrentAttackTarget.GameObject.layer);

        if (visibility) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
