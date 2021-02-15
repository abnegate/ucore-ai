using Apex.AI;

public sealed class HasAttackTargetOutsideScanRange : ContextualScorerBase<EnemyContext>
{
    public override float Score(EnemyContext context)
    {
        var entity = context.entity;

        var attackTarget = entity.attackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var distance = (entity.position - attackTarget.position).sqrMagnitude;
        if (distance > (entity.entityScanRange * entity.entityScanRange)) {
            return score;
        }

        return 0f;
    }
}
