using System.Diagnostics;
using Apex.AI;
using Apex.Serialization;

/// <summary>
/// An AI scorer for evaluating whether the entity's attack target is visible or not, within the given range.
/// </summary>
public sealed class IsAttackTargetVisible : ContextualScorerBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    [ApexSerialization, FriendlyName("Use Scan Range", "Set to true to use the scan range"), MemberDependency("useAttackRange", false)]
    public bool useScanRange = false;

    [ApexSerialization, FriendlyName("Use Attack Range", "Set to true to use the attack range"), MemberDependency("useScanRange", false)]
    public bool useAttackRange = false;

    [ApexSerialization, FriendlyName("Custom Range", "Input a custom range here (if not using scan or attack range"), MemberDependency("useScanRange", false), MemberDependency("useAttackRange", false)]
    public float customRange = 10f;

    public override float Score(EnemyContext context)
    {
        var entity = context.entity;

        var attackTarget = entity.attackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var range = customRange;
        if (useScanRange) {
            range = entity.entityScanRange;
        } else if (useAttackRange) {
            range = entity.attackRange;
        }

        var visibility = ApexUtils.IsVisible(
            entity.position,
            entity.attackTarget.position,
            range);

        if (visibility) {
            return not ? 0f : score;
        }

        return not ? score : 0f;
    }
}
