using Apex.AI;
using Apex.Serialization;

public class HasPlayerInChargeRange : ContextualScorerBase<ContextBase>
{
    [ApexSerialization, FriendlyName("Not", "Set to true to reverse the logic of the scorer")]
    public bool not = false;

    public override float Score(ContextBase context)
    {
        var entity = context.Entity;
        var attackTarget = entity.CurrentAttackTarget;

        if (attackTarget == null) {
            return not ? score : 0f;
        }

        var distance = (entity.Position - attackTarget.Position).sqrMagnitude;
        if (distance <= ((Enemy)context.Entity).chargeRange) {
            return not ? 0f : score;
        }
        return not ? score : 0f;
    }
}