using Apex.AI;

public sealed class HasAttackTargetOutsideScanRange : ContextualScorerBase<ContextBase>
{
    public override float Score(ContextBase context)
    {
        var entity = context.Entity;

        var attackTarget = entity.CurrentAttackTarget;
        if (attackTarget == null) {
            return 0f;
        }

        var distance = (entity.Position - attackTarget.Position).sqrMagnitude;
        if (distance > (entity.EntityScanRange * entity.EntityScanRange)) {
            return score;
        }

        return 0f;
    }
}
